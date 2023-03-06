// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace TestDoubles;
/*************************************
      InMemoryAsyncQueryable<T>    
**************************************/

public class InMemoryAsyncQueryable<T> : IOrderedQueryable<T>, IAsyncEnumerable<T>
{
    private readonly Action<string, IEnumerable> _include;
    private readonly IQueryable<T> _queryable;

    // Constructor for an IEnumerable data source
    public InMemoryAsyncQueryable(IEnumerable<T> enumerable, Action<string, IEnumerable> include = null)
        : this(enumerable.AsQueryable(), include)
    {
    }

    // Constructor for an IQueryable data source
    public InMemoryAsyncQueryable(IQueryable<T> queryable, Action<string, IEnumerable> include = null)
    {
        _queryable = queryable;
        _include = include;
    }

    // TODO: Implement cancellation token
    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new InMemoryAsyncEnumerator<T>(GetEnumerator());
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _queryable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Expression Expression => _queryable.Expression;

    public Type ElementType => _queryable.ElementType;

    public IQueryProvider Provider => new InMemoryAsyncQueryProvider(_queryable.Provider, _include);

    public IQueryable<T> Include(string path)
    {
        _include?.Invoke(path, _queryable);
        return this;
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator()
    {
        return new InMemoryAsyncEnumerator<T>(GetEnumerator());
    }
}

/**************************************
      InMemoryAsyncQueryProvider    
***************************************/

internal class InMemoryAsyncQueryProvider : IQueryProvider, IAsyncQueryProvider
{
    private static readonly MethodInfo _createQueryMethod
        = typeof(InMemoryAsyncQueryProvider).GetTypeInfo().DeclaredMethods
            .Single(m => m.IsGenericMethodDefinition && m.Name == nameof(CreateQuery));

    private static readonly MethodInfo _executeMethod
        = typeof(InMemoryAsyncQueryProvider).GetTypeInfo().DeclaredMethods
            .Single(m => m.IsGenericMethodDefinition && m.Name == nameof(Execute));

    private readonly Action<string, IEnumerable> _include;

    private readonly IQueryProvider _provider;

    public InMemoryAsyncQueryProvider(IQueryProvider provider, Action<string, IEnumerable> include = null)
    {
        _provider = provider;
        _include = include;
    }

    TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable CreateQuery(Expression expression)
    {
        return (IQueryable)_createQueryMethod
            .MakeGenericMethod(TryGetElementType(expression.Type))
            .Invoke(this, new object[] { expression });
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new InMemoryAsyncQueryable<TElement>(_provider.CreateQuery<TElement>(expression), _include);
    }

    public object Execute(Expression expression)
    {
        return _executeMethod.MakeGenericMethod(expression.Type).Invoke(this, new object[] { expression });
    }

    public TResult Execute<TResult>(Expression expression)
    {
        return _provider.Execute<TResult>(expression);
    }

    public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
    {
        return Task.FromResult(Execute(expression));
    }

    public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        return Task.FromResult(Execute<TResult>(expression));
    }

    private static Type TryGetElementType(Type type)
    {
        if (!type.GetTypeInfo().IsGenericTypeDefinition)
        {
            var interfaceImpl = type.GetInterfaces().Union(new[] { type })
                .FirstOrDefault(t =>
                    t.GetType().IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            if (interfaceImpl != null)
                return interfaceImpl.GetGenericArguments().Single();
        }

        return type;
    }
}

/******************************************
        InMemoryAsyncEnumerator<T>     
 ******************************************/

internal class InMemoryAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _enumerator;

    public InMemoryAsyncEnumerator(IEnumerator<T> enumerator)
    {
        _enumerator = enumerator;
    }

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(Task.FromResult(_enumerator.MoveNext()));
    }

    public ValueTask DisposeAsync()
    {
        return new ValueTask(Task.CompletedTask);
    }

    public T Current => _enumerator.Current;

    public void Dispose()
    {
    }

    public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_enumerator.MoveNext());
    }
}
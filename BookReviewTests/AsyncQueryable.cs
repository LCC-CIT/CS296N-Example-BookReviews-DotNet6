// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading.Tasks;
using System;

namespace TestDoubles
{
    /*************************************
          InMemoryAsyncQueryable<T>    
    **************************************/

    public class InMemoryAsyncQueryable<T> : IOrderedQueryable<T>, IAsyncEnumerable<T>
    {
        private readonly IQueryable<T> _queryable;
        private readonly Action<string, IEnumerable> _include;

        // Constructor for an IEnumerable data source
        public InMemoryAsyncQueryable(IEnumerable<T> enumerable, Action<string, IEnumerable> include = null)
            : this(enumerable.AsQueryable(), include) { }

        // Constructor for an IQueryable data source
        public InMemoryAsyncQueryable(IQueryable<T> queryable, Action<string, IEnumerable> include = null)
        {
            _queryable = queryable;
            _include = include;
        }

        public IEnumerator<T> GetEnumerator() => _queryable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Expression Expression => _queryable.Expression;

        public Type ElementType => _queryable.ElementType;

        public IQueryProvider Provider => new InMemoryAsyncQueryProvider(_queryable.Provider, _include);

        public IQueryable<T> Include(string path)
        {
            _include?.Invoke(path, _queryable);
            return this;
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator() => new InMemoryAsyncEnumerator<T>(GetEnumerator());

        // TODO: Implement cancellation token
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
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
                                                .Single(m => m.IsGenericMethodDefinition && m.Name == nameof(InMemoryAsyncQueryProvider.CreateQuery));

        private static readonly MethodInfo _executeMethod
            = typeof(InMemoryAsyncQueryProvider).GetTypeInfo().DeclaredMethods
                                                .Single(m => m.IsGenericMethodDefinition && m.Name == nameof(InMemoryAsyncQueryProvider.Execute));

        private readonly IQueryProvider _provider;
        private readonly Action<string, IEnumerable> _include;

        public InMemoryAsyncQueryProvider(IQueryProvider provider, Action<string, IEnumerable> include = null)
        {
            _provider = provider;
            _include = include;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return (IQueryable)_createQueryMethod
                .MakeGenericMethod(TryGetElementType(expression.Type))
                .Invoke(this, new object[] { expression });
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new InMemoryAsyncQueryable<TElement>(_provider.CreateQuery<TElement>(expression), _include);
        public object Execute(Expression expression) => _executeMethod.MakeGenericMethod(expression.Type).Invoke(this, new object[] { expression });
        public TResult Execute<TResult>(Expression expression) => _provider.Execute<TResult>(expression);
        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken) => Task.FromResult(Execute(expression));
        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) => Task.FromResult(Execute<TResult>(expression));
        private static Type TryGetElementType(Type type)
        {
            if (!type.GetTypeInfo().IsGenericTypeDefinition)
            {
                var interfaceImpl = type.GetInterfaces().Union(new[] { type })
                    .FirstOrDefault(t => t.GetType().IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
                if (interfaceImpl != null)
                    return interfaceImpl.GetGenericArguments().Single();
            }
            return type;
        }

        TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        public void Dispose()
        {
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken) => Task.FromResult(_enumerator.MoveNext());

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(Task.FromResult(_enumerator.MoveNext()));
        }

        public ValueTask DisposeAsync()
        {
            return new ValueTask(Task.CompletedTask);
        }

        public T Current => _enumerator.Current;
    }
}


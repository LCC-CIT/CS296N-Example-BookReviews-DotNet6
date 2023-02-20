using System;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;

namespace FakeDbAsync
{



    internal class AsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal AsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new AsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }

        TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


    public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public AsyncEnumerable(Expression expression)
            : base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerator<T> GetEnumerator() =>
            new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }


    public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;

        public AsyncEnumerator(IEnumerator<T> enumerator) =>
            this.enumerator = enumerator ?? throw new ArgumentNullException();

        public T Current => enumerator.Current;

        public void Dispose() => enumerator.Dispose();

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> MoveNext() =>
            Task.FromResult(enumerator.MoveNext());

        public Task<bool> MoveNextAsync() => Task.FromResult(enumerator.MoveNext());

        ValueTask<bool> IAsyncEnumerator<T>.MoveNextAsync()
        {
            throw new NotImplementedException();
        }
    }


}


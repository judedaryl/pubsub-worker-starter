using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using PubSubWorkerStarter.Contracts;

namespace PubSubWorkerStarter.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection;
        private bool _disposed;

        public UnitOfWork(IDatabaseConfig databaseConfig)
        {
            var connectionFactory = databaseConfig.GetConnectionFactory();
            _connection = connectionFactory();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public virtual Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null)
        {
            if (param == null) return _connection.QueryAsync<T>(query, transaction: _transaction);
            return _connection.QueryAsync<T>(query, param, transaction: _transaction);
        }

        public virtual Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TResult>(string query, Func<TFirst, TSecond, TResult> map, object param = null, string splitOn = "Id")
        {
            if (param == null) return _connection.QueryAsync(query, map: map, splitOn: splitOn, transaction: _transaction);
            return _connection.QueryAsync(query, map: map, param, splitOn: splitOn, transaction: _transaction);
        }

        public virtual Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TThird, TResult>(string query, Func<TFirst, TSecond, TThird, TResult> map, object param = null, string splitOn = "Id")
        {
            if (param == null) return _connection.QueryAsync(query, map: map, splitOn: splitOn, transaction: _transaction);
            return _connection.QueryAsync(query, map: map, param, splitOn: splitOn, transaction: _transaction);
        }

        public virtual Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TThird, TFourth, TResult>(string query, Func<TFirst, TSecond, TThird, TFourth, TResult> map, object param = null, string splitOn = "Id")
        {
            if (param == null) return _connection.QueryAsync(query, map: map, splitOn: splitOn, transaction: _transaction);
            return _connection.QueryAsync(query, map: map, param, splitOn: splitOn, transaction: _transaction);
        }

        public virtual Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TResult>(string query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TResult> map, object param = null, string splitOn = "Id")
        {
            if (param == null) return _connection.QueryAsync(query, map: map, splitOn: splitOn, transaction: _transaction);
            return _connection.QueryAsync(query, map: map, param, splitOn: splitOn, transaction: _transaction);
        }

        public virtual Task<T> QueryFirstOrDefaultAsync<T>(string query, object param = null)
        {
            if (param == null) return _connection.QueryFirstOrDefaultAsync<T>(query, transaction: _transaction);
            return _connection.QueryFirstOrDefaultAsync<T>(query, param, transaction: _transaction);
        }

        public virtual Task<int> ExecuteAsync(string query, object param)
        {
            return _connection.ExecuteAsync(query, param);
        }

        public virtual Task<bool> ExecuteScalarAsync(string query, object param)
        {
            return _connection.ExecuteScalarAsync<bool>(query, param);
        }

        public virtual Task<T> ExecuteScalarAsync<T>(string query, object param)
        {
            return _connection.ExecuteScalarAsync<T>(query, param);
        }


        public void SaveChanges()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }


        ~UnitOfWork()
        {
            Dispose(false);
        }
    }

}

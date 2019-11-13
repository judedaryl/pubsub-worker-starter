using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PubSubWorkerStarter
{
    public interface IUnitOfWork
    {
        void Dispose();
        Task<int> ExecuteAsync(string query, object param);
        Task<bool> ExecuteScalarAsync(string query, object param);
        Task<T> ExecuteScalarAsync<T>(string query, object param);
        Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null);
        Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TResult>(string query, Func<TFirst, TSecond, TResult> map, object param = null, string splitOn = "Id");
        Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TResult>(string query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TResult> map, object param = null, string splitOn = "Id");
        Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TThird, TFourth, TResult>(string query, Func<TFirst, TSecond, TThird, TFourth, TResult> map, object param = null, string splitOn = "Id");
        Task<IEnumerable<TResult>> QueryAsync<TFirst, TSecond, TThird, TResult>(string query, Func<TFirst, TSecond, TThird, TResult> map, object param = null, string splitOn = "Id");
        Task<T> QueryFirstOrDefaultAsync<T>(string query, object param = null);
        void Rollback();
        void SaveChanges();
    }
}
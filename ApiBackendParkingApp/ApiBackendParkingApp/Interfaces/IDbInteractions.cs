using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBackendParkingApp.Interfaces
{
    public interface IDbInteractions : IDisposable
    {
        IEnumerable<T> Query<T>(string sql, object param);
        T QueryFirstOrDefault<T>(string sql, object param);
        int Execuce(string sql, object param);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param);
        Task<int> ExecuteAsync(string sql, object param);
    }
}

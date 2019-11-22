using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PubSubWorkerStarter.Contracts
{
    public interface IApiConnect
    {
        Task<T> PostDataAsync<T, T2>(string endPoint, T2 dto);
        Task<T> GetDataAsync<T>(string endPoint);
    }
}

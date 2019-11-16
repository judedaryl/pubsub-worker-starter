using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PubSubWorkerStarter.Contracts
{
    public interface IApiConnect<T> where T : class
    {
        Task<T> GetSampleData(long id);
    }
}

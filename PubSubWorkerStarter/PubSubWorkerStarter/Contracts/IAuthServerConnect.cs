using System.Threading.Tasks;

namespace PubSubWorkerStarter.Contracts
{
    public interface IAuthServerConnect
    {
        Task<string> RequestClientCredentialsTokenAsync();
    }
}

using Google.Cloud.PubSub.V1;
using PubSubWorkerStarter.Entity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PubSubWorkerStarter
{
    public interface IPubSubService
    {
        Task<bool> CreateUserAsync(User user);
        void Dispose();
        Task SubscribeUserCreationAsync(Func<User, Task<SubscriberClient.Reply>> feedHandler, CancellationToken stoppingToken);
    }
}
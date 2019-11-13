using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace PubSubWorkerStarter.Subscribers
{
    public class UserCreationSubscriber : BackgroundService
    {
        private readonly IPubSubService _pubSubService;

        public UserCreationSubscriber(IPubSubService pubSubService)
        {
            _pubSubService = pubSubService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            /// This function call only requires a lambda expression that spits out a
            /// User object and expects a SubscriberClient.Reply enum value
            /// 
            /// Note that PubSub may return duplicated messages
            /// It is up to you to manage this duplication either through a persistence check
            /// or through a caching mechanism
            await _pubSubService.SubscribeUserCreationAsync(async (user) => {

                await Task.Delay(1000);

                /// Ack to acknowledge
                /// Nack to not acknowledge
                return SubscriberClient.Reply.Ack;

            }, stoppingToken);
        }
    }
}

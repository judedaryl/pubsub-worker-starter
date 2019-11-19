using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PubSubWorkerStarter.Entity;
using PubSubWorkerStarter.Infrastructure.Helpers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PubSubWorkerStarter.Services
{
    internal class PubSubService : IDisposable, IPubSubService
    {
        private readonly string _pubSubCredentials;
        private ILogger<PubSubService> _logger;

        #region Topic Names
        /// <summary>
        /// Topic for publishing to the user creation channel in GCP PubSub
        /// </summary>
        private readonly TopicName _userCreationTopic;
        #endregion

        #region Subscription Name
        /// <summary>
        /// SubscriptionName for the user creation channel in GCP PubSub
        /// </summary>
        private readonly SubscriptionName _userCreationSubscription;
        #endregion

        public PubSubService(
            ILogger<PubSubService> logger,
            IConfiguration configuration
            )
        {
            _logger = logger;

            /// This may depend on how your application is hosted.
            /// But if the application is served through kestrel, the credentials are usually found in the
            /// same folder as the published dlls.
            _pubSubCredentials = configuration["GCP:Credentials"];
            _pubSubCredentials = Directory.GetCurrentDirectory() + $"/{_pubSubCredentials}";

            /// Setup GCP variables
            var projectId = configuration["GCP:ProjectID"];
            var userCreateTopicId = configuration["GCP:Topics:createUser"];
            var userCreateSubscriptionId = configuration["GCP:Subscriptions:createUser"];

            /// Create the topic names
            _userCreationTopic = new TopicName(projectId, userCreateTopicId);

            /// Create the subscription names
            _userCreationSubscription = new SubscriptionName(projectId, userCreateSubscriptionId);
        }

        #region Publisher Methods
        public async Task<bool> CreateUserAsync(User user)
        {
            var publisher = PubSubHelper.CreatePublisher(_pubSubCredentials);
            await PublishAsync(publisher, _userCreationTopic, user);
            return true;
        }
        #endregion


        #region Subscriber Methods
        public async Task SubscribeUserCreationAsync(Func<User, Task<SubscriberClient.Reply>> feedHandler, CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Initializing subscription for {_pubSubCredentials} using { _userCreationSubscription }");
            SubscriberServiceApiClient subscriber = PubSubHelper.CreateSubscriber(_pubSubCredentials);
            await SubscribeAsync(subscriber, _userCreationSubscription, feedHandler, stoppingToken);
        }
        #endregion

        #region Generic publisher
        private Task<PublishResponse> PublishAsync<T>(PublisherServiceApiClient publisher, TopicName topic, T obj)
        {
            var message = PubSubHelper.CreateMessage(obj);
            return publisher.PublishAsync(topic, new[] { message });
        }
        #endregion

        #region Generic feed subscription
        private async Task SubscribeAsync<T>(SubscriberServiceApiClient subscriber, SubscriptionName subscription, Func<T, Task<SubscriberClient.Reply>> feedHandler, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                /// The returnImmediately parameter allows the pull async function
                /// to get a response even when the channel is empty.
                /// You can set this to false so that the current context
                /// waits for a message for a bounded amount of time
                PullResponse response = await subscriber.PullAsync(
                    subscription,
                    returnImmediately: true,
                    maxMessages: 20
                );

                foreach (var receivedMessage in response.ReceivedMessages)
                {
                    var mesId = receivedMessage.Message.MessageId;
                    var message = receivedMessage.Message;
                    _logger.LogDebug($"Message received: {message.Data.ToStringUtf8()}");
                    try
                    {
                        /// The extraction process assumes that the best practice of
                        /// wrapping PubSub data in base64 encoding, thus it goes through
                        /// the process of decoding before finally parsing it as a C# object
                        /// This can be disabled by setting the base64decode parameter to false
                        /// e.g. PubSubHelper.Extract<T>(message, false)
                        var extracted = PubSubHelper.Extract<T>(message);
                        var subscriptionResult = await feedHandler(extracted);
                        if (subscriptionResult == SubscriberClient.Reply.Ack)
                        {
                            await subscriber.AcknowledgeAsync(subscription, new[] { receivedMessage.AckId });
                        }
                    }
                    catch (Exception exc)
                    {
                        _logger.LogError($"Error in subscription {subscription.SubscriptionId}\n {exc.Message}\n {exc.StackTrace}");
                    }
                }

                // Add 10s delay per pull
                await Task.Delay(1000);
            }
        }
        #endregion


        #region Cleanup
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                }
                // Release unmanaged resources.
                // Set large fields to null.                
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~PubSubService()
        {
            Dispose(false);
        }
        #endregion
    }
}

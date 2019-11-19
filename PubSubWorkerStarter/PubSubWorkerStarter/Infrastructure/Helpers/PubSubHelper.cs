using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Grpc.Auth;
using Grpc.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PubSubWorkerStarter.Infrastructure.Helpers
{
    /// <summary>
    /// Contains helper methods for GCP PubSub
    /// </summary>
    internal static class PubSubHelper
    {
        /// <summary>
        /// Creates a subscriber client using the SubscriberServiceApiClient.
        /// SubscriberServiceApiClient is a wrapper to the SubscriberClient object
        /// that exposes a creation method by passing a GCP.Core.Channel.
        /// The GCP.Core.Channel can be configured to use a credential path instead of
        /// storing the credential path as a Environment Variable as stated in the 
        /// GCP PubSub docs https://cloud.google.com/pubsub/docs/reference/libraries
        /// </summary>
        /// <param name="credentialsPath"></param>
        /// <returns></returns>
        public static SubscriberServiceApiClient CreateSubscriber(string credentialsPath)
        {
            var channel = CreateChannel(PubSubClientType.SUBSCRIBER, credentialsPath);
            return SubscriberServiceApiClient.Create(channel);
        }

        /// <summary>
        /// Creates a publisher client using the PublisherServiceApiClient.
        /// PublisherServiceApiClient is a wrapper to the PublisherClient object
        /// that exposes a creation method by passing a GCP.Core.Channel.
        /// The GCP.Core.Channel can be configured to use a credential path instead of
        /// storing the credential path as a Environment Variable as stated in the 
        /// GCP PubSub docs https://cloud.google.com/pubsub/docs/reference/libraries
        /// </summary>
        /// <param name="credentialsPath"></param>
        /// <returns></returns>
        public static PublisherServiceApiClient CreatePublisher(string credentialsPath)
        {
            var channel = CreateChannel(PubSubClientType.PUBLISHER, credentialsPath);
            return PublisherServiceApiClient.Create(channel);
        }

        private static Channel CreateChannel(PubSubClientType clientType, string credentialsPath)
        {
            var credentials = GoogleCredential.FromFile(credentialsPath);
            var channelCredentials = credentials.ToChannelCredentials();

            // Set endpoint
            var endpoint = PublisherServiceApiClient.DefaultEndpoint.ToString();
            if (clientType == PubSubClientType.SUBSCRIBER)
                endpoint = SubscriberServiceApiClient.DefaultEndpoint.ToString();

            // Return GCP Channel
            return new Channel(endpoint, channelCredentials);
        }

        public static T Extract<T>(PubsubMessage message, bool base64decode = true)
        {
            var data = message.Data.ToStringUtf8();
            if(base64decode)
                data = Decode(data);
            var obj = JsonConvert.DeserializeObject<T>(data);
            return obj;
        }


        private static string Decode(string encoded)
        {
            var decodedBytes = Convert.FromBase64String(encoded);
            return Encoding.UTF8.GetString(decodedBytes);
        }

        public static string Serialize<T>(T obj)
        {
            JsonSerializerSettings camelCase = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            return JsonConvert.SerializeObject(obj, camelCase);
        }

        public static string Encode(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        public static PubsubMessage CreateMessage<T>(T obj, bool base64Encode = true)
        {
            var defaultAttributes = new Dictionary<string, string>() { { "DateUTC", DateTime.UtcNow.ToString("yyyy-MM-dd") } };
            return CreateMessage(obj, defaultAttributes);
        }

        public static PubsubMessage CreateMessage<T>(T obj, IDictionary<string, string> attributes, bool base64Encode = true)
        {
            var data = Serialize(obj);
            if (base64Encode)
                data = Encode(data);
            var byteString = ByteString.CopyFromUtf8(data);
            var message = new PubsubMessage
            {
                Data = byteString,
                Attributes =
                {
                   attributes
                }
            };
            return message;
        }
    }

}

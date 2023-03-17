using System;
using System.Threading.Tasks;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using Google.Api.Gax.Grpc;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Grpc.Core;
using Microsoft.Graph;
using Newtonsoft.Json;

namespace DbCore
{
    public class PubSub
    {
        private string projectId;
        private string topicId;
        private string subscriptionId;
        private string pushEndpoint;

        public PubSub(string projectId, string topicId, string subscriptionId, string pushEndpoint)
        {
            this.projectId = projectId;
            this.topicId = topicId;
            this.subscriptionId = subscriptionId;
            this.pushEndpoint = pushEndpoint;
        }
        public PubSub() { }
        //Creates a pub/sub subcription or returns it if it alredy exists
        private Google.Cloud.PubSub.V1.Subscription createPushSubscription(string filter)
        {
            SubscriberServiceApiClient subscriber = SubscriberServiceApiClient.Create();
            SubscriberServiceApiClientBuilder builder = new SubscriberServiceApiClientBuilder();
            TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);
            SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription(projectId, subscriptionId);

            PushConfig pushConfig = new PushConfig { PushEndpoint = pushEndpoint };
            Google.Cloud.PubSub.V1.Subscription subscription = null;
            try
            {
                Google.Cloud.PubSub.V1.Subscription subscriptionRequest = new Google.Cloud.PubSub.V1.Subscription
                {
                    SubscriptionName = subscriptionName,
                    TopicAsTopicName = topicName,
                    Filter = filter,
                    PushConfig = pushConfig
                };       

                subscription = subscriber.CreateSubscription(subscriptionRequest);
            }
            catch (RpcException e) when (e.Status.StatusCode == StatusCode.AlreadyExists)
            {
                Console.WriteLine(e.Message);
            }

            return subscription;
        }
        private async Task publishCallWithRetry(int maxAttempts, int initialBackoff, int maxBackoffSeconds, int totalTimeoutSeconds, object message)
        {

            var publisher = await new PublisherClientBuilder
            {
                TopicName = TopicName.FromProjectTopic(projectId, topicId),
                ApiSettings = new PublisherServiceApiSettings
                {
                    PublishSettings = CallSettings.FromRetry(RetrySettings.FromExponentialBackoff(
                               maxAttempts: maxAttempts,
                               initialBackoff: TimeSpan.FromSeconds(initialBackoff),
                               maxBackoff: TimeSpan.FromSeconds(maxBackoffSeconds),
                               backoffMultiplier: 2.00,
                               retryFilter: RetrySettings.FilterForStatusCodes(StatusCode.Unavailable)))
                       .WithTimeout(TimeSpan.FromSeconds(totalTimeoutSeconds))
                }
            }.BuildAsync();
            var pubsubMessage = new PubsubMessage
            {
                Data = ByteString.CopyFromUtf8(JsonConvert.SerializeObject(message)),
                Attributes =
            {
                { "organization", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar() },
            }
            };
            publisher.PublishAsync(pubsubMessage);
        }
        public void PublishPubSub(string filter, int maxAttempts, int initialBackoff, int maxBackoffSeconds, int totalTimeoutSeconds, object message)
        {
            //createPushSubscription(filter);
            publishCallWithRetry(maxAttempts, initialBackoff, maxBackoffSeconds, totalTimeoutSeconds, message);
        }
    }
}

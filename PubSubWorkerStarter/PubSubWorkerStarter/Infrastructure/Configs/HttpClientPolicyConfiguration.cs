namespace PubSubWorkerStarter
{
    internal class HttpClientPolicyConfiguration
    {
        public int RetryCount { get; set; }
        public int RetryDelayInMs { get; set; }
        public int RetryTimeoutInSeconds { get; set; }
        public int BreakDurationInSeconds { get; set; }
        public int MaxAttemptBeforeBreak { get; set; }
        public int HandlerTimeoutInMinutes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PubSubWorkerStarter.Helpers
{
    internal class HttpClientPolicyConfigs
    {
        public int RetryCount { get; set; }
        public int RetryDelayInMs { get; set; }
        public int RetryTimeoutInSeconds { get; set; }
        public int BreakDurationInSeconds { get; set; }
        public int MaxAttemptBeforeBreak { get; set; }
        public int HandlerTimeoutInMinutes { get; set; }
    }
}

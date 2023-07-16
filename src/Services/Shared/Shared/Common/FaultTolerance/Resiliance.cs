using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Polly;
using System;
using System.Threading.Tasks;

namespace SharedKernal.Common.FaultTolerance
{
    public static class Resiliance
    {
        
        

        public static Polly.Retry.RetryPolicy retryPolicy()
        {
            var retry = Policy.Handle<Exception>()
                            .WaitAndRetry(
                                retryCount: 5,
                                sleepDurationProvider: retryAttempt => 
                                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2,4,8,16,32 sc
                                onRetry: (exception, retryCount, context) =>
                                {
                                   // Log.Error($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                                });
            return retry;
        }
        public static async Task<Polly.Wrap.AsyncPolicyWrap> serviceFaultPolicy(ILogger logger)
        {
            var retry = Policy.Handle<Exception>()
                            .WaitAndRetryAsync(
                                retryCount: 5,
                                sleepDurationProvider: retryAttempt =>
                                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), 
                                onRetry: (exception, retryCount, context) =>
                                {
                                    logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                                });

            var circuitBreaker = Policy.Handle<Exception>()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

            var finalPolicy =  retry.WrapAsync(circuitBreaker);


            return finalPolicy;
        }
        public static async Task<Polly.Wrap.AsyncPolicyWrap> mongoDbFaultPolicy(ILogger logger)
        {
            var retry = Policy.Handle<MongoException>()
                            .WaitAndRetryAsync(
                                retryCount: 5,
                                sleepDurationProvider: retryAttempt =>
                                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                onRetry: (exception, retryCount, context) =>
                                {
                                    logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                                });

            var circuitBreaker = Policy.Handle<Exception>()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

            var finalPolicy = retry.WrapAsync(circuitBreaker);


            return finalPolicy;
        }

        public static async Task<Polly.Retry.AsyncRetryPolicy >retryPolicyDb(ILogger logger)
        {
            var retry = Policy.Handle<Exception>()
                            .WaitAndRetryAsync(
                                retryCount: 5,
                                sleepDurationProvider: retryAttempt =>
                                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2,4,8,16,32 sc
                                onRetry: (exception, retryCount, context) =>
                                {
                                    logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                                });
            return retry;
        }
    }
}

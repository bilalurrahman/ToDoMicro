using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Health.V1;
using Grpc.Net.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SharedKernal.Common.Exceptions.Resources;

namespace Authentication.API
{
    public class GrpcHealthCheck : IHealthCheck
    {
        private readonly string _grpcServerUrl;
        private readonly GrpcChannel _channel;

        public GrpcHealthCheck(string grpcServerUrl)
        {
            _grpcServerUrl = grpcServerUrl;
            _channel = GrpcChannel.ForAddress(_grpcServerUrl);
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {

                //var channel_ = GrpcChannel.ForAddress("http://host.docker.internal:5701/LocalizationProtoService");
                //var client = new Health.HealthClient(channel_);
                //var request = new HealthCheckRequest { Service = "LocalizationProtoService" }; // Set the name of the gRPC service to check

                //var response = await client.CheckAsync(new HealthCheckRequest());

                var resp = Resources.GetResources("healthcheck");


                if (resp!=null)
                {
                    return HealthCheckResult.Healthy();
                }
                else
                {
                    return HealthCheckResult.Unhealthy($"gRPC service is not serving. Status: Failed");
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }
        }
    }

}
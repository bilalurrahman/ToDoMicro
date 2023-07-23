using Authentication.Application.Contracts.Persistance;
using Authentication.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedKernal.Common.FaultTolerance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.Persistance
{

   
    public class DeviceQueryRepository : IDeviceQueryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DeviceQueryRepository> logger;

        public DeviceQueryRepository(ILogger<DeviceQueryRepository> logger, 
            IConfiguration configuration)
        {
            this.logger = logger;
            _configuration = configuration;
        }

        private IDbConnection GetQueryConnection()
        {
            return new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:UserDBQueryConnection"));

        }
        public async Task<List<UserNotificationDevices>> getUserDevices(int userId)
        {
            return await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(async () =>
            {
                using (IDbConnection _dbConnection = this.GetQueryConnection())
                {
                    string query = @"SELECT [id]
                              ,[user_id]   
                              ,[device_token]                                                         
                          FROM [dbo].[UserNotificationDevices] with (nolock)
                          where [user_id] = @userId";

                    var devices = await _dbConnection.QueryAsync<UserNotificationDevices>
                    (query, new { userId = userId });

                    return devices.ToList();

                }
            });
        }
    }
}

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
    public class DevicesQueryRepository : IDevicesQueryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DevicesQueryRepository> logger;
        public DevicesQueryRepository(IConfiguration configuration,
            ILogger<DevicesQueryRepository> logger)
        {
            _configuration = configuration;
            this.logger = logger;
        }

        private IDbConnection GetQueryConnection()
        {
            return new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:UserDBQueryConnection"));
        }
        public async Task<List<UserNotificationDevices>> getAllDevices(int userId)
        {
            return await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(async () =>
            {
                using (IDbConnection _dbConnection = this.GetQueryConnection())
                {
                    string query = @"SELECT [id]
                              ,[username] Username   
                              ,[password] Password
                              ,[is_active] isActive                              
                          FROM [dbo].[Users] with (nolock)
                          where [username] = @Username";

                    var getDevices = await _dbConnection.QueryAsync<UserNotificationDevices>
                    (query, new { userID = userId });                    
                    return getDevices.ToList();
                }
            });
        }
    }
}

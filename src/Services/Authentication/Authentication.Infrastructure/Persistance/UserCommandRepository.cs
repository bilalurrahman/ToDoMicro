using Authentication.Application.Contracts.Persistance;
using Authentication.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedKernal.Common.FaultTolerance;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.Persistance
{
    public class UserCommandRepository : IUserCommandRepository
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<UserCommandRepository> logger;
        private IDbConnection GetConnection()
        {

            return new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:UserDBConnection"));
        }

        public async Task<bool> InsertUser(RegisterUser user)
        {
            return await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(async () =>
            {
                using (IDbConnection _dbConnection = this.GetConnection())
                {
                    string query = @"INSERT INTO [dbo].[Users]
                                       ([username]
                                       ,[password]
                                       ,[is_active]
                                       )
                                 VALUES
                                       (@username
                                       ,@password
                                       ,@is_active
                                       )";

                    await _dbConnection.ExecuteAsync(query,
                       new
                       {
                           username = user.Username,
                           password = user.Password,
                           is_active = 1,
                       });
                    return true;
                }
            });
        }

        public Task<bool> UpdateUser(RegisterUser user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateRefreshToken(UserToken userToken)
        {
            return await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(async () =>
            {
                using (IDbConnection _dbConnection = this.GetConnection())
                {
                    string query = @"UPDATE [dbo].[Users]
                                   SET 
		                               [refresh_token] = @refresh_token
                                      ,[refresh_token_expiry] = @refresh_token_expiry
                                 WHERE 
                                       [username]= @username";

                    await _dbConnection.ExecuteAsync(query,
                       new
                       {
                           username = userToken.Username,
                           refresh_token = userToken.refresh_token,
                           refresh_token_expiry = userToken.refresh_token_expiry
                       });
                    return true;
                }
            });
        }

        public async Task<bool> InsertUserDevice(UserNotificationDevices userNotificationDevices)
        {
            return await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(async () =>
            {
                using (IDbConnection _dbConnection = this.GetConnection())
                {
                    string query = @"INSERT INTO [UsersDb].[dbo].[UserNotificationDevices] ([user_id], [device_token])
                                        SELECT @userid, @devicetoken
                                        WHERE NOT EXISTS (
                                            SELECT 1
                                            FROM [dbo].[UserNotificationDevices]
                                            WHERE [device_token] = @devicetoken
                                        )";

                    await _dbConnection.ExecuteAsync(query,
                       new
                       {
                           userid = userNotificationDevices.user_id,
                           devicetoken = userNotificationDevices.device_token                           
                       });
                    return true;
                }
            });
        }

        public UserCommandRepository(IConfiguration configuration, ILogger<UserCommandRepository> logger)
        {
            _configuration = configuration;
            this.logger = logger;
        }


    }
}

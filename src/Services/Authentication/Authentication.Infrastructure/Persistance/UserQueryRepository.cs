using Authentication.Application.Contracts.Persistance;
using Authentication.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedKernal.Common.FaultTolerance;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.Persistance
{
    public class UserQueryRepository : IUserQueryRepository
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<UserQueryRepository> logger;
        private  IDbConnection GetQueryConnection()
        {
         return new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:UserDBQueryConnection"));
             
        }
        public UserQueryRepository(IConfiguration configuration, ILogger<UserQueryRepository> logger)
        {
            _configuration = configuration;
            this.logger = logger;
        }
        public async Task<RegisterUser> GetUserInfo(string username)
        {
            return await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(async () =>
             {
                 using (IDbConnection _dbConnection =  this.GetQueryConnection())
                 {
                     string query = @"SELECT [id]
                              ,[username] Username   
                              ,[password] Password
                              ,[is_active] isActive                              
                          FROM [dbo].[Users] with (nolock)
                          where [username] = @Username";

                     var registeredUser = await _dbConnection.QueryFirstOrDefaultAsync<RegisterUser>(query, new { Username = username });

                     return registeredUser;

                 }
             });
     
  
        }

        public async Task<UserToken> GetRefreshToken(string username)
        {
            return await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(async () =>
            {
                using (IDbConnection _dbConnection =  this.GetQueryConnection())
                {
                    string query = @"SELECT 
                              [refresh_token]
                             ,[refresh_token_expiry]                             
                          FROM [dbo].[Users] with (nolock)
                          where [username] = @Username";

                    var registeredUser = await _dbConnection.QueryFirstOrDefaultAsync<UserToken>(query, new { Username = username });

                    return registeredUser;

                }
            });
        }

        public async Task<int> GetClientLogin(Client client)
        {
            return await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(async () =>
            {
                using (IDbConnection _dbConnection =  this.GetQueryConnection())
                {
                    string query = @"SELECT [ID]                             
                          FROM [dbo].[Clients] with (nolock)
                          WHERE [client_id] = @Username
                          AND  [client_password] = @Password
                          AND [is_active] = 1";

                    var clientExists = await _dbConnection.QueryFirstOrDefaultAsync<int>(query, new { Username = client.Username, Password = client.Password });

                    return clientExists;

                }
            });
        }
    }
}

using Authentication.Application.Contracts.Persistance;
using Authentication.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.Persistance
{
    public class UserQueryRepository : IUserQueryRepository
    {

        private readonly IConfiguration _configuration;
       

        private IDbConnection GetQueryConnection()
        {
            return new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:UserDBQueryConnection"));
        }
        public UserQueryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        public async Task<RegisterUser> GetUserInfo(string username)
        {
 
                using (IDbConnection _dbConnection = this.GetQueryConnection())
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
     
  
        }

        public async Task<UserToken> GetRefreshToken(string username)
        {
            using (IDbConnection _dbConnection = this.GetQueryConnection())
            {
                string query = @"SELECT 
                              [refresh_token]
                             ,[refresh_token_expiry]                             
                          FROM [dbo].[Users] with (nolock)
                          where [username] = @Username";

                var registeredUser = await _dbConnection.QueryFirstOrDefaultAsync<UserToken>(query, new { Username = username });

                return registeredUser;

            }

        }

        public async Task<int> GetClientLogin(Client client)
        {
            using (IDbConnection _dbConnection = this.GetQueryConnection())
            {
                string query = @"SELECT [ID]                             
                          FROM [dbo].[Clients] with (nolock)
                          WHERE [client_id] = @Username
                          AND  [client_password] = @Password
                          AND [is_active] = 1";

                var clientExists = await _dbConnection.QueryFirstOrDefaultAsync<int>(query, new { Username = client.Username, Password = client.Password });

                return clientExists;

            }
        }
    }
}

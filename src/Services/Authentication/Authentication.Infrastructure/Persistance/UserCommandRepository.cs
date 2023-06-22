using Authentication.Application.Contracts.Persistance;
using Authentication.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.Persistance
{
    public class UserCommandRepository : IUserCommandRepository
    {

        private readonly IConfiguration _configuration;
        private IDbConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:UserDBConnection"));
        }

        public async Task<bool> InsertUser(RegisterUser user)
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
        }

        public Task<bool> UpdateUser(RegisterUser user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateRefreshToken(UserToken userToken)
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
        }

        public UserCommandRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

    }
}

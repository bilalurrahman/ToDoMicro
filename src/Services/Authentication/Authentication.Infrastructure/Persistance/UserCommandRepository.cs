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

        public async Task<bool> Insert(RegisterUser user)
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

        public Task<bool> Update(RegisterUser user)
        {
            throw new System.NotImplementedException();
        }

        public UserCommandRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

    }
}

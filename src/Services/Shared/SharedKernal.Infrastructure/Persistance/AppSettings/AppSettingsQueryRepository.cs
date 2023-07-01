using SharedKernal.Core.Interfaces.AppSettings;
using SharedKernal.Core.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SharedKernal.Infrastructure.Persistance.AppSettings
{
    public class AppSettingsQueryRepository : IAppSettingsQueryRepository
    {
        private readonly IConfiguration _configuration;

        public AppSettingsQueryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection GetQueryConnection()
        {
            return new SqlConnection(_configuration["DatabaseSettings:AppSettingsDBConnection"]);
        }

        public async Task<AppSettingsModel> GetAppSettingAsync(int appSettingId)
        {
            using (IDbConnection _dbConnection = this.GetQueryConnection())
            {
                string query = @"SELECT [id]
                              ,[AppSettingName]
                              ,[AppSettingValue]
                              ,[DataType]                              
                          FROM [dbo].[AppSettings] with (nolock)
                          where [id] = @Id";

                var registeredUser = await _dbConnection.QueryFirstOrDefaultAsync<AppSettingsModel>(query, new { Id = appSettingId });

                return registeredUser;

            }
        }
    }
}

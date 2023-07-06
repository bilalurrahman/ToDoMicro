using Dapper;
using Localization.Application.Contracts.Persistance;
using Localization.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Localization.Integration.Persistance
{
    public class LocalizationQueryRepository: ILocalizationQueryRepository
    {
        private readonly IConfiguration _configuration;
        public LocalizationQueryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private IDbConnection GetQueryConnection()
        {
            return new SqlConnection(_configuration["DatabaseSettings:LocalizationRecordsDBQueryConnection"]);
        }
        public async Task<IEnumerable<Records>> GetAll()
        {
            using (IDbConnection _dbConnection = GetQueryConnection())
            {

                List<Records> localizationRecords = null;

                var result = await _dbConnection.QueryAsync<Records, LanguagesEntity, IEnumerable<Records>>(
                    @"
                       SELECT 
	                   P.[Id]
                      ,P.[Key]
                      ,P.[Text]
                      ,P.[LanguageId]                     
                      ,P.[DateCreated]
                      ,P.[DateModified]                  
	                  ,C.[Id]
                      ,C.[Name]                      
                  FROM [Records] P with(nolock) inner JOIN [Languages] C with(nolock) ON P.LanguageId = C.Id", (P, C) =>
                    {
                        if (localizationRecords == null)
                        {
                            localizationRecords = new List<Records>();
                        }
                        P.LocalizationLanguage = C;
                        localizationRecords.Add(P);
                        return localizationRecords;
                    });


                return localizationRecords;
            }
        }



        
    }
}

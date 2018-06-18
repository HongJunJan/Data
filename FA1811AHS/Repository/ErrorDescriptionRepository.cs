using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 錯誤描述資料層
    /// </summary>
    public class ErrorDescriptionRepository : BaseConnetcion<ErrorDescription>, IErrorDescriptionRepository
    {
        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        public List<ErrorDescription> GetAllData()
        {
            List<ErrorDescription> errorDescription = new List<ErrorDescription>();
            const string Sql = @"SELECT [ErrorID]
                                       ,[Ch_Description]
                                       ,[En_Description]
                                   FROM [FA1811-AHS].[dbo].[Error_Description]";
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                return sqlConnection.Query<ErrorDescription>(Sql).ToList();
            }
        }
    }
}
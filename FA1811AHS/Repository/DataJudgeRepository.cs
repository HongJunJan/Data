using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// Judge判定資料層
    /// </summary>
    public class DataJudgeRepository : BaseConnetcion<DataJudge>, IDataJudgeRepository
    {
        /// <summary>
        /// 取得全部資料
        /// </summary>
        /// <returns>串列資料</returns>
        public List<DataJudge> GetAllData()
        {
            List<DataJudge> dataJudge = new List<DataJudge>();
            const string Sql = @"SELECT [Id]
                                       ,[Name]
                                   FROM [FA1811-AHS].[dbo].[DataJudge]
                               ORDER BY [Id]";
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                return sqlConnection.Query<DataJudge>(Sql).ToList();
            }
        }
    }
}
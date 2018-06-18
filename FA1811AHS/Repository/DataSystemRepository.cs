using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 資料層
    /// </summary>
    public class DataSystemRepository : BaseConnetcion<DataSystem>, IDataSystemRepository
    {
        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        public List<DataSystem> GetAllData()
        {
            List<DataSystem> dataSystem = new List<DataSystem>();
            const string Sql = @"SELECT [DataSystem_Group]
                                       ,[DataSystem_No]
                                       ,[DataSystem_Name]
                                       ,[DataSystem_Parameter]
                                       ,[DataSystem_Enable]
                                  FROM [FA1811-AHS].[dbo].[DataSystem]
                                  ORDER BY [DataSystem_Group]";
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                return sqlConnection.Query<DataSystem>(Sql).ToList();
            }
        }

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        public int UpdateData(DataSystem dataPath)
        {
            throw new NotImplementedException();
        }
    }
}
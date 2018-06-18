using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    public class DataPathRepository : BaseConnetcion<DataPath>, IDataPathRepository
    {
        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        public List<DataPath> GetAllData()
        {
            List<DataPath> dataSystem = new List<DataPath>();
            const string Sql = @"SELECT [DataPath_Group]
                                       ,[DataPath_No]
                                       ,[DataPath_Name]
                                       ,[DataPath_Path]
                                       ,[DataPath_Port]
                                       ,[DataPath_AcctNo]
                                       ,[DataPath_Password]
                                  FROM [FA1811-AHS].[dbo].[DataPath]
                                  ORDER BY [DataSystem_Group]";
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                return sqlConnection.Query<DataPath>(Sql).ToList();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        public int UpdateData(DataPath dataPath)
        {
            throw new NotImplementedException();
        }
    }
}
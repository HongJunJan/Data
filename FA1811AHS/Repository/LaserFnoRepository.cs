using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 雷射程序資料層
    /// </summary>
    public class LaserFnoRepository : BaseConnetcion<LaserFNO>, ILaserFnoRepository
    {
        /// <summary>
        /// 取得全部資料，
        /// 回傳物件:串列資料物件
        /// </summary>
        /// <returns>串列資料</returns>
        public List<LaserFNO> GetAllData()
        {
            List<LaserFNO> laserFNO = new List<LaserFNO>();
            const string Sql = @"SELECT [LaserFNO_No]
                                FROM [FA1811-AHS].[dbo].[LaserFNO]
                                ORDER BY [LaserFNO_No]";
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                return sqlConnection.Query<LaserFNO>(Sql).ToList();
            }
        }

        /// <summary>
        /// 取得條件資料，
        /// 傳入條件:程序編號，
        /// 回傳物件:雷射程序物件物件
        /// </summary>
        /// <param name="keyNo">程序編號</param>
        /// <returns></returns>
        public LaserFNO GetConditionData(string keyNo)
        {
            LaserFNO laserFNO = null;
            const string Sql = @"SELECT [LaserFNO_No]
                                FROM [FA1811-AHS].[dbo].[LaserFNO]
                                WHERE [LaserFNO_No] = @keyNo
                                ORDER BY [LaserFNO_No]";
            object param = new { keyNo };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                laserFNO = sqlConnection.Query<LaserFNO>(Sql, param).FirstOrDefault();
            }
            return laserFNO;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns>key</returns>
        public int AddData(LaserFNO model)
        {
            int result = 0;
            const string sql =
                @"INSERT INTO [FA1811-AHS].[dbo].[LaserFNO] ([LaserFNO_No])
                  OUTPUT Inserted.[Id]
                  VALUES (@LaserFnoNo)";
            object param = model;
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                result = sqlConnection.Execute(sql, param);
            }

            return result;
        }
    }
}
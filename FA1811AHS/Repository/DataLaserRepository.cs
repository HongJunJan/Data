using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 雷射參數資料層
    /// </summary>
    public class DataLaserRepository : BaseConnetcion<DataLaser>, IDataLaserRepository
    {
        public List<DataLaser> GetAllData()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns>key</returns>
        public int AddData(DataLaser model)
        {
            int result = 0;
            const string sql =
              @"INSERT INTO [FA1811-AHS].[dbo].[DataLaser] (
	                      [DataLaser_PartNo]
                          ,[DataLaser_FnoNo]
                          ,[DataLaser_Xoffset]
                          ,[DataLaser_Yoffset]
                          ,[DataLaser_Power]
                          ,[DataLaser_Speed])
                OUTPUT Inserted.[DataLaser_PartNo]
                VALUES (@PartNo, @FnoNo, @Xoffset, @Yoffset, @Power, @Speed)";
            object param = model;
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                result = sqlConnection.Execute(sql, param);
            }

            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns>key</returns>
        public int UpdateData(DataLaser model)
        {
            int result = 0;
            const string sql =
                @"UPDATE [dbo].[DataLaser]
                     SET [DataLaser_FnoNo] = @FnoNo
                         ,[DataLaser_Xoffset] = @Xoffset
                         ,[DataLaser_Yoffset] = @Yoffset
                         ,[DataLaser_Power] = @Power
                         ,[DataLaser_Speed] = @Speed
                   WHERE [DataLaser].DataLaser_PartNo = @PartNo";
            object param = new
            {
                model.FnoNo,
                model.Xoffset,
                model.Yoffset,
                model.Power,
                model.Speed,
                model.PartNo
            };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                result = sqlConnection.Execute(sql, param);
            }

            return result;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="keyNo">DB物件</param>
        /// <returns>key</returns>
        public int DeleteData(string keyNo)
        {
            int result = 0;
            const string sql = @"Delete [FA1811-AHS].[dbo].[DataLaser] WHERE [DataLaser_PartNo] = @keyNo";
            object param = new { keyNo };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                result = sqlConnection.Execute(sql, param);
            }

            return result;
        }
    }
}
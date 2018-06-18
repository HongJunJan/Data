using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 料號關聯雷射參數 資料層
    /// </summary>
    public class PartNoJoinLaserRepository : BaseConnetcion<PartNoJoinLaser>, IPartNoJoinLaserRepository
    {
        /// <summary>
        /// 取得料號關聯雷射參數資料
        /// 查詢條件: 料號、查詢方式: 模糊查詢
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns>料號關聯雷射參數物件</returns>
        public List<PartNoJoinLaser> GetLikeConditionData(string keyNo)
        {
            List<PartNoJoinLaser> partNoJoinlaser = null;
            const string Sql =
                @"SELECT [DataPartNo_No]
                          ,[DataPartNo_TrayNo]
                          ,[DataPartNo_PieceSizeX]
                          ,[DataPartNo_PieceSizeY]
                          ,[DataPartNo_PieceSizeT]
                          ,[DataPartNo_2DPositionX]
                          ,[DataPartNo_2DPositionY]
                          ,[DataPartNo_UsesIten]
                          ,[DataPartNo_JudgeStatus]
	                      ,[DataLaser_FnoNo]
	                      ,[DataLaser_Xoffset]
	                      ,[DataLaser_Yoffset]
                          ,[DataLaser_Power]
                          ,[DataLaser_Speed]
                    FROM [FA1811-AHS].[dbo].[DataPartNo] LEFT join [FA1811-AHS].[dbo].[DataLaser]
                    ON [DataPartNo].DataPartNo_No = [DataLaser].DataLaser_PartNo
                    WHERE [DataPartNo_No] LIKE @parameter
                    AND [DataPartNo_UsesIten] != '1'
                    ORDER BY [DataPartNo_No] DESC";
            object param = new { parameter = "%" + keyNo + "%" };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                partNoJoinlaser = sqlConnection.Query<PartNoJoinLaser>(Sql, param).ToList();
            }

            return partNoJoinlaser;
        }
    }
}
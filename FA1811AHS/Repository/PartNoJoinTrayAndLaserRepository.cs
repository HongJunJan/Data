using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 料號關聯料盤與雷射參數 資料層
    /// </summary>
    public class PartNoJoinTrayAndLaserRepository : BaseConnetcion<PartNoJoinTrayAndLaser>, IPartNoJoinTrayAndLaserRepository
    {
        /// <summary>
        /// 取得料號關聯多表單全部資料
        /// </summary>
        /// <returns>串列資料</returns>
        public List<PartNoJoinTrayAndLaser> GetAllData()
        {
            List<PartNoJoinTrayAndLaser> dataPartNo = new List<PartNoJoinTrayAndLaser>();
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
                         ,[DataTray_Name]
                         ,[DataTray_DivideNoX]
                         ,[DataTray_DivideNoY]
                         ,[DataTray_DividePitchX]
                         ,[DataTray_DividePitchY]
                         ,[DataTray_PieceCenterX]
                         ,[DataTray_PieceCenterY]
                         ,[DataTray_TrayThickness]
                         ,[DataTray_TrayCenter]
                         ,[DataTray_TrayLength]
                         ,[DataTray_TrayOffsetX]
                         ,[DataTray_TrayOffsetY]
	                     ,[DataLaser_FnoNo]
	                     ,[DataLaser_Xoffset]
	                     ,[DataLaser_Yoffset]
	                     ,[DataLaser_Power]
	                     ,[DataLaser_Speed]
                    FROM [FA1811-AHS].[dbo].[DataPartNo]
                    LEFT JOIN [dbo].[DataTray]
                    ON [DataPartNo].DataPartNo_TrayNo = [DataTray].DataTray_No
                    LEFT JOIN [dbo].[DataLaser]
                    ON [DataPartNo].DataPartNo_No = [DataLaser].DataLaser_PartNo
                    ORDER BY [DataPartNo_No] DESC";
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                return sqlConnection.Query<PartNoJoinTrayAndLaser>(Sql).ToList();
            }
        }

        /// <summary>
        /// 取得料號關聯多表單資料，
        /// 查詢條件: 料號，查詢方式: 絕對值查詢
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns>串列資料</returns>
        public PartNoJoinTrayAndLaser GetConditionData(string keyNo)
        {
            PartNoJoinTrayAndLaser partNoJoinTrayAndLaser = null;
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
                         ,[DataTray_Name]
                         ,[DataTray_DivideNoX]
                         ,[DataTray_DivideNoY]
                         ,[DataTray_DividePitchX]
                         ,[DataTray_DividePitchY]
                         ,[DataTray_PieceCenterX]
                         ,[DataTray_PieceCenterY]
                         ,[DataTray_TrayThickness]
                         ,[DataTray_TrayCenter]
                         ,[DataTray_TrayLength]
                         ,[DataTray_TrayOffsetX]
                         ,[DataTray_TrayOffsetY]
	                     ,[DataLaser_FnoNo]
	                     ,[DataLaser_Xoffset]
	                     ,[DataLaser_Yoffset]
	                     ,[DataLaser_Power]
	                     ,[DataLaser_Speed]
                    FROM [FA1811-AHS].[dbo].[DataPartNo]
                    LEFT JOIN [dbo].[DataTray]
                    ON [DataPartNo].DataPartNo_TrayNo = [DataTray].DataTray_No
                    LEFT JOIN [dbo].[DataLaser]
                    ON [DataPartNo].DataPartNo_No = [DataLaser].DataLaser_PartNo
                    WHERE [DataPartNo_No] = @keyNo";
            object param = new { keyNo };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                partNoJoinTrayAndLaser = sqlConnection.Query<PartNoJoinTrayAndLaser>(Sql, param).FirstOrDefault();
            }

            return partNoJoinTrayAndLaser;
        }

        /// <summary>
        /// 取得料號關聯多表單資料，
        /// 查詢條件: 料號，查詢方式: 模糊查詢
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns>串列資料</returns>
        public List<PartNoJoinTrayAndLaser> GetLikeConditionData(string keyNo)
        {
            List<PartNoJoinTrayAndLaser> partNoJoinTray = null;
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
                         ,[DataTray_Name]
                         ,[DataTray_DivideNoX]
                         ,[DataTray_DivideNoY]
                         ,[DataTray_DividePitchX]
                         ,[DataTray_DividePitchY]
                         ,[DataTray_PieceCenterX]
                         ,[DataTray_PieceCenterY]
                         ,[DataTray_TrayThickness]
                         ,[DataTray_TrayCenter]
                         ,[DataTray_TrayLength]
                         ,[DataTray_TrayOffsetX]
                         ,[DataTray_TrayOffsetY]
	                     ,[DataLaser_FnoNo]
	                     ,[DataLaser_Xoffset]
	                     ,[DataLaser_Yoffset]
	                     ,[DataLaser_Power]
	                     ,[DataLaser_Speed]
                    FROM [FA1811-AHS].[dbo].[DataPartNo]
                    LEFT JOIN [dbo].[DataTray]
                    ON [DataPartNo].DataPartNo_TrayNo = [DataTray].DataTray_No
                    LEFT JOIN [dbo].[DataLaser]
                    ON [DataPartNo].DataPartNo_No = [DataLaser].DataLaser_PartNo
                    WHERE [DataPartNo_No] LIKE @parameter
                    ORDER BY [DataPartNo_No] DESC";
            object param = new { parameter = "%" + keyNo + "%" };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                partNoJoinTray = sqlConnection.Query<PartNoJoinTrayAndLaser>(Sql, param).ToList();
            }

            return partNoJoinTray;
        }
    }
}
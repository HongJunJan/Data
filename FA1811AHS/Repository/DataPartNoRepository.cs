using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 料號庫資料層
    /// </summary>
    public class DataPartNoRepository : BaseConnetcion<DataPartNo>, IDataPartNoRepository
    {
        /// <summary>
        /// 取得料號資料，
        /// 傳入條件:料號，
        /// 回傳物件: 料號物件
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns></returns>
        public DataPartNo GetConditionData(string keyNo)
        {
            DataPartNo dataPartNo = null;
            const string Sql =
                @"SELECT [DataPartNo_CreateDate]
                        ,[DataPartNo_UpdateDate]
                        ,[DataPartNo_EditUser]
                        ,[DataPartNo_No]
                        ,[DataPartNo_TrayNo]
                        ,[DataPartNo_PieceSizeX]
                        ,[DataPartNo_PieceSizeY]
                        ,[DataPartNo_PieceSizeT]
                        ,[DataPartNo_2DPositionX]
                        ,[DataPartNo_2DPositionY]
                        ,[DataPartNo_UsesIten]
                        ,[DataPartNo_JudgeStatus]
                    FROM [FA1811-AHS].[dbo].[DataPartNo]
                    WHERE [DataPartNo_No] = @keyNo";
            object param = new { keyNo };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                dataPartNo = sqlConnection.Query<DataPartNo>(Sql, param).FirstOrDefault();
            }

            return dataPartNo;
        }

        /// <summary>
        /// 取得料號資料(模糊查詢)，
        /// 傳入條件:料號，
        /// 回傳串列物件: 料號資料物件
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns>料號資料物件</returns>
        public List<DataPartNo> GetLikeConditionData(string keyNo)
        {
            List<DataPartNo> dataPartNo = null;
            const string Sql =
                @"SELECT [DataPartNo_CreateDate]
                        ,[DataPartNo_UpdateDate]
                        ,[DataPartNo_EditUser]
                        ,[DataPartNo_No]
                        ,[DataPartNo_TrayNo]
                        ,[DataPartNo_PieceSizeX]
                        ,[DataPartNo_PieceSizeY]
                        ,[DataPartNo_PieceSizeT]
                        ,[DataPartNo_2DPositionX]
                        ,[DataPartNo_2DPositionY]
                        ,[DataPartNo_UsesIten]
                        ,[DataPartNo_JudgeStatus]
                      FROM [FA1811-AHS].[dbo].[DataPartNo]
                      WHERE [DataPartNo_No] LIKE @parameter
                      AND [DataPartNo_UsesIten] != '1'
                      ORDER BY [DataPartNo_No] DESC";
            object param = new { parameter = "%" + keyNo + "%" };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                dataPartNo = sqlConnection.Query<DataPartNo>(Sql, param).ToList();
            }

            return dataPartNo;
        }

        public List<DataPartNo> GetAllData()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">物件</param>
        /// <returns>key</returns>
        public int AddData(DataPartNo model)
        {
            int result = 0;
            const string sql =
              @"INSERT INTO [FA1811-AHS].[dbo].[DataPartNo] (
	                      [DataPartNo_CreateDate]
                         ,[DataPartNo_UpdateDate]
                         ,[DataPartNo_EditUser]
                         ,[DataPartNo_No]
                         ,[DataPartNo_TrayNo]
                         ,[DataPartNo_PieceSizeX]
                         ,[DataPartNo_PieceSizeY]
                         ,[DataPartNo_PieceSizeT]
                         ,[DataPartNo_2DPositionX]
                         ,[DataPartNo_2DPositionY]
                         ,[DataPartNo_UsesIten]
                         ,[DataPartNo_JudgeStatus])
                OUTPUT Inserted.[DataPartNo_No]
                VALUES (
	                     @CreateDate
	                    ,@UpdateDate
	                    ,@EditUser
	                    ,@PartNo
	                    ,@TrayNo
	                    ,@PieceSizeX
	                    ,@PieceSizeY
	                    ,@PieceSizeT
	                    ,@PositionX2D
                        ,@PositionY2D
                        ,@UsesIten
                        ,@JudgeStatus)";
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
        /// <param name="model">物件</param>
        /// <returns>key</returns>
        public int UpdateData(DataPartNo model)
        {
            int result = 0;
            const string sql =
                @"UPDATE [FA1811-AHS].[dbo].[DataPartNo]
                     SET [DataPartNo_UpdateDate] = @UpdateDate
                        ,[DataPartNo_EditUser] = @EditUser
                        ,[DataPartNo_TrayNo] = @TrayNo
                        ,[DataPartNo_PieceSizeX] = @PieceSizeX
                        ,[DataPartNo_PieceSizeY] = @PieceSizeY
                        ,[DataPartNo_PieceSizeT] = @PieceSizeT
                        ,[DataPartNo_2DPositionX] = @PositionX2D
                        ,[DataPartNo_2DPositionY] = @PositionY2D
                        ,[DataPartNo_UsesIten] = @UsesIten
                        ,[DataPartNo_JudgeStatus] = @JudgeStatus
                   WHERE [DataPartNo_No] = @PartNo";
            object param = new
            {
                model.UpdateDate,
                model.EditUser,
                model.TrayNo,
                model.PieceSizeX,
                model.PieceSizeY,
                model.PieceSizeT,
                model.PositionX2D,
                model.PositionY2D,
                model.UsesIten,
                model.JudgeStatus,
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
        /// <param name="keyNo">key編號</param>
        /// <returns>key</returns>
        public int DeleteData(string keyNo)
        {
            int result = 0;
            const string sql = @"Delete [FA1811-AHS].[dbo].[DataPartNo] WHERE [DataPartNo_No] = @keyNo";
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
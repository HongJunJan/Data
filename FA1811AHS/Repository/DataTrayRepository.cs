using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 料盤資料層
    /// </summary>
    public class DataTrayRepository : BaseConnetcion<DataTray>, IDataTrayRepository
    {
        /// <summary>
        /// 取得料盤查詢條件資料，
        /// 傳入條件: 料盤編號
        /// 回傳物件: 料盤物件
        /// </summary>
        /// <param name="keyNo">料盤編號</param>
        /// <returns></returns>
        public DataTray GetConditionData(string keyNo)
        {
            DataTray dataTray = null;
            const string Sql =
                @"SELECT [DataTray_CreateDate]
                        ,[DataTray_UpdateDate]
                        ,[DataTray_EditUser]
                        ,[DataTray_No]
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
                   FROM [FA1811-AHS].[dbo].[DataTray]
                   WHERE [DataTray_No] = @keyNo
                   ORDER BY [DataTray_No] DESC";
            object param = new { keyNo };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                dataTray = sqlConnection.Query<DataTray>(Sql, param).FirstOrDefault();
            }

            return dataTray;
        }

        /// <summary>
        /// 取得料盤查詢條件資料(模糊查詢)，
        /// 傳入條件: 料盤編號
        /// 回傳串列物件: 料盤資料物件
        /// </summary>
        /// <param name="keyNo">料盤編號</param>
        /// <returns>料盤物件</returns>
        public List<DataTray> GetLikeConditionData(string keyNo)
        {
            List<DataTray> dataTray = null;
            const string Sql =
                @"SELECT [DataTray_CreateDate]
                        ,[DataTray_UpdateDate]
                        ,[DataTray_EditUser]
                        ,[DataTray_No]
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
                    FROM [FA1811-AHS].[dbo].[DataTray]
                    WHERE [DataTray_No] LIKE @parameter
                    ORDER BY [DataTray_No] DESC";
            object param = new { parameter = "%" + keyNo + "%" };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                dataTray = sqlConnection.Query<DataTray>(Sql, param).ToList();
            }

            return dataTray;
        }

        /// <summary>
        /// 取得全部資料
        /// </summary>
        /// <returns>串列資料</returns>
        public List<DataTray> GetAllData()
        {
            List<DataTray> dataTray = new List<DataTray>();
            const string Sql = @"SELECT [DataTray_CreateDate]
                                       ,[DataTray_UpdateDate]
                                       ,[DataTray_EditUser]
                                       ,[DataTray_No]
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
                                FROM [FA1811-AHS].[dbo].[DataTray]
                                ORDER BY [DataTray_No] DESC";
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                return sqlConnection.Query<DataTray>(Sql).ToList();
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns>key</returns>
        public int AddData(DataTray model)
        {
            int result = 0;
            const string sql =
              @"INSERT INTO [FA1811-AHS].[dbo].[DataTray] (
                            [DataTray_CreateDate]
                           ,[DataTray_UpdateDate]
                           ,[DataTray_EditUser]
                           ,[DataTray_No]
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
                           ,[DataTray_TrayOffsetY])
                OUTPUT Inserted.[DataTray_No]
                VALUES ( @CreateDate
                        ,@UpdateDate
                        ,@EditUser
                        ,@TrayNo
                        ,@TrayName
                        ,@DivideNoX
                        ,@DivideNoY
                        ,@DividePitchX
                        ,@DividePitchY
                        ,@PieceCenterX
                        ,@PieceCenterY
                        ,@TrayThickness
                        ,@TrayCenter
                        ,@TrayLength
                        ,@TrayOffsetX
                        ,@TrayOffsetY)";
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
        public int UpdateData(DataTray model)
        {
            int result = 0;
            const string sql =
                @"UPDATE [FA1811-AHS].[dbo].[DataTray]
                     SET [DataTray_UpdateDate] = @UpdateDate
                        ,[DataTray_EditUser] = @EditUser
                        ,[DataTray_Name] = @TrayName
                        ,[DataTray_DivideNoX] = @DivideNoX
                        ,[DataTray_DivideNoY] = @DivideNoY
                        ,[DataTray_DividePitchX] = @DividePitchX
                        ,[DataTray_DividePitchY] = @DividePitchY
                        ,[DataTray_PieceCenterX] = @PieceCenterX
                        ,[DataTray_PieceCenterY] = @PieceCenterY
                        ,[DataTray_TrayThickness] = @TrayThickness
                        ,[DataTray_TrayCenter] = @TrayCenter
                        ,[DataTray_TrayLength] = @TrayLength
                        ,[DataTray_TrayOffsetX] = @TrayOffsetX
                        ,[DataTray_TrayOffsetY] = @TrayOffsetY
                   WHERE [DataTray_No] = @TrayNo";
            object param = new
            {
                model.UpdateDate,
                model.EditUser,
                model.TrayName,
                model.DivideNoX,
                model.DivideNoY,
                model.DividePitchX,
                model.DividePitchY,
                model.PieceCenterX,
                model.PieceCenterY,
                model.TrayThickness,
                model.TrayCenter,
                model.TrayLength,
                model.TrayOffsetX,
                model.TrayOffsetY,
                model.TrayNo
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
        /// <param name="model">key編號</param>
        /// <returns>key</returns>
        public int DeleteData(string keyNo)
        {
            int result = 0;
            const string sql = @"Delete [FA1811-AHS].[dbo].[DataTray] WHERE [DataTray_No] = @keyNo";
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
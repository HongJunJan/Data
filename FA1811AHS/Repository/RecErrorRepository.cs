using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 錯誤紀錄資料層
    /// </summary>
    public class RecErrorRepository : BaseConnetcion<RecError>, IRecErrorRepository
    {
        /// <summary>
        /// 取得故障表查詢條件資料，
        /// 傳入條件:1.開始區間日期、2.結束區間日期
        /// 回傳:List資料
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<RecError> GetConditionData(DateTime startDate, DateTime endDate)
        {
            List<RecError> recError = null;
            const string Sql =
                @"SELECT [EndTime]
                        ,[StartTime]
                        ,[Message]
                        ,[UserName]
                        ,[ErrorID]
                    FROM [FA1811-AHS].[dbo].[Rec_Error]
                    WHERE [EndTime] > @startDate  AND [EndTime] < @endDate
                    ORDER BY [EndTime] DESC";
            object param = new { startDate, endDate };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                recError = sqlConnection.Query<RecError>(Sql, param).ToList();
            }
            return recError;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">錯誤紀錄物件</param>
        /// <returns>key</returns>
        public int AddData(RecError model)
        {
            int result = 0;
            const string sql =
                @"INSERT INTO [dbo].[Rec_Error] (
                                    [EndTime]
                                   ,[StartTime]
                                   ,[Message]
                                   ,[UserName]
                                   ,[ErrorID])
                       VALUES (@EndTime
                              ,@StartTime
                              ,@Message
                              ,@UserName
                              ,@ErrorID)";
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
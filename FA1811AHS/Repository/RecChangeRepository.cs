using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 異動紀錄資料層
    /// </summary>
    public class RecChangeRepository : BaseConnetcion<RecChange>, IRecChangeRepository
    {
        /// <summary>
        /// 取得異動紀錄查詢條件資料，
        /// 傳入條件:1.開始日期、2.結束日期
        /// 回傳:List資料
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<RecChange> GetConditionData(DateTime startDate, DateTime endDate)
        {
            List<RecChange> recChange = null;
            const string Sql =
                @"SELECT [NowTime]
                        ,[Message]
                        ,[UserName]
                    FROM [FA1811-AHS].[dbo].[Rec_Change]
                    WHERE [NowTime] > @startDate  AND [NowTime] < @endDate
                    ORDER BY [NowTime] DESC";
            object param = new { startDate, endDate };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                recChange = sqlConnection.Query<RecChange>(Sql, param).ToList();
            }
            return recChange;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddData(RecChange model)
        {
            int result = 0;
            const string sql =
                @"INSERT INTO [dbo].[Rec_Change] (
                                    [NowTime]
                                   ,[Message]
                                   ,[UserName])
                       VALUES (@NowTime
                              ,@Message
                              ,@UserName)";
            object param = model;
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                result = sqlConnection.Execute(sql, param);
            }
            return result;
        }

        public List<RecChange> GetAllData()
        {
            throw new NotImplementedException();
        }

        public int UpdateData(RecChange model)
        {
            throw new System.NotImplementedException();
        }

        public int DeleteData(string keyNo)
        {
            throw new System.NotImplementedException();
        }
    }
}
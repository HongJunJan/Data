using System;
using System.Collections.Generic;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 異動紀錄-介面
    /// </summary>
    public interface IRecChangeRepository : IBaseRepository<RecChange>
    {
        /// <summary>
        /// 取得異動紀錄資料，
        /// 傳入條件:1.開始日期、2.結束日期
        /// 回傳:List資料
        /// </summary>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <returns>List資料</returns>
        List<RecChange> GetConditionData(DateTime startDate, DateTime endDate);
    }
}
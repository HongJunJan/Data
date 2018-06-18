using System;
using System.Collections.Generic;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 錯誤紀錄資料層-介面
    /// </summary>
    public interface IRecErrorRepository
    {
        /// <summary>
        /// 取得故障表查詢條件資料，
        /// 傳入條件:1.開始日期、2.結束日期
        /// 回傳:List資料
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<RecError> GetConditionData(DateTime startDate, DateTime endDate);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns>key</returns>
        int AddData(RecError model);
    }
}
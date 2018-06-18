using FA1811AHS.Repository;
using System;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 錯誤紀錄服務-介面
    /// </summary>
    public interface IRecErrorService
    {
        /// <summary>
        /// 取得故障錯誤紀錄資料，
        /// 傳入條件:1.開始區間日期、2.結束區間日期
        /// 回傳:List資料
        /// </summary>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <returns>List資料</returns>
        ResponseModel GetConditionData(DateTime startDate, DateTime endDate);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">錯誤紀錄物件</param>
        /// <returns>資料回傳物件</returns>
        ResponseModel AddData(RecError model);
    }
}
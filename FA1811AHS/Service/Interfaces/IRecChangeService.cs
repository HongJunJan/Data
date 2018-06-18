using FA1811AHS.Repository;
using System;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 異動紀錄服務-介面
    /// </summary>
    public interface IRecChangeService
    {
        /// <summary>
        /// 取得異動紀錄資料，
        /// 傳入條件:1.開始日期、2.結束日期
        /// 回傳:資料回傳物件
        /// </summary>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <returns>List資料</returns>
        ResponseModel GetConditionData(DateTime startDate, DateTime endDate);

        /// <summary>
        /// 新增，
        /// 傳入條件:異動紀錄物件，
        /// 回傳:資料回傳物件
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns></returns>
        ResponseModel AddData(RecChange model);

        /// <summary>
        /// 刪除，
        /// 傳入條件:當月第一天，
        /// 回傳:資料回傳物件
        /// </summary>
        /// <param name="firstDate"></param>
        /// <returns></returns>
        ResponseModel DeleteData(DateTime firstDate);
    }
}
using System.Collections.Generic;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 料盤資料層
    /// </summary>
    public interface IDataTrayRepository : IBaseRepository<DataTray>
    {
        /// <summary>
        /// 取得料盤查詢條件資料，
        /// 傳入條件: 料盤編號
        /// 回傳物件: 料盤物件
        /// </summary>
        /// <param name="keyNo">料盤編號</param>
        /// <returns></returns>
        DataTray GetConditionData(string keyNo);

        /// <summary>
        /// 取得料盤查詢條件資料(模糊查詢)，
        /// 傳入條件: 料盤編號
        /// 回傳串列物件: 料盤資料物件
        /// </summary>
        /// <param name="keyNo">料盤編號</param>
        /// <returns></returns>
        List<DataTray> GetLikeConditionData(string keyNo);
    }
}
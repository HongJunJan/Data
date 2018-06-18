using System.Collections.Generic;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 料號庫資料層
    /// </summary>
    public interface IDataPartNoRepository : IBaseRepository<DataPartNo>
    {
        /// <summary>
        /// 取得料號資料，
        /// 傳入條件:料號，
        /// 回傳物件: 料號物件
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns>料號物件</returns>
        DataPartNo GetConditionData(string keyNo);

        /// <summary>
        /// 取得料號資料(模糊查詢)，
        /// 傳入條件:料號，
        /// 回傳串列物件: 料號資料物件
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns>料號資料物件</returns>
        List<DataPartNo> GetLikeConditionData(string keyNo);
    }
}
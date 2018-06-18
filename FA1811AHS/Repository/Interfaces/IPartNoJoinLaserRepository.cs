using System.Collections.Generic;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 料號join雷射參數資料層
    /// </summary>
    public interface IPartNoJoinLaserRepository
    {
        /// <summary>
        /// 取得料號關聯雷射參數資料
        /// 查詢條件: 料號、查詢方式: 模糊查詢
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns>料號關聯雷射參數物件</returns>
        List<PartNoJoinLaser> GetLikeConditionData(string keyNo);
    }
}
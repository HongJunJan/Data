using System.Collections.Generic;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 料號join料盤與雷射參數資料層
    /// </summary>
    public interface IPartNoJoinTrayAndLaserRepository
    {
        /// <summary>
        /// 取得料號關聯多表單全部資料
        /// </summary>
        /// <returns>串列資料</returns>
        List<PartNoJoinTrayAndLaser> GetAllData();

        /// <summary>
        /// 取得料號關聯多表單資料，
        /// 查詢條件: 料號，
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns>物件</returns>
        PartNoJoinTrayAndLaser GetConditionData(string keyNo);

        /// <summary>
        /// 取得料號關聯多表單資料(模糊查詢)，
        /// 查詢條件: 料號
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns>串列資料物件</returns>
        List<PartNoJoinTrayAndLaser> GetLikeConditionData(string keyNo);
    }
}
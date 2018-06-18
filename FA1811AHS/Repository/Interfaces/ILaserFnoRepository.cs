using System.Collections.Generic;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 雷射程序資料層
    /// </summary>
    public interface ILaserFnoRepository
    {
        /// <summary>
        /// 取得全部資料，
        /// 回傳物件:串列資料物件
        /// </summary>
        /// <returns>串列資料</returns>
        List<LaserFNO> GetAllData();

        /// <summary>
        /// 取得條件資料，
        /// 傳入條件:程序編號，
        /// 回傳物件:雷射程序物件物件
        /// </summary>
        /// <param name="keyNo">程序編號</param>
        /// <returns></returns>
        LaserFNO GetConditionData(string keyNo);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns>key</returns>
        int AddData(LaserFNO model);
    }
}
using System.Collections.Generic;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 路徑資料-介面
    /// </summary>
    public interface IDataPathRepository
    {
        /// <summary>
        /// 取得所有資料，
        /// 回傳:List資料物件
        /// </summary>
        /// <returns></returns>
        List<DataPath> GetAllData();

        /// <summary>
        /// 更新資料，
        /// 傳入條件:DB物件
        /// </summary>
        /// <param name="dataPath">DB物件</param>
        /// <returns></returns>
        int UpdateData(DataPath dataPath);
    }
}
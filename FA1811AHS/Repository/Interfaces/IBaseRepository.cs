using System.Collections.Generic;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// Base資料層泛型介面
    /// </summary>
    /// <typeparam name="DbModel">DB物件</typeparam>
    public interface IBaseRepository<DbModel>
    {
        /// <summary>
        /// 取得全部資料，
        /// 回傳物件:串列資料物件
        /// </summary>
        /// <returns>串列資料</returns>
        List<DbModel> GetAllData();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns>key</returns>
        int AddData(DbModel model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns>key</returns>
        int UpdateData(DbModel model);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="keyNo">key編號</param>
        /// <returns>key</returns>
        int DeleteData(string keyNo);
    }
}
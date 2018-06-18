using System.Collections.Generic;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 故障描述資料層-介面
    /// </summary>
    public interface IErrorDescriptionRepository
    {
        /// <summary>
        /// 取得全部資料，
        /// 回傳物件:串列資料物件
        /// </summary>
        /// <returns>串列資料</returns>
        List<ErrorDescription> GetAllData();
    }
}
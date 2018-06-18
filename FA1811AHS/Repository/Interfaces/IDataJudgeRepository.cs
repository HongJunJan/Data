using System.Collections.Generic;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// Judge判定資料層
    /// </summary>
    public interface IDataJudgeRepository
    {
        /// <summary>
        /// 取得全部資料
        /// </summary>
        /// <returns>串列資料</returns>
        List<DataJudge> GetAllData();
    }
}
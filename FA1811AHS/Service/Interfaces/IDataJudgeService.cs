using FA1811AHS.Repository;

namespace FA1811AHS.Service
{
    /// <summary>
    /// Judge判定服務
    /// </summary>
    public interface IDataJudgeService
    {
        /// <summary>
        /// 取得所有Judge代碼
        /// </summary>
        /// <returns>回傳帳戶輸出物件</returns>
        ResponseModel GetAllData();
    }
}
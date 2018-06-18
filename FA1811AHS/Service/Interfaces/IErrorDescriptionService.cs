namespace FA1811AHS.Service
{
    /// <summary>
    /// 錯誤描述服務-介面
    /// </summary>
    public interface IErrorDescriptionService
    {
        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns>回傳輸出物件</returns>
        ResponseModel GetAllData();
    }
}
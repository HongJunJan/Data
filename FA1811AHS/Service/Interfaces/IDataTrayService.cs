using FA1811AHS.Repository;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 料盤服務(邏輯層)-介面
    /// </summary>
    public interface IDataTrayService : IBaseService<DataTray, ResponseModel>
    {
        /// <summary>
        /// 取得料盤所有資料
        /// </summary>
        /// <returns>資料回傳物件</returns>
        ResponseModel GetAllData();

        /// <summary>
        /// 取得料盤查詢條件資料
        /// 傳入條件: 料盤編號
        /// </summary>
        /// <param name="trayNo">料盤編號</param>
        /// <returns>資料回傳物件</returns>
        ResponseModel GetConditionData(string trayNo);

        /// <summary>
        /// 取得料盤查詢條件資料(模糊查詢)
        /// 查詢條件: 料盤編號
        /// </summary>
        /// <param name="trayNo">料盤編號</param>
        /// <returns>資料回傳物件</returns>
        ResponseModel GetLikeCondition(string trayNo);
    }
}
namespace FA1811AHS.Service
{
    /// <summary>
    /// 料號關聯料盤與雷射服務-介面
    /// </summary>
    public interface IPartNoJoinTrayAndLaserService
    {
        /// <summary>
        /// 取得所有料號關聯料盤與雷射資料
        /// </summary>
        /// <returns>資料回傳物件</returns>
        ResponseModel GetAllData();

        /// <summary>
        /// 取得料號關聯料盤與雷射資料，
        /// 查詢條件:料號
        /// </summary>
        /// <param name="partNo">料號</param>
        /// <returns>資料回傳物件</returns>
        ResponseModel GetConditionMain(string partNo);

        /// <summary>
        /// 取得料號關聯料盤與雷射資料(模糊查詢)，
        /// 查詢條件:料號
        /// </summary>
        /// <param name="partNo">料號</param>
        /// <returns>資料回傳物件</returns>
        ResponseModel GetLikeConditionMain(string partNo);
    }
}
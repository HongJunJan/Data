namespace FA1811AHS.Service
{
    /// <summary>
    /// 料號關聯雷射參數-介面
    /// </summary>
    public interface IPartNoJoinLaserService
    {
        /// <summary>
        /// 取得料號關聯雷射參數資料(模糊查詢)
        /// 查詢條件:料號
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns>資料回傳物件</returns>
        ResponseModel GetLikeConditionMain(string keyNo);
    }
}
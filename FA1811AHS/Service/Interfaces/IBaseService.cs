namespace FA1811AHS.Service
{
    /// <summary>
    /// 邏輯層泛型物件-介面
    /// </summary>
    /// <typeparam name="DbModel">DB物件</typeparam>
    /// <typeparam name="ResponseModel">資料回傳物件</typeparam>
    public interface IBaseService<DbModel, ResponseModel>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns>資料回傳物件</returns>
        ResponseModel AddData(DbModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns>資料回傳物件</returns>
        ResponseModel UpdateData(DbModel model);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="keyNo">key編號</param>
        /// <returns>資料回傳物件</returns>
        ResponseModel DeleteData(string keyNo);
    }
}
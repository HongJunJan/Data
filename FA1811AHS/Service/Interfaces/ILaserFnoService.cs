using FA1811AHS.Repository;

namespace FA1811AHS.Service
{
    /// <summary>
    ///  雷射程序服務(邏輯層)-介面
    /// </summary>
    public interface ILaserFnoService
    {
        /// <summary>
        /// 取得所有雷射程序編號
        /// </summary>
        /// <returns>資料回傳物件</returns>
        ResponseModel GetAllData();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">DB物件</param>
        /// <returns>資料回傳物件</returns>
        ResponseModel AddData(LaserFNO model);
    }
}
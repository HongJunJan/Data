using FA1811AHS.Repository;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 批號邏輯層
    /// </summary>
    public interface IDataLotService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">批號物件</param>
        /// <returns>資料回傳物件</returns>
        ResponseModel AddData(DataLot model);
    }
}
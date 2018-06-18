using FA1811AHS.Repository;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 料號庫服務(邏輯層)-介面
    /// </summary>
    public interface IDataPartNoService
    {
        /// <summary>
        /// 取得料號資料，只搜尋有使用雷射的資料(模糊查詢)
        /// 查詢條件:料號 與 雷射使用控制(0:全使用、2:使用雷射)、
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns></returns>
        ResponseModel GetLikeCondition(string keyNo);

        /// <summary>
        /// 新增料號與雷射參數
        /// </summary>
        /// <param name="dataPartNo">料號庫物件</param>
        /// <param name="dataLaser">雷射參數物件</param>
        /// <returns></returns>
        ResponseModel AddPartNoAndLaser(DataPartNo dataPartNo, DataLaser dataLaser);

        /// <summary>
        /// 更新料號與雷射參數
        /// </summary>
        /// <param name="dataPartNo">料號庫物件</param>
        /// <param name="dataLaser">雷射參數物件</param>
        /// <returns></returns>
        ResponseModel UpdatePartNoAndLaser(DataPartNo dataPartNo, DataLaser dataLaser);

        /// <summary>
        /// 刪除料號與雷射參數
        /// </summary>
        /// <param name="partNo">料號</param>
        /// <returns></returns>
        ResponseModel DeletePartNoAndLaser(string keyNo);
    }
}
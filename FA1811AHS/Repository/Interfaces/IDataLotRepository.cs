namespace FA1811AHS.Repository
{
    /// <summary>
    /// 批號資料層
    /// </summary>
    public interface IDataLotRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">批號物件</param>
        /// <returns></returns>
        int AddData(DataLot model);
    }
}
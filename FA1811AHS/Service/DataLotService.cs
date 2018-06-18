using FA1811AHS.Repository;
using FA1811AHS.Shared;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 批號邏輯層
    /// </summary>
    public class DataLotService : IDataLotService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel AddData(DataLot model)
        {
            IDataLotRepository dataLotRepository = new DataLotRepository();
            ResponseModel ResponseModel = new ResponseModel();
            int response = dataLotRepository.AddData(model);
            if (response.Equals(0))
            {
                ResponseModel.ResponseMsg = StatusEnum.Error3.GetEnumDescription();
            }
            else
            {
                ResponseModel.Status = StatusEnum.Ok;
                ResponseModel.ResponseMsg = StatusEnum.Ok.GetEnumDescription();
            }

            return ResponseModel;
        }
    }
}
using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 雷射參數服務(邏輯層)
    /// </summary>
    public class DataLaserService : IDataLaserService
    {
        private IDataLaserRepository dataLaserRepository = new DataLaserRepository();
        private IDataPartNoRepository dataPartNoRepository = new DataPartNoRepository();

        public ResponseModel AddData(DataLaser model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel UpdateData(DataLaser model)
        {
            ResponseModel ResponseModel = new ResponseModel();
            DataPartNo dataPartNo = dataPartNoRepository.GetConditionData(model.PartNo);
            if (dataPartNo == null)
            {
                ResponseModel.ResponseMsg = PartNoEnum.Error2.GetEnumDescription();
            }
            else
            {
                int response = dataLaserRepository.UpdateData(model);
                if (response.Equals(0))
                {
                    ResponseModel.ResponseMsg = StatusEnum.Error4.GetEnumDescription();
                }
                else
                {
                    ResponseModel.Status = StatusEnum.Ok;
                    ResponseModel.ResponseMsg = StatusEnum.Ok.GetEnumDescription();
                }
            }

            return ResponseModel;
        }

        public ResponseModel DeleteData(string keyNo)
        {
            throw new NotImplementedException();
        }
    }
}
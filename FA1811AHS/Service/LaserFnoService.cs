using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System;
using System.Collections.Generic;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 雷射程序服務(邏輯層)
    /// </summary>
    public class LaserFnoService : ILaserFnoService
    {
        private ILaserFnoRepository laserFnoRepository = new LaserFnoRepository();

        /// <summary>
        /// 取得所有雷射程序編號
        /// </summary>
        /// <returns></returns>
        public ResponseModel GetAllData()
        {
            List<LaserFNO> list = laserFnoRepository.GetAllData();
            return new ResponseModel()
            {
                Status = StatusEnum.Ok,
                LaserFnoList = list
            };
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">程序編號</param>
        /// <returns></returns>
        public ResponseModel AddData(LaserFNO model)
        {
            ResponseModel responseModel = new ResponseModel();
            LaserFNO laserFNO = laserFnoRepository.GetConditionData(model.LaserFnoNo);
            if (laserFNO == null)
            {
                int response = laserFnoRepository.AddData(model);
                if (response.Equals(0))
                {
                    responseModel.ResponseMsg = StatusEnum.Error3.GetEnumDescription();
                }
                else
                {
                    responseModel.Status = StatusEnum.Ok;
                    responseModel.ResponseMsg = StatusEnum.Ok.GetEnumDescription();
                }
            }
            else
            {
                responseModel.Status = StatusEnum.Ok;
                responseModel.ResponseMsg = StatusEnum.Ok.GetEnumDescription();
            }

            return responseModel;
        }
    }
}
using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System;
using System.Collections.Generic;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 錯誤紀錄服務
    /// </summary>
    public class RecErrorService : IRecErrorService
    {
        private IRecErrorRepository recErrorRepository = new RecErrorRepository();

        /// <summary>
        /// 取得故障錯誤紀錄資料，
        /// 傳入條件:1.開始區間日期、2.結束區間日期
        /// 回傳:List資料
        /// </summary>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <returns>List資料</returns>
        public ResponseModel GetConditionData(DateTime startDate, DateTime endDate)
        {
            List<RecError> recChange = recErrorRepository.GetConditionData(startDate, endDate);
            return new ResponseModel
            {
                Status = StatusEnum.Ok,
                RecErrorList = recChange
            };
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel AddData(RecError model)
        {
            ResponseModel responseModel = new ResponseModel();
            int response = recErrorRepository.AddData(model);
            if (response.Equals(0))
            {
                responseModel.ResponseMsg = StatusEnum.Error3.GetEnumDescription();
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
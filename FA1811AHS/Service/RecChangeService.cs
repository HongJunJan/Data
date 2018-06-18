using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System;
using System.Collections.Generic;

namespace FA1811AHS.Service
{
    public class RecChangeService : IRecChangeService
    {
        private IRecChangeRepository recChangeRepository = new RecChangeRepository();

        /// <summary>
        /// 取得異動紀錄資料，
        /// 傳入條件:1.開始日期、2.結束日期
        /// 回傳:List資料
        /// </summary>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <returns>List資料</returns>
        public ResponseModel GetConditionData(DateTime startDate, DateTime endDate)
        {
            List<RecChange> recChange = recChangeRepository.GetConditionData(startDate, endDate);
            return new ResponseModel
            {
                Status = StatusEnum.Ok,
                RecChangeList = recChange
            };
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel AddData(RecChange model)
        {
            ResponseModel ResponseModel = new ResponseModel();
            int response = recChangeRepository.AddData(model);
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

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="firstDate"></param>
        /// <returns></returns>
        public ResponseModel DeleteData(DateTime firstDate)
        {
            throw new NotImplementedException();
        }
    }
}
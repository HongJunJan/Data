using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System.Collections.Generic;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 料盤服務(邏輯層)
    /// </summary>
    public class DataTrayService : IDataTrayService
    {
        /// <summary>
        /// 料盤資料層
        /// </summary>
        private IDataTrayRepository dataTrayRepository = new DataTrayRepository();

        /// <summary>
        /// 查詢是否有資料，有資料:Enum代碼:1(Error1)、無資料:Enum代碼:2(Error2)
        /// </summary>
        /// <param name="trayNo">料盤編號</param>
        /// <param name="responseModel">資料回傳物件</param>
        private void CheckDataExist(string trayNo, ResponseModel responseModel)
        {
            DataTray dataTray = dataTrayRepository.GetConditionData(trayNo);
            if (dataTray != null) { responseModel.TrayEnum = TrayEnum.Error1; }
        }

        /// <summary>
        /// 取得所有料盤資料
        /// </summary>
        /// <returns>料盤輸出物件</returns>
        public ResponseModel GetAllData()
        {
            List<DataTray> dataTrayList = dataTrayRepository.GetAllData();
            return new ResponseModel
            {
                Status = StatusEnum.Ok,
                DataTrayList = dataTrayList
            };
        }

        /// <summary>
        /// 取得料盤查詢條件資料
        /// 傳入條件: 料盤編號
        /// </summary>
        /// <param name="trayNo">料盤編號</param>
        /// <returns></returns>
        public ResponseModel GetConditionData(string trayNo)
        {
            DataTray dataTray = dataTrayRepository.GetConditionData(trayNo);
            return new ResponseModel
            {
                Status = StatusEnum.Ok,
                DataTray = dataTray
            };
        }

        /// <summary>
        /// 取得料盤查詢條件資料(模糊查詢)
        /// 查詢條件: 料盤編號
        /// </summary>
        /// <param name="trayNo">料盤編號</param>
        /// <returns>料盤輸出物件</returns>
        public ResponseModel GetLikeCondition(string trayNo)
        {
            List<DataTray> dataTrayList = dataTrayRepository.GetLikeConditionData(trayNo);
            return new ResponseModel
            {
                Status = StatusEnum.Ok,
                DataTrayList = dataTrayList
            };
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">料盤物件</param>
        /// <returns></returns>
        public ResponseModel AddData(DataTray model)
        {
            ResponseModel ResponseModel = new ResponseModel();
            int response = dataTrayRepository.AddData(model);
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
        /// 更新
        /// </summary>
        /// <param name="model">料盤物件</param>
        /// <returns></returns>
        public ResponseModel UpdateData(DataTray model)
        {
            ResponseModel ResponseModel = new ResponseModel();
            CheckDataExist(model.TrayNo.ToString(), ResponseModel);
            if (ResponseModel.TrayEnum.Equals(TrayEnum.Error1))
            {
                int response = dataTrayRepository.UpdateData(model);
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
            else
            {
                ResponseModel.ResponseMsg = TrayEnum.Error2.GetEnumDescription();
            }

            return ResponseModel;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="keyNo">料盤編號</param>
        /// <returns></returns>
        public ResponseModel DeleteData(string keyNo)
        {
            ResponseModel ResponseModel = new ResponseModel();
            CheckDataExist(keyNo, ResponseModel);
            if (ResponseModel.TrayEnum.Equals(TrayEnum.Error1))
            {
                int response = dataTrayRepository.DeleteData(keyNo);
                if (response.Equals(0))
                {
                    ResponseModel.ResponseMsg = StatusEnum.Error5.GetEnumDescription();
                }
                else
                {
                    ResponseModel.Status = StatusEnum.Ok;
                    ResponseModel.ResponseMsg = StatusEnum.Ok.GetEnumDescription();
                }
            }
            else
            {
                ResponseModel.ResponseMsg = PartNoEnum.Error2.GetEnumDescription();
            }

            return ResponseModel;
        }
    }
}
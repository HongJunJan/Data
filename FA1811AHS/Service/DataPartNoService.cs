using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System.Collections.Generic;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 料號庫服務(邏輯層)
    /// </summary>
    public class DataPartNoService : IDataPartNoService
    {
        #region 私有方法 與 服務

        /// <summary>
        /// 料號庫服務
        /// </summary>
        private IDataPartNoRepository dataPartNoRepository = new DataPartNoRepository();

        /// <summary>
        /// 雷射參數服務
        /// </summary>
        private IDataLaserRepository dataLaserRepository = new DataLaserRepository();

        /// <summary>
        /// 查詢資料是否已存在，有資料:Enum代碼:1(Error1)、無資料:Enum代碼:2(Error2)
        /// 傳入條件:1.料號、2.資料回傳物件
        /// </summary>
        /// <param name="userNo">料號</param>
        /// <param name="responseModel">資料回傳物件</param>
        private void CheckDataExist(string partNo, ResponseModel ResponseModel)
        {
            DataPartNo dataPartNo = dataPartNoRepository.GetConditionData(partNo);
            if (dataPartNo != null)
            {
                ResponseModel.PartNoEnum = PartNoEnum.Error1;
            }
        }

        #endregion 私有方法 與 服務

        /// <summary>
        /// 取得料號資料，只搜尋有使用雷射的資料(模糊查詢)
        /// 查詢條件:料號 與 雷射使用控制(0:全使用、2:使用雷射)、
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns></returns>
        public ResponseModel GetLikeCondition(string keyNo)
        {
            List<DataPartNo> dataPartNo = dataPartNoRepository.GetLikeConditionData(keyNo);
            return new ResponseModel
            {
                Status = StatusEnum.Ok,
                DataPartNoList = dataPartNo
            };
        }

        /// <summary>
        /// 新增 料號與雷射參數
        /// </summary>
        /// <param name="dataPartNo">料號庫物件</param>
        /// <param name="dataLaser">雷射參數物件</param>
        /// <returns></returns>
        public ResponseModel AddPartNoAndLaser(DataPartNo dataPartNo, DataLaser dataLaser)
        {
            ResponseModel ResponseModel = new ResponseModel();
            CheckDataExist(dataPartNo.PartNo, ResponseModel);
            if (ResponseModel.PartNoEnum.Equals(PartNoEnum.Error1))
            {
                ResponseModel.ResponseMsg = PartNoEnum.Error1.GetEnumDescription();
                return ResponseModel;
            }

            // Todo: TransactionScope
            int responsePN = dataPartNoRepository.AddData(dataPartNo);
            int responseLaser = dataLaserRepository.AddData(dataLaser);
            if (responsePN.Equals(0) && responseLaser.Equals(0))
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
        /// 更新 料號與雷射參數
        /// </summary>
        /// <param name="dataPartNo">料號庫物件</param>
        /// <param name="dataLaser">雷射參數物件</param>
        /// <returns></returns>
        public ResponseModel UpdatePartNoAndLaser(DataPartNo dataPartNo, DataLaser dataLaser)
        {
            ResponseModel ResponseModel = new ResponseModel();
            CheckDataExist(dataPartNo.PartNo, ResponseModel);
            if (ResponseModel.PartNoEnum.Equals(PartNoEnum.Error2))
            {
                ResponseModel.ResponseMsg = PartNoEnum.Error2.GetEnumDescription();
                return ResponseModel;
            }

            // Todo: TransactionScope
            int responsePN = dataPartNoRepository.UpdateData(dataPartNo);
            int responseLaser = dataLaserRepository.UpdateData(dataLaser);
            if (responsePN.Equals(0) && responseLaser.Equals(0))
            {
                ResponseModel.ResponseMsg = StatusEnum.Error4.GetEnumDescription();
            }
            else
            {
                ResponseModel.Status = StatusEnum.Ok;
                ResponseModel.ResponseMsg = StatusEnum.Ok.GetEnumDescription();
            }

            return ResponseModel;
        }

        /// <summary>
        /// 刪除 料號與雷射參數
        /// </summary>
        /// <param name="partNo">料號</param>
        /// <returns>回傳筆數</returns>
        public ResponseModel DeletePartNoAndLaser(string keyNo)
        {
            ResponseModel ResponseModel = new ResponseModel();
            CheckDataExist(keyNo, ResponseModel);
            if (ResponseModel.PartNoEnum.Equals(PartNoEnum.Error2))
            {
                ResponseModel.ResponseMsg = PartNoEnum.Error2.GetEnumDescription();
                return ResponseModel;
            }

            // Todo: TransactionScope
            int responsePN = dataPartNoRepository.DeleteData(keyNo);
            int responseLaser = dataLaserRepository.DeleteData(keyNo);
            if (responsePN.Equals(0) && responseLaser.Equals(0))
            {
                ResponseModel.ResponseMsg = StatusEnum.Error5.GetEnumDescription();
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
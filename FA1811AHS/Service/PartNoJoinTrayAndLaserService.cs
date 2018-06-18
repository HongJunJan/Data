using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System.Collections.Generic;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 料號關聯料盤服務(邏輯層)
    /// </summary>
    public class PartNoJoinTrayAndLaserService : IPartNoJoinTrayAndLaserService
    {
        /// <summary>
        /// 料號關聯料盤與雷射資料
        /// </summary>
        private IPartNoJoinTrayAndLaserRepository prtNoJoinTablesRepository = new PartNoJoinTrayAndLaserRepository();

        /// <summary>
        /// 取得所有料號關聯料盤與雷射資料
        /// </summary>
        /// <returns></returns>
        public ResponseModel GetAllData()
        {
            List<PartNoJoinTrayAndLaser> partNoJoinTrayAndLaser = prtNoJoinTablesRepository.GetAllData();
            return new ResponseModel
            {
                Status = StatusEnum.Ok,
                PartNoJoinTrayAndLaserList = partNoJoinTrayAndLaser
            };
        }

        /// <summary>
        /// 取得料號關聯料盤與雷射資料，
        /// 傳入條件:料號
        /// </summary>
        /// <param name="partNo">料號</param>
        /// <returns></returns>
        public ResponseModel GetConditionMain(string partNo)
        {
            ResponseModel responseModel = new ResponseModel();
            PartNoJoinTrayAndLaser partNoJoinTrayAndLaser = prtNoJoinTablesRepository.GetConditionData(partNo);
            if (partNoJoinTrayAndLaser != null)
            {
                responseModel.PartNoJoinTrayAndLaser = partNoJoinTrayAndLaser;
                responseModel.Status = StatusEnum.Ok;
                responseModel.ResponseMsg = StatusEnum.Ok.GetEnumDescription();
            }
            else
            {
                responseModel.ResponseMsg = PartNoEnum.Error2.GetEnumDescription();
            }

            return responseModel;
        }

        /// <summary>
        /// 取得料號關聯料盤與雷射資料(模糊查詢)，
        /// 查詢條件:料號
        /// </summary>
        /// <param name="partNo">料號</param>
        /// <returns></returns>
        public ResponseModel GetLikeConditionMain(string partNo)
        {
            List<PartNoJoinTrayAndLaser> partNoJoinTrayAndLaser = prtNoJoinTablesRepository.GetLikeConditionData(partNo);
            return new ResponseModel
            {
                Status = StatusEnum.Ok,
                PartNoJoinTrayAndLaserList = partNoJoinTrayAndLaser
            };
        }
    }
}
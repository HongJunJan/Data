using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System.Collections.Generic;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 料號關聯雷射參數服務(邏輯層)
    /// </summary>
    public class PartNoJoinLaserService : IPartNoJoinLaserService
    {
        /// <summary>
        /// 取得料號關聯雷射參數資料(模糊查詢)
        /// 查詢條件:料號
        /// </summary>
        /// <param name="keyNo">料號</param>
        /// <returns></returns>
        public ResponseModel GetLikeConditionMain(string keyNo)
        {
            IPartNoJoinLaserRepository partNoJoinLaserRepository = new PartNoJoinLaserRepository();
            List<PartNoJoinLaser> partNoJoinLaser = partNoJoinLaserRepository.GetLikeConditionData(keyNo);
            return new ResponseModel
            {
                Status = StatusEnum.Ok,
                PartNoJoinLaserList = partNoJoinLaser
            };
        }
    }
}
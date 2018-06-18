using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System.Collections.Generic;

namespace FA1811AHS.Service
{
    /// <summary>
    /// Judge判定服務(邏輯層)
    /// </summary>
    public class DataJudgeService : IDataJudgeService
    {
        /// <summary>
        /// 取得所有Judge代碼
        /// </summary>
        /// <returns>回傳帳戶輸出物件</returns>
        public ResponseModel GetAllData()
        {
            IDataJudgeRepository dataJudgeRepository = new DataJudgeRepository();
            List<DataJudge> dataJudges = dataJudgeRepository.GetAllData();
            return new ResponseModel()
            {
                Status = StatusEnum.Ok,
                DataJudgeList = dataJudges
            };
        }
    }
}
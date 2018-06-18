using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System.Collections.Generic;

namespace FA1811AHS.Service
{
    public class ErrorDescriptionService : IErrorDescriptionService
    {
        public IErrorDescriptionRepository errorDescriptionRepository = new ErrorDescriptionRepository();

        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns>回傳輸出物件</returns>
        public ResponseModel GetAllData()
        {
            List<ErrorDescription> errorDescription = errorDescriptionRepository.GetAllData();
            return new ResponseModel()
            {
                Status = StatusEnum.Ok,
                ErrorDescriptionList = errorDescription
            };
        }
    }
}
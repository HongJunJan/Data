using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System.Collections.Generic;
using System.Linq;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 帳戶服務(邏輯層)
    /// </summary>
    public class AccountService : IAccountService
    {
        /// <summary>
        /// 帳戶資料層
        /// </summary>
        private IAccountRepository accountRepository = new AccountRepository();

        #region 私有方法

        /// <summary>
        /// 查詢資料是否已存在，有資料:Enum代碼:1(Error1)、無資料:Enum代碼:2(Error2)
        /// 傳入條件:1.帳號、2.資料回傳物件
        /// </summary>
        /// <param name="userNo">帳號</param>
        /// <param name="responseModel">資料回傳物件</param>
        private void CheckDataExist(string userNo, ResponseModel responseModel)
        {
            Account account = accountRepository.GetConditionData(userNo);
            if (account != null) { responseModel.AccountEnum = AccountEnum.Error1; }
        }

        #endregion 私有方法

        /// <summary>
        /// 登入帳戶，
        /// 傳入條件:1.帳號、2.密碼，
        /// 回傳物件:資料回傳物件
        /// </summary>
        /// <param name="acctNo">帳號</param>
        /// <param name="password">密碼</param>
        /// <returns>回傳帳戶輸出物件</returns>
        public ResponseModel LoginAccount(string acctNo, string password)
        {
            ResponseModel responseModel = new ResponseModel();

            // 取得帳戶資料，linq比對帳戶
            List<Account> list = accountRepository.GetAllData();
            Account account = list.Where(x => x.AcctNo.Equals(acctNo) && x.Password.Equals(password)).FirstOrDefault();
            if (account == null)
            {
                responseModel.ResponseMsg = AccountEnum.Error2.GetEnumDescription();
            }
            else
            {
                responseModel.Status = StatusEnum.Ok;
                responseModel.DataAccount = account;
                responseModel.ResponseMsg = AccountEnum.Login.GetEnumDescription();
            }

            return responseModel;
        }

        /// <summary>
        /// 取得所有帳戶人員
        /// </summary>
        /// <returns>回傳帳戶輸出物件</returns>
        public ResponseModel GetAllData()
        {
            List<Account> accounts = accountRepository.GetAllData();
            return new ResponseModel()
            {
                Status = StatusEnum.Ok,
                AccountList = accounts
            };
        }

        /// <summary>
        /// 新增服務
        /// </summary>
        /// <param name="account">帳戶物件</param>
        /// <returns>回傳帳戶輸出物件</returns>
        public ResponseModel AddData(Account account)
        {
            ResponseModel responseModel = new ResponseModel();
            CheckDataExist(account.AcctNo, responseModel);
            if (responseModel.AccountEnum.Equals(AccountEnum.Error2))
            {
                int response = accountRepository.AddData(account);
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
                responseModel.ResponseMsg = AccountEnum.Error1.GetEnumDescription();
            }

            return responseModel;
        }

        /// <summary>
        /// 更新服務
        /// </summary>
        /// <param name="account">帳戶物件</param>
        /// <returns>回傳帳戶輸出物件</returns>
        public ResponseModel UpdateData(Account account)
        {
            ResponseModel responseModel = new ResponseModel();
            CheckDataExist(account.AcctNo, responseModel);
            if (responseModel.AccountEnum.Equals(AccountEnum.Error1))
            {
                int response = accountRepository.UpdateData(account);
                if (response.Equals(0))
                {
                    responseModel.ResponseMsg = StatusEnum.Error4.GetEnumDescription();
                }
                else
                {
                    responseModel.Status = StatusEnum.Ok;
                    responseModel.ResponseMsg = StatusEnum.Ok.GetEnumDescription();
                }
            }
            else
            {
                responseModel.ResponseMsg = AccountEnum.Error2.GetEnumDescription();
            }

            return responseModel;
        }

        /// <summary>
        /// 刪除服務
        /// </summary>
        /// <param name="userNo">帳號</param>
        /// <returns>回傳帳戶輸出物件</returns>
        public ResponseModel DeleteData(string userNo)
        {
            ResponseModel responseModel = new ResponseModel();
            CheckDataExist(userNo, responseModel);
            if (responseModel.AccountEnum.Equals(AccountEnum.Error1))
            {
                int response = accountRepository.DeleteData(userNo);
                if (response.Equals(0))
                {
                    responseModel.ResponseMsg = StatusEnum.Error5.GetEnumDescription();
                }
                else
                {
                    responseModel.Status = StatusEnum.Ok;
                    responseModel.ResponseMsg = StatusEnum.Ok.GetEnumDescription();
                }
            }
            else
            {
                responseModel.ResponseMsg = AccountEnum.Error2.GetEnumDescription();
            }

            return responseModel;
        }
    }
}
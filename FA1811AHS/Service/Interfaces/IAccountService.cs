using FA1811AHS.Repository;

namespace FA1811AHS.Service
{
    /// <summary>
    ///  帳戶服務(邏輯層)-介面
    /// </summary>
    public interface IAccountService : IBaseService<Account, ResponseModel>
    {
        /// <summary>
        /// 取得所有帳戶人員
        /// </summary>
        /// <returns>回傳帳戶輸出物件</returns>
        ResponseModel GetAllData();

        /// <summary>
        /// 登入帳戶，
        /// 傳入條件:1.帳號、2.密碼，
        /// 回傳物件:資料回傳物件
        /// </summary>
        /// <param name="acctNo">帳號</param>
        /// <param name="password">密碼</param>
        /// <returns>回傳帳戶輸出物件</returns>
        ResponseModel LoginAccount(string acctNo, string password);
    }
}
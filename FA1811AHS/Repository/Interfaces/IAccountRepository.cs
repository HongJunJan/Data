namespace FA1811AHS.Repository
{
    /// <summary>
    /// 帳戶資料層
    /// </summary>
    public interface IAccountRepository : IBaseRepository<Account>
    {
        /// <summary>
        /// 取得條件資料，
        /// 傳入條件:帳號，
        /// 回傳物件: 帳戶物件
        /// </summary>
        /// <param name="keyNo">帳號</param>
        /// <returns></returns>
        Account GetConditionData(string keyNo);
    }
}
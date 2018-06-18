using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 帳戶資料層
    /// </summary>
    public class AccountRepository : BaseConnetcion<Account>, IAccountRepository
    {
        /// <summary>
        /// 取得全部資料，
        /// 回傳物件:串列資料物件
        /// </summary>
        /// <returns>串列資料</returns>
        public List<Account> GetAllData()
        {
            List<Account> account = new List<Account>();
            const string Sql = @"SELECT  [Account_CreateDate]
                                        ,[Account_UpdateDate]
                                        ,[Account_EditUser]
                                        ,[Account_No]
                                        ,[Account_Password]
                                        ,[Account_Limit]
                                        ,[Account_remark]
                                  FROM [FA1811-AHS].[dbo].[Account]
                                  ORDER BY [Id]";
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                return sqlConnection.Query<Account>(Sql).ToList();
            }
        }

        /// <summary>
        /// 取得條件資料，
        /// 傳入條件:帳號，
        /// 回傳物件: 帳戶物件
        /// </summary>
        /// <param name="acctNo">帳號</param>
        /// <returns></returns>
        public Account GetConditionData(string acctNo)
        {
            Account account = null;
            const string Sql = @"SELECT [Account_CreateDate]
                                        ,[Account_UpdateDate]
                                        ,[Account_EditUser]
                                        ,[Account_No]
                                        ,[Account_Password]
                                        ,[Account_Limit]
                                        ,[Account_remark]
                                FROM [FA1811-AHS].[dbo].[Account]
                                WHERE [Account_No] = @acctNo
                                ORDER BY [Id]";
            object param = new { acctNo };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                account = sqlConnection.Query<Account>(Sql, param).FirstOrDefault();
            }
            return account;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">物件</param>
        /// <returns>key</returns>
        public int AddData(Account model)
        {
            int result = 0;
            const string sql =
                @"INSERT INTO [FA1811-AHS].[dbo].[Account] (
                                       [Account_CreateDate]
                                      ,[Account_UpdateDate]
                                      ,[Account_EditUser]
                                      ,[Account_No]
                                      ,[Account_Password]
                                      ,[Account_Limit]
                                      ,[Account_remark])
                              VALUES (@CreateDate
                                     ,@UpdateDate
                                     ,@EditUser
		                             ,@AcctNo
		                             ,@Password
		                             ,@Limit
                                     ,@Remark)";
            object param = model;
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                result = sqlConnection.Execute(sql, param);
            }

            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">物件</param>
        /// <returns>key</returns>
        public int UpdateData(Account model)
        {
            int result = 0;
            const string sql =
                @"UPDATE [FA1811-AHS].[dbo].[Account]
                     SET [Account_UpdateDate] = @UpdateDate
                        ,[Account_EditUser] = @EditUser
                        ,[Account_Password] = @Password
                        ,[Account_Limit] = @Limit
                   WHERE [Account_No] = @AcctNo";
            object param = new
            {
                model.UpdateDate,
                model.EditUser,
                model.Password,
                model.Limit,
                model.AcctNo
            };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                result = sqlConnection.Execute(sql, param);
            }
            return result;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="acctNo">帳號</param>
        /// <returns>key</returns>
        public int DeleteData(string acctNo)
        {
            int result = 0;
            const string sql = @"DELETE [FA1811-AHS].[dbo].[Account] WHERE [Account_No] = @acctNo";
            object param = new { acctNo };
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                result = sqlConnection.Execute(sql, param);
            }

            return result;
        }
    }
}
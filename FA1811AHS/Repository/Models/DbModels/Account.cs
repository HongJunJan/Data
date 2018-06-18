using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 帳戶物件
    /// </summary>
    public class Account
    {
        public Account()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.Password = "000";
            this.Remark = string.Empty;
        }

        /// <summary>
        /// 建立日期
        /// </summary>
        [Column("Account_CreateDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        [Column("Account_UpdateDate")]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 編輯者
        /// </summary>
        [Column("Account_EditUser")]
        public string EditUser { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        [Column("Account_No")]
        public string AcctNo { get; set; }

        /// <summary>
        /// 使用者密碼
        /// </summary>
        [Column("Account_Password")]
        public string Password { get; set; }

        /// <summary>
        /// 允許權限控制
        /// </summary>
        [Column("Account_Limit")]
        public string Limit { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [Column("Account_remark")]
        public string Remark { get; set; }
    }
}
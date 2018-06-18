using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 異動紀錄物件
    /// </summary>
    public class RecChange
    {
        /// <summary>
        /// 現在時間
        /// </summary>
        [Column("NowTime")]
        public DateTime NowTime { get; set; }

        /// <summary>
        /// 異動訊息
        /// </summary>
        [Column("Message")]
        public string Message { get; set; }

        /// <summary>
        /// 使用者
        /// </summary>
        [Column("User_Name")]
        public string UserName { get; set; }
    }
}
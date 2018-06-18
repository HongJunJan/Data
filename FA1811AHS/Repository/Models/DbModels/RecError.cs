using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 故障錯誤紀錄物件
    /// </summary>
    public class RecError
    {
        /// <summary>
        /// 結束時間
        /// </summary>
        [Column("EndTime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 開始時間
        /// </summary>
        [Column("StartTime")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        [Column("Message")]
        public string Message { get; set; }

        /// <summary>
        /// 使用者
        /// </summary>
        [Column("User_Name")]
        public string UserName { get; set; }

        /// <summary>
        /// 故障代碼
        /// </summary>
        [Column("ErrorID")]
        public string ErrorID { get; set; }
    }
}
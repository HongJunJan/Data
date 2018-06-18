using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 錯誤描述物件
    /// </summary>
    public class ErrorDescription
    {
        /// <summary>
        /// 錯誤代碼
        /// </summary>
        [Column("ErrorID")]
        public string ErrorID { get; set; }

        /// <summary>
        /// 中文描述內容
        /// </summary>
        [Column("Ch_Description")]
        public string ChDescription { get; set; }

        /// <summary>
        /// 英文描述內容
        /// </summary>
        [Column("En_Description")]
        public string EnDescription { get; set; }
    }
}
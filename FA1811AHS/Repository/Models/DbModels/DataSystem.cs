using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 系統控制開關物件
    /// </summary>
    public class DataSystem
    {
        /// <summary>
        /// 群組
        /// </summary>
        [Column("DataSystem_Group")]
        public string Group { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        [Column("DataSystem_No")]
        public string No { get; set; }

        /// <summary>
        /// 設備名稱
        /// </summary>
        [Column("DataSystem_Name")]
        public string Name { get; set; }

        /// <summary>
        /// 系統參數
        /// </summary>
        [Column("DataSystem_Parameter")]
        public string Parameter { get; set; }

        /// <summary>
        /// 是否啟用
        /// </summary>
        [Column("DataSystem_Enable")]
        public string Enable { get; set; }
    }
}
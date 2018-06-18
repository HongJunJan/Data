using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 路徑資料物件
    /// </summary>
    public class DataPath
    {
        /// <summary>
        /// 群組
        /// </summary>
        [Column("DataPath_Group")]
        public string Group { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        [Column("DataPath_No")]
        public string No { get; set; }

        /// <summary>
        /// 設備名稱
        /// </summary>
        [Column("DataPath_Name")]
        public string Name { get; set; }

        /// <summary>
        /// IP路徑
        /// </summary>
        [Column("DataPath_Path")]
        public string Path { get; set; }

        /// <summary>
        /// 通訊埠
        /// </summary>
        [Column("DataPath_Port")]
        public string Port { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        [Column("DataPath_AcctNo")]
        public string AcctNo { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Column("DataPath_Password")]
        public string Password { get; set; }
    }
}
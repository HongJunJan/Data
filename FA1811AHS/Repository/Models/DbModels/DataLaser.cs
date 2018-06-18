using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 雷射參數物件
    /// </summary>
    public class DataLaser
    {
        public DataLaser()
        {
            this.Xoffset = "0.000";
            this.Yoffset = "0.000";
            this.Power = "30";
            this.Speed = "500";
        }

        /// <summary>
        /// 料號
        /// </summary>
        [Column("DataLaser_PartNo")]
        public string PartNo { get; set; }

        /// <summary>
        /// 程序編號(FNO)
        /// </summary>
        [Column("DataLaser_FnoNo")]
        public string FnoNo { get; set; }

        /// <summary>
        /// X偏移
        /// </summary>
        [Column("DataLaser_Xoffset")]
        public string Xoffset { get; set; }

        /// <summary>
        /// Y偏移
        /// </summary>
        [Column("DataLaser_Yoffset")]
        public string Yoffset { get; set; }

        /// <summary>
        /// 雷射功率
        /// </summary>
        [Column("DataLaser_Power")]
        public string Power { get; set; }

        /// <summary>
        /// 掃描速度
        /// </summary>
        [Column("DataLaser_Speed")]
        public string Speed { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 批號物件
    /// </summary>
    public class DataLot
    {
        public DataLot()
        {
            this.CreateDate = DateTime.Now;
        }

        /// <summary>
        /// 建立日期
        /// </summary>
        [Column("DataLot_CreateDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 建立者
        /// </summary>
        [Column("DataLot_CreateUser")]
        public string CreateUser { get; set; }

        /// <summary>
        /// 批號
        /// </summary>
        [Column("DataLot_No")]
        public string LotNo { get; set; }

        /// <summary>
        /// 料號
        /// </summary>
        [Column("DataLot_PartNo")]
        public string PartNo { get; set; }
    }
}
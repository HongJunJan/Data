using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 料號庫資料物件
    /// </summary>
    public class DataPartNo
    {
        public DataPartNo()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.PieceSizeT = "0.000";
            this.JudgeStatus = "ON、ON、ON、ON、ON、ON、ON、ON、ON、ON、ON、ON、ON";
        }

        /// <summary>
        /// 建立日期
        /// </summary>
        [Column("DataPartNo_CreateDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [Column("DataPartNo_UpdateDate")]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 編輯者
        /// </summary>
        [Column("DataPartNo_EditUser")]
        public string EditUser { get; set; }

        /// <summary>
        /// 料號
        /// </summary>
        [Column("DataPartNo_No")]
        public string PartNo { get; set; }

        /// <summary>
        /// 料盤編號
        /// </summary>
        [Column("DataPartNo_TrayNo")]
        public int TrayNo { get; set; }

        /// <summary>
        /// 板尺寸X
        /// </summary>
        [Column("DataPartNo_PieceSizeX")]
        public string PieceSizeX { get; set; }

        /// <summary>
        /// 板尺寸Y
        /// </summary>
        [Column("DataPartNo_PieceSizeY")]
        public string PieceSizeY { get; set; }

        /// <summary>
        /// 板厚度T
        /// </summary>
        [Column("DataPartNo_PieceSizeT")]
        public string PieceSizeT { get; set; }

        /// <summary>
        /// 2D X位置
        /// </summary>
        [Column("DataPartNo_2DPositionX")]
        public string PositionX2D { get; set; }

        /// <summary>
        /// 2D Y位置
        /// </summary>
        [Column("DataPartNo_2DPositionY")]
        public string PositionY2D { get; set; }

        /// <summary>
        /// 2D讀碼與雷射使用控制
        /// </summary>
        [Column("DataPartNo_UsesIten")]
        public string UsesIten { get; set; }

        /// <summary>
        /// Judge參數
        /// </summary>
        [Column("DataPartNo_JudgeStatus")]
        public string JudgeStatus { get; set; }
    }
}
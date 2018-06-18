using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 料盤資料物件
    /// </summary>
    public class DataTray
    {
        public DataTray()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>
        /// 建立日期
        /// </summary>
        [Column("DataTray_CreateDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [Column("DataTray_UpdateDate")]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 編輯者
        /// </summary>
        [Column("DataTray_EditUser")]
        public string EditUser { get; set; }

        /// <summary>
        /// 料盤編號
        /// </summary>
        [Column("DataTray_No")]
        public int TrayNo { get; set; }

        /// <summary>
        /// 料盤名稱
        /// </summary>
        [Column("DataTray_Name")]
        public string TrayName { get; set; }

        /// <summary>
        /// X格數量
        /// </summary>
        [Column("DataTray_DivideNoX")]
        public int DivideNoX { get; set; }

        /// <summary>
        /// Y格數量
        /// </summary>
        [Column("DataTray_DivideNoY")]
        public int DivideNoY { get; set; }

        /// <summary>
        /// X間隔
        /// </summary>
        [Column("DataTray_DividePitchX")]
        public string DividePitchX { get; set; }

        /// <summary>
        /// Y間隔
        /// </summary>
        [Column("DataTray_DividePitchY")]
        public string DividePitchY { get; set; }

        /// <summary>
        /// 板中心 X位置
        /// </summary>
        [Column("DataTray_PieceCenterX")]
        public string PieceCenterX { get; set; }

        /// <summary>
        /// 板中心 Y位置
        /// </summary>
        [Column("DataTray_PieceCenterY")]
        public string PieceCenterY { get; set; }

        /// <summary>
        /// 料盤厚度
        /// </summary>
        [Column("DataTray_TrayThickness")]
        public string TrayThickness { get; set; }

        /// <summary>
        /// 中心
        /// </summary>
        [Column("DataTray_TrayCenter")]
        public string TrayCenter { get; set; }

        /// <summary>
        /// 長度
        /// </summary>
        [Column("DataTray_TrayLength")]
        public string TrayLength { get; set; }

        /// <summary>
        /// X 偏移
        /// </summary>
        [Column("DataTray_TrayOffsetX")]
        public string TrayOffsetX { get; set; }

        /// <summary>
        /// Y 偏移
        /// </summary>
        [Column("DataTray_TrayOffsetY")]
        public string TrayOffsetY { get; set; }
    }
}
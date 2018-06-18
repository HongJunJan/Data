using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 料號join料盤與雷射參數物件
    /// </summary>
    public class PartNoJoinTrayAndLaser
    {
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
        /// 板厚T
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
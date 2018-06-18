using FA1811AHS.Shared;
using System;
using System.Configuration;

namespace FA1811AHS
{
    /// <summary>
    /// 全域變數
    /// </summary>
    public static class GlobalParameter
    {
        /// <summary>
        /// 使用模式，測試模式:0， 正式模式:1
        /// </summary>
        public static string UseMode = ConfigurationManager.AppSettings["UseMode"];

        #region 帳戶相關參數

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public static string UserName { get; set; } = "James";

        /// <summary>
        /// 允許控制項目
        /// </summary>
        public static string AllowItem { get; set; }

        #endregion 帳戶相關參數

        #region 料號與料盤相關參數

        /// <summary>
        /// 批號
        /// </summary>
        public static string LotNo { get; set; }

        /// <summary>
        /// 料號
        /// </summary>
        public static string PartNo { get; set; }

        /// <summary>
        /// 料盤編號
        /// </summary>
        public static string TaryNo { get; set; }

        #endregion 料號與料盤相關參數

        #region 2D讀碼、雷射相關參數

        /// <summary>
        /// Hioki機台IP位置
        /// </summary>
        public static string HiokiIP = ConfigurationManager.AppSettings["Hioki_IP"];

        /// <summary>
        /// 讀取器IP位置
        /// </summary>
        public static string ReadIP = ConfigurationManager.AppSettings["Read2d_IP"];

        /// <summary>
        /// 2D讀取內容
        /// </summary>
        public static string ReadContent { get; set; }

        /// <summary>
        /// 雷射SerialPort
        /// </summary>
        public static SerialPortModel LaserSerialPort = new SerialPortModel
        {
            LaserPortName = ConfigurationManager.AppSettings["Laser_PortName"],
            LaserBaudRate = Convert.ToInt32(ConfigurationManager.AppSettings["Laser_BaudRate"])
        };

        /// <summary>
        /// 載台與雷射中心點差距
        /// </summary>
        public static int VehicleAndPieceCenter = 40;

        #endregion 2D讀碼、雷射相關參數

        #region PLC相關參數

        /// <summary>
        /// PLC_IP位置
        /// </summary>
        public static string PLCIP = ConfigurationManager.AppSettings["PLC_IP"];

        #endregion PLC相關參數
    }
}
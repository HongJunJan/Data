using PanasonicLP410;
using System.IO.Ports;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// 通訊埠功能項目
    /// </summary>
    public static class SerialPortMethod
    {
        /// <summary>
        /// 雷射通訊埠開啟，
        /// 傳入條件:SerialPort物件，
        /// 回傳結果(布林):連線成功:true、連線失敗:false
        /// </summary>
        /// <param name="serialPortModel">SerialPort物件</param>
        public static bool LaserOpenPort(SerialPortModel serialPortModel)
        {
            bool check = false;
            if (!LaserConnectionPort.LaserPort.IsOpen)
            {
                SerialPort serialPort = new SerialPort
                {
                    PortName = serialPortModel.LaserPortName,
                    BaudRate = serialPortModel.LaserBaudRate,
                    Parity = serialPortModel.LaserParity,
                    StopBits = serialPortModel.LaserStopBits,
                    DataBits = serialPortModel.LaserDataBits,
                };
                LaserConnectionPort.OpenPort(serialPort);
                check = true;
            }
            return check;
        }
    }

    /// <summary>
    /// SerialPort物件
    /// </summary>
    public class SerialPortModel
    {
        public SerialPortModel()
        {
            this.LaserPortName = "COM6";
            this.LaserBaudRate = 9600;
            this.LaserParity = Parity.None;
            this.LaserStopBits = StopBits.One;
            this.LaserDataBits = 8;
        }

        /// <summary>
        /// Com名稱
        /// </summary>
        public string LaserPortName { get; set; }

        /// <summary>
        /// 序列傳輸速率
        /// </summary>
        public int LaserBaudRate { get; set; }

        /// <summary>
        /// 同位檢查通訊協定
        /// </summary>
        public Parity LaserParity { get; set; }

        /// <summary>
        /// 設定每位元組之停止位元的標準數目
        /// </summary>
        public StopBits LaserStopBits { get; set; }

        /// <summary>
        /// 設定每一位元組之資料位元的標準長度
        /// </summary>
        public int LaserDataBits { get; set; }
    }
}
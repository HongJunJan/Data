using ConnPLCcommand;
using FA1811AHS.Repository;
using FA1811AHS.Service;
using FA1811AHS.Shared;
using Keyence.AutoID.SDK;
using NLog;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace FA1811AHS
{
    public partial class Reader : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IRecChangeService recChangeService = new RecChangeService();
        private ReaderAccessor readerAccessor = new ReaderAccessor();
        private Socket socket;
        private bool CCD讀取Tag = false;

        public Reader()
        {
            InitializeComponent();
            ButtonProperty.SetProperty(btnRead, true);
            ButtonProperty.SetProperty(btnReadWrite, true);
            if (GlobalParameter.UseMode.Equals("1"))
            {
                #region 測試後不用要刪除

                PLCMethod.PLC_Connect();
                Thread.Sleep(1000);

                #endregion 測試後不用要刪除

                // PLC連線、條碼讀取器連線
                try
                {
                    if (!GlobalData.PLC初始連線是否成功) { MessageBox.Show("PLC連線失敗:請檢查PLC連線IP、機台是否異常。"); return; }
                    if (!GlobalData.PLC線路異常) { ThreadPool.QueueUserWorkItem(DM8120讀取2D碼執行緒); }
                    else { MessageBox.Show("PLC線路異常。"); return; }

                    bool check = LiveviewFormProperty.ReadConnect(liveviewForm1, readerAccessor, GlobalParameter.ReadIP);
                    if (!check) { MessageBox.Show("條碼讀取器連線異常。"); return; }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception錯誤");
                    MessageBox.Show("Exception錯誤:" + ex.Message);
                    return;
                }

                // Hoioke機台連線
                try
                {
                    IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(GlobalParameter.HiokiIP), 32006);
                    this.socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    Thread tread = new Thread(new ThreadStart(CheckSocketConnect));
                    tread.Start();
                    this.socket.Connect(ipe);
                }
                catch (Exception)
                {
                    MessageBox.Show("Hiorki連線: Hiorki機台連線異常，請檢查IP位置或網路線路是否異常。");
                }
            }
        }

        /// <summary>
        /// 使用者異動紀錄
        /// </summary>
        /// <param name="message"></param>
        private void AddRecChangeMethod(string message)
        {
            recChangeService.AddData(new RecChange
            {
                NowTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:ss:mm")),
                Message = message,
                UserName = GlobalParameter.UserName
            });
        }

        /// <summary>
        /// 檢查Socket_Timeout
        /// </summary>
        private void CheckSocketConnect()
        {
            Thread.Sleep(1000);
            if (!socket.Connected)
            {
                socket.Close();
            }
            Thread.CurrentThread.Abort();
        }

        /// <summary>
        /// 關閉離開
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnExit_Click(object sender, EventArgs e)
        {
            if (GlobalParameter.UseMode.Equals("1")) { LiveviewFormProperty.ReadClose(liveviewForm1, readerAccessor); }
            this.Close();
        }

        /// <summary>
        /// 條碼讀取器-讀取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                // 異動紀錄
                AddRecChangeMethod("手動流程:條碼讀取");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (GlobalParameter.UseMode.Equals("0")) { MessageBox.Show("測試模式，無法讀取。"); return; }
            if (GlobalData.DM[DmTable.DM_8111允許手動].Equals("1"))
            {
                PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Single,
                       PLCcommand.PLC_IO.DM, DmTable.DM_8213手動2D通知, 1, 0, null);
                CCD讀取Tag = true;
            }
            else
            {
                MessageBox.Show("PLC點位[DM_8111允許手動]:不允許手動，請檢查PLC參數。");
                return;
            }
        }

        private void DM8120讀取2D碼執行緒(object sender)
        {
            while (true)
            {
                if (!GlobalData.PLC線路異常)
                {
                    if (CCD讀取Tag)
                    {
                        DM8120讀取2D();
                    }
                }
            }
        }

        private void DM8120讀取2D()
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)DM8120讀取2D);
            }
            else
            {
                if (GlobalData.DM[DmTable.DM_8120CCD讀取2D碼].Equals("1"))
                {
                    string readCode = readerAccessor.ExecCommand("LON", 8000);
                    if (!string.IsNullOrEmpty(readCode))
                    {
                        PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Single,
                                 PLCcommand.PLC_IO.DM, DmTable.DM_8220CCD讀2D碼完成, 1, 0, null);
                        lblText.Text = readCode;
                        MessageBox.Show("讀取成功。");
                    }
                    else
                    {
                        PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Single,
                                     PLCcommand.PLC_IO.DM, DmTable.DM_8221CCD讀2D檔失敗, 1, 0, null);
                        liveviewForm1.EndReceive();
                        MessageBox.Show("讀取錯誤，請檢查讀取器或材料是否有異常。");
                    }

                    PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Single,
                         PLCcommand.PLC_IO.DM, DmTable.DM_8120CCD讀取2D碼, 0, 0, null);
                    CCD讀取Tag = false;
                }
            }
        }

        /// <summary>
        /// 條碼讀取器-讀取/寫入Hioki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadWrite_Click(object sender, EventArgs e)
        {
            try
            {
                // 異動紀錄
                AddRecChangeMethod("手動流程:條碼讀取並寫入Hioki機台");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (GlobalParameter.UseMode.Equals("0")) { MessageBox.Show("測試模式，無法讀取。"); return; }
            try
            {
                string readCode = readerAccessor.ExecCommand("LON", 8000);
                lblText.Text = readCode;
                if (string.IsNullOrEmpty(readCode))
                {
                    readerAccessor.ExecCommand("LOFF");
                    MessageBox.Show("讀取錯誤，請檢查讀取器或材料是否有異常。");
                    return;
                }

                string xmlString = "<?xml version='1.0' encoding='UTF-8'?>"
                         + "<COMMAND>"
                         + "<COM_NUM>11</COM_NUM>"
                         + "<INFORMATION>"
                         + "<ID>" + readCode + "</ID>"
                         + "</INFORMATION>"
                         + "</COMMAND>";
                socket.SendTimeout = 10000;
                socket.ReceiveTimeout = 5000;

                // 送出XML
                XmlDocument doc = new XmlDocument();
                doc.InnerXml = xmlString;
                Byte[] sendBuf = Encoding.UTF8.GetBytes(doc.OuterXml);
                socket.Send(sendBuf, sendBuf.Length, SocketFlags.None);

                // 回傳XML
                Byte[] recvBuf = new Byte[0x40000];
                for (Int32 i = 0; i < recvBuf.Length; i++) { recvBuf[i] = 0; }
                socket.Receive(recvBuf, 0, recvBuf.Length, SocketFlags.None);

                // 取得子節點參數，正確:0、錯誤:1
                XElement xmlroot = XElement.Parse(TypeMethod.FormtXmlDoc(Encoding.UTF8.GetString(recvBuf)));
                string error = ((XElement)(xmlroot.Element("ERROR"))).Value;
                MessageBox.Show(error.Equals("0") ? "寫入成功" : "寫入失敗");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception錯誤:" + ex.Message);
            }
        }
    }
}
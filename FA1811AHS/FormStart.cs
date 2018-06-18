using ConnPLCcommand;
using FA1811AHS.Repository;
using FA1811AHS.Service;
using FA1811AHS.Shared;
using Keyence.AutoID.SDK;
using NLog;
using PanasonicLP410;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace FA1811AHS
{
    public partial class FormStart : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IRecChangeService recChangeService = new RecChangeService();
        private ReaderAccessor readerAccessor = new ReaderAccessor();
        private FormStartModel formStartModel = new FormStartModel();
        private PartNoJoinTrayAndLaser partNoJoinTrayAndLaser;
        private Socket socket;
        private bool GREEN = false, YELLOW = false, RED = false;

        public FormStart()
        {
            InitializeComponent();
            SetDefultEvent();

            // 條碼讀取器，預設顯示還是不顯示
            label3.Visible = lblCCD.Visible = liveviewForm1.Visible = false;
            ToolStripMenuItemSW(false, "FormStart");
            DefultListViewInit();
            ThreadPool.QueueUserWorkItem(日期時間執行緒);
            if (GlobalParameter.UseMode.Equals("1"))
            {
                // PLC連線
                PLCMethod.PLC_Connect();
                Thread.Sleep(1000);
                if (GlobalData.PLC初始連線是否成功) { ThreadPool.QueueUserWorkItem(警示燈執行緒); }
                else { MessageBox.Show("PLC連線失敗:請檢查PLC連線IP、機台是否異常。"); return; }

                // 雷射功能連線
                try
                {
                    SerialPortMethod.LaserOpenPort(GlobalParameter.LaserSerialPort);
                    if (LaserConnectionPort.LaserPort.IsOpen)
                    {
                        ModelSTS modelSTS = LaserExecuteWrite.執行STS狀態請求讀取(LaserCommand.STS狀態請求讀取);
                        formStartModel.LSR雷射狀態 = modelSTS.LSRStatus; formStartModel.SHT遮光閥狀態 = modelSTS.SHTStatus;
                        DefultLaserControll();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("請檢查雷射是否斷線、未開機、未開啟REMOTE。" + ex.Message);
                    return;
                }

                // 條碼讀取器連線
                try
                {
                    bool check = LiveviewFormProperty.ReadConnect(liveviewForm1, readerAccessor, GlobalParameter.ReadIP);
                    if (!check)
                    {
                        MessageBox.Show("條碼讀取器連線異常、請確認IP位置。");
                        return;
                    }
                    label3.Visible = lblCCD.Visible = liveviewForm1.Visible = true;
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
                catch (Exception ex)
                {
                    MessageBox.Show("Hiorki連線: Hiorki機台連線異常，請檢查IP位置或網路線路是否異常。" + ex.Message);
                }
            }
        }

        #region 私有事件

        /// <summary>
        /// 預設事件設定
        /// </summary>
        private void SetDefultEvent()
        {
            // ToolStripMenuItemEven
            partNoToolStripMenuItem.Click += ToolStripMenuItemClick;
            trayToolStripMenuItem.Click += ToolStripMenuItemClick;
            setToolStripMenuItem.Click += ToolStripMenuItemClick;
            printToolStripMenuItem.Click += ToolStripMenuItemClick;
            accountToolStripMenuItem.Click += ToolStripMenuItemClick;
            ExitToolStripMenuItem.Click += ToolStripMenuItemClick;
            recChangeToolStripMenuItem.Click += ToolStripMenuItemClick;
            recErrorToolStripMenuItem.Click += ToolStripMenuItemClick;
            plcToolStripMenuItem.Click += ToolStripMenuItemClick;
            ReadToolStripMenuItem.Click += ToolStripMenuItemClick;
        }

        /// <summary>
        /// ToolStripMenuItem_Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            switch (toolStripMenuItem.Name)
            {
                case "partNoToolStripMenuItem":
                    PartNoSet partNoSet = new PartNoSet();
                    partNoSet.ShowDialog();
                    break;

                case "trayToolStripMenuItem":
                    TraySet traySet = new TraySet();
                    traySet.ShowDialog();
                    break;

                case "plcToolStripMenuItem":
                    PartNoWritePLC partNoWritePLC = new PartNoWritePLC();
                    partNoWritePLC.ShowDialog();
                    break;

                case "setToolStripMenuItem":
                    LaserSet laserSet = new LaserSet();
                    laserSet.ShowDialog();
                    break;

                case "printToolStripMenuItem":
                    LaserPrinting laserPrinting = new LaserPrinting();
                    laserPrinting.ShowDialog();
                    break;

                case "ReadToolStripMenuItem":
                    if (GlobalParameter.UseMode.Equals("1"))
                    { LiveviewFormProperty.ReadClose(liveviewForm1, readerAccessor); }
                    Reader firstStepApp = new Reader();
                    firstStepApp.ShowDialog();
                    break;

                case "recChangeToolStripMenuItem":
                    RecChangeReport recChangeReport = new RecChangeReport();
                    recChangeReport.ShowDialog();
                    break;

                case "recErrorToolStripMenuItem":
                    RecErrorReport recErrorReport = new RecErrorReport();
                    recErrorReport.ShowDialog();
                    break;

                case "accountToolStripMenuItem":
                    AccountSet accountSet = new AccountSet();
                    accountSet.ShowDialog();
                    break;

                case "pathToolStripMenuItem":

                    break;

                case "controlToolStripMenuItem":

                    break;

                default:
                    Environment.Exit(0);
                    break;
            }
        }

        #endregion 私有事件

        #region 執行緒，Todo:警示燈點位待測試

        private void 日期時間執行緒(object obj)
        {
            while (true)
            {
                Thread.Sleep(500);
                日期時間設定();
            }
        }

        private void 日期時間設定()
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)日期時間設定);
            }
            else
            {
                lblDateTime.Text = DateTime.Now.ToString();
            }
        }

        private void 警示燈執行緒(object obj)
        {
            while (true)
            {
                if (!GlobalData.PLC線路異常)
                {
                    Thread.Sleep(100);
                    bool GREEN = YELLOW = RED = false;
                    if (GlobalData.DM[DmTable.DM_8142綠燈].Equals("1")) { GREEN = true; }
                    if (GlobalData.DM[DmTable.DM_8143黃燈].Equals("1")) { YELLOW = true; }
                    if (GlobalData.DM[DmTable.DM_8144紅燈].Equals("1")) { RED = true; }
                    Thread.Sleep(100);
                    警示燈設定();
                }
            }
        }

        private void 警示燈設定()
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)警示燈設定);
            }
            else
            {
                picturebGREEN.Image = FA1811AHS.Properties.Resources.GREEN_S;
                picturebYELLOW.Image = FA1811AHS.Properties.Resources.YELLOW_S;
                picturebRED.Image = FA1811AHS.Properties.Resources.RED_S;
                if (GREEN.Equals("picturebGREEN")) { picturebGREEN.Image = FA1811AHS.Properties.Resources.GREEN亮; }
                if (YELLOW.Equals("picturebYELLOW")) { picturebYELLOW.Image = FA1811AHS.Properties.Resources.YELLOW亮; }
                if (RED.Equals("picturebRED")) { picturebRED.Image = FA1811AHS.Properties.Resources.RED亮; }
            }
        }

        #endregion 執行緒，Todo:警示燈點位待測試

        #region 開關設定

        /// <summary>
        /// 雷射預設控制開關: 1.LSR雷射狀態，2.SHT遮光閥狀態
        /// </summary>
        private void DefultLaserControll()
        {
            string cmd = string.Empty; string response = string.Empty;
            if (formStartModel.LSR雷射狀態 == "0")
            {
                cmd = LaserCommand.LSR激光控制設定("1");
                response = LaserExecuteWrite.執行LSR激光控制設定(cmd);
                if (ValidateUtility.DisplayMessage(response)) { return; }
                LaserReady laserReady = new LaserReady();
                laserReady.ShowDialog();
                formStartModel.LSR雷射狀態 = "1";
            }

            if (formStartModel.SHT遮光閥狀態 == "0")
            {
                cmd = LaserCommand.SHT遮光閥控制設定("1");
                response = LaserExecuteWrite.執行SHT遮光閥控制設定(cmd);
                if (ValidateUtility.DisplayMessage(response)) { return; }
                formStartModel.SHT遮光閥狀態 = "1";
            }
        }

        /// <summary>
        /// ToolStripMenu開關: 1.開關(布林值)、2.使用代碼(若代碼"PLC"，會連動login登入控制)
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="usesCode"></param>
        private void ToolStripMenuItemSW(bool sw, string usesCode)
        {
            if (usesCode.Equals("PLC")) { LoginToolStripMenuItem.Enabled = sw; }
            ItemToolStripMenuItem.Enabled = LaserToolStripMenuItem.Enabled =
            ReadToolStripMenuItem.Enabled = SystemToolStripMenuItem.Enabled =
            splitContainer1.Enabled = ReportToolStripMenuItem.Enabled = sw;
        }

        /// <summary>
        /// 登入後，開啟控制項
        /// </summary>
        public void OpenController()
        {
            lblUserName.Text = "登入者: " + GlobalParameter.UserName;
            LoginToolStripMenuItem.Text = "登出";
            List<string> list = GlobalParameter.AllowItem.Split('、').ToList<string>();
            foreach (var item in list)
            {
                if (item == "1") { ItemToolStripMenuItem.Enabled = true; }

                if (item == "2") { LaserToolStripMenuItem.Enabled = true; }

                if (item == "3") { ReadToolStripMenuItem.Enabled = true; }

                if (item == "4") { SystemToolStripMenuItem.Enabled = true; }
            }

            ReportToolStripMenuItem.Enabled = true;
        }

        #endregion 開關設定

        #region 私有方法

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
        /// DefultListViewInit
        /// </summary>
        private void DefultListViewInit()
        {
            ListViewProperty.SetProperty(listView1);
            listView1.Columns.Add("異常起始時間", 150, HorizontalAlignment.Center);
            listView1.Columns.Add("異常訊息", listView1.Width - 280, HorizontalAlignment.Left);
            listView1.Columns.Add("異常代碼", 130, HorizontalAlignment.Center);
            GlobalData.ListView_Show_OnCode_比對用 = string.Empty;
            GlobalData.dvTable_All_Error.Columns.Add("Time");
            GlobalData.dvTable_All_Error.Columns.Add("Message");
            GlobalData.dvTable_All_Error.Columns.Add("ID");
        }

        #endregion 私有方法

        /// <summary>
        /// 登入/登出設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoginToolStripMenuItem.Text.Equals("登入"))
                {
                    Login login = new Login(this);
                    login.ShowDialog();
                    splitContainer1.Enabled = true;
                }
                else
                {
                    if (MessageBox.Show("登出確定: " + GlobalParameter.UserName, "LoginOut",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        // 使用者異動紀錄
                        recChangeService.AddData(new RecChange
                        {
                            NowTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:ss:mm")),
                            Message = "Login登出",
                            UserName = GlobalParameter.UserName
                        });

                        // 清空參數
                        LoginToolStripMenuItem.Text = "登入";
                        lblUserName.Text = "登入者: ";
                        lblPN.Text = string.Empty;
                        GlobalParameter.UserName = string.Empty;
                        GlobalParameter.AllowItem = string.Empty;
                        txtLot.Clear();

                        // ToolStripMenuItem開關
                        ToolStripMenuItemSW(false, "Login");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 讀取料號
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnloadPN_Click(object sender, EventArgs e)
        {
            // 預設值
            formStartModel.允許2D讀取Tag = formStartModel.允許雷射打印Tag = txtLot.Enabled = false;
            ButtonProperty.SetProperty(btnloadMaterial, false);

            // PLC:DM_8101自動AUTOREADY，正式模式才會執行此點位檢查
            if (GlobalParameter.UseMode.Equals("1"))
            {
                if (!GlobalData.PLC線路異常)
                {
                    if (GlobalData.DM[DmTable.DM_8101自動AUTOREADY].Equals("0"))
                    {
                        MessageBox.Show("PLC異常:DM_8101自動AUTOREADY，機台未準備好，請檢查機台或PLC。");
                        return;
                    }
                }
            }

            // 驗證參數
            if (ValidateUtility.DisplayMessage(ValidateUtility.CheckParameter(txtPN.Text))) { return; }
            try
            {
                if (MessageBox.Show("讀取確認:" + txtPN.Text, "Load",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information).Equals(DialogResult.Yes))
                {
                    ResponseModel responseModel = new PartNoJoinTrayAndLaserService().GetConditionMain(txtPN.Text);
                    partNoJoinTrayAndLaser = responseModel.PartNoJoinTrayAndLaser;
                    if (partNoJoinTrayAndLaser != null)
                    {
                        switch (partNoJoinTrayAndLaser.UsesIten)
                        {
                            case "0":
                                formStartModel.允許2D讀取Tag = formStartModel.允許雷射打印Tag = true;
                                break;

                            case "1":
                                formStartModel.允許2D讀取Tag = true;
                                break;

                            default:
                                formStartModel.允許雷射打印Tag = true;
                                break;
                        }
                        txtLot.Enabled = true;
                        ButtonProperty.SetProperty(btnloadMaterial, true);
                        MessageBox.Show("讀取成功。");
                    }
                    else
                    {
                        MessageBox.Show("查無此料號。");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception錯誤");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 上料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnloadMaterial_Click(object sender, EventArgs e)
        {
            if (GlobalParameter.UseMode.Equals("0")) { MessageBox.Show("測試模式，無法使用上料。"); return; }
            PieceResultModel pieceResultModel = Step1驗證與板子計算();
            if (pieceResultModel == null) { return; }
            try
            {
                if (MessageBox.Show("上料確認:" + partNoJoinTrayAndLaser.PartNo, "Load",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information).Equals(DialogResult.Yes))
                {
                    if (GlobalData.PLC線路異常) { MessageBox.Show("PLC斷線異常"); return; }
                    Step2寫入PLC資料();
                    Step3寫入Laser資料(pieceResultModel);
                    Step4寫入DB_Table_LOT資料();
                    Step5雷射執行動作();
                    Step6條碼讀取器執行動作();
                    Step7結批執行動作();

                    // 使用者異動紀錄
                    recChangeService.AddData(new RecChange
                    {
                        NowTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:ss:mm")),
                        Message = "執行上料",
                        UserName = GlobalParameter.UserName
                    });
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception_Error");
                MessageBox.Show(ex.Message);
            }
        }

        #region 上料執行步驟

        private PieceResultModel Step1驗證與板子計算()
        {
            // 驗證
            List<string> parameter = new List<string> { { txtLot.Text }, { txtPN.Text } };
            if (ValidateUtility.DisplayMessage(ValidateUtility.CheckParameter(parameter))) { return null; }

            // 板子計算方式
            return new PieceCalculationMethod().PieceCalculation(new PieceRequestModel
            {
                板子X尺寸 = Convert.ToDouble(partNoJoinTrayAndLaser.PieceSizeX),
                板子Y尺寸 = Convert.ToDouble(partNoJoinTrayAndLaser.PieceSizeY),
                X偏移位置 = Convert.ToDouble(partNoJoinTrayAndLaser.Xoffset),
                Y偏移位置 = Convert.ToDouble(partNoJoinTrayAndLaser.Xoffset),
                載台與雷射中心點差距 = GlobalParameter.VehicleAndPieceCenter
            });
        }

        private void Step2寫入PLC資料()
        {
            // DWORD
            List<string> data = new List<string>();
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PieceSizeX, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PieceSizeY, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PieceSizeT, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PositionX2D, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PositionY2D, data);
            PLCMethod.SetDefultParameter("0", 20, data);
            PLCMethod.SetParameterSplit(partNoJoinTrayAndLaser.TrayNo.ToString(), data);
            PLCMethod.SetParameterSplit(partNoJoinTrayAndLaser.DivideNoX.ToString(), data);
            PLCMethod.SetParameterSplit(partNoJoinTrayAndLaser.DivideNoY.ToString(), data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.DividePitchX, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.DividePitchY, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PieceCenterX, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PieceCenterY, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.TrayThickness, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.TrayCenter, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.TrayLength, data);
            PLCMethod.SetDefultParameter("0", 10, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.TrayOffsetX, data);
            PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.TrayOffsetY, data);
            PLCMethod.SetDefultParameter("0", 6, data);
            PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Multiple,
                PLCcommand.PLC_IO.DM, DmTable.DM_8300DWORD_板寬X, 0, data.Count, data);

            // judge判斷寫入PLC
            List<string> listJudge = partNoJoinTrayAndLaser.JudgeStatus.Split('、').ToList();
            List<int> listDM = new List<int> {
                            { DmTable.DM_8231PASS }, { DmTable.DM_8232UNTEST }, { DmTable.DM_8233LO },
                            { DmTable.DM_8234LEAK }, { DmTable.DM_8235CCD }, { DmTable.DM_8236OPEN },
                            { DmTable.DM_8237LOW4W}, { DmTable.DM_8238VISUAL}, { DmTable.DM_8239FAIL10 },
                            { DmTable.DM_8240FAIL11}, { DmTable.DM_8241FAIL12}, { DmTable.DM_8242FAIL13 },
                            { DmTable.DM_8243SKIP} };
            if (listJudge.Count == listDM.Count)
            {
                for (int i = 0; i < listJudge.Count; i++)
                {
                    int value = listJudge[i] == "ON" ? 1 : 0;
                    PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Single,
                        PLCcommand.PLC_IO.DM, listDM[i], value, 0, null);
                }
            }
        }

        private void Step3寫入Laser資料(PieceResultModel pieceResultModel)
        {
            string cmd = string.Empty, response = string.Empty, record = string.Empty;
            if (formStartModel.允許雷射打印Tag)
            {
                // 設定 MKM
                if (formStartModel.SHT遮光閥狀態.Equals("1"))
                {
                    cmd = LaserCommand.MKM指令接收控制設定("0");
                    response = LaserExecuteWrite.執行MRK印字觸控制設定(cmd);
                    if (ValidateUtility.DisplayMessage(response)) { return; }
                    formStartModel.SHT遮光閥狀態 = "0";
                    record += "1.SHT,";
                }

                // 設定 FNO
                cmd = LaserCommand.FNO文件變更控制設定(partNoJoinTrayAndLaser.FnoNo);
                response = LaserExecuteWrite.執行FNO文件變更控制設定(cmd);
                if (ValidateUtility.DisplayMessage(response)) { return; }
                record += "2.FNO,";

                // 雷射功率
                cmd = LaserCommand.LPW激光功率控制設定(string.Format("{0:000.0}",
                    Convert.ToInt32(partNoJoinTrayAndLaser.Power)));
                response = LaserExecuteWrite.執行LPW激光功率控制設定(cmd);
                if (ValidateUtility.DisplayMessage(response)) { return; }
                record += "3.LPW,";

                // 掃描速度
                cmd = LaserCommand.SSP掃描速度控制設定(string.Format("{0:00000}",
                    Convert.ToInt32(partNoJoinTrayAndLaser.Speed)));
                response = LaserExecuteWrite.執行SSP掃描速度控制設定(cmd);
                if (ValidateUtility.DisplayMessage(response)) { return; }
                record += "4.SSP,";

                // 設定座標位置
                List<double> list = new List<double> { { pieceResultModel.X位置 }, { pieceResultModel.Y位置 } };
                List<string> laserList = TypeMethod.SetFormatValue(list, "{0:000.000}");
                ModelSPC modelSPC = new ModelSPC
                {
                    SonCmd = "S",
                    Xposition = laserList[0],
                    Yposition = laserList[1]
                };
                cmd = LaserCommand.SPC文字條件控制設定(modelSPC);
                response = LaserExecuteWrite.執行SPC文字條件控制設定(cmd);
                if (ValidateUtility.DisplayMessage(response)) { return; }
                record += "5.SPC,";

                // 雷射參數儲存
                LaserExecuteWrite.執行FOR文件覆盖保存控制設定(LaserCommand.FOR文件覆盖保存控制設定);
                record += "6.FOR,";
            }
        }

        private void Step4寫入DB_Table_LOT資料()
        {
            ResponseModel ResponseModel = new DataLotService().AddData(new DataLot
            {
                CreateUser = GlobalParameter.UserName,
                LotNo = txtLot.Text,
                PartNo = txtPN.Text
            });
            if (ResponseModel.Status == StatusEnum.Ok)
            {
                GlobalParameter.LotNo = txtLot.Text;
                GlobalParameter.PartNo = partNoJoinTrayAndLaser.PartNo;
                lblPN.Text = partNoJoinTrayAndLaser.PartNo;
                txtPN.Clear();
            }
        }

        private void Step5雷射執行動作()
        {
            if (!GlobalData.PLC線路異常)
            {
                if (GlobalData.DM[DmTable.DM_8102自動運轉中].Equals("1"))
                {
                    lblAutoRun.Text = "自動運轉中......";
                    splitContainer1.Enabled = false;
                    ThreadPool.QueueUserWorkItem(雷射動作執行緒);
                }
            }
        }

        private void 雷射動作執行緒(object obj)
        {
            while (true)
            {
                if (GlobalData.DM[DmTable.DM_8123雷射打印開始].Equals("1"))
                {
                    if (formStartModel.允許雷射打印Tag)
                    {
                        if (GlobalData.DM[DmTable.DM_8104停止].Equals("1") || GlobalData.DM[DmTable.DM_8110結批].Equals("1"))
                        {
                            lblAutoRun.Text = "停止運轉";
                            splitContainer1.Enabled = true;
                        }
                        else
                        {
                            string cmd = string.Empty, response = string.Empty;
                            Thread.Sleep(200);
                            if (formStartModel.SHT遮光閥狀態.Equals("0"))
                            {
                                cmd = LaserCommand.MKM指令接收控制設定("1");
                                response = LaserExecuteWrite.執行MRK印字觸控制設定(cmd);
                                if (ValidateUtility.DisplayMessage(response)) { return; }
                                formStartModel.SHT遮光閥狀態 = "1";
                            }

                            Thread.Sleep(200);
                            cmd = LaserCommand.MRK印字觸控制設定("1");
                            response = LaserExecuteWrite.執行MRK印字觸控制設定(cmd);
                            int dm = ValidateUtility.DisplayMessage(response).Equals(true) ?
                                DmTable.DM_8230雷射打印異常 : DmTable.DM_8229雷射打印完畢;
                            PLCMethod.WriteSingle(dm, 1);
                        }
                    }
                    else
                    {
                        PLCMethod.WriteSingle(DmTable.DM_8229雷射打印完畢, 1);
                    }

                    // PLC:DM_8123雷射打印開始，清空點位
                    PLCMethod.WriteSingle(DmTable.DM_8123雷射打印開始, 0);
                }

                Thread.Sleep(100);
            }
        }

        private void Step6條碼讀取器執行動作()
        {
            if (GlobalData.DM[DmTable.DM_8102自動運轉中].Equals("1"))
            {
                lblAutoRun.Text = "自動運轉中......";
                ThreadPool.QueueUserWorkItem(條碼讀取器動作執行緒);
            }
        }

        private void 條碼讀取器動作執行緒(object obj)
        {
            while (true)
            {
                // 讀檔
                if (GlobalData.DM[DmTable.DM_8120CCD讀取2D碼].Equals("1"))
                {
                    if (formStartModel.允許2D讀取Tag)
                    {
                        GlobalParameter.ReadContent = readerAccessor.ExecCommand("LON", 8000);
                        if (string.IsNullOrEmpty(GlobalParameter.ReadContent))
                        {
                            readerAccessor.ExecCommand("LOFF");
                            PLCMethod.WriteSingle(DmTable.DM_8221CCD讀2D檔失敗, 1);
                        }
                        else
                        {
                            PLCMethod.WriteSingle(DmTable.DM_8220CCD讀2D碼完成, 1);
                        }
                    }
                    else
                    {
                        PLCMethod.WriteSingle(DmTable.DM_8220CCD讀2D碼完成, 1);
                    }

                    PLCMethod.WriteSingle(DmTable.DM_8120CCD讀取2D碼, 0);
                }
                Thread.Sleep(100);

                // 寫入
                if (GlobalData.DM[DmTable.DM_81212D碼通知寫入].Equals("1"))
                {
                    string xmlString = "<?xml version='1.0' encoding='UTF-8'?>"
                           + "<COMMAND>"
                           + "<COM_NUM>11</COM_NUM>"
                           + "<INFORMATION>"
                           + "<ID>" + GlobalParameter.ReadContent + "</ID>"
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

                    // 取得子節點參數，正確:"0"、錯誤:"1"
                    XElement xmlroot = XElement.Parse(TypeMethod.FormtXmlDoc(Encoding.UTF8.GetString(recvBuf)));
                    string error = ((XElement)(xmlroot.Element("ERROR"))).Value;
                    int dm = (error == "0") ? DmTable.DM_82222D碼寫入完成 : DmTable.DM_82232D碼寫入失敗;
                    PLCMethod.WriteSingle(dm, 1);

                    // PLC:DM_81212D碼通知寫入，清空點位
                    PLCMethod.WriteSingle(DmTable.DM_81212D碼通知寫入, 0);
                }
                Thread.Sleep(100);
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
                lblCCD.Text = GlobalParameter.ReadContent;
            }
        }

        private void Step7結批執行動作()
        {
            if (GlobalData.DM[DmTable.DM_8102自動運轉中].Equals("1"))
            {
                lblAutoRun.Text = "自動運轉中......";
                ThreadPool.QueueUserWorkItem(DM8110結批);
            }
        }

        private void DM8110結批(object obj)
        {
            while (true)
            {
                if (GlobalData.DM[DmTable.DM_8110結批].Equals("1"))
                {
                    // 產出資料

                    // PLC:DM_8110結批，清空點位
                    PLCMethod.WriteSingle(DmTable.DM_8110結批, 0);
                }

                Thread.Sleep(100);
            }
        }

        #endregion 上料執行步驟

        #region ***********　Error Code　***********

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GlobalData.PLC初始連線是否成功)
            {
                if (!GlobalData.PLC線路異常)
                {
                    定時偵測故障碼();
                }
            }
        }

        private void 定時偵測故障碼()
        {
            try
            {
                string Alert_ON = string.Empty; string Alert_OFF = string.Empty;
                int[] Error_On = new int[16]; int[] Error_Xor_On = new int[16];
                GlobalData.ListView_Show_OnCode = string.Empty;

                // 故障碼總組數10組
                for (int i = 0; i < 10; i++)
                {
                    // 比對故障碼 DM Word 是否有變化
                    if (GlobalData.Err_Change_Word[i] != GlobalData.DM[DmTable.DM_8000_Err_DM_故障碼起始 + i])
                    {
                        GlobalData.Err_Change_Word[i] = GlobalData.DM[DmTable.DM_8000_Err_DM_故障碼起始 + i];

                        // (此Word為16進位)將16進位的字串,轉成2進位,並補足16個點位
                        GlobalData.Err_Change_Bit[i] = APP.補足位數(APP.各種進位轉換(GlobalData.Err_Change_Word[i], 16, 2), 16);
                        for (int x = 0; x < 16; x++)
                        {
                            Error_On[x] = Convert.ToInt32(GlobalData.Err_Change_Bit[i].Substring(x, 1));
                            Error_Xor_On[x] = Error_On[x] ^ Convert.ToInt32(GlobalData.Err_比對用_Bit[i].Substring(x, 1));
                            if (Error_Xor_On[x] == 1 && Error_On[x] == 1)
                            {//異常碼 ON
                                string parameter = APP.補足位數((15 - x).ToString(), 2);
                                Alert_ON += (DmTable.Err_HR_點位起始 + i) + "." + parameter + ";";
                            }
                            else
                            { //異常碼 OFF
                                string parameter = APP.補足位數((15 - x).ToString(), 2);
                                Alert_OFF += (DmTable.Err_HR_點位起始 + i) + "." + parameter + ";";
                            }
                        }

                        GlobalData.Err_比對用_Bit[i] = GlobalData.Err_Change_Bit[i];
                    }
                    for (int m = 0; m < 16; m++)
                    {
                        Error_On[m] = Convert.ToInt32(GlobalData.Err_Change_Bit[i].Substring(m, 1));
                        if (Error_On[m] == 1)
                        {
                            string parameter = APP.補足位數((15 - m).ToString(), 2);
                            GlobalData.ListView_Show_OnCode += (DmTable.Err_HR_點位起始 + i) + "." + parameter + ";";
                        }
                    }
                }

                if (Alert_ON != "")
                {
                    紀錄_寫入故障紀錄("ON", Alert_ON);
                }

                if (Alert_OFF != "")
                {
                    紀錄_寫入故障紀錄("OFF", Alert_OFF);
                }

                故障碼View_Show();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception錯誤");
            }
        }

        private void 紀錄_寫入故障紀錄(string ON_OFF, string alert)
        {
            try
            {
                IErrorDescriptionService errorDescriptionService = new ErrorDescriptionService();
                ResponseModel responseModel = errorDescriptionService.GetAllData();
                List<string> errorList = alert.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var errorId in errorList)
                {
                    ErrorDescription list = responseModel.ErrorDescriptionList.Where(x => x.ErrorID == errorId).FirstOrDefault();
                    if (!string.IsNullOrEmpty(list.ChDescription))
                    {
                        int one = Convert.ToInt32(list.ErrorID.Substring(0, 3)) - DmTable.Err_HR_點位起始;
                        int two = Convert.ToInt32(APP.右邊開始取字串(list.ErrorID, 2));
                        if (ON_OFF == "ON")
                        {
                            GlobalData.ALL_Error[one, two, 0] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            GlobalData.ALL_Error[one, two, 1] = list.ChDescription;
                            GlobalData.ALL_Error[one, two, 4] = list.ErrorID;
                        }
                        else
                        {
                            string startTime = GlobalData.ALL_Error[one, two, 0];
                            string endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            string message = GlobalData.ALL_Error[one, two, 1];

                            // 寫入DB
                            IRecErrorService recErrorService = new RecErrorService();
                            recErrorService.AddData(new RecError
                            {
                                StartTime = Convert.ToDateTime(startTime),
                                EndTime = Convert.ToDateTime(endTime),
                                ErrorID = errorId,
                                Message = message,
                                UserName = GlobalParameter.UserName
                            });

                            // 寫出txt檔
                            string month = string.Format("{0}月", DateTime.Now.Month.ToString());
                            string fileName = string.Format("ErrorCode{0}.txt", DateTime.Now.ToString("yyyy/MM/dd"));
                            string pathWrite = Path.Combine("C:", month, fileName);
                            string content = string.Format("{0},{1},{2},{3}", startTime, endTime, errorId, message);
                            File.AppendAllText(pathWrite, content, Encoding.Default);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception錯誤");
            }
        }

        private void 故障碼View_Show()
        {
            try
            {
                if (GlobalData.ListView_Show_OnCode != GlobalData.ListView_Show_OnCode_比對用)
                {
                    listView1.Items.Clear();
                    GlobalData.ListView_Show_OnCode_比對用 = string.Empty;
                    string ListTime = string.Empty; string ListMsg = string.Empty; string Err = string.Empty;
                    string[] ErrorStrArray = GlobalData.ListView_Show_OnCode.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    int ErrorNum = ErrorStrArray.Length;
                    if (ErrorNum >= 1)
                    {
                        for (int i = 0; i < ErrorNum; i++)
                        {
                            string Error_only_Str_Temp = ErrorStrArray[i];
                            int one, two;
                            string numStr1 = Error_only_Str_Temp.Substring(0, 3);
                            string numStr2 = APP.右邊開始取字串(Error_only_Str_Temp, 2);
                            one = Convert.ToInt32(numStr1) - DmTable.Err_HR_點位起始;
                            two = Convert.ToInt32(numStr2);
                            ListTime = GlobalData.ALL_Error[one, two, 0];
                            ListMsg = GlobalData.ALL_Error[one, two, 1];
                            Err = Error_only_Str_Temp;
                            if (!string.IsNullOrEmpty(ListTime) && !string.IsNullOrEmpty(ListMsg))
                            {
                                DataRow row = GlobalData.dvTable_All_Error.NewRow();
                                row[0] = ListTime;
                                row[1] = ListMsg;
                                row[2] = Err;

                                GlobalData.dvTable_All_Error.Rows.Add(row);
                                GlobalData.dvTable_All_Error.DefaultView.Sort = "Time DESC";
                                GlobalData.dvTable_All_Error = GlobalData.dvTable_All_Error.DefaultView.ToTable();
                            }
                        }
                        foreach (DataRow row in GlobalData.dvTable_All_Error.Rows)
                        {
                            ListViewItem item = new ListViewItem(row[0].ToString());
                            for (int x = 1; x < GlobalData.dvTable_All_Error.Columns.Count; x++)
                            {
                                item.SubItems.Add(row[x].ToString());
                            }
                            listView1.Items.Add(item);
                        }

                        GlobalData.dvTable_All_Error.Rows.Clear();
                        GlobalData.ListView_Show_OnCode_比對用 = GlobalData.ListView_Show_OnCode.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception錯誤");
            }
        }

        #endregion ***********　Error Code　***********
    }
}
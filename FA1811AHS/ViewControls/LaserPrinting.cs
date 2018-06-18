using ConnPLCcommand;
using FA1811AHS.Repository;
using FA1811AHS.Service;
using FA1811AHS.Shared;
using NLog;
using PanasonicLP410;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FA1811AHS
{
    public partial class LaserPrinting : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IRecChangeService recChangeService = new RecChangeService();
        private FormStartModel formStartModel = new FormStartModel();
        private List<PartNoJoinLaser> partNoJoinLaser = new List<PartNoJoinLaser>();
        private DataTray dataTray = new DataTray();
        private string SHT遮光閥狀態 { get; set; }
        private string LSR雷射狀態 { get; set; }

        public LaserPrinting()
        {
            InitializeComponent();
            SetDefultEvent();
            toolStripbtnSet.Enabled = false;
            toolStripbtnPoint.Enabled = false;

            if (GlobalParameter.UseMode.Equals("1"))
            {
                #region 測試後不用要刪除

                PLCMethod.PLC_Connect();
                Thread.Sleep(1000);

                #endregion 測試後不用要刪除

                panel1.Enabled = false;
                try
                {
                    if (!GlobalData.PLC初始連線是否成功) { MessageBox.Show("PLC連線失敗:請檢查PLC連線IP、機台是否異常。"); return; }
                    if (!GlobalData.PLC線路異常) { ThreadPool.QueueUserWorkItem(DM8123雷射打印執行緒); }
                    else { MessageBox.Show("PLC線路異常。"); return; }
                    SerialPortMethod.LaserOpenPort(GlobalParameter.LaserSerialPort);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception錯誤");
                    MessageBox.Show("Exception錯誤:" + ex.Message);
                    return;
                }

                try
                {
                    if (LaserConnectionPort.LaserPort.IsOpen)
                    {
                        ModelSTS modelSTS = LaserExecuteWrite.執行STS狀態請求讀取(LaserCommand.STS狀態請求讀取);
                        formStartModel.LSR雷射狀態 = modelSTS.LSRStatus; formStartModel.SHT遮光閥狀態 = modelSTS.SHTStatus;
                        DefultLaserControll();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception錯誤");
                    MessageBox.Show("請檢查雷射是否斷線、未開機、未開啟REMOTE。");
                }

                panel1.Enabled = true;
            }
        }

        #region 私有事件、方法

        /// <summary>
        /// 預設事件設定
        /// </summary>
        private void SetDefultEvent()
        {
            // TextBoxEvent
            txtXOffset.Click += TextBoxEvent.ClickForNegative;
            txtYOffset.Click += TextBoxEvent.ClickForNegative;
            txtPower.Click += TextBoxEvent.ClickEvent;
            txtSpeed.Click += TextBoxEvent.ClickEvent;
            txtXOffset.TextChanged += TextBoxEvent.TextChangedForLaser;
            txtYOffset.TextChanged += TextBoxEvent.TextChangedForLaser;
            txtPower.TextChanged += TextBoxEvent.TextChangedForLaser;
            txtSpeed.TextChanged += TextBoxEvent.TextChangedForLaser;
            txtPower.TextChanged += TextBoxTextChanged;
            txtSpeed.TextChanged += TextBoxTextChanged;
            lblXoffset.TextChanged += LabelEvent.TextChangedForLaser;
            lblYoffset.TextChanged += LabelEvent.TextChangedForLaser;

            // ButtonEvent
            btnLeft.Click += ButtonClick;
            btnRight.Click += ButtonClick;
            btnTop.Click += ButtonClick;
            btnBottom.Click += ButtonClick;
        }

        /// <summary>
        /// ButtonClick事件，計算偏移值 連動控制Label_Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, EventArgs e)
        {
            double xoffsetOld = Convert.ToDouble(lblXoffset.Text);
            double xoffset = txtXOffset.Text.Equals(string.Empty) ? 0.000 : Convert.ToDouble(txtXOffset.Text);
            double yoffsetOld = Convert.ToDouble(lblYoffset.Text);
            double yoffset = txtYOffset.Text.Equals(string.Empty) ? 0.000 : Convert.ToDouble(txtYOffset.Text);
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "btnLeft":
                    lblXoffset.Text = (xoffsetOld - xoffset).ToString("f3");
                    break;

                case "btnRight":
                    lblXoffset.Text = (xoffsetOld + xoffset).ToString("f3");
                    break;

                case "btnTop":
                    lblYoffset.Text = (yoffsetOld + yoffset).ToString("f3");
                    break;

                default:
                    lblYoffset.Text = (yoffsetOld - yoffset).ToString("f3");
                    break;
            }
        }

        /// <summary>
        /// TextBoxTextChangedg事件，連動控制Label_Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxTextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            switch (textBox.Name)
            {
                case "txtPower":
                    lblPower.Text = textBox.Text;
                    break;

                case "txtSpeed":
                    lblSpeed.Text = textBox.Text;
                    break;

                default:
                    break;
            }
        }

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

        #endregion 私有事件、方法

        #region 查詢料號

        /// <summary>
        /// 料號查詢，只搜尋能使用雷射的料號
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeatch_Click(object sender, EventArgs e)
        {
            SeatchPratNoAndTray();
        }

        /// <summary>
        /// 料號查詢，下拉選擇參數改變時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBoxPartNo_SelectedValueChanged(object sender, EventArgs e)
        {
            SeatchPratNoAndTray();
        }

        /// <summary>
        /// 料號料盤查詢
        /// </summary>
        private void SeatchPratNoAndTray()
        {
            try
            {
                IPartNoJoinLaserService partNoJoinLaserService = new PartNoJoinLaserService();
                ResponseModel ResponseModel = partNoJoinLaserService.GetLikeConditionMain(cbBoxPartNo.Text);
                cbBoxPartNo.DataSource = ResponseModel.PartNoJoinLaserList.Select(x => x.PartNo).ToList();
                partNoJoinLaser = ResponseModel.PartNoJoinLaserList.Where(x => x.PartNo == cbBoxPartNo.Text).ToList();
                if (partNoJoinLaser.Count > 0)
                {
                    IDataTrayService dataTrayService = new DataTrayService();
                    ResponseModel responseModel = dataTrayService.GetConditionData(partNoJoinLaser[0].TrayNo.ToString());
                    dataTray = responseModel.DataTray;
                }

                if (partNoJoinLaser.Count > 0 && dataTray != null)
                {
                    lblFNO.Text = partNoJoinLaser[0].FnoNo;
                    lblXoffset.Text = partNoJoinLaser[0].Xoffset;
                    lblYoffset.Text = partNoJoinLaser[0].Yoffset;
                    lblPower.Text = partNoJoinLaser[0].Power;
                    lblSpeed.Text = partNoJoinLaser[0].Speed;
                    toolStripbtnSet.Enabled = true;
                }
                else
                {
                    lblFNO.Text = lblPower.Text = txtPower.Text = string.Empty;
                    lblSpeed.Text = txtSpeed.Text = string.Empty;
                    lblXoffset.Text = lblYoffset.Text = "0.000";
                    txtXOffset.Text = txtYOffset.Text = "0.000";
                    toolStripbtnSet.Enabled = false;

                    // 清除combobox資料
                    cbBoxPartNo.DataSource = null;
                    cbBoxPartNo.Text = string.Empty;
                    MessageBox.Show("查無此料號。");
                }
                toolStripbtnPoint.Enabled = false;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error");
                MessageBox.Show(ex.Message);
            }
        }

        #endregion 查詢料號

        /// <summary>
        /// 設定存檔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnSet_Click(object sender, EventArgs e)
        {
            string cmd = string.Empty, response = string.Empty, record = string.Empty;
            toolStripbtnSet.Enabled = false;
            toolStripbtnPoint.Enabled = true;
            try
            {
                // 異動紀錄
                AddRecChangeMethod("手動流程:雷射設定存檔");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (GlobalParameter.UseMode.Equals("0")) { MessageBox.Show("測試模式，無法設定存檔。"); return; }

            // 驗證資料
            List<string> parameter = new List<string> {
                {cbBoxPartNo.Text },{ lblFNO.Text},{ lblPower.Text },{ lblSpeed.Text}};
            response = ValidateUtility.CheckParameter(parameter);
            if (ValidateUtility.DisplayMessage(response)) { return; }

            // 板子計算方式
            PieceResultModel pieceResultModel = new PieceCalculationMethod()
                .PieceCalculation(new PieceRequestModel
                {
                    板子X尺寸 = Convert.ToDouble(partNoJoinLaser[0].PieceSizeX),
                    板子Y尺寸 = Convert.ToDouble(partNoJoinLaser[0].PieceSizeY),
                    X偏移位置 = Convert.ToDouble(lblXoffset.Text),
                    Y偏移位置 = Convert.ToDouble(lblYoffset.Text),
                    載台與雷射中心點差距 = GlobalParameter.VehicleAndPieceCenter
                });

            #region PLC寫入

            try
            {
                if (!GlobalData.PLC線路異常)
                {
                    List<string> data = new List<string>();
                    PLCMethod.SetParameterSplitForPoint(partNoJoinLaser[0].PieceSizeX, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinLaser[0].PieceSizeY, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinLaser[0].PieceSizeT, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinLaser[0].PositionX2D, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinLaser[0].PositionY2D, data);
                    PLCMethod.SetDefultParameter("0", 20, data);
                    PLCMethod.SetParameterSplit(dataTray.TrayNo.ToString(), data);
                    PLCMethod.SetParameterSplit(dataTray.DivideNoX.ToString(), data);
                    PLCMethod.SetParameterSplit(dataTray.DivideNoY.ToString(), data);
                    PLCMethod.SetParameterSplitForPoint(dataTray.DividePitchX, data);
                    PLCMethod.SetParameterSplitForPoint(dataTray.DividePitchY, data);
                    PLCMethod.SetParameterSplitForPoint(dataTray.PieceCenterX, data);
                    PLCMethod.SetParameterSplitForPoint(dataTray.PieceCenterY, data);
                    PLCMethod.SetParameterSplitForPoint(dataTray.TrayThickness, data);
                    PLCMethod.SetParameterSplitForPoint(dataTray.TrayCenter, data);
                    PLCMethod.SetParameterSplitForPoint(dataTray.TrayLength, data);
                    PLCMethod.SetDefultParameter("0", 10, data);
                    PLCMethod.SetParameterSplitForPoint(dataTray.TrayOffsetX, data);
                    PLCMethod.SetParameterSplitForPoint(dataTray.TrayOffsetY, data);
                    PLCMethod.SetDefultParameter("0", 6, data);
                    PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Multiple,
                        PLCcommand.PLC_IO.DM, DmTable.DM_8300DWORD_板寬X, 0, data.Count, data);
                }
                else
                {
                    MessageBox.Show("PLC線路異常。");
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception錯誤");
                MessageBox.Show("Exception_PLC系統異常錯誤:" + ex.Message);
                return;
            }

            #endregion PLC寫入

            #region 雷射寫入

            try
            {
                if (SHT遮光閥狀態.Equals("1"))
                {
                    cmd = LaserCommand.MKM指令接收控制設定("0");
                    response = LaserExecuteWrite.執行MRK印字觸控制設定(cmd);
                    if (ValidateUtility.DisplayMessage(response)) { return; }
                    SHT遮光閥狀態 = "0";
                    record += "1.SHT,";
                }

                // 設定 FNO
                cmd = LaserCommand.FNO文件變更控制設定(lblFNO.Text);
                response = LaserExecuteWrite.執行FNO文件變更控制設定(cmd);
                if (ValidateUtility.DisplayMessage(response)) { return; }
                record += "2.FNO,";

                // 雷射功率
                cmd = LaserCommand.LPW激光功率控制設定(string.Format("{0:000.0}",
                    Convert.ToInt32(lblPower.Text)));
                response = LaserExecuteWrite.執行LPW激光功率控制設定(cmd);
                if (ValidateUtility.DisplayMessage(response)) { return; }
                record += "3.LPW,";

                // 掃描速度
                cmd = LaserCommand.SSP掃描速度控制設定(string.Format("{0:00000}",
                    Convert.ToInt32(lblSpeed.Text)));
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

                // DB資料寫入
                IDataLaserService dataLaserService = new DataLaserService();
                ResponseModel ResponseModel = dataLaserService.UpdateData(new DataLaser
                {
                    PartNo = cbBoxPartNo.Text,
                    FnoNo = lblFNO.Text,
                    Xoffset = lblXoffset.Text,
                    Yoffset = lblYoffset.Text,
                    Power = lblPower.Text,
                    Speed = lblSpeed.Text
                });
                if (ResponseModel.Status.Equals(StatusEnum.Error))
                {
                    logger.Error(ResponseModel.ResponseMsg);
                    MessageBox.Show(ResponseModel.ResponseMsg);
                    return;
                }
                record += "7.DB";

                if (!ValidateUtility.DisplayMessage(response))
                {
                    MessageBox.Show("設定完成，執行紀錄:" + record);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception_Error:" + record);
                MessageBox.Show("Exception_雷射系統異常錯誤:" + ex.Message + "，執行紀錄:" + record);
            }

            #endregion 雷射寫入
        }

        #region 雷射打印，雷射執行緒

        /// <summary>
        /// 雷射打印，[DM_8111允許手動][DM_8214手動打印通知]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnPoint_Click(object sender, EventArgs e)
        {
            toolStripbtnSet.Enabled = true;
            toolStripbtnPoint.Enabled = false;

            try
            {
                // 異動紀錄
                AddRecChangeMethod("手動流程:雷射打印執行");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (GlobalParameter.UseMode.Equals("0")) { MessageBox.Show("測試模式，無法雷射打印。"); return; }
            try
            {
                if (GlobalData.PLC線路異常) { MessageBox.Show("PLC線路異常。"); return; }
                if (GlobalData.DM[DmTable.DM_8111允許手動] != "1")
                {
                    MessageBox.Show("PLC讀取異常[DM_8111允許手動]:不允許手動，請檢查PLC參數。");
                    return;
                }

                if (SHT遮光閥狀態 == "0")
                {
                    string cmd = LaserCommand.MKM指令接收控制設定("1");
                    string response = LaserExecuteWrite.執行MRK印字觸控制設定(cmd);
                    if (ValidateUtility.DisplayMessage(response)) { return; }
                    SHT遮光閥狀態 = "1";
                }

                PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Single,
                          PLCcommand.PLC_IO.DM, DmTable.DM_8214手動打印通知, 1, 0, null);
                formStartModel.允許雷射打印Tag = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception錯誤");
                MessageBox.Show("Exception_PLC系統異常錯誤:" + ex.Message);
                return;
            }
        }

        private void DM8123雷射打印執行緒(object sender)
        {
            string cmd = string.Empty, response = string.Empty;
            while (true)
            {
                if (!GlobalData.PLC線路異常)
                {
                    if (formStartModel.允許雷射打印Tag)
                    {
                        if (GlobalData.DM[DmTable.DM_8123雷射打印開始].Equals("1"))
                        {
                            try
                            {
                                cmd = LaserCommand.MRK印字觸控制設定("1");
                                response = LaserExecuteWrite.執行MRK印字觸控制設定(cmd);
                                Thread.Sleep(500);
                                if (ValidateUtility.DisplayMessage(response))
                                {
                                    PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Single,
                                        PLCcommand.PLC_IO.DM, DmTable.DM_8230雷射打印異常, 1, 0, null);
                                    MessageBox.Show(response);
                                }
                                else
                                {
                                    PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Single,
                                        PLCcommand.PLC_IO.DM, DmTable.DM_8229雷射打印完畢, 1, 0, null);
                                    MessageBox.Show("打印成功");
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Exception錯誤");
                                MessageBox.Show(ex.Message);
                            }

                            PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Single,
                                 PLCcommand.PLC_IO.DM, DmTable.DM_8123雷射打印開始, 0, 0, null);
                            formStartModel.允許雷射打印Tag = false;
                        }
                    }
                }
            }
        }

        #endregion 雷射打印，雷射執行緒

        /// <summary>
        /// 關閉離開
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 初始設定(覆歸使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLaserInitial_Click(object sender, EventArgs e)
        {
            lblXoffset.Text = lblYoffset.Text = "0.000";
            txtXOffset.Text = txtYOffset.Text = "0.000";
            lblPower.Text = txtPower.Text = "30";
            lblSpeed.Text = txtSpeed.Text = "500";
        }
    }
}
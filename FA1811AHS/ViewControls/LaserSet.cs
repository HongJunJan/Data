using FA1811AHS.Repository;
using FA1811AHS.Service;
using FA1811AHS.Shared;
using NLog;
using PanasonicLP410;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FA1811AHS
{
    public partial class LaserSet : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IRecChangeService recChangeService = new RecChangeService();

        public LaserSet()
        {
            InitializeComponent();
            SetDefultEvent();
            if (GlobalParameter.UseMode.Equals("1"))
            {
                try
                {
                    SerialPortMethod.LaserOpenPort(GlobalParameter.LaserSerialPort);
                    if (LaserConnectionPort.LaserPort.IsOpen)
                    {
                        ModelSTS modelSTS = LaserExecuteWrite.執行STS狀態請求讀取(LaserCommand.STS狀態請求讀取);
                        SHT遮光閥狀態 = modelSTS.SHTStatus;
                        toolStripbtnSave.Enabled = true;
                        panel1.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error");
                    MessageBox.Show("請檢查雷射是否斷線、未開機、未開啟REMOTE。");
                    toolStripbtnSave.Enabled = false;
                    panel1.Enabled = false;
                }
            }
        }

        #region 私有事件、變數

        /// <summary>
        /// 預設事件設定
        /// </summary>
        private void SetDefultEvent()
        {
            txtFNO.Click += TextBoxEvent.ClickEvent;
            txtHeight.Click += TextBoxEvent.ClickEvent;
            txtWidth.Click += TextBoxEvent.ClickEvent;
            txtFNO.TextChanged += TextBoxEvent.TextChangedForLaser;
            txtHeight.TextChanged += TextBoxEvent.TextChangedForLaser;
            txtWidth.TextChanged += TextBoxEvent.TextChangedForLaser;
        }

        private string SHT遮光閥狀態 { get; set; }

        #endregion 私有事件、變數

        /// <summary>
        /// JIS文字選擇
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJisSelete_Click(object sender, EventArgs e)
        {
            lblJisCode.Text = JisForm.GetTextBoxValue();
        }

        /// <summary>
        /// 程序確定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnSave_Click(object sender, EventArgs e)
        {
            string cmd = string.Empty, response = string.Empty, record = string.Empty;
            try
            {
                // 使用者異動紀錄
                recChangeService.AddData(new RecChange
                {
                    NowTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:ss:mm")),
                    Message = "雷射設定",
                    UserName = GlobalParameter.UserName
                });

                // 檢查欄位是否有空白
                List<string> parameter = new List<string> {
                    { txtFNO.Text }, { lblJisCode.Text }, { txtHeight.Text }, { txtWidth.Text } };
                response = ValidateUtility.CheckParameter(parameter);
                if (ValidateUtility.DisplayMessage(response)) { return; }
                if (GlobalParameter.UseMode.Equals("1"))
                {
                    // 寫入db
                    ILaserFnoService laserFnoService = new LaserFnoService();
                    LaserFNO laserFNO = new LaserFNO { LaserFnoNo = txtFNO.Text };
                    ResponseModel ResponseModel = laserFnoService.AddData(laserFNO);
                    if (ResponseModel.Status != StatusEnum.Ok)
                    {
                        MessageBox.Show(ResponseModel.ResponseMsg);
                        return;
                    }

                    // 寫入雷射
                    if (SHT遮光閥狀態 == "1")
                    {
                        cmd = LaserCommand.MKM指令接收控制設定("0");
                        response = LaserExecuteWrite.執行MRK印字觸控制設定(cmd);
                        if (ValidateUtility.DisplayMessage(response)) { return; }
                        record += "1.SHT,";
                        SHT遮光閥狀態 = "0";
                    }

                    // 設定 FNO
                    cmd = LaserCommand.FNO文件變更控制設定(txtFNO.Text);
                    response = LaserExecuteWrite.執行FNO文件變更控制設定(cmd);
                    if (ValidateUtility.DisplayMessage(response)) { return; }
                    record += "2.FNO,";

                    // 雷射功率
                    cmd = LaserCommand.LPW激光功率控制設定(string.Format("{0:000.0}", 40));
                    response = LaserExecuteWrite.執行LPW激光功率控制設定(cmd);
                    if (ValidateUtility.DisplayMessage(response)) { return; }
                    record += "3.LPW,";

                    // 掃描速度
                    cmd = LaserCommand.SSP掃描速度控制設定(string.Format("{0:00000}", 500));
                    response = LaserExecuteWrite.執行SSP掃描速度控制設定(cmd);
                    if (ValidateUtility.DisplayMessage(response)) { return; }
                    record += "4.SSP,";

                    // 設定條件 ALC
                    cmd = LaserCommand.ALC整體條件的控制設定(new ModelALC
                    {
                        SonCmd = "S",
                        Xoffset = "+000.000",
                        Yoffset = "+000.000",
                        RotationOffset = "-090.00"
                    });
                    response = LaserExecuteWrite.執行ALC整體條件的控制設定(cmd);
                    if (ValidateUtility.DisplayMessage(response)) { return; }
                    record += "5.ALC,";

                    // 設定文字型狀 STR
                    cmd = LaserCommand.STR印字文字列設定(lblJisCode.Text);
                    response = LaserExecuteWrite.執行STR印字文字列設定(cmd);
                    if (ValidateUtility.DisplayMessage(response)) { return; }
                    record += "6.STR,";

                    /* 文字條件控制設定STC
                     * 範例:"STCS010010101006.000006.000+000.000+000.000000.000000.000+100.0011.000100100"; */
                    ModelSTC modelSTC = new ModelSTC
                    {
                        SonCmd = "S",
                        Height = string.Format("{0:000.000}", Convert.ToDouble(txtHeight.Text)),
                        Width = string.Format("{0:000.000}", Convert.ToDouble(txtWidth.Text)),
                        Xposition = "+000.000",
                        Yposition = "+000.000",
                        Bold = "0.000"
                    };
                    cmd = LaserCommand.STC文字條件控制設定(modelSTC);
                    response = LaserExecuteWrite.執行STC文字條件控制設定(cmd);
                    if (ValidateUtility.DisplayMessage(response)) { return; }
                    record += "7.STC,";

                    // 雷射參數儲存
                    LaserExecuteWrite.執行FOR文件覆盖保存控制設定(LaserCommand.FOR文件覆盖保存控制設定);
                    record += "8.FOR";

                    if (!ValidateUtility.DisplayMessage(response)) { MessageBox.Show("設定完成，執行紀錄:" + record); }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 關閉離開
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnExit_Click(object sender, EventArgs e)
        {
            if (GlobalParameter.UseMode.Equals("1"))
            {
                try
                {
                    // 遮光閥開啟
                    if (SHT遮光閥狀態.Equals("0") || SHT遮光閥狀態.Equals("\0"))
                    {
                        string cmd = LaserCommand.MKM指令接收控制設定("1");
                        string response = LaserExecuteWrite.執行MRK印字觸控制設定(cmd);
                        if (ValidateUtility.DisplayMessage(response)) { return; }
                        SHT遮光閥狀態 = "1";
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error");
                }
            }

            this.Close();
        }
    }
}
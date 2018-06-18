using FA1811AHS.Repository;
using FA1811AHS.Service;
using FA1811AHS.Shared;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FA1811AHS
{
    /// <summary>
    /// 料號設定項目
    /// </summary>
    public partial class PartNoSet : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IRecChangeService recChangeService = new RecChangeService();
        private IDataPartNoService partNoService = new DataPartNoService();
        private IDataJudgeService judgeService = new DataJudgeService();
        private ILaserFnoService laserFnoService = new LaserFnoService();
        private IPartNoJoinTrayAndLaserService partNoJainTablesService = new PartNoJoinTrayAndLaserService();

        public PartNoSet()
        {
            InitializeComponent();
            DefultEvent();
            SetPropertyControl();
            DefaultGridViewValue();
        }

        #region 私有事件

        /// <summary>
        /// 預設事件設定
        /// </summary>
        private void DefultEvent()
        {
            // TextBoxEvent
            txtPieceX.Click += TextBoxEvent.ClickEvent;
            txtPieceY.Click += TextBoxEvent.ClickEvent;
            txt2DPositionX.Click += TextBoxEvent.ClickForNegative;
            txt2DPositionY.Click += TextBoxEvent.ClickForNegative;
            txtPower.Click += TextBoxEvent.ClickEvent;
            txtSpeed.Click += TextBoxEvent.ClickEvent;
            txtPieceX.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txtPieceY.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txt2DPositionX.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txt2DPositionY.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txtPower.TextChanged += TextBoxEvent.TextChangedForLaser;
            txtSpeed.TextChanged += TextBoxEvent.TextChangedForLaser;

            // dataGridViewEvent
            dataGridView1.CellClick += DataGridViewCellClickEvent;
            datagvJudge.CellClick += DataGridViewCellClickEvent;
        }

        /// <summary>
        /// DataGridViewCellClick事件，選取DataGridView，顯示資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewCellClickEvent(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.Name.Equals("dataGridView1"))
            {
                if (e.RowIndex > -1)
                {
                    txtPartNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
                    txtTrayNo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
                    txtPieceX.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString().Trim();
                    txtPieceY.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString().Trim();
                    txt2DPositionX.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim();
                    txt2DPositionY.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString().Trim();
                    switch (dataGridView1.CurrentRow.Cells[7].Value.ToString().Trim())
                    {
                        case "0":
                            radiobtnAll.Checked = true;
                            break;

                        case "1":
                            radiobtn2DRead.Checked = true;
                            break;

                        default:
                            radiobtnLaser.Checked = true;
                            break;
                    }

                    // judge參數
                    string[] judgeAry = dataGridView1.CurrentRow.Cells[8].Value.ToString().Trim().Split('、');
                    int count = 0, length = judgeAry.Length;
                    foreach (var item in judgeAry)
                    {
                        if (count <= length)
                        {
                            DataGridViewCell judge = datagvJudge.Rows[count].Cells[1];
                            judge.Value = item.Equals("ON") ? "ON" : "OFF";
                            judge.Style.ForeColor = item.Equals("ON") ? Color.Red : Color.Gray;
                            count++;
                        }
                    }
                    lblTrayName.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString().Trim();

                    // 雷射參數
                    comboFNO.Text = dataGridView1.CurrentRow.Cells[21].Value.ToString().Trim();
                    txtPower.Text = dataGridView1.CurrentRow.Cells[24].Value.ToString().Trim();
                    txtSpeed.Text = dataGridView1.CurrentRow.Cells[25].Value.ToString().Trim();
                }
            }
            else if (dataGridView.Name.Equals("datagvJudge"))
            {
                if (e.RowIndex > -1)
                {
                    string value = datagvJudge.CurrentRow.Cells[1].Value.ToString().Trim();
                    DataGridViewCell judge = datagvJudge.Rows[e.RowIndex].Cells[1];
                    judge.Value = value.Equals("ON") ? "OFF" : "ON";
                    judge.Style.ForeColor = value.Equals("ON") ? Color.Gray : Color.Red;
                }
            }
        }

        #endregion 私有事件

        #region 私有方法

        /// <summary>
        /// 預設值:設定元件屬性樣式
        /// </summary>
        private void SetPropertyControl()
        {
            ButtonProperty.SetProperty(btnSelectPartNo, true);
            ButtonProperty.SetProperty(btnTraySelect, true);
            DataGridviewProperty.SetProperty(dataGridView1);
            DataGridviewProperty.SetProperty(datagvJudge);
        }

        /// <summary>
        /// 預設值:取得db資料 TO DataGridView
        /// </summary>
        private void DefaultGridViewValue()
        {
            try
            {
                #region partNo

                // 取得資料給GridView
                ResponseModel responseModel = partNoJainTablesService.GetAllData();
                dataGridView1.DataSource = responseModel.PartNoJoinTrayAndLaserList;

                // 標題:名稱、樣式 調整
                dataGridView1.Columns["PartNo"].HeaderCell.Value = "料號";
                dataGridView1.Columns["PartNo"].HeaderCell.Style.Font = new Font("微軟正黑體", 12F);

                // 隱藏欄位
                List<string> list = new List<string>
                {
                    {"TrayNo"},{"PieceSizeX"},{"PieceSizeY"},{"PieceSizeT"},{"PositionX2D"},{"PositionY2D"},
                    {"UsesIten"},{"JudgeStatus"},{"TrayName"},{"DivideNoX"},{"DivideNoY"},
                    {"DividePitchX"},{"DividePitchY"},{"PieceCenterX"},{"PieceCenterY"},{"TrayThickness"},
                    {"TrayCenter"},{"TrayLength"},{"TrayOffsetX"},{"TrayOffsetY"},{"FnoNo"},
                    {"Xoffset"},{"Yoffset"},{"Power"},{"Speed"}
                };
                foreach (var item in list) { dataGridView1.Columns[item].Visible = false; }

                #endregion partNo

                // 取得資料給GridView
                responseModel = judgeService.GetAllData();
                datagvJudge.RowCount = responseModel.DataJudgeList.Count;

                // 新增DataGridView Column
                datagvJudge.ColumnCount = 2;
                int rowSum = 0;
                foreach (var item in responseModel.DataJudgeList)
                {
                    datagvJudge[0, rowSum].Value = item.Name;
                    datagvJudge[1, rowSum].Value = "ON";
                    datagvJudge[1, rowSum].Style.ForeColor = Color.Red;
                    rowSum++;
                }

                // Fno
                responseModel = laserFnoService.GetAllData();
                comboFNO.DataSource = responseModel.LaserFnoList.Select(x => x.LaserFnoNo).ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 驗證空值，[無空值]正確:true、[有空值]錯誤:false
        /// </summary>
        /// <returns>bool值</returns>
        private bool CheckValueEmpty()
        {
            bool check = true;
            List<string> parameter = new List<string>
            {
                {txtPartNo.Text },{ txtPieceX.Text },{ txtPieceY.Text},
                {txt2DPositionX.Text},{ txt2DPositionY.Text},{ txtTrayNo.Text},
                {txtPower.Text},{txtSpeed.Text}
            };
            string response = ValidateUtility.CheckParameter(parameter);
            if (!string.IsNullOrEmpty(response))
            {
                MessageBox.Show(response);
                check = false;
            }

            return check;
        }

        /// <summary>
        /// Judge資料，參數組合(新增、更新)使用
        /// </summary>
        /// <returns>Judge資料</returns>
        private string SetJudgeParameter()
        {
            string judgeStr = string.Empty;
            for (int i = 0; i < datagvJudge.RowCount; i++)
            {
                judgeStr += datagvJudge.Rows[i].Cells[1].Value.ToString() + "、";
            }

            judgeStr = judgeStr.Substring(0, judgeStr.Length - 1);
            return judgeStr;
        }

        /// <summary>
        /// 啟用控制設定
        /// </summary>
        /// <returns>啟用控制代碼</returns>
        private string SetRadioButtonItemSW()
        {
            string usesIten = string.Empty;
            Dictionary<string, string> dictionary = new EquipmentDictionary().SetCustomizeItem();
            foreach (RadioButton item in groupBoxItem.Controls)
            {
                if (item.Checked.Equals(true)) { usesIten = dictionary[item.Name]; }
            }

            return usesIten;
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

        #endregion 私有方法

        /// <summary>
        /// 料號查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectPartNo_Click(object sender, EventArgs e)
        {
            try
            {
                ResponseModel responseModel = partNoJainTablesService.GetLikeConditionMain(txtSelectPartNo.Text);
                if (responseModel.Status.Equals(StatusEnum.Ok))
                {
                    dataGridView1.DataSource = responseModel.PartNoJoinTrayAndLaserList;
                    dataGridView1.Enabled = responseModel.PartNoJoinTrayAndLaserList.Count > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception錯誤");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // 驗證
                if (!CheckValueEmpty()) { return; }

                // 資料準備
                DataPartNo dataPartNo = new DataPartNo
                {
                    EditUser = GlobalParameter.UserName,
                    PartNo = txtPartNo.Text,
                    PieceSizeX = txtPieceX.Text,
                    PieceSizeY = txtPieceY.Text,
                    PositionX2D = txt2DPositionX.Text,
                    PositionY2D = txt2DPositionY.Text,
                    TrayNo = Convert.ToInt32(txtTrayNo.Text),
                    JudgeStatus = SetJudgeParameter(),
                    UsesIten = SetRadioButtonItemSW()
                };

                DataLaser dataLaser = new DataLaser
                {
                    PartNo = txtPartNo.Text,
                    FnoNo = comboFNO.Text,
                    Power = txtPower.Text,
                    Speed = txtSpeed.Text
                };

                // 呼叫服務
                ResponseModel ResponseModel = partNoService.AddPartNoAndLaser(dataPartNo, dataLaser);
                if (ResponseModel.Status == StatusEnum.Ok) { DefaultGridViewValue(); }
                MessageBox.Show(ResponseModel.ResponseMsg);

                AddRecChangeMethod("料號新增");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception錯誤");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 驗證
                if (!CheckValueEmpty()) { return; }

                // 資料準備
                DataPartNo dataPartNo = new DataPartNo
                {
                    EditUser = GlobalParameter.UserName,
                    PartNo = txtPartNo.Text,
                    PieceSizeX = txtPieceX.Text,
                    PieceSizeY = txtPieceY.Text,
                    PositionX2D = txt2DPositionX.Text,
                    PositionY2D = txt2DPositionY.Text,
                    TrayNo = Convert.ToInt32(txtTrayNo.Text),
                    JudgeStatus = SetJudgeParameter(),
                    UsesIten = SetRadioButtonItemSW()
                };

                // 呼叫服務
                ResponseModel ResponseModel = partNoService.UpdatePartNoAndLaser(dataPartNo, new DataLaser
                {
                    PartNo = txtPartNo.Text,
                    FnoNo = comboFNO.Text,
                    Power = txtPower.Text,
                    Speed = txtSpeed.Text
                });
                if (ResponseModel.Status == StatusEnum.Ok) { DefaultGridViewValue(); }
                MessageBox.Show(ResponseModel.ResponseMsg);

                AddRecChangeMethod("料號更新");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception錯誤");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 驗證
                if (string.IsNullOrEmpty(txtPartNo.Text))
                {
                    MessageBox.Show("請輸入料號。");
                    return;
                }

                if (MessageBox.Show("刪除: " + txtPartNo.Text, "Delete", MessageBoxButtons.YesNo,
                   MessageBoxIcon.Information).Equals(DialogResult.Yes))
                {
                    // 呼叫服務
                    ResponseModel ResponseModel = partNoService.DeletePartNoAndLaser(txtPartNo.Text);
                    if (ResponseModel.Status == StatusEnum.Ok) { DefaultGridViewValue(); }
                    MessageBox.Show(ResponseModel.ResponseMsg);

                    // 預設值
                    txtPartNo.Clear();
                    comboFNO.Text = "0000";
                    txtPieceX.Text = "80.000";
                    txtPieceY.Text = "80.000";
                    txt2DPositionX.Text = "00.000";
                    txt2DPositionY.Text = "00.000";
                    txtTrayNo.Text = "0";

                    AddRecChangeMethod("料號刪除");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception錯誤");
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
            this.Close();
        }

        /// <summary>
        /// 開啟料盤查詢，取得料盤編號
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTraySelect_Click(object sender, EventArgs e)
        {
            List<string> list = TraySearch.GetTrayValue();
            txtTrayNo.Text = list[0];
            lblTrayName.Text = list[1];
        }
    }
}
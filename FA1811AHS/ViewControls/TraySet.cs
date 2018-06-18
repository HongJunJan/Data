using FA1811AHS.Repository;
using FA1811AHS.Service;
using FA1811AHS.Shared;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace FA1811AHS
{
    public partial class TraySet : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IRecChangeService recChangeService = new RecChangeService();
        private IDataTrayService trayService = new DataTrayService();

        public TraySet()
        {
            InitializeComponent();
            SetDefultEvent();
            ButtonProperty.SetProperty(btnSelectTrayNo, true);
            DataGridviewProperty.SetProperty(dataGridView1);
            GetDefaultTrayNo();
        }

        #region 區域變數、事件

        // 料盤最大編號
        private int trayNoMax;

        /// <summary>
        /// 預設事件設定
        /// </summary>
        private void SetDefultEvent()
        {
            // TextBoxEvent
            txtTrayCenter.Click += TextBoxEvent.ClickEvent;
            txtTrayLength.Click += TextBoxEvent.ClickEvent;
            txtThickness.Click += TextBoxEvent.ClickEvent;
            txtDivideNoX.Click += TextBoxEvent.ClickEvent;
            txtDivideNoY.Click += TextBoxEvent.ClickEvent;
            txtDividePitchX.Click += TextBoxEvent.ClickEvent;
            txtDividePitchY.Click += TextBoxEvent.ClickEvent;
            txtPieceCenterX.Click += TextBoxEvent.ClickEvent;
            txtPieceCenterY.Click += TextBoxEvent.ClickEvent;
            txtTrayOffsetX.Click += TextBoxEvent.ClickForNegative;
            txtTrayOffsetY.Click += TextBoxEvent.ClickForNegative;
            txtTrayCenter.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txtTrayLength.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txtThickness.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txtDividePitchX.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txtDividePitchY.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txtPieceCenterX.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txtPieceCenterY.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txtTrayOffsetX.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
            txtTrayOffsetY.TextChanged += TextBoxEvent.TextChangedForPartNoAndTray;
        }

        #endregion 區域變數、事件

        #region 私有方法

        /// <summary>
        /// 預設值，取得料盤資料TO DataGridView
        /// </summary>
        private void GetDefaultTrayNo()
        {
            try
            {
                ResponseModel ResponseModel = trayService.GetAllData();
                dataGridView1.DataSource = ResponseModel.DataTrayList;
                trayNoMax = ResponseModel.DataTrayList[0].TrayNo;

                // 隱藏欄位
                List<string> list = new List<string> {
                    {"CreateDate"},{"UpdateDate"},{"EditUser"},{"DivideNoX"},{"DivideNoY"},
                    {"DividePitchX"},{"DividePitchY"},{"PieceCenterX"},{"PieceCenterY"},{"TrayThickness"},
                    {"TrayCenter"},{"TrayLength"},{"TrayOffsetX"},{"TrayOffsetY"},{"TrayNo"} };
                foreach (var item in list)
                {
                    dataGridView1.Columns[item].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 驗證欄位空值
        /// 正確:true 、 錯誤:false
        /// </summary>
        /// <returns>bool值</returns>
        private bool CheckValueEmpty()
        {
            bool check = true;
            List<string> parameter = new List<string>
            {
                {txtTrayName.Text } ,{ txtTrayCenter.Text} ,{ txtTrayLength.Text } ,{ txtThickness.Text}
                ,{ txtDivideNoX.Text} ,{ txtDivideNoY.Text} ,{ txtDividePitchX.Text},{ txtDividePitchY.Text}
                ,{ txtPieceCenterX.Text} ,{ txtPieceCenterY.Text} ,{ txtTrayOffsetX.Text},{ txtTrayOffsetY.Text}
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
        /// 選取DataGridView，顯示資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblTrayNo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString().Trim();
            txtTrayName.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim();
            txtDivideNoX.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim();
            txtDivideNoY.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString().Trim();
            txtDividePitchX.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString().Trim();
            txtDividePitchY.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString().Trim();
            txtPieceCenterX.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString().Trim();
            txtPieceCenterY.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString().Trim();
            txtThickness.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString().Trim();
            txtTrayCenter.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString().Trim();
            txtTrayLength.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString().Trim();
            txtTrayOffsetX.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString().Trim();
            txtTrayOffsetY.Text = dataGridView1.CurrentRow.Cells[15].Value.ToString().Trim();
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
        /// 查詢料盤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectTrayNo_Click(object sender, EventArgs e)
        {
            try
            {
                ResponseModel ResponseModel = trayService.GetLikeCondition(txtSelect.Text);
                dataGridView1.DataSource = ResponseModel.DataTrayList;
                dataGridView1.Enabled = ResponseModel.DataTrayList.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error");
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
                AddRecChangeMethod("料盤新增");

                // 先判斷筆數是否已達到最大限制，若要增加請更新config設定檔
                ResponseModel ResponseModel = trayService.GetAllData();
                int trayCounts = Convert.ToInt32(ConfigurationManager.AppSettings["Traylimit"]);
                if (ResponseModel.DataTrayList.Count > trayCounts)
                {
                    MessageBox.Show(TrayEnum.Error3.GetEnumDescription());
                    return;
                }

                // 驗證
                if (!CheckValueEmpty()) { return; }

                // 資料準備
                DataTray dataTray = new DataTray
                {
                    EditUser = GlobalParameter.UserName,
                    TrayNo = trayNoMax + 1,
                    TrayName = txtTrayName.Text,
                    DivideNoX = Convert.ToInt32(txtDivideNoX.Text),
                    DivideNoY = Convert.ToInt32(txtDivideNoY.Text),
                    DividePitchX = txtDividePitchX.Text,
                    DividePitchY = txtDividePitchY.Text,
                    PieceCenterX = txtPieceCenterX.Text,
                    PieceCenterY = txtPieceCenterY.Text,
                    TrayOffsetX = txtTrayOffsetX.Text,
                    TrayOffsetY = txtTrayOffsetY.Text,
                    TrayCenter = txtTrayCenter.Text,
                    TrayLength = txtTrayLength.Text,
                    TrayThickness = txtThickness.Text
                };

                // 呼叫料盤服務
                ResponseModel = trayService.AddData(dataTray);
                if (ResponseModel.Status == StatusEnum.Ok) { GetDefaultTrayNo(); }
                MessageBox.Show(ResponseModel.ResponseMsg);
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
                AddRecChangeMethod("料盤更新");

                // 驗證
                if (!CheckValueEmpty()) { return; }

                // 資料準備
                DataTray dataTray = new DataTray
                {
                    EditUser = GlobalParameter.UserName,
                    TrayNo = Convert.ToInt32(lblTrayNo.Text),
                    TrayName = txtTrayName.Text,
                    DivideNoX = Convert.ToInt32(txtDivideNoX.Text),
                    DivideNoY = Convert.ToInt32(txtDivideNoY.Text),
                    DividePitchX = txtDividePitchX.Text,
                    DividePitchY = txtDividePitchY.Text,
                    PieceCenterX = txtPieceCenterX.Text,
                    PieceCenterY = txtPieceCenterY.Text,
                    TrayOffsetX = txtTrayOffsetX.Text,
                    TrayOffsetY = txtTrayOffsetY.Text,
                    TrayCenter = txtTrayCenter.Text,
                    TrayLength = txtTrayLength.Text,
                    TrayThickness = txtThickness.Text
                };

                ResponseModel ResponseModel = trayService.UpdateData(dataTray);
                if (ResponseModel.Status == StatusEnum.Ok) { GetDefaultTrayNo(); }
                MessageBox.Show(ResponseModel.ResponseMsg);
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
                AddRecChangeMethod("料盤刪除");

                // 驗證
                if (string.IsNullOrEmpty(lblTrayNo.Text))
                {
                    MessageBox.Show("請選擇預刪除的料盤編號。");
                    return;
                }

                if (MessageBox.Show("刪除: " + lblTrayNo.Text, "Delete", MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information).Equals(DialogResult.Yes))
                {
                    ResponseModel ResponseModel = trayService.DeleteData(lblTrayNo.Text);
                    if (ResponseModel.Status == StatusEnum.Ok) { GetDefaultTrayNo(); }
                    MessageBox.Show(ResponseModel.ResponseMsg);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception錯誤");
                MessageBox.Show(ex.Message);
            }
        }
    }
}
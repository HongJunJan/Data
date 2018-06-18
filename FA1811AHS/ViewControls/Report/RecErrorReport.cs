using FA1811AHS.Repository;
using FA1811AHS.Service;
using FA1811AHS.Shared;
using NLog;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FA1811AHS
{
    public partial class RecErrorReport : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IRecErrorService recErrorService = new RecErrorService();

        public RecErrorReport()
        {
            InitializeComponent();
            ButtonProperty.SetProperty(btnSearch, true);
            DateTimeProprty.SetForMonthly(dateTimePicker1);
            DataGridviewProperty.SetProperty(dataGridView1);
            dataGridView1.DataSource = new List<RecError>();
            SetGridViewColumnName();
        }

        /// <summary>
        /// DataGridView欄位名稱
        /// </summary>
        private void SetGridViewColumnName()
        {
            dataGridView1.Columns[0].HeaderText = "結束日期";
            dataGridView1.Columns[1].HeaderText = "開始日期";
            dataGridView1.Columns[2].HeaderText = "內容";
            dataGridView1.Columns[3].HeaderText = "使用者";
            dataGridView1.Columns[4].HeaderText = "故障編號";
        }

        /// <summary>
        /// 關閉離開
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string endDateString = dateTimePicker1.Value.ToString("yyyy/MM/dd");
                DateTime startDate = Convert.ToDateTime(endDateString);
                endDateString = endDateString + " 23:59:59.999";
                DateTime endDate = Convert.ToDateTime(endDateString);
                ResponseModel responseModel = recErrorService.GetConditionData(startDate, endDate);
                if (responseModel.Status.Equals(StatusEnum.Ok))
                {
                    dataGridView1.DataSource = responseModel.RecErrorList;
                    SetGridViewColumnName();
                    dataGridView1.ClearSelection();
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
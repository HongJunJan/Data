using FA1811AHS.Repository;
using FA1811AHS.Service;
using FA1811AHS.Shared;
using NLog;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FA1811AHS
{
    public partial class RecChangeReport : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IRecChangeService recChangeService = new RecChangeService();

        public RecChangeReport()
        {
            InitializeComponent();
            ButtonProperty.SetProperty(btnSearch, true);
            DateTimeProprty.SetForMonthly(dateTimePicker1);
            DateTimeProprty.SetForMonthly(dateTimePicker2);
            DataGridviewProperty.SetProperty(dataGridView1);
            dataGridView1.DataSource = new List<RecChange>();
            SetGridViewColumnName();
        }

        /// <summary>
        /// DataGridView欄位名稱
        /// </summary>
        private void SetGridViewColumnName()
        {
            dataGridView1.Columns[0].HeaderText = "日期";
            dataGridView1.Columns[1].HeaderText = "內容";
            dataGridView1.Columns[2].HeaderText = "使用者";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                string endDateString = dateTimePicker2.Value.ToString("yyyy/MM/dd ") + "23:59:59.999";
                DateTime endDate = Convert.ToDateTime(endDateString);
                ResponseModel responseModel = recChangeService.GetConditionData(startDate, endDate);
                if (responseModel.Status.Equals(StatusEnum.Ok))
                {
                    dataGridView1.DataSource = responseModel.RecChangeList;
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

        /// <summary>
        /// 關閉離開
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
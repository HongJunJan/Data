using FA1811AHS.Repository;
using FA1811AHS.Service;
using FA1811AHS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FA1811AHS
{
    /// <summary>
    /// 料盤搜尋項目
    /// </summary>
    public partial class TraySearch : Form
    {
        private IDataTrayService trayService = new DataTrayService();
        private List<DataTray> dataTrayList = new List<DataTray>();
        private DataTray dataTray = new DataTray();
        private string noResult = string.Empty;
        private string nameResult = string.Empty;

        public TraySearch()
        {
            InitializeComponent();
            GetDefaultTrayNo();
        }

        /// <summary>
        /// 預設值，取得料盤資料 TO DataGridView
        /// </summary>
        private void GetDefaultTrayNo()
        {
            try
            {
                // 初始化控制項參數
                DataGridviewProperty.SetProperty(dataGridView1);
                DataGridviewProperty.SetProperty(dataGridView2);
                dataTrayList = trayService.GetAllData().DataTrayList;
                dataGridView1.DataSource = dataTrayList;

                // 更改欄位標題
                dataGridView1.Columns["TrayNo"].HeaderCell.Value = "料盤編號";
                dataGridView1.Columns["TrayName"].HeaderCell.Value = "料盤名稱";

                // 隱藏欄位
                List<string> list = new List<string>
                {
                    {"CreateDate"},{"UpdateDate"},{"EditUser"},{"DivideNoX"},{"DivideNoY"}
                    ,{"DividePitchX"},{"DividePitchY"},{"PieceCenterX"},{"PieceCenterY"}
                    ,{"TrayThickness"},{"TrayCenter"},{"TrayLength"},{"TrayOffsetX"},{"TrayOffsetY"}
                };
                foreach (var item in list) { dataGridView1.Columns[item].Visible = false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 選取GridView的資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.ColumnCount = 2;
            dataGridView2.Columns[0].Name = "項目";
            dataGridView2.Columns[1].Name = "參數";
            dataGridView2.Rows.Clear();
            if (e.RowIndex > -1)
            {
                int trayNo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value);
                dataTray = dataTrayList.Where(x => x.TrayNo == trayNo).FirstOrDefault();
                noResult = dataTray.TrayNo.ToString();
                nameResult = dataTray.TrayName;
                List<string[]> list = new List<string[]>
                {
                   new string[] { "TrayNo", dataTray.TrayNo.ToString() },
                   new string[] { "TrayName", dataTray.TrayName },
                   new string[] { "DivideNoX", dataTray.DivideNoX.ToString() },
                   new string[] { "DivideNoY", dataTray.DivideNoY.ToString() },
                   new string[] { "DividePitchX", dataTray.DividePitchX },
                   new string[] { "DividePitchY", dataTray.DividePitchY },
                   new string[] { "PieceCenterX", dataTray.PieceCenterX },
                   new string[] { "PieceCenterY", dataTray.PieceCenterY },
                   new string[] { "TrayThickness", dataTray.TrayThickness },
                   new string[] { "TrayCenter", dataTray.TrayCenter },
                   new string[] { "TrayLength", dataTray.TrayLength },
                   new string[] { "TrayOffsetX", dataTray.TrayOffsetX },
                   new string[] { "TrayOffsetY", dataTray.TrayOffsetY }
                };
                foreach (string[] rowArray in list)
                {
                    dataGridView2.Rows.Add(rowArray);
                }
                dataGridView2.ClearSelection();
            }
        }

        /// <summary>
        /// 取得Tray編號
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTrayValue()
        {
            List<string> result = new List<string>();
            TraySearch traySearch = new TraySearch();
            traySearch.ShowDialog();
            if (!string.IsNullOrEmpty(traySearch.noResult) && !string.IsNullOrEmpty(traySearch.nameResult))
            {
                result.Add(traySearch.noResult);
                result.Add(traySearch.nameResult);
            }
            return result;
        }

        /// <summary>
        /// 選擇確定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
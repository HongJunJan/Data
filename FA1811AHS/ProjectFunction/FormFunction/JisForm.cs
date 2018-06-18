using FA1811AHS.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FA1811AHS
{
    public partial class JisForm : Form
    {
        public JisForm()
        {
            InitializeComponent();
            SetJisData();
        }

        private JisNumDictionary jisNumDictionary = new JisNumDictionary();

        #region 私有方法、變數

        /// <summary>
        /// 參數值
        /// </summary>
        private string value = string.Empty;

        /// <summary>
        /// jis資料初始設定
        /// </summary>
        private void SetJisData()
        {
            dataGridView1.RowCount = 13;
            dataGridView1.ColumnCount = 17;
            dataGridView1[0, 0].Value = "Shift JIS";
            dataGridView1[0, 1].Value = "813F";
            dataGridView1[0, 2].Value = "814F";
            dataGridView1[0, 3].Value = "815F";
            dataGridView1[0, 4].Value = "816F";
            dataGridView1[0, 5].Value = "8180";
            dataGridView1[0, 6].Value = "8190";
            dataGridView1[0, 7].Value = "819E";
            dataGridView1[0, 8].Value = "81AE";
            dataGridView1[0, 9].Value = "81BE";
            dataGridView1[0, 10].Value = "81CE";
            dataGridView1[0, 11].Value = "81DE";
            dataGridView1[0, 12].Value = "81FE";
            for (int i = 1; i < 17; i++) { dataGridView1[i, 0].Value = (i - 1).ToString("X1"); }
            for (int i = 2; i < 16; i++) { dataGridView1[i + 1, 1].Value = JisConverter((Convert.ToInt32("813F", 16) + i).ToString("X4")); }
            for (int i = 0; i < 16; i++) { dataGridView1[i + 1, 2].Value = JisConverter((Convert.ToInt32("814F", 16) + i).ToString("X4")); }
            for (int i = 0; i < 16; i++) { dataGridView1[i + 1, 3].Value = JisConverter((Convert.ToInt32("815F", 16) + i).ToString("X4")); }
            for (int i = 0; i < 16; i++) { dataGridView1[i + 1, 4].Value = JisConverter((Convert.ToInt32("816F", 16) + i).ToString("X4")); }
            for (int i = 0; i < 16; i++) { dataGridView1[i + 1, 5].Value = JisConverter((Convert.ToInt32("8180", 16) + i).ToString("X4")); }
            for (int i = 0; i < 15; i++) { dataGridView1[i + 1, 6].Value = JisConverter((Convert.ToInt32("8190", 16) + i).ToString("X4")); }
            for (int i = 1; i < 15; i++) { dataGridView1[i + 1, 7].Value = JisConverter((Convert.ToInt32("819E", 16) + i).ToString("X4")); }
            for (int i = 10; i < 16; i++) { dataGridView1[i + 1, 8].Value = JisConverter((Convert.ToInt32("81AE", 16) + i).ToString("X4")); }
            for (int i = 0; i < 16; i++)
            {
                if (i < 2 || i > 9) { dataGridView1[i + 1, 9].Value = JisConverter((Convert.ToInt32("81BE", 16) + i).ToString("X4")); }
            }

            for (int i = 0; i < 16; i++)
            {
                if (i == 0 || i > 11) { dataGridView1[i + 1, 10].Value = JisConverter((Convert.ToInt32("81CE", 16) + i).ToString("X4")); }
            }
            for (int i = 0; i < 11; i++) { dataGridView1[i + 1, 11].Value = JisConverter((Convert.ToInt32("81DE", 16) + i).ToString("X4")); }
            for (int i = 2; i < 10; i++) { dataGridView1[i + 1, 12].Value = JisConverter((Convert.ToInt32("81F0", 16) + i - 2).ToString("X4")); }
            foreach (DataGridViewRow _row in dataGridView1.Rows) { _row.Height = 35; }
            dataGridView1.Columns[0].Width = 80;
        }

        /// <summary>
        /// Jis轉換
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        private string JisConverter(string _str)
        {
            Encoding encoding = Encoding.GetEncoding("Shift-JIS");
            byte[] bytes = new byte[2];
            bytes[0] = Convert.ToByte(_str.Substring(0, 2), 16);
            bytes[1] = Convert.ToByte(_str.Substring(2, 2), 16);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 取得JIS RowNum
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetRowNum(int index)
        {
            string result = string.Empty;
            if (index != 0)
            {
                Dictionary<int, string> dictionary = jisNumDictionary.SetCustomizeItem();
                result = dictionary[index];
            }

            return result;
        }

        #endregion 私有方法、變數

        /// <summary>
        /// 取得選擇的jis代碼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string BaseValue = GetRowNum(e.RowIndex);
            if (!string.IsNullOrEmpty(BaseValue))
            {
                value = (Convert.ToInt32(BaseValue, 16) + e.ColumnIndex - 1).ToString("X4");
            }

            this.Close();
        }

        /// <summary>
        /// 文字選擇器
        /// 取得選擇的值(Jis格式)
        /// </summary>
        /// <returns>Jis代碼</returns>
        public static string GetTextBoxValue()
        {
            string result = string.Empty;
            JisForm jisForm = new JisForm();
            jisForm.ShowDialog();
            if (!string.IsNullOrEmpty(jisForm.value))
            {
                result = jisForm.value.Trim();
            }

            return result;
        }
    }
}
using System.Drawing;
using System.Windows.Forms;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// DataGridview設定 共用項目
    /// </summary>
    public static class DataGridviewProperty
    {
        /// <summary>
        /// DataGridview屬性設定
        /// </summary>
        /// <param name="dataGridview">物件</param>
        public static void SetProperty(DataGridView dataGridview)
        {
            dataGridview.DefaultCellStyle.Font = new Font("微軟正黑體", 10F);
            dataGridview.AllowUserToAddRows = false;
            dataGridview.AllowUserToDeleteRows = false;
            dataGridview.AllowUserToResizeColumns = false;
            dataGridview.MultiSelect = false;
            dataGridview.ReadOnly = true;
            dataGridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
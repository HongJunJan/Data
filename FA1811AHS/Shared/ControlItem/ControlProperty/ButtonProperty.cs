using System.Drawing;
using System.Windows.Forms;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// Button設定 共用項目
    /// </summary>
    public static class ButtonProperty
    {
        /// <summary>
        /// Button屬性設定，1.物件,2.啟用或不啟用狀態
        /// </summary>
        /// <param name="button">物件</param>
        /// <param name="enabled">啟用或不啟用</param>
        public static void SetProperty(Button button, bool enabled)
        {
            button.Font = new Font("微軟正黑體", 12F);
            if (enabled)
            {
                button.Enabled = true;
                button.BackColor = Color.RoyalBlue;
            }
            else
            {
                button.Enabled = false;
                button.BackColor = Color.Gray;
            }
        }
    }
}
using System;
using System.Windows.Forms;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// Label事件 共用項目
    /// </summary>
    public static class LabelEvent
    {
        /// <summary>
        /// 檢查參數是否在範圍內
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TextChangedForLaser(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            string result = string.Empty;
            double parameter = 999999;
            try
            {
                if (!string.IsNullOrEmpty(label.Text))
                {
                    switch (label.Name)
                    {
                        case "lblXoffset":
                        case "lblYoffset":
                            parameter = Convert.ToDouble(label.Text);
                            result = ValidateUtility.CheckParameter(parameter, 55.000, -55.000);
                            break;

                        default:
                            result = "檢查參數範圍，無元件代碼。";
                            label.Text = "0.000";
                            break;
                    }

                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("錯誤:" + result);
                        label.Text = "0.000";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("錯誤:" + ex.Message);
                label.Text = "0.000";
            }
        }
    }
}
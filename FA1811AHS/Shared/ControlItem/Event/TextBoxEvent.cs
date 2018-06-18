using System;
using System.Windows.Forms;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// TextBox事件 共用項目
    /// </summary>
    public static class TextBoxEvent
    {
        /// <summary>
        /// [小算盤功能]，允許負符號
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ClickForNegative(object sender, EventArgs e)
        {
            string value = Keyboard.GetTextBoxValue(true, true);
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(value))
            {
                switch (textBox.Name)
                {
                    default:
                        textBox.Text = Convert.ToDouble(value).ToString("f3");
                        break;
                }
            }
        }

        /// <summary>
        /// [小算盤功能]，不允許負符號
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ClickEvent(object sender, EventArgs e)
        {
            string value = Keyboard.GetTextBoxValue(true, false);
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(value))
            {
                switch (textBox.Name)
                {
                    case "txtFNO":
                        textBox.Text = string.Format("{0:0000}", Convert.ToDouble(value));
                        break;

                    case "txtPower":
                    case "txtSpeed":
                    case "txtDivideNoX":
                    case "txtDivideNoY":
                        textBox.Text = value;
                        break;

                    default:
                        textBox.Text = Convert.ToDouble(value).ToString("f3");
                        break;
                }
            }
        }

        /// <summary>
        /// 檢查料號與料盤參數是否在範圍內
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TextChangedForPartNoAndTray(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string result = string.Empty;
            double parameter = 99999;
            try
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    switch (textBox.Name)
                    {
                        case "txtPieceX":
                        case "txtPieceY":
                            parameter = Convert.ToDouble(textBox.Text);
                            result = ValidateUtility.CheckParameter(parameter, 80.100, 10.000);
                            break;

                        case "txt2DPositionX":
                        case "txt2DPositionY":
                        case "txtTrayOffsetX":
                        case "txtTrayOffsetY":
                            parameter = Convert.ToDouble(textBox.Text);
                            result = ValidateUtility.CheckParameter(parameter, 100.000, -100.000);
                            break;

                        case "txtTrayCenter":
                        case "txtTrayLength":
                            parameter = Convert.ToDouble(textBox.Text);
                            result = ValidateUtility.CheckParameter(parameter, 1000.000, 0.000);
                            break;

                        case "txtThickness":
                        case "txtDividePitchX":
                        case "txtDividePitchY":
                        case "txtPieceCenterX":
                        case "txtPieceCenterY":
                            parameter = Convert.ToDouble(textBox.Text);
                            result = ValidateUtility.CheckParameter(parameter, 100.000, 0.000);
                            break;

                        default:
                            result = "錯誤: 無元件代碼。";
                            break;
                    }

                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("錯誤:" + result);
                        textBox.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("錯誤:" + ex.Message);
                textBox.Clear();
            }
        }

        /// <summary>
        /// 檢查雷射參數是否在範圍內
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TextChangedForLaser(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string result = string.Empty;
            int intParameter = 99999;
            double parameter = 99999;
            try
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    switch (textBox.Name)
                    {
                        case "txtFNO":
                            intParameter = Convert.ToInt32(textBox.Text);
                            result = ValidateUtility.CheckParameter(intParameter, 2000, 0000);
                            break;

                        case "txtPower":
                            intParameter = Convert.ToInt32(textBox.Text);
                            result = ValidateUtility.CheckParameter(intParameter, 100, 10);
                            break;

                        case "txtSpeed":
                            intParameter = Convert.ToInt32(textBox.Text);
                            result = ValidateUtility.CheckParameter(intParameter, 2000, 1);
                            break;

                        case "txtXOffset":
                        case "txtYOffset":
                            parameter = Convert.ToDouble(textBox.Text);
                            result = ValidateUtility.CheckParameter(parameter, 55.000, -55.000);
                            break;

                        case "txtHeight":
                        case "txtWidth":
                            parameter = Convert.ToDouble(textBox.Text);
                            result = ValidateUtility.CheckParameter(parameter, 80.000, 5.000);
                            break;

                        default:
                            result = "錯誤: 無元件代碼。";
                            break;
                    }

                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("錯誤:" + result);
                        textBox.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("錯誤:" + ex.Message);
                textBox.Clear();
            }
        }
    }
}
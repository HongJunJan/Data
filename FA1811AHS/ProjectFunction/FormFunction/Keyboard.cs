using System;
using System.Windows.Forms;

namespace FA1811AHS
{
    /// <summary>
    /// 小鍵盤功能
    /// </summary>
    public partial class Keyboard : Form
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="usesPoint">是否使用小數點</param>
        public Keyboard(bool usesPoint)
        {
            InitializeComponent();
            SetEvent();

            if (usesPoint)
            {
                butPoint.Enabled = true;
                butPoint.Text = ".";
            }
            else
            {
                butPoint.Enabled = false;
            }
        }

        /// <summary>
        /// 參數值
        /// </summary>
        private string value = string.Empty;

        /// <summary>
        /// 控制字元
        /// </summary>
        private bool UsesNegative { get; set; }

        /// <summary>
        /// 預設事件
        /// </summary>
        private void SetEvent()
        {
            btn0.Click += BtnValueClick;
            btn1.Click += BtnValueClick;
            btn2.Click += BtnValueClick;
            btn3.Click += BtnValueClick;
            btn4.Click += BtnValueClick;
            btn5.Click += BtnValueClick;
            btn6.Click += BtnValueClick;
            btn7.Click += BtnValueClick;
            btn8.Click += BtnValueClick;
            btn9.Click += BtnValueClick;
            butPoint.Click += BtnValueClick;
            btnEnter.Click += KeyboardClick;
            btnEsc.Click += KeyboardClick;
            btnDelete.Click += KeyboardClick;
            txtValue.KeyDown += TextBoxKeyDown;
            txtValue.KeyPress += TextBoxKeyPress;
        }

        /// <summary>
        /// button_Click事件
        /// 數字 按鍵使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValueClick(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "." && txtValue.Text.Contains("."))
            {
                return;
            }
            else
            {
                txtValue.Text += ((Button)sender).Text;
            }
        }

        /// <summary>
        /// button_Click事件
        /// 新增、刪除、取消 按鍵使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyboardClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "btnEnter":
                    value = txtValue.Text;
                    this.Close();
                    break;

                case "btnEsc":
                    txtValue.Clear();
                    this.Close();
                    break;

                case "btnDelete":
                    if (!string.IsNullOrEmpty(txtValue.Text))
                    {
                        txtValue.Text = txtValue.Text.Substring(0, txtValue.Text.Length - 1);
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// TextBox_KeyDown事件
        /// 判斷按下確定或離開按鍵時，觸發事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double number;
                value = double.TryParse(txtValue.Text, out number).Equals(true) ? txtValue.Text : string.Empty;
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                value = string.Empty;
                this.Close();
            }
        }

        /// <summary>
        /// TextBox_KeyPress事件，
        /// 限制只能輸入數值、符號、一個小數點
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (UsesNegative)
            {
                if (!char.IsDigit(e.KeyChar)
                    && !char.IsControl(e.KeyChar)
                    && !e.KeyChar.ToString().Equals("-")
                    && !e.KeyChar.ToString().Equals("."))
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar)
                    && !char.IsControl(e.KeyChar)
                    && !e.KeyChar.ToString().Equals("."))
                {
                    e.Handled = true;
                }
            }

            if (e.KeyChar == '.' && txtValue.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 小鍵盤功能_取得小鍵盤的值，
        /// 是否啟用小數點、是否啟用負數符號
        /// </summary>
        /// <param name="usesPoint">是否啟用小數點</param>
        /// <param name="usesControl">是否啟用負數符號</param>
        /// <returns>數值</returns>
        public static string GetTextBoxValue(bool usesPoint, bool usesNegative)
        {
            string result = string.Empty;
            Keyboard keyboard = new Keyboard(usesPoint);
            keyboard.UsesNegative = usesNegative;
            keyboard.ShowDialog();
            if (!string.IsNullOrEmpty(keyboard.value))
            {
                result = keyboard.value.Trim();
            }

            return result;
        }
    }
}
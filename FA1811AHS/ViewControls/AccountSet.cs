using FA1811AHS.Repository;
using FA1811AHS.Service;
using FA1811AHS.Shared;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FA1811AHS
{
    public partial class AccountSet : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IRecChangeService recChangeService = new RecChangeService();
        private IAccountService accountService = new AccountService();
        private List<Account> accountList = new List<Account>();
        private Account account = new Account();
        private IbassDictionary<string> formStartDictionary = new FormStartDictionary();

        public AccountSet()
        {
            InitializeComponent();
            DefaultAccount();
            GlobalParameter.UserName = "James";
            toolStripbtnUpdate.Enabled = toolStripbtnDelete.Enabled = false;
        }

        #region 私有方法

        /// <summary>
        /// 取得所有帳戶人員，To dataGridView
        /// </summary>
        private void DefaultAccount()
        {
            try
            {
                // 初始化控制項參數
                txtNo.Clear();
                txtPassword.Clear();
                dataGridView1.Rows.Clear();
                DataGridviewProperty.SetProperty(dataGridView1);
                accountList = accountService.GetAllData().AccountList
                    .Where(x => !x.AcctNo.Contains("admin") && !x.AcctNo.Contains("James")).ToList();
                dataGridView1.ColumnCount = 1;
                dataGridView1.Columns[0].Name = "帳號";
                if (accountList.Count > 0)
                {
                    foreach (var item in accountList) { dataGridView1.Rows.Add(item.AcctNo); }
                }
                else
                {
                    dataGridView1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 驗證欄位參數是否有空白值，
        /// </summary>
        /// <returns></returns>
        private bool CheckValueEmpty()
        {
            string result = string.Empty;
            List<string> parameter = new List<string> { { txtNo.Text }, { txtPassword.Text } };
            result = ValidateUtility.CheckParameter(parameter);
            return ValidateUtility.DisplayMessage(result);
        }

        /// <summary>
        /// CheckBox項目，初始化參數
        /// </summary>
        private void DefultCheckBoxChecked(bool check)
        {
            chkbPartNo.Checked = check;
            chkbLaserSet.Checked = check;
            chkbRead2D.Checked = check;
            chkbSystem.Checked = check;
        }

        /// <summary>
        /// CheckBox項目，權限欄位資料設定
        /// 正確:權限字串、錯誤:空值
        /// </summary>
        /// <param name="dictionary">dictionary自訂資料</param>
        /// <returns></returns>
        private string SetCheckBoxItemLimit(Dictionary<string, string> dictionary)
        {
            string item = string.Empty;
            foreach (Control control in groupBox1.Controls)
            {
                if (control is CheckBox)
                {
                    if (((CheckBox)control).Checked)
                    {
                        item += dictionary[control.Name] + "、";
                    }
                }
            }

            if (!string.IsNullOrEmpty(item)) { item = item.TrimEnd('、'); }
            return item;
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
        /// DataGridView 選取欄位給予控制元件參數
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                toolStripbtnUpdate.Enabled = toolStripbtnDelete.Enabled = true;
                toolStripbtnAdd.Enabled = txtNo.Enabled = false;
                DefultCheckBoxChecked(false);
                txtNo.Text = dataGridView1.CurrentRow.Cells["帳號"].Value.ToString().Trim();
                Account account = accountList.Where(x => x.AcctNo == txtNo.Text).FirstOrDefault();
                List<string> list = account.Limit.Split('、').ToList();
                foreach (var item in list)
                {
                    if (item.Equals("1")) { chkbPartNo.Checked = true; }
                    if (item.Equals("2")) { chkbLaserSet.Checked = true; }
                    if (item.Equals("3")) { chkbRead2D.Checked = true; }
                    if (item.Equals("4")) { chkbSystem.Checked = true; }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Button清空欄位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNo.Clear();
            txtPassword.Clear();
            DefultCheckBoxChecked(false);
        }

        /// <summary>
        /// toolStripButton關閉離開
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 新增帳戶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> dictionary = formStartDictionary.SetCustomizeItem();
                if (CheckValueEmpty()) { return; }

                if (MessageBox.Show(string.Format("確定新增:{0}", txtNo.Text), "Add",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information).Equals(DialogResult.Yes))
                {
                    ResponseModel ResponseModel = accountService.AddData(new Account
                    {
                        EditUser = GlobalParameter.UserName,
                        AcctNo = txtNo.Text,
                        Password = txtPassword.Text,
                        Limit = SetCheckBoxItemLimit(dictionary)
                    });

                    if (ResponseModel.Status == StatusEnum.Ok) { DefaultAccount(); }
                    MessageBox.Show(ResponseModel.ResponseMsg);
                    AddRecChangeMethod("新增帳戶");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 更新帳戶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> dictionary = formStartDictionary.SetCustomizeItem();
                if (string.IsNullOrEmpty(txtNo.Text))
                {
                    MessageBox.Show("請選取預更新人員。");
                    return;
                }

                if (MessageBox.Show(string.Format("確定更新:{0}", txtNo.Text), "Update",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Information).Equals(DialogResult.Yes))
                {
                    // 準備資料
                    Account model = new Account
                    {
                        EditUser = GlobalParameter.UserName,
                        AcctNo = txtNo.Text,
                        Password = txtPassword.Text,
                        Limit = SetCheckBoxItemLimit(dictionary)
                    };
                    if (string.IsNullOrEmpty(txtPassword.Text)) { model.Password = this.account.Password; }

                    // 呼叫服務
                    ResponseModel ResponseModel = accountService.UpdateData(model);
                    if (ResponseModel.Status == StatusEnum.Ok) { DefaultAccount(); }
                    MessageBox.Show(ResponseModel.ResponseMsg);

                    // 使用者異動紀錄
                    AddRecChangeMethod("更新帳戶");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 刪除帳戶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNo.Text))
                {
                    MessageBox.Show("請選取預刪除人員。");
                    return;
                }

                if (MessageBox.Show(string.Format("確定刪除:{0}", txtNo.Text), "Delete", MessageBoxButtons.YesNo,
                   MessageBoxIcon.Information).Equals(DialogResult.Yes))
                {
                    ResponseModel ResponseModel = accountService.DeleteData(txtNo.Text);
                    if (ResponseModel.Status == StatusEnum.Ok) { DefaultAccount(); }
                    MessageBox.Show(ResponseModel.ResponseMsg);
                    AddRecChangeMethod("刪除帳戶");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error");
                MessageBox.Show(ex.Message);
            }
        }
    }
}
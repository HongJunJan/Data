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
    public partial class Login : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IAccountService AccountService = new AccountService();
        private IRecChangeService recChangeService = new RecChangeService();

        public Login(FormStart formStart)
        {
            InitializeComponent();
            GetDefaultAccount();
            this.FormStart = formStart;
        }

        /// <summary>
        /// 預設值，取得所有帳戶資料 To comboBox
        /// </summary>
        private void GetDefaultAccount()
        {
            ResponseModel responseModel = AccountService.GetAllData();
            cb_userNo.DataSource = responseModel.AccountList
                .Where(x => x.AcctNo != "admin").Select(x => x.AcctNo).ToList();
            cb_userNo.SelectedIndex = -1;
        }

        /// <summary>
        /// 主畫面
        /// </summary>
        private FormStart FormStart;

        /// <summary>
        /// 取消離開
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCanal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 確定，資料庫取得資料比對
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // 驗證欄位，是否有空值，若有空值會產生錯誤訊息
                List<string> parameter = new List<string> { { cb_userNo.Text }, { txtPasswd.Text } };
                string errorMsg = ValidateUtility.CheckParameter(parameter);
                if (ValidateUtility.DisplayMessage(errorMsg)) { return; }
                ResponseModel ResponseModel = AccountService.LoginAccount(cb_userNo.Text, txtPasswd.Text);
                if (ResponseModel.Status == StatusEnum.Ok)
                {
                    // 給予全域變數參數
                    GlobalParameter.UserName = ResponseModel.DataAccount.AcctNo;
                    GlobalParameter.AllowItem = ResponseModel.DataAccount.Limit;

                    // 異動紀錄
                    recChangeService.AddData(new RecChange
                    {
                        NowTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:ss:mm")),
                        Message = "Login登入",
                        UserName = ResponseModel.DataAccount.AcctNo
                    });

                    // 呼叫主Form開啟控制項
                    FormStart.OpenController();
                }

                this.Close();
                MessageBox.Show(ResponseModel.ResponseMsg);
            }
            catch (Exception ex)
            {
                this.Close();
                logger.Error(ex, "Error");
                MessageBox.Show(ex.Message);
            }
        }
    }
}
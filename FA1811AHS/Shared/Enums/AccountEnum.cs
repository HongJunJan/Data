using System.ComponentModel;

namespace FA1811AHS.Shared
{
    public enum AccountEnum
    {
        /// <summary>
        /// 此帳戶已存在
        /// </summary>
        [Description("此帳戶已存在。")]
        Error1 = 1,

        /// <summary>
        /// 此帳戶不存在
        /// </summary>
        [Description("此帳戶不存在。")]
        Error2 = 2,

        /// <summary>
        /// 登入成功
        /// </summary>
        [Description("登入成功。")]
        Login = 11,
    }
}
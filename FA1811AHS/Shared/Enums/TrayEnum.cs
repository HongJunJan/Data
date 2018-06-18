using System.ComponentModel;

namespace FA1811AHS.Shared
{
    public enum TrayEnum
    {
        /// <summary>
        /// 此料盤編號已存在
        /// </summary>
        [Description("此料盤編號已存在。")]
        Error1 = 1,

        /// <summary>
        /// 此料盤編號不存在
        /// </summary>
        [Description("此料盤編號不存在。")]
        Error2 = 2,

        /// <summary>
        /// 儲存筆數已達上限
        /// </summary>
        [Description("儲存筆數已達上限。")]
        Error3 = 3,
    }
}
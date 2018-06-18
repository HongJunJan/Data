using System.ComponentModel;

namespace FA1811AHS.Shared
{
    public enum PartNoEnum
    {
        /// <summary>
        /// 此料號已存在
        /// </summary>
        [Description("此料號已存在。")]
        Error1 = 1,

        /// <summary>
        /// 此料號不存在
        /// </summary>
        [Description("此料號不存在。")]
        Error2 = 2,
    }
}
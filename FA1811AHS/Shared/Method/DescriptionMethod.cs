using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// 取得描述項目
    /// </summary>
    public static class DescriptionMethod
    {
        /// <summary>
        /// 取得enum 描敘，
        /// 傳入條件: enum代碼，
        /// 回傳字串: 描述文字內容
        /// </summary>
        /// <param name="code">enum代碼</param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum code)
        {
            DescriptionAttribute attribute = code.GetType()
                 .GetField(code.ToString())
                 .GetCustomAttributes(typeof(DescriptionAttribute), false)
                 .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? code.ToString() : attribute.Description;
        }

        /// <summary>
        /// 取得enum 描敘
        /// 傳入條件: 1.字串代碼(例:"1")、2.enum類型(例:Type: ErrorCodeEnum)，
        /// 回傳字串: 描述文字內容
        /// </summary>
        /// <param name="code">代碼</param>
        /// <param name="type">enum類別名稱</param>
        /// <returns></returns>
        public static string GetEnumDescription(this string code, Type type)
        {
            string name = Enum.GetName(type, Convert.ToInt32(code));
            MemberInfo[] memInfo = type.GetMember(name);
            object[] attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            string description = ((DescriptionAttribute)attributes[0]).Description;
            return description != string.Empty ? description : string.Empty;
        }
    }
}
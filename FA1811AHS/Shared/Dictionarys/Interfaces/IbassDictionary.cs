using System.Collections.Generic;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// 自訂資料的泛型介面
    /// </summary>
    /// <typeparam name="T">屬性</typeparam>
    public interface IbassDictionary<T>
    {
        /// <summary>
        /// 自定義項目
        /// </summary>
        /// <returns>串列資料</returns>
        Dictionary<T, string> SetCustomizeItem();
    }
}
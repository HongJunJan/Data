using System.Collections.Generic;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// FormStart控制項目管理
    /// 若要新增新控制項目需要至此來建立新的控制項的key與value
    /// </summary>
    public class FormStartDictionary : IbassDictionary<string>
    {
        public Dictionary<string, string> SetCustomizeItem()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("chkbPartNo", "1");
            dictionary.Add("chkbLaserSet", "2");
            dictionary.Add("chkbRead2D", "3");
            dictionary.Add("chkbSystem", "4");
            return dictionary;
        }
    }
}
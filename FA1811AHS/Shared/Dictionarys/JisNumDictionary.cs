using System.Collections.Generic;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// 雷射JIS代碼Dictionary
    /// </summary>
    public class JisNumDictionary : IbassDictionary<int>
    {
        public Dictionary<int, string> SetCustomizeItem()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "813F");
            dictionary.Add(2, "814F");
            dictionary.Add(3, "815F");
            dictionary.Add(4, "816F");
            dictionary.Add(5, "8180");
            dictionary.Add(6, "8190");
            dictionary.Add(7, "819E");
            dictionary.Add(8, "81AE");
            dictionary.Add(9, "81BE");
            dictionary.Add(10, "81CE");
            dictionary.Add(11, "81DE");
            dictionary.Add(12, "81FE");
            return dictionary;
        }
    }
}
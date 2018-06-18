using System.Collections.Generic;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// 設備控制Dictionary: 2D讀碼、雷射，
    /// 0:全部使用、1:2D讀碼使用、2:雷射使用
    /// </summary>
    public class EquipmentDictionary : IbassDictionary<string>
    {
        public Dictionary<string, string> SetCustomizeItem()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("radiobtnAll", "0");
            dictionary.Add("radiobtn2DRead", "1");
            dictionary.Add("radiobtnLaser", "2");
            return dictionary;
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// 參數型態處理方法_共用項目
    /// </summary>
    public static class TypeMethod
    {
        /// <summary>
        /// 參數方向位置補數值"0"，
        /// 傳入條件: 1參數、2是否往左方向、3總長度，
        /// 回傳字串: 補數後的新字串值
        /// </summary>
        /// <param name="parameter">參數</param>
        /// <param name="isLeft">是否往左方向</param>
        /// <param name="totalWidth">總長度</param>
        /// <returns></returns>
        public static string SetParameterPad(string parameter, bool isLeft, int totalWidth)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(parameter))
            {
                result = isLeft.Equals(true) ?
                    parameter.PadLeft(totalWidth, '0') : parameter.PadRight(totalWidth, '0');
            }

            return result;
        }

        /// <summary>
        /// 參數格式轉換並檢查是否補[正符號]，
        /// 傳入條件:1.double串列參數、2.預轉換的格式(例:{0:000.000})
        /// </summary>
        /// <param name="list">double串列參數</param>
        /// <param name="format">預轉換的格式</param>
        /// <returns>轉換後的新串列資料</returns>
        public static List<string> SetFormatValue(List<double> list, string format)
        {
            List<string> results = new List<string>();
            foreach (double item in list)
            {
                string value = item >= 0 ? "+" + string.Format(format, item) : string.Format(format, item);
                results.Add(value);
            }

            return results;
        }

        /// <summary>
        /// xml格式內容編譯
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormtXmlDoc(string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.InnerXml = value;
                MemoryStream stream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.IndentChar = ' ';
                doc.WriteContentTo(writer);
                writer.Flush();
                stream.Flush();
                stream.Position = 0;
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch
            {
                return "XML format error";
            }
        }
    }
}
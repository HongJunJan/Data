using System.Collections.Generic;
using System.Windows.Forms;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// 驗證功能方法_共用項目
    /// </summary>
    public class ValidateUtility
    {
        /// <summary>
        /// 檢查參數是否有空白值錯誤，
        /// 傳入條件:預檢查參數，
        /// 回傳字串:正確:空值、錯誤:錯誤訊息
        /// </summary>
        /// <param name="parameter">預檢查參數</param>
        /// <returns>回傳錯誤訊息</returns>
        public static string CheckParameter(string parameter)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(parameter))
            {
                result = StatusEnum.ErrorMsg7.GetEnumDescription();
            }

            return result;
        }

        /// <summary>
        /// 檢查參數是否有空白值錯誤(多筆)，
        /// 傳入條件:預檢查參數(多筆)，
        /// 回傳字串:正確:空值、錯誤:錯誤訊息
        /// </summary>
        /// <param name="parameter">預檢查參數</param>
        /// <returns>回傳錯誤訊息</returns>
        public static string CheckParameter(List<string> parameter)
        {
            string result = string.Empty;
            foreach (string item in parameter)
            {
                if (string.IsNullOrEmpty(item))
                {
                    result = StatusEnum.ErrorMsg7.GetEnumDescription();
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 檢查欄位參數是否在範圍許可內(int)，
        /// 傳入條件:1.預檢查參數、2.限制最大值、3.限制最小值，
        /// 正確:空值、錯誤:錯誤訊息
        /// </summary>
        /// <param name="parameter">預檢查參數</param>
        /// <param name="max">限制最大值</param>
        /// <param name="min">限制最小值</param>
        /// <returns>回傳錯誤字串</returns>
        public static string CheckParameter(int parameter, int max, int min)
        {
            string result = string.Empty;
            if (parameter > max || parameter < min)
            {
                result = StatusEnum.ErrorMsg8.GetEnumDescription();
            }
            return result;
        }

        /// <summary>
        /// 檢查欄位參數是否在範圍許可內(Double)，
        /// 傳入條件:1.預檢查參數、2.限制最大值、3.限制最小值，
        /// 正確:空值、錯誤:錯誤訊息
        /// </summary>
        /// <param name="parameter">預檢查參數</param>
        /// <param name="max">限制最大值</param>
        /// <param name="min">限制最小值</param>
        /// <returns>回傳錯誤字串</returns>
        public static string CheckParameter(double parameter, double max, double min)
        {
            string result = string.Empty;
            if (parameter > max || parameter < min)
            {
                result = StatusEnum.ErrorMsg8.GetEnumDescription();
            }
            return result;
        }

        #region WinForm專用

        /// <summary>
        /// (WinForm專用)MessageBox元件顯示錯誤訊息，
        /// 傳入條件: 字串參數，
        /// 回傳結果(布林值): 有訊息:true、無訊息:false
        /// </summary>
        /// <param name="parameter">字串參數</param>
        /// <returns></returns>
        public static bool DisplayMessage(string parameter)
        {
            bool check = string.IsNullOrEmpty(parameter).Equals(true) ? false : true;
            if (check) { MessageBox.Show(parameter); }
            return check;
        }

        #endregion WinForm專用
    }
}
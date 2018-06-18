using System;
using System.Drawing;
using System.Windows.Forms;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// 日期屬性設定共用項目
    /// </summary>
    public class DateTimeProprty
    {
        /// <summary>
        /// 當月日期屬性設定
        /// </summary>
        /// <param name="dateTimePicker"></param>
        public static void SetForMonthly(DateTimePicker dateTimePicker)
        {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;
            int days = DateTime.DaysInMonth(year, month);
            dateTimePicker.MinDate = new DateTime(year, month, 1);
            dateTimePicker.MaxDate = new DateTime(year, month, days);

            // 自訂義格式
            dateTimePicker.CustomFormat = "yyyy/MM/dd";
            dateTimePicker.Format = DateTimePickerFormat.Custom;

            // 字型
            dateTimePicker.Font = new Font("微軟正黑體", 12F);
        }
    }
}
using System.ComponentModel;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// 執行狀態Enum
    /// </summary>
    public enum StatusEnum
    {
        /// <summary>
        /// 執行成功
        /// </summary>
        [Description("執行成功。")]
        Ok = 1,

        /// <summary>
        /// 執行失敗
        /// </summary>
        [Description("執行失敗。")]
        Error = 2,

        /// <summary>
        /// 新增錯誤、請檢查資料庫
        /// </summary>
        [Description("新增錯誤，請檢查資料來源。")]
        Error3 = 3,

        /// <summary>
        /// 更新錯誤、請檢查資料庫
        /// </summary>
        [Description("更新錯誤、請檢查資料來源。")]
        Error4 = 4,

        /// <summary>
        /// 刪除錯誤、請檢查資料庫
        /// </summary>
        [Description("刪除錯誤、請檢查資料來源。")]
        Error5 = 5,

        /// <summary>
        /// 取消刪除
        /// </summary>
        [Description("取消刪除。")]
        Error6 = 6,

        /// <summary>
        /// 欄位值不能為空，請輸入參數
        /// </summary>
        [Description("欄位值不能為空，請輸入資料。")]
        ErrorMsg7 = 7,

        /// <summary>
        /// 超出規定的數值
        /// </summary>
        [Description("超出規定的數值。")]
        ErrorMsg8 = 8,
    }
}
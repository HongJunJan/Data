namespace FA1811AHS.Service
{
    /// <summary>
    /// FormStart使用物件
    /// </summary>
    public class FormStartModel
    {
        public FormStartModel()
        {
            允許2D讀取Tag = false;
            允許2D寫入Tag = false;
            允許雷射打印Tag = false;
        }

        public string SHT遮光閥狀態 { get; set; }
        public string LSR雷射狀態 { get; set; }
        public bool 允許2D讀取Tag { get; set; }
        public bool 允許2D寫入Tag { get; set; }
        public bool 允許雷射打印Tag { get; set; }
    }
}
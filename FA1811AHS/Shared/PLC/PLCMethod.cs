using ConnPLCcommand;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// PLC功能項目
    /// </summary>
    public static class PLCMethod
    {
        /// <summary>
        /// PLC參數拆解設定(小數點處理)，
        /// 傳入條件: 1.參數(例:123.000)、2.儲存資料物件
        /// </summary>
        /// <param name="parameter">參數</param>
        /// <param name="date">儲存資料物件</param>
        public static void SetParameterSplitForPoint(string parameter, List<string> date)
        {
            if (!string.IsNullOrEmpty(parameter))
            {
                string[] 參數分割 = parameter.Split('.');
                string 小數點左邊 = 參數分割[0], 小數點右邊 = TypeMethod.SetParameterPad(參數分割[1], false, 3);
                string DM_1 = string.Empty, DM_2 = string.Empty, Error = string.Empty;
                PLCcommand.DWORD拆解(小數點左邊 + 小數點右邊, ref DM_1, ref DM_2, ref Error);
                date.Add(DM_1);
                date.Add(DM_2);
            }
            else
            {
                PLCMethod.SetDefultParameter("0", 2, date);
            }
        }

        /// <summary>
        /// PLC參數拆解設定，
        /// 傳入條件: 1.參數(例:3)、2.儲存資料物件
        /// </summary>
        /// <param name="parameter">參數</param>
        /// <param name="date">儲存資料物件</param>
        public static void SetParameterSplit(string parameter, List<string> date)
        {
            if (!string.IsNullOrEmpty(parameter))
            {
                string DM_1 = string.Empty, DM_2 = string.Empty, Error = string.Empty;
                PLCcommand.DWORD拆解(parameter, ref DM_1, ref DM_2, ref Error);
                date.Add(DM_1);
                date.Add(DM_2);
            }
            else
            {
                PLCMethod.SetDefultParameter("0", 2, date);
            }
        }

        /// <summary>
        /// PLC儲存預設基本參數，
        /// 傳入條件: 1.儲存的預設值、2.儲存筆數、3.儲存資料物件
        /// </summary>
        /// <param name="value">儲存的預設值</param>
        /// <param name="count">儲存筆數</param>
        /// <param name="date">儲存資料物件</param>
        public static void SetDefultParameter(string value, int count, List<string> date)
        {
            for (int i = 1; i <= count; i++)
            {
                date.Add(value);
            }
        }

        /// <summary>
        /// PLC單次寫入，
        /// 傳入條件: 1.DM點位、2.數值:0或1
        /// </summary>
        /// <param name="dm">DM</param>
        /// <param name="number">數值</param>
        public static void WriteSingle(int dm, int number)
        {
            PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Single, PLCcommand.PLC_IO.DM, dm, number, 0, null);
        }

        /// <summary>
        /// PLC連線
        /// </summary>
        public static void PLC_Connect()
        {
            GlobalData.PLC_D區起始位址 = 8000; GlobalData.PLC_D區結束位址 = 8400;
            GlobalData.PLC_DM_Scan_NUM = GlobalData.PLC_D區結束位址 - GlobalData.PLC_D區起始位址;
            GlobalData.DM = new string[GlobalData.PLC_D區結束位址];
            APP.全部陣列區域歸零_Initial();
            GlobalData.PLC_Remote_Port = 8501;
            string error = string.Empty;
            APP.Connect_Keyence_PLC(GlobalParameter.PLCIP, GlobalData.PLC_Remote_Port, ref error);
            if (GlobalData.PLC初始連線是否成功) { ThreadPool.QueueUserWorkItem(PLC循環); }
        }

        /// <summary>
        /// PLC循環
        /// </summary>
        /// <param name="sender"></param>
        private static void PLC循環(object sender)
        {
            while (true)
            {
                if (!GlobalData.PLC線路異常)
                {
                    Thread.Sleep(100);
                    PLCcommand.PLC_Read(PLCcommand.Cmd_Mode.Mode_Read_Multiple,
                        PLCcommand.PLC_IO.DM, GlobalData.PLC_D區起始位址,
                        (GlobalData.PLC_D區結束位址 - GlobalData.PLC_D區起始位址));
                }
            }
        }
    }
}
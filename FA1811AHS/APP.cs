using System;
using System.Net.Sockets;

namespace FA1811AHS
{
    public class APP
    {
        public static void Connect_Keyence_PLC(string PLC_IP, int PLC_port, ref string Error)
        {
            try
            {
                GlobalData.KeyencePLC_TCPClient = new TcpClient();
                GlobalData.KeyencePLC_TCPClient.Connect(PLC_IP, PLC_port);
                GlobalData.KeyencePLC_NetworkStream = GlobalData.KeyencePLC_TCPClient.GetStream();
                GlobalData.PLC初始連線是否成功 = true;
            }
            catch
            {
                Error = "PLC連線失敗!";
            }
        }

        public static void PLC放入對應的DM位置(string ReciveStr)
        {
            int ResponseLen = ReciveStr.Length / 5;
            if (ResponseLen == (GlobalData.PLC_D區結束位址 - GlobalData.PLC_D區起始位址))
            {
                int k = 0;
                for (int j = 0; j < GlobalData.PLC_DM_Scan_NUM; j++)
                {
                    GlobalData.DM[GlobalData.PLC_D區起始位址 + j + ((0) * GlobalData.PLC_DM_Scan_NUM)] = APP.紀錄字串轉int(ReciveStr.Substring(k, 5));
                    GlobalData.DM[GlobalData.PLC_D區起始位址 + j + ((0) * GlobalData.PLC_DM_Scan_NUM)] = 各種進位轉換(ReciveStr.Substring(k, 5), 10, 16);
                    var xxx = 各種進位轉換(ReciveStr.Substring(k, 5), 10, 16);
                    k = k + 5;
                }
            }
        }

        public static void PLC放入對應的R位置(string ReciveStr)
        {
            int ResponseLen = ReciveStr.Length;
            if (ResponseLen == (GlobalData.PLC_R區結束位址 - GlobalData.PLC_R區起始位址))
            {
                int k = 0;
                for (int j = 0; j < GlobalData.PLC_R_Scan_NUM; j++)
                {
                    GlobalData.R[GlobalData.PLC_R區起始位址 + j + ((0) * GlobalData.PLC_R_Scan_NUM)] = APP.紀錄字串轉int(ReciveStr.Substring(k, 1));
                    k = k + 1;
                }
            }
        }

        public static string 各種進位轉換(string value, int fromBase, int toBase)
        {
            int intValue = Convert.ToInt32(value, fromBase);
            return Convert.ToString(intValue, toBase);
        }

        public static string 紀錄字串轉int(string Str)
        {
            try
            {
                int Temp = Convert.ToInt32(Str);
                string S = Temp.ToString();
                return S;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static void 全部陣列區域歸零_Initial()
        {
            GlobalData.Err_Change_Word = new string[GlobalData.Err_故障碼總組數];
            GlobalData.Err_Change_Bit = new string[GlobalData.Err_故障碼總組數];
            GlobalData.Err_比對用_Bit = new string[GlobalData.Err_故障碼總組數];
            GlobalData.ALL_Error = new String[GlobalData.Err_故障碼總組數, 16, 5];

            for (int i = GlobalData.PLC_D區起始位址; i < GlobalData.PLC_D區結束位址; i++)
            {
                GlobalData.DM[i] = "0";
            }

            for (int i = GlobalData.PLC_R區起始位址; i < GlobalData.PLC_R區結束位址; i++)
            {
                GlobalData.R[i] = "0";
            }

            #region 故障碼

            for (int i = 0; i < GlobalData.Err_故障碼總組數; i++)
            {
                GlobalData.Err_Change_Word[i] = "0000";
                GlobalData.Err_Change_Bit[i] = "0000000000000000";
                GlobalData.Err_比對用_Bit[i] = "0000000000000000";
            }

            #endregion 故障碼
        }

        public static string 補足位數(string Value, int Num)
        {
            return Value.PadLeft(Num, '0');
        }

        public static string 右邊開始取字串(string s, int length)
        {
            length = Math.Max(length, 0);

            if (s.Length > length)
            {
                return s.Substring(s.Length - length, length);
            }
            else
            {
                return s;
            }
        }
    }
}
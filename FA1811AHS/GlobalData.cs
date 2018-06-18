using System;
using System.Data;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace FA1811AHS
{
    public class GlobalData
    {
        public static NetworkStream KeyencePLC_NetworkStream;

        public static TcpClient KeyencePLC_TCPClient { get; set; }
        public static UdpClient myUdp;
        public static IPEndPoint ipe;
        public static Ping mPing = new Ping();
        public static PingReply mPingReply;
        public static int PLC斷線恢復 { get; set; }
        public static int PLC通訊連線異常次數 { get; set; }
        public static bool PLC初始連線是否成功 { get; set; }
        public static bool PLC線路異常 { get; set; }
        public static int PLC線路異常字體放大Cycle { get; set; }
        public static int PLC_DM_Scan_NUM { get; set; }
        public static int PLC_R_Scan_NUM { get; set; }
        public static string[] DM = new string[PLC_D區結束位址];
        public static string[] R;

        public static int PLC_D區起始位址 { get; set; }
        public static int PLC_D區結束位址 { get; set; }
        public static int PLC_R區起始位址 { get; set; }
        public static int PLC_R區結束位址 { get; set; }

        public static string PLC網路IP { get; set; }
        public static int PLC_Remote_Port { get; set; }
        public static string[] Err_Change_Word;
        public static string[] Err_Change_Bit;
        public static string[] Err_比對用_Bit;
        public static String[,,] ALL_Error;
        public static DataTable dvTable_All_Error = new DataTable();
        public static string ListView_Show_OnCode_比對用;
        public static string ListView_Show_OnCode;
        public static int Err_故障碼總組數 = 10;
    }
}
namespace FA1811AHS.Shared
{
    /// <summary>
    /// PLC 點位 全域變數
    /// </summary>
    public static class DmTable
    {
        #region DI: PLC TO PC

        public static int DM_8100手動 = 8100;
        public static int DM_8101自動AUTOREADY = 8101;
        public static int DM_8102自動運轉中 = 8102;
        public static int DM_8103暫停 = 8103;
        public static int DM_8104停止 = 8104;
        public static int DM_8110結批 = 8110;
        public static int DM_8111允許手動 = 8111;
        public static int DM_8119放板完成 = 8119;
        public static int DM_8120CCD讀取2D碼 = 8120;
        public static int DM_81212D碼通知寫入 = 8121;
        public static int DM_8123雷射打印開始 = 8123;
        public static int DM_8142綠燈 = 8142;
        public static int DM_8143黃燈 = 8143;
        public static int DM_8144紅燈 = 8144;
        public static int DM_8170DWORD_PASS數量統計 = 8170;
        public static int DM_8172DWORD_4W數量統計 = 8172;
        public static int DM_8174DWORD_OPEN數量統計 = 8174;

        #endregion DI: PLC TO PC

        #region DO: PC TO PLC

        public static int DM_8206開啟2D功能 = 8206;
        public static int DM_8213手動2D通知 = 8213;
        public static int DM_8214手動打印通知 = 8214;
        public static int DM_8215RECIPE_OK自動模式 = 8215;
        public static int DM_8220CCD讀2D碼完成 = 8220;
        public static int DM_8221CCD讀2D檔失敗 = 8221;
        public static int DM_82222D碼寫入完成 = 8222;
        public static int DM_82232D碼寫入失敗 = 8223;
        public static int DM_8229雷射打印完畢 = 8229;
        public static int DM_8230雷射打印異常 = 8230;
        public static int DM_8231PASS = 8231;
        public static int DM_8232UNTEST = 8232;
        public static int DM_8233LO = 8233;
        public static int DM_8234LEAK = 8234;
        public static int DM_8235CCD = 8235;
        public static int DM_8236OPEN = 8236;
        public static int DM_8237LOW4W = 8237;
        public static int DM_8238VISUAL = 8238;
        public static int DM_8239FAIL10 = 8239;
        public static int DM_8240FAIL11 = 8240;
        public static int DM_8241FAIL12 = 8241;
        public static int DM_8242FAIL13 = 8242;
        public static int DM_8243SKIP = 8243;

        #endregion DO: PC TO PLC

        #region DO: PC TO PLC DWORD

        public static int DM_8300DWORD_板寬X = 8300;
        public static int DM_8302DWORD_板長Y = 8302;
        public static int DM_8304DWORD_板厚T = 8304;
        public static int DM_8306DWORD_2D碼位置X = 8306;
        public static int DM_8308DWORD_2D碼位置Y = 8308;
        public static int DM_8310DWORD_預留欄位 = 8310;
        public static int DM_8312DWORD_預留欄位 = 8312;
        public static int DM_8314DWORD_預留欄位 = 8314;
        public static int DM_8316DWORD_預留欄位 = 8316;
        public static int DM_8318DWORD_預留欄位 = 8318;
        public static int DM_8320DWORD_預留欄位 = 8320;
        public static int DM_8322DWORD_預留欄位 = 8322;
        public static int DM_8324DWORD_預留欄位 = 8324;
        public static int DM_8326DWORD_預留欄位 = 8326;
        public static int DM_8328DWORD_預留欄位 = 8328;
        public static int DM_8330DWORD_TrayNo料盤編號 = 8330;
        public static int DM_8332DWORD_DivideNoX軸個數 = 8332;
        public static int DM_8334DWORD_DivideNoY軸個數 = 8334;
        public static int DM_8336DWORD_DividePitchX分隔間距 = 8336;
        public static int DM_8338DWORD_DividePitchY分隔間距 = 8338;
        public static int DM_8340DWORD_PieceCenterX軸板中心 = 8340;
        public static int DM_8342DWORD_PieceCenterY軸板中心 = 8342;
        public static int DM_8344DWORD_TrayThickness料盤厚度 = 8344;
        public static int DM_8346DWORD_TrayCenter料盤中心 = 8346;
        public static int DM_8348DWORD_TrayLength料盤長度 = 8348;
        public static int DM_8350DWORD_預留欄位 = 8350;
        public static int DM_8352DWORD_預留欄位 = 8352;
        public static int DM_8354DWORD_預留欄位 = 8354;
        public static int DM_8356DWORD_預留欄位 = 8356;
        public static int DM_8358DWORD_預留欄位 = 8358;
        public static int DM_8360DWORD_TrayOffsetX料盤偏移 = 8360;
        public static int DM_8362DWORD_TrayOffsetY料盤偏移 = 8362;
        public static int DM_8364DWORD_預留欄位 = 8364;
        public static int DM_8366DWORD_預留欄位 = 8366;
        public static int DM_8368DWORD_預留欄位 = 8368;

        #endregion DO: PC TO PLC DWORD

        public static int DM_8000_Err_DM_故障碼起始 = 8000;
        public static int DM_8003_Err_DM_週邊故障碼起始 = 8003;
        public static int Err_HR_點位起始 = 700;
        public static int Err_HR_周邊點位起始 = 703;
    }
}
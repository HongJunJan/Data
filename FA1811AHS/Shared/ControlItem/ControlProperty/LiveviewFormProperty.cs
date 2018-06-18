using Keyence.AutoID.SDK;

namespace FA1811AHS.Shared
{
    /// <summary>
    /// liveviewForm設定(條碼讀取器) 共用項目
    /// </summary>
    public static class LiveviewFormProperty
    {
        /// <summary>
        /// 條碼讀取器連線，傳入條件:1.liveviewForm物件、2.readerAccessor物件、3.讀取器IP
        /// 回傳:成功:true、失敗:false
        /// </summary>
        /// <param name="liveviewForm">liveviewForm物件</param>
        /// <param name="readerAccessor">readerAccessor物件</param>
        /// <param name="ip">讀取器IP</param>
        /// <returns></returns>
        public static bool ReadConnect(LiveviewForm liveviewForm, ReaderAccessor readerAccessor, string ip)
        {
            liveviewForm.EndReceive();
            liveviewForm.IpAddress = readerAccessor.IpAddress = ip;
            liveviewForm.BeginReceive();
            return readerAccessor.Connect();
        }

        /// <summary>
        /// 條碼讀取器關閉，傳入條件:1.liveviewForm物件、2.readerAccessor物件
        /// </summary>
        /// <param name="liveviewForm">liveviewForm物件</param>
        /// <param name="readerAccessor">readerAccessor物件</param>
        public static void ReadClose(LiveviewForm liveviewForm, ReaderAccessor readerAccessor)
        {
            liveviewForm.EndReceive();
            readerAccessor.Dispose();
        }
    }
}
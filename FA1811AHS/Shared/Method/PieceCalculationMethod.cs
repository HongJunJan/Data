namespace FA1811AHS.Shared
{
    /// <summary>
    /// 板子計算方法項目
    /// </summary>
    public class PieceCalculationMethod
    {
        /// <summary>
        /// 板子點位計算方法(南亞、景碩)
        /// </summary>
        /// <param name="pieceRequestModel">板子物件(輸入請求)</param>
        /// <returns>板子物件結果</returns>
        public PieceResultModel PieceCalculation(PieceRequestModel pieceRequestModel)
        {
            double 板子X尺寸 = pieceRequestModel.板子X尺寸;
            double 板子Y尺寸 = pieceRequestModel.板子Y尺寸;
            double 板子X中心 = 板子X尺寸 / 2;
            double 板子Y中心 = 板子Y尺寸 / 2;
            double X位置 = 板子X中心 - pieceRequestModel.載台與雷射中心點差距;
            double X偏移位置 = pieceRequestModel.X偏移位置;
            X位置 = X位置 + X偏移位置;
            double Y位置 = 0;
            double Y偏移位置 = pieceRequestModel.Y偏移位置;
            Y位置 = Y位置 + Y偏移位置;
            return new PieceResultModel
            {
                X位置 = X位置,
                Y位置 = Y位置
            };
        }
    }

    /// <summary>
    /// 板子物件(輸入請求)
    /// </summary>
    public class PieceRequestModel
    {
        public double 板子X尺寸 { get; set; }
        public double 板子Y尺寸 { get; set; }
        public double X偏移位置 { get; set; }
        public double Y偏移位置 { get; set; }
        public int 載台與雷射中心點差距 { get; set; }
    }

    /// <summary>
    /// 板子物件(輸出結果)
    /// </summary>
    public class PieceResultModel
    {
        public double X位置 { get; set; }
        public double Y位置 { get; set; }
    }
}
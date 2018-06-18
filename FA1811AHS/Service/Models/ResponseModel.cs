using FA1811AHS.Repository;
using FA1811AHS.Shared;
using System.Collections.Generic;

namespace FA1811AHS.Service
{
    /// <summary>
    /// 資料回傳物件
    /// </summary>
    public class ResponseModel
    {
        public ResponseModel()
        {
            this.DataPartNoList = new List<DataPartNo>();
            this.DataTrayList = new List<DataTray>();
            this.PartNoJoinTrayAndLaserList = new List<PartNoJoinTrayAndLaser>();
            this.PartNoJoinLaserList = new List<PartNoJoinLaser>();
            this.DataJudgeList = new List<DataJudge>();
            this.DataLaserList = new List<DataLaser>();
            this.AccountList = new List<Account>();
            this.LaserFnoList = new List<LaserFNO>();
            this.ErrorDescriptionList = new List<ErrorDescription>();
            this.RecChangeList = new List<RecChange>();
            this.RecErrorList = new List<RecError>();
            this.Status = StatusEnum.Error;
            this.PartNoEnum = PartNoEnum.Error2;
            this.TrayEnum = TrayEnum.Error2;
            this.AccountEnum = AccountEnum.Error2;
            this.ResponseMsg = string.Empty;
        }

        /// <summary>
        /// 料號物件(多筆)
        /// </summary>
        public List<DataPartNo> DataPartNoList { get; set; }

        /// <summary>
        /// 料盤物件(多筆)
        /// </summary>
        public List<DataTray> DataTrayList { get; set; }

        /// <summary>
        /// 料盤物件
        /// </summary>
        public DataTray DataTray { get; set; }

        /// <summary>
        /// 料號join料盤與雷射物件(多筆)
        /// </summary>
        public List<PartNoJoinTrayAndLaser> PartNoJoinTrayAndLaserList { get; set; }

        /// <summary>
        /// 料號join料盤與雷射物件
        /// </summary>
        public PartNoJoinTrayAndLaser PartNoJoinTrayAndLaser { get; set; }

        /// <summary>
        /// 料號join雷射物件(多筆)
        /// </summary>
        public List<PartNoJoinLaser> PartNoJoinLaserList { get; set; }

        /// <summary>
        /// Judge判定資料物件(多筆)
        /// </summary>
        public List<DataJudge> DataJudgeList { get; set; }

        /// <summary>
        /// 雷射參數資料(多筆)
        /// </summary>
        public List<DataLaser> DataLaserList { get; set; }

        /// <summary>
        /// 帳戶物件(多筆)
        /// </summary>
        public List<Account> AccountList { get; set; }

        /// <summary>
        /// 帳戶物件
        /// </summary>
        public Account DataAccount { get; set; }

        /// <summary>
        /// 雷射程序資料物件(多筆)
        /// </summary>
        public List<LaserFNO> LaserFnoList { get; set; }

        /// <summary>
        /// 錯誤描述物件(多筆)
        /// </summary>
        public List<ErrorDescription> ErrorDescriptionList { get; set; }

        /// <summary>
        /// 異動紀錄物件(多筆)
        /// </summary>
        public List<RecChange> RecChangeList { get; set; }

        /// <summary>
        /// 故障錯誤紀錄物件(多筆)
        /// </summary>
        public List<RecError> RecErrorList { get; set; }

        /// <summary>
        /// 狀態Enum代碼，用來判斷執行狀況
        /// </summary>
        public StatusEnum Status { get; set; }

        /// <summary>
        /// 料號Enum代碼
        /// </summary>
        public PartNoEnum PartNoEnum { get; set; }

        /// <summary>
        /// 料盤Enum代碼
        /// </summary>
        public TrayEnum TrayEnum { get; set; }

        /// <summary>
        /// 帳戶Enum代碼
        /// </summary>
        public AccountEnum AccountEnum { get; set; }

        /// <summary>
        /// 回傳訊息
        /// </summary>
        public string ResponseMsg { get; set; }
    }
}
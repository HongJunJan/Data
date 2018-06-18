using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 雷射程序物件
    /// </summary>
    public class LaserFNO
    {
        /// <summary>
        /// 雷射程序編號
        /// </summary>
        [Column("LaserFNO_No")]
        public string LaserFnoNo { get; set; }
    }
}
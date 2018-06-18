using System.ComponentModel.DataAnnotations.Schema;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// Judge判定物件
    /// </summary>
    public class DataJudge
    {
        /// <summary>
        /// 編號
        /// </summary>
        [Column("Id")]
        public int Id { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }
    }
}
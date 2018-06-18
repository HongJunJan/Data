using Dapper;
using System.Data.SqlClient;

namespace FA1811AHS.Repository
{
    /// <summary>
    /// 批號資料層
    /// </summary>
    public class DataLotRepository : BaseConnetcion<DataLot>, IDataLotRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddData(DataLot model)
        {
            int result = 0;
            const string sql =
              @"INSERT INTO [FA1811-AHS].[dbo].[DataLot] (
                            [DataLot_CreateDate]
                           ,[DataLot_CreateUser]
                           ,[DataLot_No]
                           ,[DataLot_PartNo])
                OUTPUT Inserted.[Id]
                VALUES (
	                     @CreateDate
	                    ,@CreateUser
	                    ,@LotNo
	                    ,@PartNo)";
            object param = model;
            using (var sqlConnection = new SqlConnection(base.SqlConnetcion))
            {
                sqlConnection.Open();
                result = sqlConnection.Execute(sql, param);
            }

            return result;
        }
    }
}
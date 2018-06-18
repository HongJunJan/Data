using Dapper;
using System.Configuration;
using System.Data.SqlClient;

namespace FA1811AHS.Repository
{
    public class BaseConnetcion<T>
    {
        public BaseConnetcion()
        {
            SetConnetcion();
            SqlMapper.SetTypeMap(typeof(T), new ColumnAttributeTypeMapper<T>());
        }

        public string SqlConnetcion = string.Empty;

        private void SetConnetcion()
        {
            SqlConnectionStringBuilder sqlsb = new SqlConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            sqlsb.InitialCatalog = "FA1811-AHS";
            sqlsb.UserID = "sa";
            sqlsb.Password = "0989435065";
            SqlConnetcion = sqlsb.ConnectionString;
        }
    }
}
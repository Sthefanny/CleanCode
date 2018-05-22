using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CleanCode.LongMethods
{
    internal class TableReader
    {
        public DataTable GetDataTable(string tableName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FooFooConnectionString"].ToString();
            var connection = new SqlConnection(connectionString);
            var adapter = new SqlDataAdapter($"SELECT * FROM [{tableName}] ORDER BY id ASC", connection);
            var dataSet = new DataSet();
            adapter.Fill(dataSet, "FooFoo");
            var table = dataSet.Tables["FooFoo"];

            return table;
        }
    }
}
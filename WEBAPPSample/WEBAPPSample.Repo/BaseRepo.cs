using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
namespace WEBAPPSample.Repo
{
    public class BaseRepo
    {
        private readonly IConfiguration _config;

        public BaseRepo(IConfiguration config)
        {
            this._config = config;
            GetBlogs();
        }
        public void GetBlogs()
        {
            using (var connection = GetConnection())
            {
                SqlCommand command = new SqlCommand("select * from blogs");
                command.Connection = connection;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var x = reader[0].ToString();
                }

            }
        }
        public SqlConnection GetConnection()
        {
            var conn = this._config["Data:DefaultConnection:ConnectionString"];
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();
            return connection;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.DisconnectedLayer
{
    class Library
    {
        public DataSet ds { get; private set; }
        public DataTable Users { get; set; }
        public DataTable Books { get; set; }
        string connectionString = ConfigurationManager.ConnectionStrings["LibraryContext"].ConnectionString;

        public Library()
        {
            string sql = @"SELECT * FROM Users;
                           SELECT * FROM Books";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                ds = new DataSet();
                adapter.Fill(ds);

                Users = ds.Tables["Users"];
                Books = ds.Tables["Books"];
            }
        }




    }
}

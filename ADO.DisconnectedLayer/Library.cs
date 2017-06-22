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
        SqlDataAdapter adapter;
        DataSet library;
        DataTable users;
        DataTable books;
        string connectionString = ConfigurationManager.ConnectionStrings["LibraryContext"].ConnectionString;

        public void AddBook(string name, string author, string publisher, int year)
        {
            string sql = @"SELECT * FROM Books;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                library = new DataSet();
                adapter.Fill(library);
                books = library.Tables[0];
                DataRow row = books.NewRow();                
                    row["Name"] = name;
                    row["Author"] = author;
                    row["Publisher"] = publisher;
                    row["Year"] = year;
                books.Rows.Add(row);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(books);
            }
        }

        public void RemoveBook(int id)
        {
            string sql = @"SELECT * FROM Books;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                library = new DataSet();
                adapter.Fill(library);
                books = library.Tables[0];
                DataRow rowToDelete = books.AsEnumerable().FirstOrDefault(row => row.Field<int>("ID") == id);
                if (rowToDelete != null)
                    rowToDelete.Delete();
                SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
                adapter.Update(books);
            }
        }




    }
}

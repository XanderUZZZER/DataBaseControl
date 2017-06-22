using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ADO.ConnectedLayer
{
    static class Library
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["LibraryContext"].ConnectionString;

        public static void AddBook(string name, string author, string publisher, int year)
        {
            string sqlExpression = "INSERT INTO Books (Name, Author, Publisher, Year) VALUES (@Name, @Author, @Publisher, @Year)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter nameParam = new SqlParameter("@Name", name);
                SqlParameter authorParam = new SqlParameter("@Author", author);
                SqlParameter publisherParam = new SqlParameter("@Publisher", publisher);
                SqlParameter yearParam = new SqlParameter("@Year", year);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(authorParam);
                command.Parameters.Add(publisherParam);
                command.Parameters.Add(yearParam);

                command.ExecuteNonQuery();
            }
        }

        public static void RemoveBook(int bookId)
        {
            string sqlExpression = $"DELETE FROM Books WHERE ID = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@Id", bookId);
                command.Parameters.Add(idParam);

                int number = command.ExecuteNonQuery();
                if (number == 0)
                {
                    Console.WriteLine("There is no such book in library");
                }
            }
        }

        public static void UpdateBook(int bookId, string name, string author, string publisher, int year)
        {
            string sqlExpression = "UPDATE Books SET Name = @Name, Author = @Author, Publisher = @Publisher, Year = @Year WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter idParam = new SqlParameter("@ID", bookId);
                SqlParameter nameParam = new SqlParameter("@Name", name);
                SqlParameter authorParam = new SqlParameter("@Author", author);
                SqlParameter publisherParam = new SqlParameter("@Publisher", publisher);
                SqlParameter yearParam = new SqlParameter("@Year", year);
                command.Parameters.Add(idParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(authorParam);
                command.Parameters.Add(publisherParam);
                command.Parameters.Add(yearParam);

                int number = command.ExecuteNonQuery();
                if (number == 0)
                {
                    Console.WriteLine("There is no such book in library");
                }
            }
        }

        public static List<Book> AllBooksTakenByUser(int userId)
        {
            List<Book> books = new List<Book>();
            string sqlExpression = "SELECT * FROM Books WHERE books.UserId = @UserID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter userIdParam = new SqlParameter("@UserID", userId);
                command.Parameters.Add(userIdParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book
                            {
                                Id = (int)reader["ID"],
                                Name = (string)reader["Name"],
                                Author = (string)reader["Author"],
                                Publisher = (string)reader["Publisher"],
                                Year = (int)reader["Year"]
                            });
                        }
                    }
                }
            }
            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
            return books;
        }

        public static int CountOfBooksWithTheSameAuthor(string author)
        {
            object count = 0;
            string sqlExpression = "SELECT COUNT(ID) FROM books where Author = @Author";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter authorParam = new SqlParameter("@Author", author);
                command.Parameters.Add(authorParam);

                count = command.ExecuteScalar();
            }
            return (int)count;
        }

        public static void BookInfoByName(string name)
        {
            List<Book> books = new List<Book>();
            string sqlExpression = "SELECT * FROM Books WHERE books.Name = @Name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter nameParam = new SqlParameter("@Name", name);
                command.Parameters.Add(nameParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book
                            {
                                Id = (int)reader["ID"],
                                Name = (string)reader["Name"],
                                Author = (string)reader["Author"],
                                Publisher = (string)reader["Publisher"],
                                Year = (int)reader["Year"],
                                UserId = (int)reader["UserId"]
                            });
                        }
                        foreach (var book in books)
                        {
                            Console.WriteLine(book.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no such a book in library");
                    }
                }
            }

        }

        public static void TakeBook(int userId, int bookId)
        {
            string sqlExpression = "UPDATE Books SET UserId = @UserId WHERE ID = @BookId AND UserId IS NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter bookIdParam = new SqlParameter("@BookId", bookId);
                SqlParameter userIdParam = new SqlParameter("@UserId", userId);
                command.Parameters.Add(bookIdParam);
                command.Parameters.Add(userIdParam);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    Console.WriteLine("There is no such book in library or there is no such user");
                }
            }
        }

        public static void ReturnBook(int userId, int bookId)
        {
            string sqlExpression = "UPDATE Books SET UserId = NULL WHERE ID = @BookId AND UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter bookIdParam = new SqlParameter("@BookId", bookId);
                SqlParameter userIdParam = new SqlParameter("@UserId", userId);
                command.Parameters.Add(bookIdParam);
                command.Parameters.Add(userIdParam);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    Console.WriteLine("There is no such book in library or there is no such user");
                }
            }
        }

        public static void AddUser(string name, int age)
        {
            string sqlExpression = "INSERT INTO Users (Name, Age) VALUES (@Name, @Age)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter nameParam = new SqlParameter("@Name", name);
                SqlParameter ageParam = new SqlParameter("@Age", age);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(ageParam);

                command.ExecuteNonQuery();
            }
        }

        public static void RemoveUser(int userId)
        {            
            string sqlExpression = @"UPDATE Books SET UserId = NULL WHERE books.UserId = @UserId SELECT * FROM Books WHERE books.UserId = @UserId;
                                     Delete from Users where ID = @UserId;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter userIdParam = new SqlParameter("@UserId", userId);
                command.Parameters.Add(userIdParam);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    Console.WriteLine("There is no such user");
                }
            }
        }
    }
}

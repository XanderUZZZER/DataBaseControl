using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Entity
{
    static class Library
    {
        public static void AddBook(string name, string author, string publisher, int year)
        {
            using (var context = new LibraryContext())
            {

                context.Books.Add(new Book
                {
                    Name = name,
                    Author = author,
                    Publisher = publisher,
                    Year = year
                });
                context.SaveChanges();
            }               
        }

        public static void RemoveBook(int bookId)
        {
            using (var context = new LibraryContext())
            {
                var book = context.Books.Where(x => x.Id == bookId).FirstOrDefault();
                if (IsBook(book))
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
            }
        }

        public static void UpdateBook(int bookId, string name, string author, string publisher, int year)
        {
            using (var context = new LibraryContext())
            {
                Book book = context.Books.Where(x => x.Id == bookId).FirstOrDefault();
                if (IsBook(book))
                {
                    book.Name = name;
                    book.Author = author;
                    book.Publisher = publisher;
                    book.Year = year;
                    context.SaveChanges();
                }
            }
        }

        public static List<Book> AllBooksTakenByUser(int userId)
        {
            List<Book> books = new List<Book>();
            using (var context = new LibraryContext())
            {
                books = context.Books.Where(x => x.UserId == userId).ToList();
                var user = context.Users.Where(x => x.Id == userId).FirstOrDefault();
                if (IsUser(user))
                {
                    if (books.Count != 0)
                    {
                        Console.WriteLine($"{user.ToString()} has:");
                        foreach (var book in books)
                        {
                            Console.WriteLine(book.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{user.ToString()} has no books");
                    }
                }
            }
            return books;
        }

        public static int CountOfBooksWithTheSameAuthor(string author)
        {
            int count = 0;
            using (var context = new LibraryContext())
            {
                count = context.Books.Where(x => x.Author == author).Count();
            }
            return count;
        }

        public static void BookInfoByName(string name)
        {
            using (var context = new LibraryContext())
            {
                var books = context.Books.Where(x => x.Name == name).ToList();
                foreach (var book in books)
                    Console.WriteLine(book.ToString());
            }
        }

        public static void TakeBook(int userId, int bookId)
        {
            using (var context = new LibraryContext())
            {
                var book = context.Books.Where(x => x.Id == bookId).FirstOrDefault();
                var user = context.Users.Where(x => x.Id == userId).FirstOrDefault();
                if (IsUser(user))
                {
                    if (book.UserId == null)
                    {
                        book.UserId = userId;
                        context.SaveChanges();
                    }
                    else if (book.UserId == userId)
                    {
                        Console.WriteLine("You already have the book");
                    }
                    else
                    {
                        Console.WriteLine("At the moment the book is not in the library");
                    }
                }
            }
        }

        public static void ReturnBook(int userId, int bookId)
        {
            using (var context = new LibraryContext())
            {
                var user = context.Users.Where(x => x.Id == userId).FirstOrDefault();
                var book = context.Books.Where(x => x.Id == bookId).FirstOrDefault();
                if (IsUser(user))
                {
                    if (book.UserId == userId)
                    {
                        book.UserId = null;
                        context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("User can not return the book that he does not have");
                    }
                }
            }
        }

        public static void AddUser(string name, int age)
        {
            using (var context = new LibraryContext())
            {
                //context.Users.Add(new User
                //{
                //    Name = name,
                //    Age = age
                //});
                //context.SaveChanges();
                var nameParam = new SqlParameter("@Name", name);
                var ageParam = new SqlParameter("@Age", age);
                var sql = @"INSERT INTO Users (Name, Age) VALUES ({0}, {1})";
                context.Database.ExecuteSqlCommand(sql, name, age);
            }
        }

        public static void RemoveUser(int userId)
        {
            using (var context = new LibraryContext())
            {
                var user = context.Users.Where(x => x.Id == userId).FirstOrDefault();
                if (IsUser(user))
                {
                    var books = AllBooksTakenByUser(userId);
                    foreach (var book in books)
                    {
                        ReturnBook(userId, book.Id);
                    }
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }
        }

        private static bool IsUser(User user)
        {
            if (user == null)
            {
                Console.WriteLine("There is no such user in DB");
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool IsBook(Book book)
        {
            if (book == null)
            {
                Console.WriteLine("There is no such book in library");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

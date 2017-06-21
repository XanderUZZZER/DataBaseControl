using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    class LibraryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public void AddBook(string name, string author, string publisher, int year, int userId)
        {
            using (this)
            {
                this.Books.Add(new Book
                {
                    Name = name,
                    Author = author,
                    Publisher = publisher,
                    Year = year,
                    UserId = userId
                });
                this.SaveChanges();
            }
        }

        public void RemoveBook(int bookId)
        {
            using (this)
            {
                Book temp = this.Books.Where( x => x.Id == bookId).FirstOrDefault();
                this.Books.Remove(temp);
                this.SaveChanges();
            }
        }

        public void UpdateBook(string name, string author, string publisher, int year, int userId)
        {
        }

        public int CountOfBookWithSameAuthor()
        {
            int count = 0;
            using (this)
            {
                //count = this.Books.Where();
            }
            return count;
        }
    }
}

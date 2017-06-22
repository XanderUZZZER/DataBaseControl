using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.DisconnectedLayer
{
    class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public int? UserId { get; set; }

        public override string ToString()
        {
            return $"BookId: {Id}; Book: {Name}; Author: {Author}; Publisher: {Publisher}; Year: {Year}; UserId: {UserId};";
        }
    }
}

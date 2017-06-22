using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.DisconnectedLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Library lib = new Library();

            //Console.WriteLine(lib.Users.Rows.Count);
            //Console.WriteLine(lib.Users.Columns.Count);

            lib.AddBook("BDfsdf", "adfasdf", "asfgdfg", 9999);

            Console.WriteLine("___________");
            Console.ReadLine();
        }
    }
}

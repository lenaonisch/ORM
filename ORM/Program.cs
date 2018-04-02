using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            EFTest();
            Console.ReadLine();

            DapperTest();
            Console.ReadLine();
        }

        private static void DapperTest()
        {
            using (var sqlConnection = new System.Data.SqlClient.SqlConnection(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\DBBooks.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                string sqlAllAuthors =
                    @"SELECT A.Name, A.Surname, A.Country, BookCount from Authors as A,
	                  (select BA.AuthorID, COUNT(BA.BookID) as BookCount 
                        FROM BooksAuthors as BA
	                    GROUP BY BA.AuthorID) as T
                      WHERE T.AuthorID=A.ID";
                PrintAuthors("Dapper", sqlConnection.Query(sqlAllAuthors));

                Console.WriteLine("\nEnter surname to search for books:");
                string surname = Console.ReadLine();
                string sqlBooks =
                    @"SELECT Books.Name as Book, Authors.Surname, Authors.Name FROM BooksAuthors, Books, Authors 
                        WHERE BooksAuthors.BookID=Books.ID AND Authors.ID=BooksAuthors.AuthorID AND
	                    BooksAuthors.AuthorID in (SELECT ID FROM Authors WHERE Surname LIKE'%" + surname + "%')";

                PrintBooks("EF", surname, sqlConnection.Query(typeof(string), sqlBooks));
            }
        }

        private static void EFTest()
        {
            using (DBBooksEntities db = new DBBooksEntities())
            {

                PrintAuthors("EF",
                            from a in db.Authors
                            select new
                            {
                                Name = a.Name,
                                Surname = a.Surname,
                                Country = a.Country,
                                BookCount = a.Books.Count
                            });

                Console.WriteLine("\nEnter surname to search for books:");
                string surname = Console.ReadLine();
                IEnumerable<string> res = from b in db.Books.Where(b => b.Authors.Count(a => a.Surname.ToLower().Contains(surname.ToLower())) > 0)
                                          select b.Name;
                PrintBooks("EF", surname, res);
            }
        }

        private static void PrintAuthors(string method, IEnumerable<dynamic> authors)
        {
            Console.WriteLine(" {0}. ALL AUTHORS:", method);
            Console.WriteLine("Name\t\tSurname\t\tCountry\t\tWriten books");
            Console.WriteLine("------------------------------------------------");
            foreach (var t in authors)
            {
                Console.WriteLine("{0}\t\t{1,-15}{2}\t\t{3}", t.Name, t.Surname, t.Country, t.BookCount);
            }
        }

        private static void PrintBooks(string method, string surname, IEnumerable<dynamic> books)
        {
            
            Console.WriteLine("  {1}. *{0}*'s books are:", surname, method);
            Console.WriteLine("------------------------------------------------");

            
            if (books.Count() > 0)
            {
                foreach (string book in books)
                {
                    Console.WriteLine(book);
                }
            }
            else
            {
                Console.WriteLine("No books found!");
            }
        }
    }
}

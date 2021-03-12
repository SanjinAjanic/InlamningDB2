using InlamningDB2.Database;
using System;

namespace InlamningDB2
{
    internal static class Program
    {
       internal static void Main()
        {
            Seeder.Seed();
            
             var webbshop = new WebbShopAPI();
             var id = webbshop.Login("TestCustomer", "Codic2021");
            Console.WriteLine("1.Logga in Testuser");
            Console.WriteLine(id);

           

            var api = new WebbShopAPI();
            var listOfGenre = api.GetCategories();
            Console.WriteLine("2.Fråga efter kategorier:");
            foreach (var genere in listOfGenre)
            {
              
                Console.WriteLine($"{genere.Name}");
            }

            
            //var listOfHorrorBooks = api.GetCategory(1);// Bestämmer villken kategori det ska vara
            //foreach (var book in listOfHorrorBooks) // Listar ut titlarna av kategorin
            //{
            //    Console.WriteLine($"{book.Titel}");
            //}
            
            var book = api.GetBook(2);
            
            Console.WriteLine($"{book.Titel}");
            

        }
    }
}

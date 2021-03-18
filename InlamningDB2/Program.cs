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
            Console.WriteLine("Loggar in Admin");
            int result = webbshop.Login("Administrator", "CodicRulez");
            Console.WriteLine(result);
            Console.WriteLine("-------------------------");

            Console.WriteLine("Loggar ut andvändare");
            webbshop.Logout(3);
            Console.WriteLine("-------------------------");

            var list = webbshop.GetCategories();
            Console.WriteLine("Listar alla kategorier");
            foreach (var category in list)
            {

              Console.WriteLine($"{category.Name}");

            }
            Console.WriteLine("-------------------------");

            var listOfComedy = webbshop.GetCategories("com");
            Console.WriteLine("Listar komedi katergori efter sökning");
            foreach (var comedy in listOfComedy)
            {

                Console.WriteLine($"{comedy.Name}");

            }
            Console.WriteLine("-------------------------");

            var listOfGenre = webbshop.GetCategory(1);
            Console.WriteLine("Listar alla skräckböcker");
            foreach (var genere in listOfGenre)
            {

                Console.WriteLine($"{genere.Titel}");
            }
            Console.WriteLine("-------------------------");

            var listOfBooks = webbshop.GetAvailableBooks(3);
            Console.WriteLine("Listar alla tillgängliga sicion-fiction böcker baserat på vad man väljer för ID");
            foreach (var book in listOfBooks)
            {

                Console.WriteLine($"{book.Titel}");
            }
            Console.WriteLine("-------------------------");

            var boook = webbshop.GetBook(2);
            Console.WriteLine("Listar bok efter ID");

            Console.WriteLine($"{boook.Titel}");
            Console.WriteLine("-------------------------");

            listOfBooks = webbshop.GetBooks("Robot");
            Console.WriteLine("Listar bok efter sökord");
            foreach (var book in listOfBooks)
            {

                Console.WriteLine($"{book.Titel}");

            }
            Console.WriteLine("-------------------------");

            listOfBooks = webbshop.GetAuthors("Stephen King");
            Console.WriteLine("Listar bok efter författare");
            foreach (var book in listOfBooks)
            {

                Console.WriteLine($"{book.Titel} {book.Author}");

            }
            Console.WriteLine("-------------------------");

            webbshop.Login("Administrator", "CodicRulez");
            bool trueOrFalse = webbshop.BuyBook(3, 2);
            Console.WriteLine("Köper boken Doctor Sleep ");
            Console.WriteLine(trueOrFalse);
            Console.WriteLine("-------------------------");

            trueOrFalse = webbshop.Register("Sanjin","123","123"); // ändra andvändare för att se om det funkar!
            Console.WriteLine("Skapar en andvändare");
            Console.WriteLine(trueOrFalse);
            Console.WriteLine("-------------------------");

            trueOrFalse = webbshop.Addbook(3, 7, "SanjinsBok", "Sanjin", 2000, 2);
            Console.WriteLine("Lägger till en bok");
            Console.WriteLine(trueOrFalse);
            Console.WriteLine("-------------------------");

            webbshop.SetAmount(3, 2, 5);
            Console.WriteLine("Ändrar antalet tillgängliga böcker");
            Console.WriteLine("-------------------------");

















            //var list = webbshop.ListUsers(3);
            //foreach (var item in list)
            //{

            //  Console.WriteLine($"{item.Name}");

            //}


            // webbshop.SetAmount(3, 2, 5);


            // var id = webbshop.Login("TestCustomer", "Codic2021");
            //Console.WriteLine("1.Logga in Testuser");
            //Console.WriteLine(id);



            //var api = new WebbShopAPI();
            //var listOfGenre = api.GetCategories();
            //Console.WriteLine("2.Fråga efter kategorier:");
            //foreach (var genere in listOfGenre)
            //{

            //    Console.WriteLine($"{genere.Name}");
            //}


            ////var listOfHorrorBooks = api.GetCategory(1);// Bestämmer villken kategori det ska vara
            ////foreach (var book in listOfHorrorBooks) // Listar ut titlarna av kategorin
            ////{
            ////    Console.WriteLine($"{book.Titel}");
            ////}




        }
    }
}

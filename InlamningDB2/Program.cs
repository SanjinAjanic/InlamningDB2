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
            var id = webbshop.Login("Sanjinss", "Realmadrid");
            Console.WriteLine(id);

            id = webbshop.Login("Sanjin", "Hund");
            Console.WriteLine(id);

        }
    }
}

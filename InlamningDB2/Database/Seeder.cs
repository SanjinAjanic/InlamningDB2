using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InlamningDB2.Models;


namespace InlamningDB2.Database
{
    public static class Seeder
    {
        public static void Seed()
        {
            using(var db= new MyContext())
            {
                if (db.Categories.Count()==0)
                {
                    db.Categories.Add(new BookCategory { Name = "Horror" });
                    db.Categories.Add(new BookCategory { Name = "Comedy" });
                    db.Categories.Add(new BookCategory { Name = "Science Fiction" });
                    db.Categories.Add(new BookCategory { Name = "Romance" });
                    db.SaveChanges();
                    
                  
                }
                if (db.Book.Count()==0)
                {
                    db.Book.Add(new Books { Titel = "Cabal (Nightbreed)", Author = "Clive Barker", Price = 250, Amount = 3, Categoryid = 1 });
                    db.Book.Add(new Books { Titel = "The Shinng", Author = "Stephen King", Price = 200, Amount = 2, Categoryid = 1 });
                    db.Book.Add(new Books { Titel = "Doctor Sleep", Author = "Stephen King", Price = 200, Amount = 1, Categoryid = 1 });
                    db.Book.Add(new Books { Titel = "I Robot", Author = "Isaac Asimov", Price = 150, Amount = 4, Categoryid = 3 });
                    db.SaveChanges();
                }
                if (db.Users.Count()==0)
                {
                    db.Users.Add(new User { Name = "Administrator", Password = "CodicRulez", IsAdmin = true });
                    db.Users.Add(new User { Name = "TestCustomer", Password = "Codic2021", IsAdmin = false });
                    
                    db.SaveChanges();
                   
                }
            }

        }
    }
}
    

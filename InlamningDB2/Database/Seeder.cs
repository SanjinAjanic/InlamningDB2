using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
                    db.Categories.Add(new Models.Category { Name = "Horror" });
                    db.Categories.Add(new Models.Category { Name = "Comedy" });
                    db.SaveChanges();
                  
                }
            }

        }
    }
}
    

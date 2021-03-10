using InlamningDB2.Database;
using InlamningDB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InlamningDB2
{

    public class WebbShopAPI
    {
        private MyContext context = new MyContext();
        public int Login(string username, string password)
        {

            var user = context.Users.FirstOrDefault(u => u.Name == username && u.Password == password && u.IsActive);//SÖKNINGAR för överiga klasser detta är link-kod

            if (user != null)
                {
                    user.LastLogin = DateTime.Now;
                    user.SessionTimer = DateTime.Now;
                    context.Users.Update(user);
                    context.SaveChanges();
                    return user.Id; // kolla om det är null och retunera informationen
                }

                // TODO: Kontrollera med databasen om andvändaren finns
                // TODO: Implementera metoden
                return 0; // 0 = user dosen't exist
            }

            public void Logout(int id)
            {

            var user = context.Users.FirstOrDefault(u =>u.Id== id && u.SessionTimer > DateTime.Now.AddMinutes(-15));//SÖKNINGAR för överiga klasser
            if (user != null)
            {
               
                user.SessionTimer = DateTime.MinValue;
                context.Users.Update(user);
                context.SaveChanges();
             
            }

            // TODO: Implementera metoden
            // TODO: Sätt timer till noll

        }
        public  List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

    }

} 


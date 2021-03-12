using InlamningDB2.Database;
using InlamningDB2.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InlamningDB2
{

    public class WebbShopAPI
    {
        private static MyContext context = new MyContext();
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

            var user = context.Users.FirstOrDefault(u => u.Id == id && u.SessionTimer > DateTime.Now.AddMinutes(-15));//SÖKNINGAR för överiga klasser
            if (user != null)
            {

                user.SessionTimer = DateTime.MinValue;
                context.Users.Update(user);
                context.SaveChanges();

            }

            // TODO: Implementera metoden
            // TODO: Sätt timer till noll

        }
        public List<BookCategory> GetCategories()
        {
            return context.Categories.OrderBy(c => c.Name).ToList();
        }


        public List<BookCategory> GetCategories(string keyword)
        {
            return context.Categories.OrderBy(c => c.Name.Contains(keyword)).ToList();
        }
        public List<Books> GetCategory(int categoryId)
        {
            return context.Book.Where(b => b.CategoryId == categoryId).ToList();
        }
        public List<Books> GetAvailableBooks(int categoryId)
        {
            return context.Book.Where(b => b.Amount > 0 && b.CategoryId == categoryId).ToList();
        }
        public Books GetBook(int bookId)
        {
            return context.Book.FirstOrDefault(b => b.Id == bookId);
        }
        public List<Books> GetBooks(string keyword)
        {
            return context.Book.Where(b => b.Titel.Contains(keyword)).ToList();
        }
        public List<Books> GetAuthors(string keyword)
        {
            return context.Book.Where(b => b.Author == keyword).ToList();
        }
        public bool Register(string name, string password, string passwordVerify)
        {
            var user = context.Users.FirstOrDefault(u => u.Name == name && u.Password == password);
            if (user == null && passwordVerify == password)
            {
               context.Users.Add(new User { Name = name , Password = password, IsAdmin = false });

               return true;
            }
            return false;
        }
    }
}


﻿using InlamningDB2.Database;
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
        /// <summary>
        /// Loggar ut andvändare och uppdaterar SessionTimer till minValue
        /// </summary>
        /// <param name="id"></param>
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
        /// <summary>
        /// Sortera kategorierna i en lista
        /// </summary>
        /// <returns></returns>
        public List<BookCategory> GetCategories()
        {
            return context.Categories.OrderBy(c => c.Name).ToList();
        }

        /// <summary>
        /// Tar emot ett sökord
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<BookCategory> GetCategories(string keyword)
        {
            return context.Categories.Where(c => c.Name.Contains(keyword)).OrderBy(c => c.Name).ToList();
        }
        /// <summary>
        /// En lista med böcker baserat på kategori
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Books> GetCategory(int categoryId)
        {
            return context.Book.Where(b => b.CategoryId == categoryId).ToList();
        }
        /// <summary>
        /// Retunerar alla tillgängliga böcker i den kategorin man väljer
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Books> GetAvailableBooks(int categoryId)
        {
            return context.Book.Where(b => b.Amount > 0 && b.CategoryId == categoryId).ToList();
        }
        /// <summary>
        /// retunerar en bok baserat på ID
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Books GetBook(int bookId)
        {
            return context.Book.FirstOrDefault(b => b.Id == bookId);
        }
        /// <summary>
        /// Bokens tiltel med sökord
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Books> GetBooks(string keyword)
        {
            return context.Book.Where(b => b.Titel.Contains(keyword)).ToList();
        }
        /// <summary>
        /// bokens författare med sökord
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Books> GetAuthors(string keyword)
        {
            return context.Book.Where(b => b.Author == keyword).ToList();
        }
        /// <summary>
        /// koperar över den solda boken till köpta böcker
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public bool BuyBook(int userId, int bookId)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            var book = context.Book.FirstOrDefault(b => b.Id == bookId);
            if (user != null && user.SessionTimer > DateTime.Now.AddMinutes(-15) && user.IsActive)
            {
                if (book.Amount > 0)
                {
                    context.SooldBook.Add(new Soldbooks
                    {
                        Titel = book.Titel,
                        Author = book.Author,
                        CategoryId = book.CategoryId,
                        Price = book.Price,
                        PurchasedDate = DateTime.Now,
                        User = user
                    });
                    book.Amount--;
                    context.SaveChanges();
                    return true;
                }
                

            }
            return false;
        }

        /// <summary>
        /// retunerar om andvändaren är online och sätter sessionTimern till nu
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string Ping(int userId)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null && user.SessionTimer > DateTime.Now.AddMinutes(-15))
            {
                user.SessionTimer = DateTime.Now;
                context.Update(user);
                context.SaveChanges();
                return "Pong";
            }
            return string.Empty;
        }
        /// <summary>
        /// Skapar en andvändare
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="passwordVerify"></param>
        /// <returns></returns>
        public bool Register(string name, string password, string passwordVerify)
        {
            var user = context.Users.FirstOrDefault(u => u.Name == name && u.Password == password);
            if (user == null && passwordVerify.Equals(password))
            {
                context.Users.Add(new User { Name = name, Password = password, IsAdmin = false, IsActive = true });
                context.SaveChanges();

                return true;
            }
            return false;
        }
        /// <summary>
        /// Lägger till bok och ökar amount
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="bookId"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="price"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool Addbook(int adminId, int bookId, string title, string author, int price, int amount)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == adminId);
            var book = context.Book.FirstOrDefault(b => b.Id == bookId);
            if (user != null && user.IsAdmin == true && user.SessionTimer > DateTime.Now.AddMinutes(-15))
            {
                if (book == null)
                {
                    context.Book.Add(new Books {
                        Titel = title,
                        Author = author,
                        Price = price,
                        Amount = amount,
                        Category = null
                    });
                    context.SaveChanges();
                    return true;
                }
                if (book!= null)
                {
                    book.Amount++;
                    context.Update(book);
                    context.SaveChanges();
                    return true;
                }
                
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="bookId"></param>
        /// <param name="amount"></param>
        public void SetAmount(int adminId, int bookId, int amount)
        {
            var admin = context.Users.FirstOrDefault(a => a.Id == adminId);
            var book = context.Book.FirstOrDefault(b => b.Id == bookId);
            if (admin!=null && admin.IsAdmin == true && admin.SessionTimer > DateTime.Now.AddMinutes(-15))
            {
                if (book != null)
                {
                    book.Amount = amount;
                    context.Update(book);
                    context.SaveChanges();
                }
            }
        }
        public List<User> ListUsers(int adminId)
        {
            var admin = context.Users.FirstOrDefault(a => a.Id == adminId);
            var userList = new List<User>();
            if (admin != null && admin.IsAdmin == true && admin.SessionTimer > DateTime.Now.AddMinutes(-15))
            {
                userList = context.Users.ToList();
                return userList;
            }
            return userList;
        }
        public List<User> FindUsers(int adminId, string keyword)
        {
            var admin = context.Users.FirstOrDefault(a => a.Id == adminId);
            var userList = new List<User>();
            if (admin != null && admin.IsAdmin == true && admin.SessionTimer > DateTime.Now.AddMinutes(-15))
            {
                userList = context.Users.Where(u => u.Name.Contains(keyword)).ToList();
                return userList;
            }
            return userList;
        }
        public bool UpdateBook(int adminId, int bookId, string title, string author, int price)
        {
            var admin = context.Users.FirstOrDefault(a => a.Id == adminId);
            var book = context.Book.FirstOrDefault(b => b.Id == bookId);
            if (admin != null && admin.IsAdmin == true && admin.SessionTimer > DateTime.Now.AddMinutes(-15)&& admin.IsActive == true)
            {
                
                if (book!=null)
                {
                    book.Titel = title;
                    book.Author = author;
                    book.Price = price;
                    context.Update(book);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

       public bool Deletebook(int adminId, int bookId)
        {
            var admin = context.Users.FirstOrDefault(a => a.Id == adminId);
            var book = context.Book.FirstOrDefault(b => b.Id == bookId);
            if (admin != null && admin.IsAdmin == true && admin.SessionTimer > DateTime.Now.AddMinutes(-15) && admin.IsActive == true)
            {
                if (book!=null)
                {

                    if (book.Amount > 1)
                    {
                        book.Amount--;
                        context.Update(book);
                        context.SaveChanges();
                        return true;
                    }
                    if (book.Amount == 1)
                    {
                        context.Remove(book);
                        context.SaveChanges();
                        return true;
                    }

                }
            }
            return false;
            
        }

        public bool AddCategory(int adminId, string name)
        {
            var admin = context.Users.FirstOrDefault(a => a.Id == adminId);
            var categoryName = context.Categories.FirstOrDefault(cn => cn.Name == name);
            if (admin != null && admin.IsAdmin == true && admin.SessionTimer > DateTime.Now.AddMinutes(-15) && admin.IsActive == true)
            {
                if (categoryName == null)
                {
                    context.Categories.Add(new BookCategory { Name = name});
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool AddBookToCategory(int adminId, int bookId, int categoryId)
        {
            var admin = context.Users.FirstOrDefault(a => a.Id == adminId);
            var book = context.Book.FirstOrDefault(b => b.Id == bookId);
            var category = context.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (admin != null && admin.IsAdmin == true && admin.SessionTimer > DateTime.Now.AddMinutes(-15) && admin.IsActive == true && category != null && book != null)
            {
                book.CategoryId = categoryId;
                context.Update(category);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        
        public bool UpdateCategory(int adminId, int categoryId, string name)
        {
            var admin = context.Users.FirstOrDefault(a => a.Id == adminId);
            var category = context.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (admin != null && admin.IsAdmin == true && admin.SessionTimer > DateTime.Now.AddMinutes(-15) && admin.IsActive == true && category != null && category.Name != name)
            {
                category.Name = name;
                context.Update(name);
                context.SaveChanges();
                return true;
            }

            return false;
        }
        /// <summary>
        /// Tar bort katergori ifall det inte finns kopplat till kategori
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool DeleteCategory(int adminId, int categoryId)
        {
            var admin = context.Users.FirstOrDefault(a => a.Id == adminId);
            var category = context.Categories.FirstOrDefault(c => c.Id == categoryId);
            var book = context.Book.FirstOrDefault(b => b.CategoryId == categoryId); 
            if (admin != null && admin.IsAdmin == true && admin.SessionTimer > DateTime.Now.AddMinutes(-15) && admin.IsActive == true && category != null)
            {
               
                if (book == null)
                {
                    context.Remove(category);
                    context.SaveChanges();
                    return true;
                }

            }
            return false;


        }
       
        /*
        public bool AddUser(int adminId, string name, string password)
        {
            var admin = context.Users.FirstOrDefault(a => a.Id == adminId);
            var user = context.Users.FirstOrDefault(u => u.Name == username && u.Password == );

        

        }
        */

    }
}


        
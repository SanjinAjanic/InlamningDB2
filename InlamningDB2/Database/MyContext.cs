using InlamningDB2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InlamningDB2.Database
{
    class MyContext: DbContext 
    {
        private const string DatabaseName = "WebbShopSanjinAjanic";
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        // TODO: Skapa DbSet för Users (customers)
        // TODO: Skapa DbSet för Category
        // TODO: Skapa DbSet för Books
        // TODO: Skapa DbSet för SoldBooks





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=true;database={DatabaseName}");
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_Shop.Models;

namespace Sport_Shop.Data
{
    public class Sport_ShopContext : DbContext
    {
        public Sport_ShopContext (DbContextOptions<Sport_ShopContext> options)
            : base(options)
        {
        }

        public DbSet<Sport_Shop.Models.Product> Product { get; set; }
        public DbSet<Sport_Shop.Models.Category> Category { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}

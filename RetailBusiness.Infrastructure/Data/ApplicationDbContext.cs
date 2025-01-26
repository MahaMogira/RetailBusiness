using Microsoft.EntityFrameworkCore;
using RetailBusiness.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailBusiness.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entities using Fluent API (optional, if needed)
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(p => p.CategoryId)
                       .HasDatabaseName("IX_Product_CategoryId")
                      .IsUnique(false); // This is fine if the index is not unique

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
            });

            // Seed the database with initial data
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Seeds initial data into the database.
        /// </summary>
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Books" },
                new Category { Id = 3, Name = "Clothing" }
            );

            // Seed products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Smartphone", Price = 699.99m, CategoryId = 1 },
                new Product { Id = 2, Name = "Laptop", Price = 1299.99m, CategoryId = 1 },
                new Product { Id = 3, Name = "Fiction Book", Price = 19.99m, CategoryId = 2 },
                new Product { Id = 4, Name = "T-Shirt", Price = 15.99m, CategoryId = 3 }
            );
        }
    }
}

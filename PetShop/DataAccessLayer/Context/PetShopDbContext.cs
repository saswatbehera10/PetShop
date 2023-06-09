﻿using Microsoft.EntityFrameworkCore;
using PetShop.DataAccessLayer.Entities;
using static System.Net.WebRequestMethods;

namespace PetShop.DataAccessLayer.Context
{
    public class PetShopDbContext : DbContext
    {
        public PetShopDbContext(DbContextOptions<PetShopDbContext> options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Pet entity configuration
            /*modelBuilder.Entity<Pet>().HasData(
                new Pet()
                {
                    PetID = 44,
                    Name = "harry",
                    Species = "dog",
                    Age = 20,
                    Price = 20,
                    ImgUrl = "https://static.businessinsider.com/image/5484d9d1eab8ea3017b17e29/image.jpg"
                },
                new Pet()
                {
                    PetID = 10,
                    Name = "hooooarry",
                    Species = "cat",
                    Age = 2,
                    Price = 220,
                    ImgUrl = "https://wallpaperaccess.com/full/275808.jpg"
                },
                new Pet()
                {
                    PetID = 5,
                    Name = "haroooory",
                    Species = "dog",
                    Age = 201,
                    Price = 220,
                    ImgUrl = "https://scx2.b-cdn.net/gfx/news/hires/2018/2-dog.jpg"
                });

            // User entity configuration
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    UserID = 1,
                    Name = "Alex",
                    Email = "alex@gmail.com",
                    Password = "password",
                   // PasswordHash = "String",
                    Phone = "123",
                    RoleID = 1
                },
                new User()
                {
                    UserID = 2,
                    Name = "Adam",
                    Email = "adamx@gmail.com",
                    Password = "password",
                    //PasswordHash = "String",
                    Phone = "123",
                    RoleID = 2
                },
                new User()
                {
                    UserID = 3,
                    Name = "Cust",
                    Email = "cust@gmail.com",
                    Password = "password",
                   // PasswordHash = "String",
                    Phone = "123",
                    RoleID = 2
                });*/

            // Role entity configuration
            modelBuilder.Entity<Role>().HasData(
                 new Role()
                 {
                     RoleID = 1,
                     RoleName = "Admin"
                 },
                 new Role()
                 {
                     RoleID = 2,
                     RoleName = "Customer"
                 });

            // Pet Price column configuration
            modelBuilder.Entity<Pet>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}

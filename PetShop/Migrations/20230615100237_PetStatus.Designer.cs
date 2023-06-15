﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetShop.DataAccessLayer.Context;

#nullable disable

namespace PetShop.Migrations
{
    [DbContext(typeof(PetShopDbContext))]
    [Migration("20230615100237_PetStatus")]
    partial class PetStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PetShop.DataAccessLayer.Entities.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"), 1L, 1);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PetID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("PetID")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PetShop.DataAccessLayer.Entities.Pet", b =>
                {
                    b.Property<int>("PetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PetID"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PetID");

                    b.ToTable("Pets");

                    b.HasData(
                        new
                        {
                            PetID = 44,
                            Age = 20,
                            ImgUrl = "https://static.businessinsider.com/image/5484d9d1eab8ea3017b17e29/image.jpg",
                            Name = "harry",
                            Price = 20m,
                            Species = "dog"
                        },
                        new
                        {
                            PetID = 10,
                            Age = 2,
                            ImgUrl = "https://wallpaperaccess.com/full/275808.jpg",
                            Name = "hooooarry",
                            Price = 220m,
                            Species = "cat"
                        },
                        new
                        {
                            PetID = 5,
                            Age = 201,
                            ImgUrl = "https://scx2.b-cdn.net/gfx/news/hires/2018/2-dog.jpg",
                            Name = "haroooory",
                            Price = 220m,
                            Species = "dog"
                        });
                });

            modelBuilder.Entity("PetShop.DataAccessLayer.Entities.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleID = 2,
                            RoleName = "Customer"
                        });
                });

            modelBuilder.Entity("PetShop.DataAccessLayer.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Email = "alex@gmail.com",
                            Name = "Alex",
                            Password = "password",
                            Phone = "123",
                            RoleID = 1
                        },
                        new
                        {
                            UserID = 2,
                            Email = "adamx@gmail.com",
                            Name = "Adam",
                            Password = "password",
                            Phone = "123",
                            RoleID = 2
                        },
                        new
                        {
                            UserID = 3,
                            Email = "cust@gmail.com",
                            Name = "Cust",
                            Password = "password",
                            Phone = "123",
                            RoleID = 2
                        });
                });

            modelBuilder.Entity("PetShop.DataAccessLayer.Entities.Order", b =>
                {
                    b.HasOne("PetShop.DataAccessLayer.Entities.Pet", "Pet")
                        .WithOne("Order")
                        .HasForeignKey("PetShop.DataAccessLayer.Entities.Order", "PetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetShop.DataAccessLayer.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetShop.DataAccessLayer.Entities.User", b =>
                {
                    b.HasOne("PetShop.DataAccessLayer.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PetShop.DataAccessLayer.Entities.Pet", b =>
                {
                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}

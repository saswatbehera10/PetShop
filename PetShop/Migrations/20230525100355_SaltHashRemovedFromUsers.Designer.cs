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
    [Migration("20230525100355_SaltHashRemovedFromUsers")]
    partial class SaltHashRemovedFromUsers
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

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PetID");

                    b.HasIndex("UserID");

                    b.ToTable("Pets");

                    b.HasData(
                        new
                        {
                            PetID = 44,
                            Age = 20,
                            Name = "harry",
                            Price = 20m,
                            Species = "dog",
                            UserID = 1
                        },
                        new
                        {
                            PetID = 10,
                            Age = 2,
                            Name = "hooooarry",
                            Price = 220m,
                            Species = "cat",
                            UserID = 2
                        },
                        new
                        {
                            PetID = 5,
                            Age = 201,
                            Name = "haroooory",
                            Price = 220m,
                            Species = "dog",
                            UserID = 3
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
                    b.HasOne("PetShop.DataAccessLayer.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetShop.DataAccessLayer.Entities.Pet", b =>
                {
                    b.HasOne("PetShop.DataAccessLayer.Entities.User", "User")
                        .WithMany("Pets")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetShop.DataAccessLayer.Entities.User", b =>
                {
                    b.HasOne("PetShop.DataAccessLayer.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PetShop.DataAccessLayer.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("PetShop.DataAccessLayer.Entities.User", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
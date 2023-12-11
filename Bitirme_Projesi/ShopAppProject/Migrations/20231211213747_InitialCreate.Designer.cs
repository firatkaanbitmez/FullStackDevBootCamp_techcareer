﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopAppProject.Data;

#nullable disable

namespace ShopAppProject.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231211213747_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("ShopAppProject.Data.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerAd")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerSoyad")
                        .HasColumnType("TEXT");

                    b.Property<string>("Eposta")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefon")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.ToTable("Customerler");
                });

            modelBuilder.Entity("ShopAppProject.Data.Kurs", b =>
                {
                    b.Property<int>("KursId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Baslik")
                        .HasColumnType("TEXT");

                    b.HasKey("KursId");

                    b.ToTable("Kurslar");
                });

            modelBuilder.Entity("ShopAppProject.Data.KursKayit", b =>
                {
                    b.Property<int>("KayitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("TEXT");

                    b.Property<int>("KursId")
                        .HasColumnType("INTEGER");

                    b.HasKey("KayitId");

                    b.ToTable("KursKayitlari");
                });
#pragma warning restore 612, 618
        }
    }
}
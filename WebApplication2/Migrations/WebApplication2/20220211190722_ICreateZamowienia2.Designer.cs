﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication2.Data;

namespace WebApplication2.Migrations.WebApplication2
{
    [DbContext(typeof(WebApplication2Context))]
    [Migration("20220211190722_ICreateZamowienia2")]
    partial class ICreateZamowienia2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication2.Models.Klienci", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firma")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwisko")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Klienci");
                });

            modelBuilder.Entity("WebApplication2.Models.Przedmioty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CenaJednostkowa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Ilosc")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Wrazliwy")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Przedmioty");
                });

            modelBuilder.Entity("WebApplication2.Models.Zamowienia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cena")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataZamowienia")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int?>("KlienciId")
                        .HasColumnType("int");

                    b.Property<int>("LiczbaPrzedmiotow")
                        .HasColumnType("int");

                    b.Property<int?>("PrzedmiotyId")
                        .HasColumnType("int");

                    b.Property<string>("StanZamowienia")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KlienciId");

                    b.HasIndex("PrzedmiotyId");

                    b.ToTable("Zamowienia");
                });

            modelBuilder.Entity("WebApplication2.Models.Zamowienia", b =>
                {
                    b.HasOne("WebApplication2.Models.Klienci", "Klienci")
                        .WithMany()
                        .HasForeignKey("KlienciId");

                    b.HasOne("WebApplication2.Models.Przedmioty", "Przedmioty")
                        .WithMany()
                        .HasForeignKey("PrzedmiotyId");
                });
#pragma warning restore 612, 618
        }
    }
}

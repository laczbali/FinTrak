﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fintrak.Data;

#nullable disable

namespace fintrak.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220902095032_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("fintrak.Data.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float?>("Amount")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<string>("CategoryName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryName");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("fintrak.Data.Models.TransactionCategory", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Name");

                    b.ToTable("transaction_categories");
                });

            modelBuilder.Entity("fintrak.Data.Models.UserQuery", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("QueryJson")
                        .HasColumnType("longtext");

                    b.HasKey("Name");

                    b.ToTable("user_queries");
                });

            modelBuilder.Entity("fintrak.Data.Models.Transaction", b =>
                {
                    b.HasOne("fintrak.Data.Models.TransactionCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryName");

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
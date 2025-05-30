﻿// <auto-generated />
using System;
using CRUDAPP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRUDAPP.Migrations
{
    [DbContext(typeof(CRUDAPPContext))]
    [Migration("20231017081611_fundname")]
    partial class fundname
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CRUDAPP.Model.Fund", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("FundDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FundGivenByID")
                        .HasColumnType("int");

                    b.Property<int?>("FundGivenToID")
                        .HasColumnType("int");

                    b.Property<int?>("givenBy")
                        .HasColumnType("int");

                    b.Property<string>("givenByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("givenTo")
                        .HasColumnType("int");

                    b.Property<string>("givenToName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("FundGivenByID");

                    b.HasIndex("FundGivenToID");

                    b.ToTable("Fund");
                });

            modelBuilder.Entity("CRUDAPP.Model.Member", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("FlatNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MobileNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalMembers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("CRUDAPP.Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CRUDAPP.Model.Fund", b =>
                {
                    b.HasOne("CRUDAPP.Model.Member", "FundGivenBy")
                        .WithMany()
                        .HasForeignKey("FundGivenByID");

                    b.HasOne("CRUDAPP.Model.Member", "FundGivenTo")
                        .WithMany()
                        .HasForeignKey("FundGivenToID");

                    b.Navigation("FundGivenBy");

                    b.Navigation("FundGivenTo");
                });
#pragma warning restore 612, 618
        }
    }
}

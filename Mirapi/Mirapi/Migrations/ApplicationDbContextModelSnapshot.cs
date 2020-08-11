﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mirapi.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mirapi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Mirapi.Core.Domain.Brands", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Mirapi.Core.Domain.Cars", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BrandId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("modelYear")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Mirapi.Core.Domain.Comments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("comment")
                        .HasColumnType("text");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Mirapi.Core.Domain.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("answer")
                        .HasColumnType("integer");

                    b.Property<Guid?>("carId")
                        .HasColumnType("uuid");

                    b.Property<bool>("isAnswered")
                        .HasColumnType("boolean");

                    b.Property<string>("message")
                        .HasColumnType("text");

                    b.Property<Guid?>("parentId")
                        .HasColumnType("uuid");

                    b.Property<string>("title")
                        .HasColumnType("text");

                    b.Property<Guid?>("userId")
                        .HasColumnType("uuid");

                    b.Property<int>("view")
                        .HasColumnType("integer");

                    b.Property<int>("vote")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("carId");

                    b.HasIndex("parentId");

                    b.HasIndex("userId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Mirapi.Core.Domain.PostCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("categoryId")
                        .HasColumnType("integer");

                    b.Property<int>("postId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("PostCategory");
                });

            modelBuilder.Entity("Mirapi.Core.Domain.PostPhotos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("postId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("PostPhotos");
                });

            modelBuilder.Entity("Mirapi.Core.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsFemale")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Mirapi.Core.Domain.Cars", b =>
                {
                    b.HasOne("Mirapi.Core.Domain.Brands", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId");
                });

            modelBuilder.Entity("Mirapi.Core.Domain.Post", b =>
                {
                    b.HasOne("Mirapi.Core.Domain.Cars", "car")
                        .WithMany()
                        .HasForeignKey("carId");

                    b.HasOne("Mirapi.Core.Domain.Post", "parent")
                        .WithMany()
                        .HasForeignKey("parentId");

                    b.HasOne("Mirapi.Core.Domain.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });
#pragma warning restore 612, 618
        }
    }
}

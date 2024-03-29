﻿// <auto-generated />
using System;
using Gradebook.ConsoleApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gradebook.ConsoleApp.Migrations
{
    [DbContext(typeof(GradebookDbContext))]
    partial class GradebookDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gradebook.ConsoleApp.Entities.Grade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<Guid>("GradebookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<int>("Subject")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GradebookId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Gradebook.ConsoleApp.Entities.Gradebook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Gradebooks");
                });

            modelBuilder.Entity("Gradebook.ConsoleApp.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<Guid>("GradebookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GradebookId")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Gradebook.ConsoleApp.Entities.Grade", b =>
                {
                    b.HasOne("Gradebook.ConsoleApp.Entities.Gradebook", "Gradebook")
                        .WithMany("Grades")
                        .HasForeignKey("GradebookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gradebook");
                });

            modelBuilder.Entity("Gradebook.ConsoleApp.Entities.Student", b =>
                {
                    b.HasOne("Gradebook.ConsoleApp.Entities.Gradebook", "Gradebook")
                        .WithOne("Student")
                        .HasForeignKey("Gradebook.ConsoleApp.Entities.Student", "GradebookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gradebook");
                });

            modelBuilder.Entity("Gradebook.ConsoleApp.Entities.Gradebook", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("Student")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

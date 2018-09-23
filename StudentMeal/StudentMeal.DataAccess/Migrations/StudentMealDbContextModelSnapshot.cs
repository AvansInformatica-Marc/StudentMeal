﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentMeal.DataAccess;

namespace StudentMeal.DataAccess.Migrations
{
    [DbContext(typeof(StudentMealDbContext))]
    partial class StudentMealDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudentMeal.Domain.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CookId");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<byte>("MaxGuests");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<float>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CookId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("StudentMeal.Domain.MealStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MealId");

                    b.Property<int?>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentMeals");
                });

            modelBuilder.Entity("StudentMeal.Domain.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentMeal.Domain.Meal", b =>
                {
                    b.HasOne("StudentMeal.Domain.Student", "Cook")
                        .WithMany("MealsAsCook")
                        .HasForeignKey("CookId");
                });

            modelBuilder.Entity("StudentMeal.Domain.MealStudent", b =>
                {
                    b.HasOne("StudentMeal.Domain.Meal", "Meal")
                        .WithMany("GuestsList")
                        .HasForeignKey("MealId");

                    b.HasOne("StudentMeal.Domain.Student", "Student")
                        .WithMany("MealsAsGuestList")
                        .HasForeignKey("StudentId");
                });
#pragma warning restore 612, 618
        }
    }
}

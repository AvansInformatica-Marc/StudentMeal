using Microsoft.EntityFrameworkCore;
using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMeal.DataAccess {
    public class StudentMealDbContext : DbContext {
        public StudentMealDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Meal>().HasOne(meal => meal.Cook).WithMany(student => student.MealsAsCook);
            modelBuilder.Entity<Meal>().HasMany(meal => meal.GuestsList).WithOne(mealStudent => mealStudent.Meal);
            modelBuilder.Entity<Student>().HasMany(student => student.MealsAsGuestList).WithOne(mealStudent => mealStudent.Student);
        }

        internal DbSet<Student> Students { get; set; }

        internal DbSet<Meal> Meals { get; set; }

        internal DbSet<MealStudent> StudentMeals { get; set; }
    }
}

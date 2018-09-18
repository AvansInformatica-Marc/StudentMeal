using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentMeal.Domain;

namespace StudentMeal.DataAccess {
    internal class DatabaseRepository : DbContext, IRepository {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=localhost;Database=StudentMeal;Trusted_Connection=True");
        }

        internal DbSet<Student> Students { get; set; }

        internal DbSet<Meal> Meals { get; set; }

        public IQueryable<Student> StudentList => Students;

        public IQueryable<Meal> MealList => Meals;

        public void AddStudent(Student student) => Students.Add(student);

        public void AddMeal(Meal meal) => Meals.Add(meal);

        public override void Dispose() {
            SaveChanges();
            base.Dispose();
        }
    }
}
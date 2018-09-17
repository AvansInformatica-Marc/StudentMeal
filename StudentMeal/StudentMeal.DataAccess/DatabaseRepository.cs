using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentMeal.Domain;

namespace StudentMeal.DataAccess {
    class DatabaseRepository : DbContext, IRepository {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=localhost;Database=StudentMeal;Trusted_Connection=True");
        }

        DbSet<Student> Students { get; set; }

        DbSet<Meal> Meals { get; set; }

        public void AddMeal(Meal meal) {
            Meals.Add(meal);
        }

        public void AddStudent(Student student) {
            Students.Add(student);
        }

        public IReadOnlyList<Meal> GetMeals() {
            return Meals.ToList().AsReadOnly();
        }

        public IReadOnlyList<Student> GetStudents() {
            return Students.ToList().AsReadOnly();
        }

        public override void Dispose() {
            SaveChanges();
            base.Dispose();
        }
    }
}
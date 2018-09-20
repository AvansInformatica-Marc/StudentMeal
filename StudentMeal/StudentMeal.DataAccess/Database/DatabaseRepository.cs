using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentMeal.Domain;

namespace StudentMeal.DataAccess {
    internal class DatabaseRepository : IRepository {
        private readonly StudentMealDbContext _context;

        internal DatabaseRepository(StudentMealDbContext context) {
            _context = context;
        }

        public IQueryable<Student> Students => _context.Students;

        public IQueryable<Meal> Meals => _context.Meals;

        public void AddStudent(Student student) => _context.Students.Add(student);

        public void AddMeal(Meal meal) => _context.Meals.Add(meal);

        public void AddStudentAsGuestToMeal(Student student, Meal meal) {
            var mealStudent = new MealStudent {
                Student = student,
                Meal = meal
            };
            _context.StudentMeals.Add(mealStudent);
            student.MealsAsGuestList.Add(mealStudent);
            meal.GuestsList.Add(mealStudent);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}
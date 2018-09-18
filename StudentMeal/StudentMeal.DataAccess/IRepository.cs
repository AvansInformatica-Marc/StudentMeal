using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentMeal.DataAccess {
    public interface IRepository : IDisposable {
        IQueryable<Student> StudentList { get; }

        IQueryable<Meal> MealList { get; }

        void AddStudent(Student student);

        void AddMeal(Meal meal);
    }
}

using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMeal.DataAccess {
    public interface IRepository : IDisposable {
        void AddStudent(Student student);

        void AddMeal(Meal meal);

        IReadOnlyList<Student> GetStudents();

        IReadOnlyList<Meal> GetMeals();
    }
}

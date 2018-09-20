using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentMeal.DataAccess {
    public interface IRepository : IDisposable {
        IQueryable<Student> Students { get; }

        IQueryable<Meal> Meals { get; }

        void AddStudent(Student student);

        void AddMeal(Meal meal);

        void AddStudentAsGuestToMeal(Student student, Meal meal);

        void SaveChanges();
    }
}

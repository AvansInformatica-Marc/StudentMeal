using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentMeal.Domain;

namespace StudentMeal.DataAccess {
    class DatabaseRepository : DbContext, IRepository {
        public void AddMeal(Meal meal) {
            throw new NotImplementedException();
        }

        public void AddStudent(Student student) {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Meal> GetMeals() {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Student> GetStudents() {
            throw new NotImplementedException();
        }
    }
}
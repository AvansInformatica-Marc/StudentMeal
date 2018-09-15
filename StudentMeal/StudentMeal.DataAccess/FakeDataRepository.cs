using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMeal.DataAccess {
    class FakeDataRepository : IRepository {
        private static readonly List<Student> _students = new List<Student>();

        private static readonly List<Meal> _meals = new List<Meal>();

        public void AddStudent(Student student) {
            _students.Add(student);
        }

        public void AddMeal(Meal meal) {
            _meals.Add(meal);
        }

        public IReadOnlyList<Student> GetStudents() {
            return _students.AsReadOnly();
        }

        public IReadOnlyList<Meal> GetMeals() {
            return _meals.AsReadOnly();
        }
    }
}

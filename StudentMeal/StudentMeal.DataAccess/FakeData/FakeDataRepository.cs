using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentMeal.DataAccess {
    internal class FakeDataRepository : IRepository {
        private static readonly ICollection<Student> _students = new HashSet<Student>();

        private static readonly ICollection<Meal> _meals = new HashSet<Meal>();

        internal FakeDataRepository() {
            if (_students.Count() == 0) {
                var person1 = new Student {
                    Id = 1,
                    Name = "Marc",
                    Email = "mj.bouwman1@student.avans.nl",
                    PhoneNumber = "+31 (6) 12345678"
                };
                var person2 = new Student {
                    Id = 2,
                    Name = "Arjen",
                    Email = "arjen.lubach@student.avans.nl",
                    PhoneNumber = "+31 (6) 01234567"
                };
                var person3 = new Student {
                    Id = 3,
                    Name = "Elon",
                    Email = "elon.musk@student.avans.nl",
                    PhoneNumber = "+31 (6) 87654321"
                };

                AddStudent(person1);
                AddStudent(person2);
                AddStudent(person3);

                var today = DateTime.Today;

                var meal1 = new Meal {
                    Id = 1,
                    DateTime = new DateTime(today.Year, today.Month, today.Day, 18, 10, 0),
                    Name = "Pannenkoeken",
                    Description = "Overheerlijke pannenkoeken.",
                    Cook = person2,
                    Price = 1.20F,
                    MaxGuests = 3
                };

                var next = today.AddDays(2);

                var meal2 = new Meal {
                    Id = 2,
                    DateTime = new DateTime(next.Year, next.Month, next.Day, 17, 55, 0),
                    Name = "Friet/patat",
                    Description = "Franse frietjes uit de oven.",
                    Cook = person3,
                    Price = 0.80F,
                    MaxGuests = 6
                };

                var next2 = today.AddDays(5);

                var meal3 = new Meal {
                    Id = 3,
                    DateTime = new DateTime(next2.Year, next2.Month, next2.Day, 18, 0, 0),
                    Name = "Spaghetti",
                    Description = "Spaghetti",
                    Cook = person3,
                    Price = 2.80F,
                    MaxGuests = 4
                };

                AddMeal(meal1);
                AddMeal(meal2);
                AddMeal(meal3);

                AddStudentAsGuestToMeal(person1, meal1);
                AddStudentAsGuestToMeal(person3, meal1);
                AddStudentAsGuestToMeal(person1, meal2);
            }
        }

        public IQueryable<Student> Students {
            get {
                return _students.AsQueryable();
            }
        }

        public IQueryable<Meal> Meals {
            get {
                return _meals.AsQueryable();
            }
        }

        public void AddStudent(Student student) => _students.Add(student);

        public void AddMeal(Meal meal) => _meals.Add(meal);

        public void AddStudentAsGuestToMeal(Student student, Meal meal) {
            var mealStudent = new MealStudent {
                Student = student,
                Meal = meal
            };
            student.MealsAsGuestList.Add(mealStudent);
            meal.GuestsList.Add(mealStudent);
        }

        public void Dispose() {}

        public void SaveChanges() {}
    }
}

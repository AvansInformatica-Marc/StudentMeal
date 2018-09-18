using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentMeal.DataAccess {
    internal class FakeDataRepository : IRepository {
        private static readonly ICollection<Student> _students = new HashSet<Student>();

        private static readonly ICollection<Meal> _meals = new HashSet<Meal>();

        public FakeDataRepository() {
            if (_students.Count() == 0) {
                var person1 = new Student {
                    Name = "Marc",
                    Email = "mj.bouwman1@student.avans.nl",
                    PhoneNumber = "+31 (6) 12345678"
                };
                var person2 = new Student {
                    Name = "Arjen",
                    Email = "arjen.lubach@student.avans.nl",
                    PhoneNumber = "+31 (6) 01234567"
                };
                var person3 = new Student {
                    Name = "Elon",
                    Email = "elon.musk@student.avans.nl",
                    PhoneNumber = "+31 (6) 87654321"
                };

                _students.Add(person1);
                _students.Add(person2);
                _students.Add(person3);

                var today = DateTime.Today;

                var meal1 = new Meal {
                    DateTime = new DateTime(today.Year, today.Month, today.Day, 18, 10, 0),
                    Name = "Pannenkoeken",
                    Description = "Overheerlijke pannenkoeken. Ook glutenvrij!",
                    Cook = person2,
                    Price = 1.20F,
                    MaxGuests = 3,
                    Guests = new HashSet<Student> { person2, person3 }
                };

                var next = today.AddDays(2);

                var meal2 = new Meal {
                    DateTime = new DateTime(next.Year, next.Month, next.Day, 17, 55, 0),
                    Name = "Franse ovenfriet",
                    Description = "Franse frietjes uit de oven.",
                    Cook = person3,
                    Price = 0.80F,
                    MaxGuests = 6,
                    Guests = new HashSet<Student> { person1 }
                };

                var next2 = today.AddDays(5);

                var meal3 = new Meal {
                    DateTime = new DateTime(next2.Year, next2.Month, next2.Day, 18, 0, 0),
                    Name = "Aardappelgerecht met wortelen",
                    Description = "Maaltijd bereid met aardappelen en wortels.",
                    Cook = person3,
                    Price = 2.80F,
                    MaxGuests = 4,
                    Guests = new HashSet<Student> { }
                };

                _meals.Add(meal1);
                _meals.Add(meal2);
                _meals.Add(meal3);
            }
        }

        public IQueryable<Student> StudentList {
            get {
                return _students.AsQueryable();
            }
        }

        public IQueryable<Meal> MealList {
            get {
                return _meals.AsQueryable();
            }
        }

        public void AddStudent(Student student) => _students.Add(student);

        public void AddMeal(Meal meal) => _meals.Add(meal);

        public void Dispose() {}
    }
}

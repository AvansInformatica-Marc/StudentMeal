using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentMeal.DataAccess {
    public class FakeDataRepository : IRepository {
        private static readonly Random _random = new Random();

        private static readonly ICollection<Student> _students = new HashSet<Student>();

        private static readonly ICollection<Meal> _meals = new HashSet<Meal>();

        public FakeDataRepository() {
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
                    DateTime = new DateTime(today.Year, today.Month, today.Day, 18, 10, 0),
                    Name = "Pannenkoeken 🥞",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur rutrum convallis luctus. Etiam facilisis placerat lorem eu finibus. Fusce ullamcorper lorem a sapien dignissim, ac facilisis ante congue. Nullam eget nisi sed velit pulvinar lacinia.",
                    Cook = person2,
                    Price = 1.20F,
                    MaxGuests = 3
                };

                var next = today.AddDays(2);

                var meal2 = new Meal {
                    DateTime = new DateTime(next.Year, next.Month, next.Day, 17, 55, 0),
                    Name = "Friet/patat 🍟",
                    Description = "Franse frietjes uit de oven.",
                    Cook = person3,
                    Price = 0.80F,
                    MaxGuests = 6
                };

                var next2 = today.AddDays(5);

                var meal3 = new Meal {
                    DateTime = new DateTime(next2.Year, next2.Month, next2.Day, 18, 0, 0),
                    Name = "Pizza 🍕",
                    Description = "Pizza",
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

        public void AddStudent(Student student) {
            student.Id = _random.Next(1000);
            _students.Add(student);
        }

        public void AddMeal(Meal meal) {
            meal.Id = _random.Next(1000);
            _meals.Add(meal);
        }

        public void UpdateMeal(Meal meal) { }

        public void DeleteMeal(Meal meal) => _meals.Remove(meal);

        public void AddStudentAsGuestToMeal(Student student, Meal meal) {
            var mealStudent = new MealStudent {
                Student = student,
                Meal = meal
            };
            student.MealsAsGuestList.Add(mealStudent);
            meal.GuestsList.Add(mealStudent);
        }

        public void RemoveStudentAsGuestFromMeal(Student student, Meal meal) {
            var mealStudent = meal.GuestsList.FirstOrDefault(ms => ms.Student == student && ms.Meal == meal);
            student.MealsAsGuestList.Remove(mealStudent);
            meal.GuestsList.Remove(mealStudent);
        }

        public void SaveChanges() {}
    }
}

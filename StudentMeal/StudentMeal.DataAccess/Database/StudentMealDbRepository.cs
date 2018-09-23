using System;
using System.Linq;
using StudentMeal.Domain;

namespace StudentMeal.DataAccess {
    public class StudentMealDbRepository : IRepository {
        private readonly StudentMealDbContext _context;

        public StudentMealDbRepository(StudentMealDbContext context) {
            _context = context;
        }

        public IQueryable<Student> Students => _context.Students;

        public IQueryable<Meal> Meals => _context.Meals;

        public void AddStudent(Student student) => _context.Students.Add(student);

        public void AddMeal(Meal meal) => _context.Meals.Add(meal);

        public void UpdateMeal(Meal newMeal) {
            var meal = _context.Meals.Find(newMeal.Id);
            meal.DateTime = newMeal.DateTime;
            meal.Description = newMeal.Description;
            meal.Name = newMeal.Name;
            meal.Price = newMeal.Price;
            meal.MaxGuests = newMeal.MaxGuests;
            _context.Update(meal);
        }

        public void DeleteMeal(Meal meal) => _context.Meals.Remove(meal);

        public void AddStudentAsGuestToMeal(Student student, Meal meal) {
            var mealStudent = new MealStudent {
                Student = student,
                Meal = meal
            };
            _context.StudentMeals.Add(mealStudent);
            student.MealsAsGuestList.Add(mealStudent);
            meal.GuestsList.Add(mealStudent);
        }

        public void RemoveStudentAsGuestFromMeal(Student student, Meal meal) {
            var mealStudent = _context.StudentMeals.FirstOrDefault(sm => sm.Student == student && sm.Meal == meal);
            student.MealsAsGuestList.Remove(mealStudent);
            meal.GuestsList.Remove(mealStudent);
            _context.StudentMeals.Remove(mealStudent);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
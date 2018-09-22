using System.Linq;
using StudentMeal.Domain;

namespace StudentMeal.DataAccess {
    internal class StudentMealDbRepository : IRepository {
        private readonly StudentMealDbContext _context;

        internal StudentMealDbRepository(StudentMealDbContext context) {
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
    }
}
using StudentMeal.Domain;
using System.Linq;

namespace StudentMeal.DataAccess {
    public interface IRepository {
        IQueryable<Student> Students { get; }

        IQueryable<Meal> Meals { get; }

        void AddStudent(Student student);

        void AddMeal(Meal meal);

        void UpdateMeal(Meal meal);

        void DeleteMeal(Meal meal);

        void AddStudentAsGuestToMeal(Student student, Meal meal);

        void RemoveStudentAsGuestFromMeal(Student student, Meal meal);

        void SaveChanges();
    }
}

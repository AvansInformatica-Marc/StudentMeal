using StudentMeal.DataAccess;
using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMeal.AppLogic {
    public class StudentMealManager {
        private readonly IRepository _dataRepository;
        public StudentMealManager(IRepository dataRepository) {
            _dataRepository = dataRepository;
        }

        public (bool /*canEnterMeal*/, string /*reason*/) CanStudentEnterMealAsGuest(Student student, Meal meal) {
            if (meal.Guests.Contains(student) && student.MealsAsGuest.Contains(meal))
                return (false, "You're already a guest!");
            if (meal.Cook == student && student.MealsAsCook.Contains(meal))
                return (false, "Cooks can't be the guests of their own meal.");
            if (meal.GuestCount + 1 > meal.MaxGuests)
                return (false, "There are no free places left for this meal.");

            return (true, null);
        }

        public (bool /*success*/, string /*errorMessage*/) AddStudentToMealAsGuest(Student student, Meal meal) {
            var canEnterMeal = CanStudentEnterMealAsGuest(student, meal);

            if (canEnterMeal.Item1) {
                meal.Guests.Add(student);
                student.MealsAsGuest.Add(meal);
            }

            return canEnterMeal;
        }
    }
}

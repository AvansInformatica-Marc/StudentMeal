using StudentMeal.DataAccess;
using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentMeal.AppLogic {
    public class StudentMealManager {
        private readonly IRepository _dataRepository;
        public StudentMealManager(IRepository dataRepository) {
            _dataRepository = dataRepository;
        }

        public void AddStudent(Student student) {
            _dataRepository.AddStudent(student);
            _dataRepository.SaveChanges();
        }

        public Student GetStudentById(int id) => _dataRepository.Students.FirstOrDefault(student => student.Id == id);

        public Student GetStudentByEmail(string email) => _dataRepository.Students.FirstOrDefault(student => student.Email == email);

        public void AddMeal(Meal meal) {
            _dataRepository.AddMeal(meal);
            meal.Cook.MealsAsCook.Add(meal);
            _dataRepository.SaveChanges();
        }

        public void UpdateMeal(int id, Func<Meal, Meal> editFunction) {
            var meal = GetMealById(id); // Retreives old meal.
            meal = editFunction(meal); // Calls the edit function with the old meal and returns the updated meal.
            _dataRepository.UpdateMeal(meal); // Update the meal.
            _dataRepository.SaveChanges();
        }

        public void DeleteMeal(Meal meal) {
            _dataRepository.DeleteMeal(meal);
            _dataRepository.SaveChanges();
        }

        public Meal GetMealById(int id) => _dataRepository.Meals.FirstOrDefault(meal => meal.Id == id);

        public IEnumerable<Meal> GetMealsForPeriod(DateTime start, DateTime end, bool ignoreTime = true) {
            // If ignoreTime is set to true, count from the beginning from the day (0:00.00) to the end of the day (23:59.59)
            var startDate = ignoreTime ? new DateTime(start.Year, start.Month, start.Day, 0, 0, 0) : start;
            var endDate = ignoreTime ? new DateTime(end.Year, end.Month, end.Day, 23, 59, 59) : end;
            
            return _dataRepository.Meals.Where(meal => meal.DateTime.CompareTo(startDate) >= 0 && meal.DateTime.CompareTo(endDate) <= 0).OrderBy(meal => meal.DateTime).ToList();
        }

        public IEnumerable<Meal> GetMealsForDate(DateTime date) => GetMealsForPeriod(date, date);

        public void AddStudentToMealAsGuest(Student student, Meal meal) {
            _dataRepository.AddStudentAsGuestToMeal(student, meal);
            _dataRepository.SaveChanges();
        }

        public void RemoveStudentFromMealAsGuest(Student student, Meal meal) {
            _dataRepository.RemoveStudentAsGuestFromMeal(student, meal);
            _dataRepository.SaveChanges();
        }
    }
}

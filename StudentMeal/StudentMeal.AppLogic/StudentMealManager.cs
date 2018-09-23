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

        public bool CanStudentEnterMealAsGuest(Student student, Meal meal) => !(
            (meal.Guests.Contains(student) && student.MealsAsGuest.Contains(meal)) || 
            (meal.Cook == student && student.MealsAsCook.Contains(meal)) || 
            meal.GuestCount + 1 > meal.MaxGuests);

        public void AddStudentToMealAsGuest(Student student, Meal meal) {
            _dataRepository.AddStudentAsGuestToMeal(student, meal);
            _dataRepository.SaveChanges();
        }

        public void AddStudent(Student student) {
            _dataRepository.AddStudent(student);
            _dataRepository.SaveChanges();
        }

        public void AddMeal(Meal meal) {
            _dataRepository.AddMeal(meal);
            meal.Cook.MealsAsCook.Add(meal);
            _dataRepository.SaveChanges();
        }

        public Meal GetMealById(int id) => _dataRepository.Meals.Where(meal => meal.Id == id).FirstOrDefault();

        public Student GetStudentById(int id) => _dataRepository.Students.Where(student => student.Id == id).FirstOrDefault();

        public Student GetStudentByEmail(string email) => _dataRepository.Students.Where(student => student.Email == email).FirstOrDefault();

        public IEnumerable<Meal> GetMealsForPeriod(DateTime start, DateTime end, bool ignoreTime = true) {
            var startDate = ignoreTime ? new DateTime(start.Year, start.Month, start.Day, 0, 0, 0) : start;
            var endDate = ignoreTime ? new DateTime(end.Year, end.Month, end.Day, 23, 59, 59) : end;
            return _dataRepository.Meals.Where(meal => meal.DateTime.CompareTo(startDate) >= 0 && meal.DateTime.CompareTo(endDate) <= 0).OrderBy(meal => meal.DateTime).ToList();
        }

        public IEnumerable<Meal> GetMealsForDate(DateTime date) => GetMealsForPeriod(date, date);

        public void UpdateMeal(int id, Func<Meal, Meal> editFunction) {
            var meal = GetMealById(id);
            meal = editFunction(meal);
            meal.Id = id;
            _dataRepository.SaveChanges();
        }

        public void UpdateStudent(int id, Func<Student, Student> editFunction) {
            var student = GetStudentById(id);
            student = editFunction(student);
            student.Id = id;
            _dataRepository.SaveChanges();
        }
    }
}

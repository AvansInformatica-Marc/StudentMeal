using StudentMeal.Domain;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMeal.AppLogic {
    public static class MealExtensions {
        public static bool CanStudentEnterAsGuest(this Meal meal, string studentEmail) =>
            meal.Guests.Where(guest => guest.Email == studentEmail).Count() == 0 &&
            meal.Cook.Email != studentEmail &&
            meal.GuestCount + 1 <= meal.MaxGuests;

        public static bool CanStudentLeave(this Meal meal, string studentEmail) =>
            meal.Guests.Where(guest => guest.Email == studentEmail).Count() > 0;

        public static bool CanStudentDelete(this Meal meal, string studentEmail) =>
            meal.Cook.Email == studentEmail &&
            meal.GuestCount == 0;

        public static bool CanStudentEdit(this Meal meal, string studentEmail) =>
            meal.Cook.Email == studentEmail &&
            meal.GuestCount == 0;
    }
}

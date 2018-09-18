using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMeal.AppLogic {
    public static class DateExtensions {
        public static bool IsBetween(this DateTime self, DateTime start, DateTime end, bool ignoreTime = true) {
            var startDate = ignoreTime ? new DateTime(start.Year, start.Month, start.Day, 0, 0, 0) : start;
            var endDate = ignoreTime ? new DateTime(end.Year, end.Month, end.Day, 23, 59, 59) : end;
            return self.CompareTo(startDate) >= 0 && self.CompareTo(endDate) <= 0;
        }
    }
}

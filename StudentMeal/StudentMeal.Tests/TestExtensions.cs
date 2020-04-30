using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentMeal.Tests {
    public static class TestExtensions {
        public static bool IsValidModel(this object testObject) {
            var context = new ValidationContext(testObject, null, null);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(testObject, context, results, true);
        }
    }
}

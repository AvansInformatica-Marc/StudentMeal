using StudentMeal.Domain;
using System.ComponentModel.DataAnnotations;

namespace StudentMeal.Presentation.Models {
    public class RegisterModel {
        public Student Student { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

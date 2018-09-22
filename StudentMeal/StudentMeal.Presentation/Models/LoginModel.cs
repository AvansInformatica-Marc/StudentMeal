using System.ComponentModel.DataAnnotations;

namespace StudentMeal.Presentation.Models {
    public class LoginModel {
        [Required, StringLength(64), EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

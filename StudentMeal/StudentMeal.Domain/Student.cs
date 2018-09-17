using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentMeal.Domain {
    public class Student {
        public int Id { get; set; }

        [Required, StringLength(64), MinLength(2)]
        public string Name { get; set; }

        [Required, StringLength(64), EmailAddress]
        public string Email { get; set; }
        
        [Required, StringLength(16), Phone, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public virtual ICollection<Meal> MealsAsCook { get; set; } = new HashSet<Meal>();

        [Required]
        public virtual ICollection<Meal> MealsAsGuest { get; set; } = new HashSet<Meal>();
    }
}

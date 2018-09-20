using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StudentMeal.Domain {
    public class Student {
        public int Id { get; set; }

        [Required, StringLength(64), MinLength(2)]
        public string Name { get; set; }

        [Required, StringLength(64), EmailAddress]
        public string Email { get; set; }
        
        [Required, StringLength(16), Phone, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Meal> MealsAsCook { get; set; } = new HashSet<Meal>();

        public virtual ICollection<MealStudent> MealsAsGuestList { get; set; } = new HashSet<MealStudent>();

        public IEnumerable<Meal> MealsAsGuest {
            get {
                return MealsAsGuestList.Select(studentMeal => studentMeal.Meal);
            }
        }
    }
}

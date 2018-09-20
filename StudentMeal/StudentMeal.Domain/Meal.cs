using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StudentMeal.Domain {
    public class Meal {
        public int Id { get; set; }

        [Required(ErrorMessage = "Dit veld is vereist"), DataType(DataType.DateTime, ErrorMessage = "Dit veld moet een datum zijn")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Dit veld is vereist"), StringLength(64, ErrorMessage = "Dit veld mag maximaal 64 tekens bevatten.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Dit veld is vereist"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual Student Cook { get; set; }

        [Required(ErrorMessage = "Dit veld is vereist")]
        public byte MaxGuests { get; set; }

        [Required(ErrorMessage = "Dit veld is vereist"), DataType(DataType.Currency, ErrorMessage = "Dit moet een geldbedrag zijn.")]
        public float Price { get; set; }

        public virtual ICollection<MealStudent> GuestsList { get; set; } = new HashSet<MealStudent>();

        public int GuestCount {
            get {
                return Guests.Count();
            }
        }

        public IEnumerable<Student> Guests {
            get {
                return GuestsList.Select(studentMeal => studentMeal.Student);
            }
        }
    }
}

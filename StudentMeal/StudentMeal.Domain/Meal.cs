using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StudentMeal.Domain {
    public class Meal {
        public int Id { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [Required, StringLength(64)]
        public string Name { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public virtual Student Cook { get; set; }

        public byte MaxGuests { get; set; }

        [DataType(DataType.Currency)]
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

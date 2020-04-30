using StudentMeal.Domain;
using System.Collections.Generic;

namespace StudentMeal.Presentation.Models {
    public class IndexViewModel {
        public IEnumerable<Meal> Today { get; set; } = new HashSet<Meal>();
        public IEnumerable<Meal> Upcoming { get; set; } = new HashSet<Meal>();
    }
}

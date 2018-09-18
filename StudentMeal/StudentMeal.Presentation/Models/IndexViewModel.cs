using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMeal.Presentation.Models {
    public class IndexViewModel {
        public IEnumerable<Meal> Today { get; set; } = new HashSet<Meal>();
        public IEnumerable<Meal> Upcoming { get; set; } = new HashSet<Meal>();
    }
}

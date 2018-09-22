namespace StudentMeal.Domain {
    public class MealStudent {
        public int Id { get; set; }

        public virtual Student Student { get; set; }

        public virtual Meal Meal { get; set; }
    }
}

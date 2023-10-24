namespace FoodFit.Models
{
    public class Workout
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int WorkoutTypeID { get; set; }
        public WorkoutType WorkoutType { get; set; }
    }
}

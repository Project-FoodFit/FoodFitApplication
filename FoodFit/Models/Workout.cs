namespace FoodFit.Models
{
    public class Workout
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int WorkoutTypeID { get; set; }
        public double DurationOfTraining { get; set; }
        public WorkoutType WorkoutType { get; set; }
    }
}

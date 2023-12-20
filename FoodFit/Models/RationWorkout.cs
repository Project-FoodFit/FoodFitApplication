namespace FoodFit.Models
{
    public class RationWorkout
    {
        public int ID { get; set; }
        public int RationID { get; set; }
        public int WorkoutID { get; set; }
        public Ration Ration { get; set; }
        public Workout Workout { get; set; }
    }
}

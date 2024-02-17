namespace Task3.Models
{
    public class Stopwatch
    {
        public bool IsRunning { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        public Stopwatch()
        {
            ElapsedTime = TimeSpan.Zero;
            IsRunning = false;
        }
    }
}

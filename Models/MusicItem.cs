namespace ScoreAnalyser.Models
{
    public class MusicItem
    {
        public string Path { get; set; }
        public (double x, double y) Position { get; set; }
    }
}
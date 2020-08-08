namespace ScoreAnalyser.Models
{
    public class MusicItem
    {
        public string Path { get; set; }
        public (double x, double y) Position { get; set; }

        public MusicItem(string path, double x, double y)
        {
            Path = path;
            Position = (x,y);
        }
    }
}
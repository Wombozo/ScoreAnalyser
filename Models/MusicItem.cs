namespace ScoreAnalyser.Models
{
    public class MusicItem
    {
        public string Path { get; set; }
        public (double x, double y) Position { get; set; }

        public MusicItem(string path, double x = 0, double y = 0)
        {
            Path = path;
            Position = (x,y);
        }

        public MusicItem Copy(double x, double y) => new MusicItem(Path, x, y); 
    }
}
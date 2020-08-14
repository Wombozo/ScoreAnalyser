using System;
using System.IO;

namespace ScoreAnalyser.Models
{
    [Serializable]
    public class MusicItem
    {
        public string Path { get; set; }
        public (double x, double y) Position { get; set; }
        public MusicItem(string fullPath, double x = 0, double y = 0)
        {
            Path = fullPath.Replace(Directory.GetCurrentDirectory() + "/Assets/png","");
            Position = (x,y);
        }
        public MusicItem Copy(double x, double y) => new MusicItem(Path, x, y);
        public MusicItem()
        {
        }
    }
}
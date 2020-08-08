using System.Collections.Generic;
using System.Linq;

namespace ScoreAnalyser.Models
{
    public class ScorePage
    {
        public double Scaling { get; set; }
        public List<MusicItem> MusicItems { get; set; }
        public int PageNumber { get; set; }

        public ScorePage(IEnumerable<MusicItem> musicItems, int pageNumber) : this(1, musicItems, pageNumber)
        {
        }

        public ScorePage(double scaling, IEnumerable<MusicItem> musicItems, int pageNumber)
        {
            Scaling = scaling;
            MusicItems = musicItems.ToList();
            PageNumber = pageNumber;
        }

        public void AddMusicItem(string path, double x, double y) => MusicItems.Add(new MusicItem(path, x, y));
    }
}
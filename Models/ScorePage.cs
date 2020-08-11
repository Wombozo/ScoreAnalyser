using System;
using System.Collections.Generic;
using System.Linq;

namespace ScoreAnalyser.Models
{
    [Serializable]
    public class ScorePage
    {
        public ScorePage()
        {
        }
        public double Scaling { get; set; }
        public List<MusicItem> MusicItems { get; }
        public int PageNumber { get; set; }

        public ScorePage(int pageNumber) : this(1, new MusicItem[0], pageNumber)
        {
        }

        public ScorePage(double scaling, IEnumerable<MusicItem> musicItems, int pageNumber)
        {
            Scaling = scaling;
            MusicItems = musicItems.ToList();
            PageNumber = pageNumber;
        }

        public void AddMusicItem(string path, double x, double y) => MusicItems.Add(new MusicItem(path, x, y));
        public void AddMusicItem(MusicItem musicItem) => MusicItems.Add(musicItem);
        public void RemoveMusicItem(MusicItem musicItem) => MusicItems.Remove(musicItem);
    }
}
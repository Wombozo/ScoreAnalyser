using System.Collections.Generic;

namespace ScoreAnalyser.Models
{
    public class ScorePage
    {
        public double Scaling { get; set; }
        public List<MusicItem> MusicItems { get; set; }
        public int PageNumber { get; set; }
    }
}
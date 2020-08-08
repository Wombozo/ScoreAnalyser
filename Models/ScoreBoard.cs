using System.Collections.Generic;

namespace ScoreAnalyser.Models
{
    public class ScoreBoard : ModelBase
    {
        public List<ScorePage> ScorePages { get; set; }
        public string PdfPath { get; set; }
    }
}
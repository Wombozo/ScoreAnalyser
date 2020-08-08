using System.Collections.Generic;

namespace ScoreAnalyser.Models
{
    public class ScoreBoard : ModelBase
    {
        public ScorePage[] ScorePages { get; set; }
        public string PdfPath { get; set; }
        public ScoreBoard(string pdfPath, ScorePage[] scorePages)
        {
            ScorePages = scorePages;
            PdfPath = pdfPath;
        }
    }
}
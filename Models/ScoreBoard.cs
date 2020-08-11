using System;

namespace ScoreAnalyser.Models
{
    [Serializable]
    public class ScoreBoard
    {
        public ScorePage[] ScorePages { get; set; }
        public string PdfPath { get; set; }
        public ScoreBoard(string pdfPath, ScorePage[] scorePages)
        {
            ScorePages = scorePages;
            PdfPath = pdfPath;
        }

        public ScoreBoard()
        {
        }
    }
}
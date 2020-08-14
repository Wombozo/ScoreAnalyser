using System;

namespace ScoreAnalyser.Models
{
    [Serializable]
    public class ScoreBoard
    {
        public string PdfPath { get; set; }
        public double ItemsSize { get; set; }
        public ScorePage[] ScorePages { get; set; }
        public ScoreBoard(string pdfPath, ScorePage[] scorePages, double itemsSize)
        {
            ScorePages = scorePages;
            PdfPath = pdfPath;
            ItemsSize = itemsSize;
        }

        public ScoreBoard()
        {
        }
    }
}
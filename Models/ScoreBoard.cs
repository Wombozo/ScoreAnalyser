using System.Collections.Generic;
using ScoreAnalyser.ViewModels;

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
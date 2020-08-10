using System.Collections.Generic;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Models
{
    public class ScoreBoard : ModelBase
    {
        public ScorePage[] ScorePages { get; }
        public string PdfPath { get; }
        public ScoreBoard(string pdfPath, ScorePage[] scorePages)
        {
            ScorePages = scorePages;
            PdfPath = pdfPath;
        }
    }
}
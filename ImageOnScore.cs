using System;

namespace ScoreAnalyser
{
    public class ImageOnScore
    {
        public (double, double) Position { get; set; }
        public string Image { get; set; }

        public ImageOnScore(string image, double x, double y)
        {
            Position = (x, y);
            Image = image;
        }

        private ImageOnScore()
        {
        }
    }
}
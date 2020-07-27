namespace ScoreAnalyser
{
    public class ImageOnScore
    {
        public (double, double) Position { get; set; }
        public string Image { get; set; }
        public int Page { get; set; }

        public ImageOnScore(int page, string image, double x, double y)
        {
            Position = (x, y);
            Image = image;
            Page = page;
        }
    }
}
using System;

namespace ScoreAnalyser
{
    public class ScoreSize : EventArgs
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public ScoreSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
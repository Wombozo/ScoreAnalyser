using ImageMagick;

namespace ScoreAnalyser.ViewModels
{
    public static class PDFToImageConverter
    {
        public static void ConvertPDFToMultipleImages()
        {
            var settings = new MagickReadSettings();
            // Settings the density to 300 dpi will create an image with a better quality
            //settings.Density = new Density(300, 300);

            using var images = new MagickImageCollection("/home/guillaume/bach.pdf");
            images.Read("/home/guillaume/bach.pdf" , settings);

            var page = 1;
            foreach (var image in images)
            {
                image.Write("/home/guillaume/" + "bach.Page" + page + ".png");
                page++;
            }
        }
    }
}
using System.Collections.Generic;
using System.IO;
using Avalonia.Media.Imaging;
using ImageMagick;

namespace ScoreAnalyser
{
    public static class PDFToImageConverter
    {
        public static IEnumerable<Bitmap> ConvertPDFToMultipleImages(string PDFFile)
        {
            var settings = new MagickReadSettings();
            // Settings the density to 300 dpi will create an image with a better quality
            //settings.Density = new Density(300, 300);
            using var images = new MagickImageCollection(PDFFile);
            images.Read(PDFFile, settings);

            var bmpImages = new List<Bitmap>();
            foreach (var image in images)
            {
                var byteBuffer = image.ToByteArray(MagickFormat.Bmp);
                var memoryStream = new MemoryStream(byteBuffer) {Position = 0};
                bmpImages.Add(new Bitmap(memoryStream));
                memoryStream.Close();
                memoryStream = null;
                // image.Write("/home/guillaume/" + "bach.Page" + page + ".png");
            }

            return bmpImages.ToArray();
        }
    }
}
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace ScoreAnalyser.Views
{
    public static class DegreePanel
    {
        public static void CreatePanel(string assetPngPath, string wrapPanelName, ISolidColorBrush color,
            UserControl userControl)
        {
            var projectPath = Directory.GetCurrentDirectory();
            var WrapPanel = userControl.FindControl<WrapPanel>(wrapPanelName);
            WrapPanel.Background = color;
            var files = Directory.GetFiles(projectPath + assetPngPath)
                .OrderBy(f => int.Parse(Path.GetFileNameWithoutExtension(f).Split('_')[0]));
            files.ToList().ForEach(f =>
            {
                var bitmap = new Bitmap(f);
                WrapPanel.Children.Add(MainWindow.CreateBorderImage(bitmap));
            });
        }
    }
}
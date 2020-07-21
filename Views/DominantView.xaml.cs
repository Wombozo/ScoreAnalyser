using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.Linq;

namespace ScoreAnalyser.Views
{
    public class DominantView : UserControl
    {
        private WrapPanel WrapPanel { get; set; }

        public DominantView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            CreatePanel();
        }

        private void CreatePanel()
        {
            var projectPath =
                Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            WrapPanel = this.FindControl<WrapPanel>("DominantWrapPanel");
            WrapPanel.Background = Brushes.LightSalmon;
            var files = Directory.GetFiles(projectPath + "/Assets/png/dominant/").OrderBy(_ => _);
            files.ToList().ForEach(f =>
                WrapPanel.Children.Add(new Image {Source = new Bitmap(f), Width = 50, Height = 50}));
        }
    }
}
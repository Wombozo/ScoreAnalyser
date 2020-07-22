using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using static ScoreAnalyser.Views.DegreePanel;

namespace ScoreAnalyser.Views
{
    public class PredominantView : UserControl
    {
        private WrapPanel WrapPanel { get; set; }
        public PredominantView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            CreatePanel("/Assets/png/predominant/", "PredominantWrapPanel", Brushes.LightBlue,this);
        }

    }
}
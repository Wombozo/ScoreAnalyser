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
            var border = this.FindControl<Border>("PredominantBorder");
            border.PointerPressed += ScoreView.DoPress;
            border.PointerReleased += ScoreView.DoRelease;
        }

    }
}
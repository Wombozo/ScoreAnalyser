using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ScoreAnalyser.Views
{
    public class PredominantView : UserControl
    {
        public PredominantView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            var border = this.FindControl<Border>("PredominantBorder");
            //border.PointerPressed += ScoreView.DoPress;
            //border.PointerReleased += ScoreView.DoRelease;
        }

    }
}
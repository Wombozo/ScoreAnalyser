using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace ScoreAnalyser.Views
{
    public class DominantView : UserControl
    {

        public DominantView()
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
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using static ScoreAnalyser.Views.DegreePanel;

namespace ScoreAnalyser.Views
{
    public class TonicView : UserControl
    {
        private WrapPanel WrapPanel { get; set; }
        public TonicView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            var border = this.FindControl<Border>("TonicBorder");
            border.PointerPressed += ScoreView.DoPress;
            border.PointerReleased += ScoreView.DoRelease;
        }
    }
}
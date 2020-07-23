using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ScoreAnalyser.ViewModels;

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
            var border = this.FindControl<Border>("DominantBorder");
            border.PointerPressed += DoPress;
            //border.PointerReleased += DoRelease;
        }

        private void DoPress(object sender, PointerPressedEventArgs _)
        {
            ((DominantViewModel) DataContext).SelectItem(sender);
        }
    }
}
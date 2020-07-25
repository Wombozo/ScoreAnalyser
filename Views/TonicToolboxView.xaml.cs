using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Views
{
    public class TonicToolboxView : UserControl
    {
        public TonicToolboxView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            var t = (TonicToolboxViewModel) DataContext;
            var s = t.Tonics;
        }
    }
}
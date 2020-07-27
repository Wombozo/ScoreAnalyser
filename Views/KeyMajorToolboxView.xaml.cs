using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ScoreAnalyser.Views
{
    public class KeyMajorToolboxView : UserControl
    {
        public KeyMajorToolboxView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
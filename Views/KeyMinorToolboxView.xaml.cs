using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ScoreAnalyser.Views
{
    public class KeyMinorToolboxView : UserControl
    {
        public KeyMinorToolboxView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
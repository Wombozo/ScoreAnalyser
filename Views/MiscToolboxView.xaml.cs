using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ScoreAnalyser.Views
{
    public class MiscToolboxView : UserControl
    {
        public MiscToolboxView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
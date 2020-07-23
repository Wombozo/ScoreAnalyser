using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
        }
    }
}
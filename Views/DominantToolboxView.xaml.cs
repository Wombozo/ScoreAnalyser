using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ScoreAnalyser.Views
{
    public class DominantToolboxView : UserControl
    {

        public DominantToolboxView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
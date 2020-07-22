using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using static ScoreAnalyser.Views.DegreePanel;

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
            CreatePanel("/Assets/png/dominant/", "DominantWrapPanel", Brushes.LightCoral,this);
        }
    }
}
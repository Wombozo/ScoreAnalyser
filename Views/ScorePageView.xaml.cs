using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Views
{
    public class ScorePageView : UserControl
    {
        public ScorePageView() => InitializeComponent();

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
        private void OnChangeDataContext(object e, EventArgs args) => ((ScorePageViewModel) DataContext).Canvas = (Canvas) e;
    }
}
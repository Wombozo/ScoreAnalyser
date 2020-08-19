using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Views
{
    public class ScorePageView : UserControl
    {
        public ScorePageView() => InitializeComponent();

        public ScorePageView(ScorePageViewModel scorePageViewModel)
        {
            DataContext = scorePageViewModel;
            InitializeComponent();
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

        private void OnChangeDataContext(object e, EventArgs args)
        {
            if (!(DataContext is ScorePageViewModel scorePageViewModel)) return;
            scorePageViewModel.Canvas = (Canvas) e;
        }
    }
}
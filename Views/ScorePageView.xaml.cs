using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Views
{
    public class ScorePageView : UserControl
    {
        public ScorePageView() => InitializeComponent();

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContextChanged += OnChangeDataContext;
        }

        private void OnChangeDataContext(object e, EventArgs args)
        {
            var canvas = this.FindControl<Canvas>("Canvas");
            ((ScorePageViewModel) DataContext).Canvas = canvas;
        }
    }
}
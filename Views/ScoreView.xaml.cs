using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Views
{
    public class ScoreView : UserControl
    {
        public ScoreView() => InitializeComponent();
        private ScoreViewModel ScoreViewModel { get; set; }
        private TabControl TabControl { get; set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContextChanged += WhenDataContextChanged;
        }
        private void WhenDataContextChanged(object o, EventArgs args)
        {
            ScoreViewModel = (ScoreViewModel) DataContext;
            TabControl = this.FindControl<TabControl>("TabControl");
            TabControl.SelectionChanged += TabItemChanged;
        }
        private void TabItemChanged(object e, EventArgs evt) => ScoreViewModel.SelectedPageViewModel = (ScorePageViewModel)((TabControl) e).SelectedContent;
    }
}
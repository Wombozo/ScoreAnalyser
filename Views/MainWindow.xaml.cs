#nullable enable
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            WindowState = WindowState.Maximized;
            KeyDown += KeyPressedScale;
            Closing += WhenClosing;
        }

        private async void WhenClosing(object? o, CancelEventArgs args)
        {
            var vm = (MainWindowViewModel) DataContext;
            if (vm.Score.ScorePagesVM.Count == 0) return;
            args.Cancel = true;
            var result =
                await MessageBoxView.Show(this, "Closing ?", "You may have unsaved work. Do you really want to exit ?");
            if (result != MessageBoxView.MessageBoxResult.Yes) return;
            Closing -= WhenClosing;
            Close();
        }

        private void KeyPressedScale(object? sender, KeyEventArgs key)
        {
            switch (key.Key)
            {
                case Key.Add:
                    ((MainWindowViewModel) DataContext).IncreaseScaling();
                    break;
                case Key.Subtract:
                    ((MainWindowViewModel) DataContext).DecreaseScaling();
                    break;
            }
        }
    }
}
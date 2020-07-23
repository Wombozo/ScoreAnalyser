#nullable enable
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
        }

        private void KeyPressedScale(object? sender, KeyEventArgs e)
        {
            ((MainWindowViewModel)DataContext).KeyPressedScale(e);
        }
    }
}
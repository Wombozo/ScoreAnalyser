#nullable enable
using System;
using System.ComponentModel;
using System.Threading.Tasks;
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
            // Closing += WhenClosing;
        }

        // private void WhenClosing(object? o, EventArgs _args)
        // {
        //     var args = (CancelEventArgs) _args;
        //     var result = Task.Run(ValidateHandleClosing).Result;
        //     args.Cancel = !result;
        // }

        // private async Task<bool> ValidateHandleClosing()
        // {
        //     var res = await MessageBoxView.Show(this, "Closing ?", "You have unsaved work. Do you really want to exit ?",MessageBoxView.MessageBoxButtons.YesNo);
        //     return res == MessageBoxView.MessageBoxResult.Yes;
        // }
        private void KeyPressedScale(object? sender, KeyEventArgs key)
        {
            switch (key.Key)
            {
                case Key.Add:
                    ((MainWindowViewModel)DataContext).IncreaseScaling();
                    break;
                case Key.Subtract:
                    ((MainWindowViewModel)DataContext).DecreaseScaling();
                    break;
            }
        }
    }

}
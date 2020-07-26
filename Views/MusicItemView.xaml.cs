using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Views
{
    public class MusicItemView : UserControl
    {
        public MusicItemView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            var border = this.FindControl<Border>("Border");
            border.PointerPressed += OnClick;
            border.PointerReleased += OnRelease;
        }

        private void OnClick(object sender, PointerPressedEventArgs args)
        {
            var vm = (MusicItemViewModel) DataContext;
            if (vm.DragAndDropContext.Authorized == false)
                return;
            vm.DragAndDropContext.IsDragging = true;
            vm.DragAndDropContext.SelectedImageSource = vm.ImagePath;
        }

        private void OnRelease(object sender, PointerReleasedEventArgs args)
        {
            var vm = (MusicItemViewModel) DataContext;
            vm.DragAndDropContext.NotifyReleased(args);
        }
    }
}
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using ScoreAnalyser.ViewModels;
using Border = Avalonia.Controls.Border;

namespace ScoreAnalyser.Views
{
    public class MusicItemView : UserControl
    {
        public MusicItemView()
        {
            InitializeComponent();
        }

        public MusicItemView(MusicItemViewModel musicItemViewModel)
        {
            DataContext = musicItemViewModel;
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
            if (!(sender is Border) || !vm.DragAndDropContext.Authorized)
                return;
            vm.DragAndDropContext.IsDragging = true;
            vm.DragAndDropContext.MusicItemViewModel = vm;
            vm.DragAndDropContext.NotifyPressed(new PointerPressedContextEventArgs(args, vm.IsInToolbox));
        }

        private void OnRelease(object sender, PointerReleasedEventArgs args)
        {
            var vm = (MusicItemViewModel) DataContext;
            vm.DragAndDropContext.NotifyReleased(args);
        }
    }

    public class PointerPressedContextEventArgs : EventArgs
    {
        public PointerPressedEventArgs PointerPressedEventArgs { get; }
        public bool IsInToolbox { get; }

        public PointerPressedContextEventArgs(PointerPressedEventArgs pointerPressedEventArgs, bool isInToolbox)
        {
            PointerPressedEventArgs = pointerPressedEventArgs;
            IsInToolbox = isInToolbox;
        }
    }
}
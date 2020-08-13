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
        private Image Image { get; set; }
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

        private void WhenDataContextChanged(object e, EventArgs args)
        {
            Image = this.FindControl<Image>("Image");
            Colorizing();
        }
        private void Colorizing()
        {
            if (Image.Source == null) return;
            var bmpSource = Image.Source;
            var streamPngFormat = new MemoryStream();
            bmpSource.Save(streamPngFormat);
            var pixels = streamPngFormat.ToArray();
            streamPngFormat.Seek(0, SeekOrigin.Begin);
            var bitmap = new Bitmap(streamPngFormat);
            
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
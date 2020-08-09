using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using ScoreAnalyser.ViewModels;

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

        public MusicItemViewModel MusicItemViewModel { get; set; }

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
            if (!(sender is Border border) || !vm.DragAndDropContext.Authorized)
                return;
            if (border.Parent.Parent is Canvas canvas && canvas.DataContext is ScorePageViewModel scorePageViewModel)
            {
                switch (args.InputModifiers)
                {
                    case InputModifiers.LeftMouseButton:
                        vm.DragAndDropContext.IsDragging = true;
                        var imageOnScore = scorePageViewModel.ImagesOnScore.Find(i => i.Image.Equals(vm.ImagePath));
                        vm.DragAndDropContext.SelectedImageSource = imageOnScore.Image;
                        break;
                    case InputModifiers.RightMouseButton:
                        RemoveImageOfScore(vm.ImagePath, scorePageViewModel, canvas);
                        break;
                }
            }
            vm.DragAndDropContext.IsDragging = true;
            vm.DragAndDropContext.SelectedImageSource = vm.ImagePath;
        }

        private void OnRelease(object sender, PointerReleasedEventArgs args)
        {
            var vm = (MusicItemViewModel) DataContext;
            vm.DragAndDropContext.NotifyReleased(args);
        }
        
        private void RemoveImageOfScore(string image, ScorePageViewModel scorePageViewModel, Canvas canvas)
        {
            var item = scorePageViewModel.ImagesOnScore.First(i => i.Image.Equals(image));
            scorePageViewModel.ImagesOnScore.Remove(item);
            canvas.Children.Remove(this);
        }

    }
}
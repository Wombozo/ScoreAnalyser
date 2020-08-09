using System;
using Avalonia.Controls;
using Avalonia.Input;
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
            DataContextChanged += WhenDataContextChanges;
        }

        private void WhenDataContextChanges(object o, EventArgs args)
        {
            ScorePageViewModel = (ScorePageViewModel) DataContext;
            Canvas = this.FindControl<Canvas>("Canvas");
            DragAndDropContext = ScorePageViewModel.ScoreViewModel.DragAndDropContext;
            DragAndDropContext.MouseReleased += OnRelease;
        }

        private Canvas Canvas { get; set; }
        private ScorePageViewModel ScorePageViewModel { get; set; }

        private MusicItemView CreateMusicItem(string source) => new MusicItemView(new MusicItemViewModel(source, DragAndDropContext));
        private DragAndDropContext DragAndDropContext { get; set; }

        private void AddImageOnScore(string imageSource, double x, double y)
        {
            var image = CreateMusicItem(imageSource);
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
            ScorePageViewModel.ImagesOnScore.Add(new ImageOnScore(imageSource, x, y));
            Canvas.Children.Add(image);
        }


        private void OnRelease(object sender, EventArgs evt)
        {
            if (!(evt is PointerReleasedEventArgs e) || DragAndDropContext.IsDragging == false)
                return;
            var point = e.GetPosition(Canvas);
            var x = point.X - 32;
            var y = point.Y - 32;
            if (!(x > 0) || !(y > 0)) return;
            AddImageOnScore(DragAndDropContext.SelectedImageSource, x, y);
            DragAndDropContext.IsDragging = false;
        }
    }
}
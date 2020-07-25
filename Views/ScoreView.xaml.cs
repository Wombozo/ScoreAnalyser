using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using DynamicData.Binding;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Views
{
    public class ScoreView : UserControl
    {
        public ScoreView() => InitializeComponent();
        private ScoreViewModel ScoreViewModel { get; set; }
        private Canvas Canvas { get; set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Canvas = this.FindControl<Canvas>("Canvas");
            DataContextChanged += WhenDataContextChanged;
        }

        private void WhenDataContextChanged(object o, EventArgs args)
        {
            ScoreViewModel = (ScoreViewModel) DataContext;
            ScoreViewModel.DragAndDropContext.MousedReleased += OnRelease;
        }

        private static Border CreateBorderImage(IBitmap source)
            => new Border {Child = new Image {Source = source, Width = 50, Height = 50, Margin = Thickness.Parse("4")}};

        private void AddImageOnScore(string imageSource, double x, double y)
        {
            var image = CreateBorderImage(new Bitmap(imageSource));
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
            Canvas.Children.Add(image);
            ScoreViewModel.ImagesOnScore.Add(new ImageOnScore(imageSource, x, y));
        }

        private void OnRelease(object sender, EventArgs evt)
        {
            if (!(evt is PointerReleasedEventArgs e))
                return;
            var dragAndDropContext = ScoreViewModel.DragAndDropContext;
            if (dragAndDropContext.isDragging == false)
                return;
            var point = e.GetPosition(Canvas);
            AddImageOnScore(dragAndDropContext.SelectedImageSource, point.X - 32, point.Y - 32);
            dragAndDropContext.isDragging = false;
        }
    }
}
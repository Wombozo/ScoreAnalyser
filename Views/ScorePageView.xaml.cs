using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Views
{
    public class ScorePageView : UserControl
    {
        public ScorePageView() => InitializeComponent();

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContextChanged += WhenDataContextChanged;
        }

        private void WhenDataContextChanged(object o, EventArgs args)
        {
            ScorePageViewModel = (ScorePageViewModel) DataContext;
            Canvas = this.FindControl<Canvas>("Canvas");
            DragAndDropContext = ScorePageViewModel.ScoreViewModel.DragAndDropContext;
            DragAndDropContext.MouseReleased += OnRelease;
        }

        private Canvas Canvas { get; set; }
        private ScorePageViewModel ScorePageViewModel { get; set; }
        private static Border CreateBorderImage(IBitmap source)
            => new Border
                {Child = new Image {Source = source, Width = 128, Height = 128, Margin = Thickness.Parse("4")}};
        private DragAndDropContext DragAndDropContext { get; set; }

        private void AddImageOnScore(string imageSource, double x, double y)
        {
            var image = CreateBorderImage(new Bitmap(imageSource));
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
            image.PointerPressed += OnImagePressed;
            image.PointerReleased += OnRelease;
            ScorePageViewModel.ImagesOnScore.Add(new ImageOnScore(imageSource, x, y));
            Canvas.Children.Add(image);
        }

        private void RemoveImageOfScore(IControl image)
        {
            // var item = ScorePageViewModel.ImagesOnScore.First(i => i.Image.Equals(image));
            // var index = ScorePageViewModel.ImagesOnScore.IndexOf(item);
            // ScorePageViewModel.ImagesOnScore.Remove(item);
            // Canvas.Children.Remove(image);
        }

        private void OnRelease(object sender, EventArgs evt)
        {
            if (!(evt is PointerReleasedEventArgs e) || DragAndDropContext.IsDragging == false)
                return;
            var point = e.GetPosition(Canvas);
            var x = point.X - 64;
            var y = point.Y - 64;
            if (!(x > 0) || !(y > 0)) return;
            AddImageOnScore(DragAndDropContext.SelectedImageSource, x, y);
            DragAndDropContext.IsDragging = false;
        }

        private void OnImagePressed(object sender, EventArgs evt)
        {
            // if (!(evt is PointerPressedEventArgs e) || !(sender is Border border) || !DragAndDropContext.Authorized)
            //     return;
            // switch (e.InputModifiers)
            // {
            //     case InputModifiers.LeftMouseButton:
            //         DragAndDropContext.IsDragging = true;
            //         var imageAndPath = ScorePageViewModel.ImagesOnScore.Find(i => i.Image.Equals(border));
            //         DragAndDropContext.SelectedImageSource = imageAndPath.Image;
            //         break;
            //     case InputModifiers.RightMouseButton:
            //         RemoveImageOfScore(border);
            //         break;
            // }
        }
    }
}
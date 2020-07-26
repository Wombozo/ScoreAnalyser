using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
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
        private LayoutTransformControl LayoutTransformControl { get; set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            LayoutTransformControl = this.FindControl<LayoutTransformControl>("LayoutTransformControl");
            DataContextChanged += WhenDataContextChanged;
            ImagesOnBoard = new List<(Border, string)>();
        }

        private void WhenDataContextChanged(object o, EventArgs args)
        {
            ScoreViewModel = (ScoreViewModel) DataContext;
            DragAndDropContext = ScoreViewModel.DragAndDropContext;
            DragAndDropContext.MouseReleased += OnRelease;
            ScoreViewModel.AvailableScore += LoadScoreToCanvas;
        }

        private static Border CreateBorderImage(IBitmap source)
            => new Border {Child = new Image {Source = source, Width = 128, Height = 128, Margin = Thickness.Parse("4")}};

        private List<(Border, string)> ImagesOnBoard { get; set; }
        private DragAndDropContext DragAndDropContext { get; set; }

        private void AddImageOnScore(string imageSource, double x, double y)
        {
            var image = CreateBorderImage(new Bitmap(imageSource));
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
            image.PointerPressed += OnImagePressed;
            image.PointerReleased += OnRelease;
            ScoreViewModel.ImagesOnScore.Add(new ImageOnScore(imageSource, x, y));
            ImagesOnBoard.Add((image, imageSource));
            Canvas.Children.Add(image);
        }

        private void RemoveImageOfScore(IControl image)
        {
            var item = ImagesOnBoard.First(t => t.Item1.Equals(image));
            var index = ImagesOnBoard.IndexOf(item);
            ScoreViewModel.ImagesOnScore.RemoveAt(index);
            ImagesOnBoard.Remove(item);
            Canvas.Children.Remove(image);
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
            if (!(evt is PointerPressedEventArgs e) || !(sender is Border border))
                return;
            switch (e.InputModifiers)
            {
                case InputModifiers.LeftMouseButton:
                    DragAndDropContext.IsDragging = true;
                    var imageAndPath = ImagesOnBoard.Find(i => i.Item1.Equals(border));
                    DragAndDropContext.SelectedImageSource = imageAndPath.Item2;
                    break;
                case InputModifiers.RightMouseButton:
                    RemoveImageOfScore(border);
                    break;
            }
        }

        private void LoadScoreToCanvas(object sender, EventArgs evt)
        {
            if (!(evt is ScoreSize e))
                return;
            var scores = ScoreViewModel.ScorePages;
            var score = scores.First();
            Canvas = new Canvas();
            Canvas.Width = e.Width;
            Canvas.Height = e.Height;
            LayoutTransformControl.Child = Canvas;
            Canvas.Background = new ImageBrush(score);
            DragAndDropContext.Authorized = true;
        }
    }
}
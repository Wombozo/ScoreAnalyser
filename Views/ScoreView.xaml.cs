using System;
using System.Collections.Generic;
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
            ImagesOnBoard = new List<(Border, string)>();
        }

        private void WhenDataContextChanged(object o, EventArgs args)
        {
            ScoreViewModel = (ScoreViewModel) DataContext;
            DragAndDropContext = ScoreViewModel.DragAndDropContext;
            DragAndDropContext.MouseReleased += OnRelease;
        }

        private static Border CreateBorderImage(IBitmap source)
            => new Border {Child = new Image {Source = source, Width = 50, Height = 50, Margin = Thickness.Parse("4")}};

        private List<(Border, string)> ImagesOnBoard { get; set; }
        private DragAndDropContext DragAndDropContext { get; set; }

        private void AddImageOnScore(string imageSource, double x, double y)
        {
            var image = CreateBorderImage(new Bitmap(imageSource));
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
            Canvas.Children.Add(image);
            image.PointerPressed += OnImagePressed;
            image.PointerReleased += OnRelease;
            ScoreViewModel.ImagesOnScore.Add(new ImageOnScore(imageSource, x, y));
            ImagesOnBoard.Add((image, imageSource));
        }

        private void RemoveImageOfScore((Border Image, string Path) imageAndPath)
        {
            var index = ImagesOnBoard.IndexOf(imageAndPath);
            ScoreViewModel.ImagesOnScore.RemoveAt(index);
            ImagesOnBoard.Remove(imageAndPath);
            Canvas.Children.Remove(imageAndPath.Image);
        }

        private void OnRelease(object sender, EventArgs evt)
        {
            if (!(evt is PointerReleasedEventArgs e))
                return;
            if (DragAndDropContext.IsDragging == false)
                return;
            var point = e.GetPosition(Canvas);
            var x = point.X - 32;
            var y = point.Y - 32;
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
                    RemoveImageOfScore((border, DragAndDropContext.SelectedImageSource));
                    break;
            }
        }
    }
}
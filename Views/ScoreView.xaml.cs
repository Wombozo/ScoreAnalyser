using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
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
        private Canvas CurrentCanvas { get; set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContextChanged += WhenDataContextChanged;
            ImagesOnBoard = new List<(int, Border, string)>();
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

        private List<(int, Border, string)> ImagesOnBoard { get; set; }
        private DragAndDropContext DragAndDropContext { get; set; }

        private void AddImageOnScore(string imageSource, double x, double y)
        {
            var image = CreateBorderImage(new Bitmap(imageSource));
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
            image.PointerPressed += OnImagePressed;
            image.PointerReleased += OnRelease;
            ScoreViewModel.ImagesOnScore.Add(new ImageOnScore(TabControl.SelectedIndex, imageSource, x, y));
            ImagesOnBoard.Add((TabControl.SelectedIndex, image, imageSource));
            CurrentCanvas.Children.Add(image);
        }

        private void RemoveImageOfScore(IControl image)
        {
            var item = ImagesOnBoard.First(t => t.Item2.Equals(image));
            var index = ImagesOnBoard.IndexOf(item);
            ScoreViewModel.ImagesOnScore.RemoveAt(index);
            ImagesOnBoard.Remove(item);
            CurrentCanvas.Children.Remove(image);
        }

        private void OnRelease(object sender, EventArgs evt)
        {
            if (!(evt is PointerReleasedEventArgs e) || DragAndDropContext.IsDragging == false)
                return;
            var point = e.GetPosition(CurrentCanvas);
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
                    DragAndDropContext.SelectedImageSource = imageAndPath.Item3;
                    break;
                case InputModifiers.RightMouseButton:
                    RemoveImageOfScore(border);
                    break;
            }
        }

        private TabControl TabControl { get; set; }

        private void TabItemChanged(object e, EventArgs evt) =>
            CurrentCanvas = (Canvas)((TabItem) ((SelectionChangedEventArgs) evt).AddedItems[0])?.Content;

        private void LoadScoreToCanvas(object sender, EventArgs evt)
        {
            if (!(evt is ScoreSize e))
                return;
            var scorePages = ScoreViewModel.ScorePages;
            TabControl = new TabControl();
            TabControl.SelectionChanged += TabItemChanged;
            var items = new List<TabItem>();
            var scrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = TabControl
            };
            var i = 1;
            foreach (var page in scorePages)
            {
                var tabItem = new TabItem {Header = "Page " + i};
                var canvas = new Canvas {Width = e.Width, Height = e.Height, Background = new ImageBrush(page)};
                tabItem.Content = canvas;
                items.Add(tabItem);
                i++;
            }

            TabControl.Items = items;
            scrollViewer.Content = TabControl;
            CurrentCanvas = (Canvas) ((IEnumerable<TabItem>)TabControl.Items).First().Content;
            Content = scrollViewer;
            DragAndDropContext.Authorized = true;
        }
    }
}
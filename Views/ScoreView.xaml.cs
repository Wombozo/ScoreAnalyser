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
        private LayoutTransformControl LayoutTransformControl { get; set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Canvas = this.FindControl<Canvas>("Canvas");
            ScoreViewModel = (ScoreViewModel) DataContext;
        }

        private Border CreateBorderImage(IBitmap source)
        {
            var border = new Border
                {Child = new Image {Source = source, Width = 50, Height = 50, Margin = Thickness.Parse("4")}};
            return border;
        }

        private void OnRelease(object sender, PointerReleasedEventArgs e)
        {
            var dragAndDropContext = ScoreViewModel.DragAndDropContext;
            if (dragAndDropContext.isDragging == false)
                return;
            var item = CreateBorderImage(new Bitmap(dragAndDropContext.SelectedImageSource));
            var point = e.GetPosition(Canvas);
            Canvas.SetLeft(item, point.X - 32);
            Canvas.SetTop(item, point.Y - 32);
            Canvas.Children.Add(item);
            dragAndDropContext.isDragging = false;
        }
    }
}
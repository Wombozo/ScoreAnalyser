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
        private LayoutTransformControl LayoutTransformControl { get; set; }
        private static Canvas Canvas { get; set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            ScoreViewModel = (ScoreViewModel) DataContext;
            Canvas = this.FindControl<Canvas>("Canvas");
        }

        private Border CreateBorderImage(string source)
        {
            var imageSource =
                (Bitmap) BitmapValueConverter.Instance.Convert((string) source, new Bitmap("").GetType(), null, null);
            var border = new Border
                {Child = new Image {Source = imageSource, Width = 50, Height = 50, Margin = Thickness.Parse("4")}};
            return border;
        }

        // private Border CreateBorderImage(IBitmap source)
        // {
        //     var border = new Border
        //         {Child = new Image {Source = source, Width = 50, Height = 50, Margin = Thickness.Parse("4")}};
        //     return border;
        // }

        private void DoRelease(object sender, PointerReleasedEventArgs e)
        {
            var point = e.GetPosition(LayoutTransformControl);
            var ItemFromPanel = CreateBorderImage(Directory.GetCurrentDirectory() + "/assets/tonic/1_I.png");
            Canvas.SetLeft(ItemFromPanel, point.X - 32);
            Canvas.SetTop(ItemFromPanel, point.Y - 32);
            Canvas.Children.Add(ItemFromPanel);
        }
    }
}
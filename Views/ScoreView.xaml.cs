using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;

namespace ScoreAnalyser.Views
{
    public class ScoreView : UserControl
    {
        public ScoreView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Canvas = this.FindControl<Canvas>("Canvas");
            LayoutTransformControl = this.FindControl<LayoutTransformControl>("LayoutTransformControl");
        }

        private Border CreateBorderImage(IBitmap source)
        {
            var border = new Border
                {Child = new Image {Source = source, Width = 50, Height = 50, Margin = Thickness.Parse("4")}};

            // border.PointerPressed += DoPress;
            // border.PointerReleased += DoRelease;
            return border;
        }
        private LayoutTransformControl LayoutTransformControl { get; set; }
        // public void DoPress(object sender, PointerPressedEventArgs e)
        // {
        //     ItemFromPanel = sender switch
        //     {
        //         Border border when true => CreateBorderImage((border.Child as Image)?.Source),
        //         _ => null
        //     };
        // }
        //
        // public void DoRelease(object sender, PointerReleasedEventArgs e)
        // {
        //     if (ItemFromPanel == null)
        //         return;
        //     var point = e.GetPosition(LayoutTransformControl);
        //     Canvas.SetLeft(ItemFromPanel, point.X - 32);
        //     Canvas.SetTop(ItemFromPanel, point.Y - 32);
        //     Canvas.Children.Add(ItemFromPanel);
        // }
        private static Canvas Canvas { get; set; }
    }
}
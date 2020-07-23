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
            ItemsOnBoard = new List<Border>();
        }
        
        public static Border CreateBorderImage(IBitmap source)
        {
            var border = new Border
                {Child = new Image {Source = source, Width = 50, Height = 50, Margin = Thickness.Parse("4")}};

            border.PointerPressed += DoPress;
            border.PointerReleased += DoRelease;
            return border;
        }

        private static Border ItemFromPanel { get; set; }
        private static List<Border> ItemsOnBoard { get; set; }

        public static void DoPress(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            ItemFromPanel = sender switch
            {
                Border border when true => CreateBorderImage((border.Child as Image)?.Source),
                _ => null
            };
        }

        private static Canvas Canvas { get; set; }

        public static void DoRelease(object sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            if (ItemFromPanel == null)
                return;
            var point = e.GetPosition(Canvas);
            Canvas.SetLeft(ItemFromPanel, point.X - 32);
            Canvas.SetTop(ItemFromPanel, point.Y - 32);
            Canvas.Children.Add(ItemFromPanel);
            ItemsOnBoard.Add(ItemFromPanel);
        }
    }
}
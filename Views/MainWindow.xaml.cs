using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using SharpDX.WIC;

namespace ScoreAnalyser.Views
{
    public class MainWindow : Window
    {
        private static Canvas Canvas { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            WindowState = WindowState.Maximized;
            Canvas = this.FindControl<Canvas>("Canvas");
        }

        public static Border CreateBorderImage(IBitmap source)
        {
            var border = new Border
                {Child = new Image {Source = source, Width = 50, Height = 50, Margin = Thickness.Parse("4")}};

            border.PointerPressed += DoPress;
            border.PointerReleased += DoRelease;
            return border;
        }

        private static Border Border { get; set; }

        private static void DoPress(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            if (sender is Border border && border.Parent is WrapPanel)
                Border = CreateBorderImage((border.Child as Image)?.Source);
            else
                Border = null;
        }

        private static void DoRelease(object sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            if (Border == null)
                return;
            var point = e.GetPosition(Canvas);
            Canvas.SetLeft(Border, point.X - 32);
            Canvas.SetTop(Border, point.Y - 32);
            Canvas.Children.Add(Border);
        }
    }
}
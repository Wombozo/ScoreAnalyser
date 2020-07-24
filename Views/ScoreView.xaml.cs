using System;
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
        public ScoreView()
        {
            InitializeComponent();
        }

        private ScoreViewModel ScoreViewModel { get; set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Canvas = this.FindControl<Canvas>("Canvas");
            ScoreViewModel = (ScoreViewModel) DataContext;
            ScoreViewModel.WhenValueChanged(x => x.KeyPressedScale)
                .Subscribe(k => KeyEventScaling((EventArgs) k));
        }

        private void KeyEventScaling(EventArgs e)
        {
            if (!(e is KeyEventArgs k))
                return;
            if ( k.Key == Key.Add)
                ScoreViewModel.SetScaling();
        }

        private Border CreateBorderImage(IBitmap source)
        {
            var border = new Border
                {Child = new Image {Source = source, Width = 50, Height = 50, Margin = Thickness.Parse("4")}};

            // border.PointerPressed += DoPress;
            // border.PointerReleased += DoRelease;
            return border;
        }

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
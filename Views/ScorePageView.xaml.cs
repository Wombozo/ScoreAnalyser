using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using ScoreAnalyser.Models;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Views
{
    public class ScorePageView : UserControl
    {
        public ScorePageView() => InitializeComponent();

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContextChanged += WhenDataContextChanges;
        }

        private void WhenDataContextChanges(object o, EventArgs args)
        {
            ScorePageViewModel = (ScorePageViewModel) DataContext;
            Canvas = this.FindControl<Canvas>("Canvas");
            DragAndDropContext = ScorePageViewModel.ScoreViewModel.DragAndDropContext;
            // DragAndDropContext.MouseReleased += OnRelease;
            DragAndDropContext.MousePressed += OnPress;
        }

        private Canvas Canvas { get; set; }
        private ScorePageViewModel ScorePageViewModel { get; set; }
        private DragAndDropContext DragAndDropContext { get; set; }

        private void AddImageOnScore(MusicItem musicItem, double x, double y)
        {
            var newMusicItemViewModel = new MusicItemViewModel(musicItem, DragAndDropContext);
            newMusicItemViewModel.MusicItem.Position = (x, y);
            var musicItemView = new MusicItemView(newMusicItemViewModel);
            Canvas.SetLeft(musicItemView, x);
            Canvas.SetTop(musicItemView, y);
            ScorePageViewModel.MusicItemViewModels.Add(newMusicItemViewModel);
            ScorePageViewModel.ScorePage.AddMusicItem(musicItem);
            // Canvas.Children.Add(musicItemView);
        }

        // private void RemoveImageOfScore(IControl musicItemView)
        // {
        //     var musicItemViewModel = ScorePageViewModel.MusicItemViewModels.First(i => i.MusicItem.Equals(DragAndDropContext.MusicItem));
        //     ScorePageViewModel.MusicItemViewModels.Remove(musicItemViewModel);
        //     ScorePageViewModel.ScorePage.RemoveMusicItem(musicItemViewModel.MusicItem);
        //     Canvas.Children.Remove(musicItemView as MusicItemView);
        // }

        private void OnRelease(object sender, EventArgs args)
        {
            if (!(args is PointerReleasedEventArgs e) ||
                e.InputModifiers == InputModifiers.LeftMouseButton ||
                DragAndDropContext.IsDragging == false)
                return;
            var point = e.GetPosition(Canvas);
            var x = point.X - 32;
            var y = point.Y - 32;
            if (!(x > 0) || !(y > 0)) return;
            AddImageOnScore(DragAndDropContext.MusicItem, x, y);
            DragAndDropContext.IsDragging = false;
        }

        private void OnPress(object sender, EventArgs args)
        {
            if (!(args is PointerPressedContextEventArgs pointerPressedContextEventArgs) ||
                !(pointerPressedContextEventArgs.SenderGrandParent is Canvas canvas) ||
                !(canvas.DataContext is ScorePageViewModel)) return;
            switch (pointerPressedContextEventArgs.PointerPressedEventArgs.InputModifiers)
            {
                case InputModifiers.LeftMouseButton:
                    DragAndDropContext.IsDragging = true;
                    var musicItem =
                        ScorePageViewModel.MusicItemViewModels.First(i =>
                            i.MusicItem.Equals(DragAndDropContext.MusicItem));
                    DragAndDropContext.MusicItem = musicItem?.MusicItem;
                    break;
                // case InputModifiers.RightMouseButton:
                //     DragAndDropContext.IsDragging = false;
                //     var musicItemView = pointerPressedContextEventArgs.Sender;
                //     RemoveImageOfScore(musicItemView);
                //     break;
            }
        }
    }
}
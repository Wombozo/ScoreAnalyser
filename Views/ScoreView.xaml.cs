using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using ScoreAnalyser.Models;
using ScoreAnalyser.ViewModels;

namespace ScoreAnalyser.Views
{
    public class ScoreView : UserControl
    {
        public ScoreView() => InitializeComponent();
        private ScoreViewModel ScoreViewModel { get; set; }
        private TabControl TabControl { get; set; }
        private DragAndDropContext DragAndDropContext { get; set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContextChanged += WhenDataContextChanged;
        }

        private void WhenDataContextChanged(object o, EventArgs args)
        {
            ScoreViewModel = (ScoreViewModel) DataContext;
            TabControl = this.FindControl<TabControl>("TabControl");
            TabControl.SelectionChanged += TabItemChanged;
            DragAndDropContext = ScoreViewModel.DragAndDropContext;
            DragAndDropContext.MouseReleased += AddItemOnScore;
            DragAndDropContext.MousePressed += OnPress;
        }

        private void OnPress(object e, EventArgs args)
        {
            if (!(args is PointerPressedContextEventArgs pointerPressedContextEventArgs) || pointerPressedContextEventArgs.IsInToolbox)
                return;
            switch (pointerPressedContextEventArgs.PointerPressedEventArgs.InputModifiers)
            {
                case InputModifiers.LeftMouseButton:
                    DragAndDropContext.IsDragging = true;
                    var musicItem =
                        ScoreViewModel.SelectedPageViewModel.MusicItemViewModels.First(i =>
                            i.MusicItem.Equals(DragAndDropContext.MusicItem));
                    DragAndDropContext.MusicItem = musicItem.MusicItem;
                    break;
                case InputModifiers.RightMouseButton:
                    DragAndDropContext.IsDragging = false;
                    var musicItemViewModel =
                        ScoreViewModel.SelectedPageViewModel.MusicItemViewModels.First(i => i.MusicItem.Equals(DragAndDropContext.MusicItem));
                    ScoreViewModel.SelectedPageViewModel.RemoveMusicItem(musicItemViewModel);
                    break;
            }
        }

        private void AddItemOnScore(object o, EventArgs args)
        {
            if (!(args is PointerReleasedEventArgs e) ||
                e.InitialPressMouseButton != MouseButton.Left ||
                DragAndDropContext.IsDragging == false)
                return;
            var newMusicItemViewModel = new MusicItemViewModel(DragAndDropContext.MusicItem, DragAndDropContext);
            // newMusicItemViewModel.MusicItem.Position = (x, y);
            ScoreViewModel.SelectedPageViewModel.AddMusicItem(newMusicItemViewModel);
            DragAndDropContext.IsDragging = false;
        }

        private void TabItemChanged(object e, EventArgs evt) => ScoreViewModel.SelectedPageViewModel =
            (ScorePageViewModel) ((TabControl) e).SelectedContent;
    }
}
using System.Runtime.CompilerServices;
using ReactiveUI;
using ScoreAnalyser.Models;

namespace ScoreAnalyser.ViewModels
{
    public class MusicItemViewModel : ViewModelBase
    {
        public MusicItemViewModel()
        {
        }
        public MusicItemViewModel(string itemPath, DragAndDropContext dragAndDropContext)
        {
            DragAndDropContext = dragAndDropContext;
            MusicItem = new MusicItem(itemPath);
            ItemPath = itemPath;
        }

        public MusicItemViewModel(DragAndDropContext dragAndDropContext, double x, double y)
        {
            DragAndDropContext = dragAndDropContext;
            ItemPath = dragAndDropContext.MusicItemViewModel.ItemPath;
            (X, Y) = (x, y);
            MusicItem = new MusicItem(ItemPath, x, y);
            IsInToolbox = false;
        }

        public MusicItemViewModel(string itemPath, DragAndDropContext dragAndDropContext, double x, double y)
            : this(itemPath, dragAndDropContext)
        {
            (X, Y) = (x, y);
            IsInToolbox = false;
        }

        private MusicItem MusicItem { get; }
        public DragAndDropContext DragAndDropContext { get; set; }
        public readonly bool IsInToolbox = true;
        public string ItemPath { get; set; }
        private double _size = 50;
        public double Size { get => _size; set => this.RaiseAndSetIfChanged(ref _size, value); }
        public double X { get => _x; set => this.RaiseAndSetIfChanged(ref _x, value); }
        public double Y { get => _y; set => this.RaiseAndSetIfChanged(ref _y, value); }
        private double _x;
        private double _y;
        public MusicItem GetMusicItem() => MusicItem;
    }
}
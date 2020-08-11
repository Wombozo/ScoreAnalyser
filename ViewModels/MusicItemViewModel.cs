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
        private MusicItem MusicItem { get; }
        public DragAndDropContext DragAndDropContext { get; }
        public readonly bool IsInToolbox = true;
        public string ItemPath { get; set; }
        public double X { get => _x; set => this.RaiseAndSetIfChanged(ref _x, value); }
        public double Y { get => _y; set => this.RaiseAndSetIfChanged(ref _y, value); }
        private double _x;
        private double _y;
        public MusicItem GetMusicItem() => MusicItem;
    }
}
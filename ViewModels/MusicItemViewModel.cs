using ScoreAnalyser.Models;

namespace ScoreAnalyser.ViewModels
{
    public class MusicItemViewModel : ViewModelBase
    {
        public MusicItemViewModel()
        {
        }

        public MusicItemViewModel(MusicItem musicItem)
        {
            MusicItem = musicItem;
        }

        public MusicItemViewModel(string itemPath, DragAndDropContext dragAndDropContext)
        {
            DragAndDropContext = dragAndDropContext;
            MusicItem = new MusicItem(itemPath);
        }
        public MusicItemViewModel(MusicItem musicItem, DragAndDropContext dragAndDropContext) : this(musicItem)
        {
            DragAndDropContext = dragAndDropContext;
        }

        public MusicItem MusicItem { get; }
        public DragAndDropContext DragAndDropContext { get; }
    }
}
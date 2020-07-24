namespace ScoreAnalyser.ViewModels
{
    public class MusicItemViewModel : ViewModelBase
    {
        public MusicItemViewModel()
        {
        }

        public MusicItemViewModel(string imagePath)
        {
            ImagePath = imagePath;
        }

        public MusicItemViewModel(string imagePath, DragAndDropContext dragAndDropContext) : this(imagePath)
        {
            DragAndDropContext = dragAndDropContext;
        }

        public string ImagePath { get; }
        public DragAndDropContext DragAndDropContext { get; set; }
    }
}
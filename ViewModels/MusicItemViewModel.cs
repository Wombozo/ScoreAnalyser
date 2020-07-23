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
      public string ImagePath { get; }
    }
}

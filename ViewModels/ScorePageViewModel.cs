using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
    public class ScorePageViewModel : ViewModelBase
    {
        public int PageNumber { get; set; }
        public float Scaling
        {
            get => _scaling;
            set => this.RaiseAndSetIfChanged(ref _scaling, value);
        }

        private float _scaling = .5f;
        // public void IncreaseScaling() => Scaling += STEP_SCALING;
        //
        // public void DecreaseScaling() => Scaling = Scaling - STEP_SCALING > 0 ? Scaling - STEP_SCALING : Scaling;

        // private const float STEP_SCALING = .5f;
        // public List<ImageOnScore> ImagesOnScore { get; set; }
        public ScorePageViewModel(int pageNumber)
        {
            PageNumber = pageNumber;
        }
    }
}
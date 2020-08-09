using System.Collections.Generic;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
    public class ScorePageViewModel : ViewModelBase
    {
        public ScorePageViewModel()
        {
            ImagesOnScore = new List<ImageOnScore>();
        }
        public int PageNumber { get; set; }
        public float Scaling
        {
            get => _scaling;
            set => this.RaiseAndSetIfChanged(ref _scaling, value);
        }

        private float _scaling = .5f;
        public void IncreaseScaling() => Scaling += STEP_SCALING;
        public void DecreaseScaling() => Scaling = Scaling - STEP_SCALING > 0 ? Scaling - STEP_SCALING : Scaling;

        private const float STEP_SCALING = .5f;
        public ScoreViewModel ScoreViewModel { get; set; }
        public List<ImageOnScore> ImagesOnScore { get; set; }
        public Bitmap BackgroundBitmap { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
    public class ScoreViewModel : ViewModelBase
    {
        public float Scaling
        {
            get => scaling;
            set => this.RaiseAndSetIfChanged(ref scaling, value);
        }

        private float scaling = .5f;
        public void IncreaseScaling() => Scaling += STEP_SCALING;

        public void DecreaseScaling() => Scaling = Scaling - STEP_SCALING > 0 ? Scaling - STEP_SCALING : Scaling;

        private const float STEP_SCALING = .5f;
        public DragAndDropContext DragAndDropContext { get; set; }

        public ScoreViewModel(DragAndDropContext dragAndDropContext)
        {
            DragAndDropContext = dragAndDropContext;
            ImagesOnScore = new List<ImageOnScore>();
        }

        public List<ImageOnScore> ImagesOnScore { get; set; }
        public IEnumerable<Bitmap> ScorePages { get; set; }

        public void SetScore(string scoreFileName)
        {
            ScorePages = PDFToImageConverter.ConvertPDFToMultipleImages(scoreFileName);
            var scorePages = ScorePages as Bitmap[] ?? ScorePages.ToArray();
            var scoreSize = new ScoreSize(scorePages.First().PixelSize.Width, scorePages.First().PixelSize.Height);
            NotifyAvailableScore(scoreSize);
        }

        public event EventHandler AvailableScore;
        private void NotifyAvailableScore(ScoreSize e) => AvailableScore?.Invoke(this, e);
    }
}
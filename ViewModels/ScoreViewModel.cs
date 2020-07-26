using System;
using System.Collections.Generic;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
    public class ScoreViewModel : ViewModelBase
    {
        public uint Scaling
        {
            get => scaling;
            set => this.RaiseAndSetIfChanged(ref scaling, value);
        }

        private uint scaling = 1;
        public void IncreaseScaling() => Scaling += STEP_SCALING;

        public void DecreaseScaling() => Scaling = Scaling - STEP_SCALING > 0 ? Scaling - STEP_SCALING : Scaling;

        private const int STEP_SCALING = 1;
        public DragAndDropContext DragAndDropContext { get; set; }

        public ScoreViewModel(DragAndDropContext dragAndDropContext)
        {
            DragAndDropContext = dragAndDropContext;
            ImagesOnScore = new List<ImageOnScore>();
        }

        public List<ImageOnScore> ImagesOnScore { get; set; }
        public IEnumerable<Bitmap> ScorePages { get; set; }

        public void SetScore(string scoreFileName) =>
            ScorePages = PDFToImageConverter.ConvertPDFToMultipleImages(scoreFileName);
    }
}
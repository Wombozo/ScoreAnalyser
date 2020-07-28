using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
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

        private ScoreViewModel()
        {
        }

        public List<ImageOnScore> ImagesOnScore { get; set; }
        [XmlIgnore] public Bitmap[] ScorePages { get; set; }

        public void SetScore(string scoreFileName)
        {
            ScorePages = PDFToImageConverter.ConvertPDFToMultipleImages(scoreFileName).ToArray();
            var scoreSize = new ScoreSize(ScorePages[0].PixelSize.Width, ScorePages[0].PixelSize.Height);
            NotifyAvailableScore(scoreSize);
        }

        public event EventHandler AvailableScore;
        private void NotifyAvailableScore(EventArgs e) => AvailableScore?.Invoke(this, e);

    }
}
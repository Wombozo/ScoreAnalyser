using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ScoreAnalyser.Models;

namespace ScoreAnalyser.ViewModels
{
    public class ScoreViewModel : ViewModelBase
    {
        public ScoreViewModel(DragAndDropContext dragAndDropContext)
        {
            ScorePagesVM = new ObservableCollection<ScorePageViewModel>();
            DragAndDropContext = dragAndDropContext;
        }

        public ScoreBoard ScoreBoard { get; set; }
        public ObservableCollection<ScorePageViewModel> ScorePagesVM { get; set; }
        public ScorePageViewModel SelectedPageViewModel { get; set; }
        public DragAndDropContext DragAndDropContext { get; }

        public void IncreaseScaling() => SelectedPageViewModel?.IncreaseScaling();

        public void DecreaseScaling() => SelectedPageViewModel?.DecreaseScaling();

        public void SetNewScore(string scoreFileName)
        {
            if (ScoreBoard != null)
            {
                for(var i = ScorePagesVM.Count - 1; i >= 0; i--)
                    ScorePagesVM.RemoveAt(i);
            }
            var scorePagesBitmap = PDFToImageConverter.ConvertPDFToMultipleImages(scoreFileName).ToArray();
            var scorePages = new List<ScorePage>();
            var numberPages = scorePagesBitmap.Length;
            for (var i = 0; i < numberPages; i++)
            {
                scorePages.Add(new ScorePage(i));
                ScorePagesVM.Add(new ScorePageViewModel
                {
                    PageNumber = i + 1, BackgroundBitmap = scorePagesBitmap[i], ScorePage = scorePages[i],
                    ScoreViewModel = this
                });
            }

            ScoreBoard = new ScoreBoard(scoreFileName, scorePages.ToArray());
            DragAndDropContext.Authorized = true;
        }

        private void RestoreModel()
        {
            if (ScorePagesVM.Count != 0)
            {
                for(var i = ScorePagesVM.Count - 1; i >= 0; i--)
                    ScorePagesVM.RemoveAt(i);
            }
            var scorePagesBitmap = PDFToImageConverter.ConvertPDFToMultipleImages(ScoreBoard.PdfPath).ToArray();
            var numberPages = scorePagesBitmap.Length;
            for (var i = 0; i < numberPages; i++)
            {
                var musicItems = ScoreBoard.ScorePages[i].MusicItems;
                var musicItemsViewModel =
                    musicItems.Select(t => new MusicItemViewModel(t.Path, DragAndDropContext, t.Position.x, t.Position.y)).ToList();

                ScorePagesVM.Add(new ScorePageViewModel
                {
                    PageNumber = i + 1, BackgroundBitmap = scorePagesBitmap[i], ScorePage = ScoreBoard.ScorePages[i],
                    ScoreViewModel = this, Scaling = ScoreBoard.ScorePages[i].Scaling,
                    MusicItemViewModels = new ObservableCollection<MusicItemViewModel>(musicItemsViewModel)
                });
            }
            DragAndDropContext.Authorized = true;
        }
        public void ImportScore(string path)
        {
            var ser = new XmlSerializer(typeof(ScoreBoard));
            using var sr = new StreamReader(path);
            ScoreBoard = ser.Deserialize(sr) as ScoreBoard;
            RestoreModel();
        }
        public void Serialize(string path)
        {
            var xsSubmit = new XmlSerializer(typeof(ScoreBoard));
            var sww = new StringWriter();
            var file = File.Create(path); 
            xsSubmit.Serialize(file, ScoreBoard);
        }
    }
}
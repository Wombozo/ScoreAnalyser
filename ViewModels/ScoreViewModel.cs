using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public int NumberPages { get; set; }
        public DragAndDropContext DragAndDropContext { get; }

        public void IncreaseScaling() => SelectedPageViewModel?.IncreaseScaling();

        public void DecreaseScaling() => SelectedPageViewModel?.DecreaseScaling();

        public void SetNewScore(string scoreFileName)
        {
            var scorePagesBitmap = PDFToImageConverter.ConvertPDFToMultipleImages(scoreFileName).ToArray();
            var scorePages = new List<ScorePage>();
            NumberPages = scorePagesBitmap.Length;
            for (var i = 0; i < NumberPages; i++)
            {
                scorePages.Add(new ScorePage(i));
                ScorePagesVM.Add(new ScorePageViewModel
                    {PageNumber = i + 1, BackgroundBitmap = scorePagesBitmap[i], ScorePage = scorePages[i], ScoreViewModel = this});
            }

            ScoreBoard = new ScoreBoard(scoreFileName, scorePages.ToArray());
            DragAndDropContext.Authorized = true;
        }
    }
}
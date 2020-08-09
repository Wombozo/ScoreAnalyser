using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media.Imaging;
using ScoreAnalyser.Models;

namespace ScoreAnalyser.ViewModels
{
    public class ScoreViewModel : ViewModelBase
    {
        public ScoreViewModel(DragAndDropContext dragAndDropContext)
        {
            //DragAndDropContext = dragAndDropContext;
            ScorePagesVMTabItem = new ObservableCollection<TabItem>();

        }

        public ScoreBoard ScoreBoard { get; set; }
        public ObservableCollection<TabItem> ScorePagesVMTabItem { get; set; }

        public Bitmap[] ScorePagesBitmap { get; set; }
        public int NumberPages { get; set; }

        public void SetNewScore(string scoreFileName)
        {
            ScorePagesBitmap = PDFToImageConverter.ConvertPDFToMultipleImages(scoreFileName).ToArray();
            // var scoreSize = new ScoreSize(ScorePagesBitmap[0].PixelSize.Width, ScorePagesBitmap[0].PixelSize.Height);
            var scorePages = new List<ScorePage>();
            var scorePagesVM = new List<ScorePageViewModel>();
            NumberPages = ScorePagesBitmap.Length;
            for (var i = 0; i < NumberPages; i++)
            {
                scorePages.Add(new ScorePage(i));
                scorePagesVM.Add(new ScorePageViewModel(i));
            }
            ScoreBoard = new ScoreBoard(scoreFileName, scorePages.ToArray());

            for (var i = 0; i < NumberPages; i++)
                ScorePagesVMTabItem.Add(new TabItem{Header = $"Page {i+1}", Content=$"Content {i}"});
        }

        public class TabItem
        {
            public string Header { get; set; }
            public string Content { get; set; }
        }
    }
}
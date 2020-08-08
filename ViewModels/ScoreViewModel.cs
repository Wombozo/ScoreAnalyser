using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ReactiveUI;
using ScoreAnalyser.Models;

namespace ScoreAnalyser.ViewModels
{
    public class ScoreViewModel : ViewModelBase
    {
        public ScoreViewModel(DragAndDropContext dragAndDropContext)
        {
            //DragAndDropContext = dragAndDropContext;
            // CREONS LES PAGES?
        }
        public ScoreBoard ScoreBoard { get; set; }
        public ObservableCollection<ScorePageViewModel> ScorePagesVM { get; set; }
        public Bitmap[] ScorePagesBitmap { get; set; }
        public int NumberPages { get; set; }
        public List<TabItem> TabItems
        {
            get => tabItems;
            set => this.RaiseAndSetIfChanged(ref tabItems, value);
        }
        private List<TabItem> tabItems;

        public void SetNewScore(string scoreFileName)
        {
            ScorePagesBitmap = PDFToImageConverter.ConvertPDFToMultipleImages(scoreFileName).ToArray();
            ScorePagesVM = new ObservableCollection<ScorePageViewModel>();
            var scoreSize = new ScoreSize(ScorePagesBitmap[0].PixelSize.Width, ScorePagesBitmap[0].PixelSize.Height);
            var scorePages = new List<ScorePage>();
            NumberPages = ScorePagesBitmap.Length;
            for (var i = 0; i < NumberPages; i++)
            {
                scorePages.Add(new ScorePage(i));
                ScorePagesVM.Add(new ScorePageViewModel(i));
            }
            ScoreBoard = new ScoreBoard(scoreFileName, scorePages.ToArray());
            CreateTabItems(scoreSize);
        }

        private void CreateTabItems(ScoreSize scoreSize)
        {
            var tabPages = new List<TabItem>();
            for (var i = 0; i < NumberPages; i++)
            {
                var canvas = new Canvas {Width = scoreSize.Width, Height = scoreSize.Height, Background = new ImageBrush(ScorePagesBitmap[i])};
                var scrollViewer = new ScrollViewer
                {
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                    Content = canvas
                };
                tabPages.Add(new TabItem
                {
                    Header = $"Page {i + 1}",
                    Content = scrollViewer
                });
            }
            
            TabItems = tabPages;
        }
    }
}
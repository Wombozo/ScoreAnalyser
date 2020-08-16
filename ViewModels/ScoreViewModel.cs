using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ReactiveUI;
using ScoreAnalyser.Models;

namespace ScoreAnalyser.ViewModels
{
    public class ScoreViewModel : ViewModelBase
    {
        public ScoreViewModel(DragAndDropContext dragAndDropContext, InfoText infoText)
        {
            ScorePagesVM = new ObservableCollection<ScorePageViewModel>();
            DragAndDropContext = dragAndDropContext;
            InfoText = infoText;
        }

        public ScoreBoard ScoreBoard { get; set; }
        public InfoText InfoText { get; set; }
        public ObservableCollection<ScorePageViewModel> ScorePagesVM { get; set; }
        public ScorePageViewModel SelectedPageViewModel { get; set; }
        public DragAndDropContext DragAndDropContext { get; }

        public string BackGroundImagePath
        {
            get => backGroundImagePath;
            set => this.RaiseAndSetIfChanged(ref backGroundImagePath, value);
        }

        private string backGroundImagePath = Directory.GetCurrentDirectory() + "/Assets/backgrounds/score.png";

        public bool SizeItemsVisible
        {
            get => _sizeItemsVisible;
            set => this.RaiseAndSetIfChanged(ref _sizeItemsVisible, value);
        }

        private bool _sizeItemsVisible;

        private double _musicItemsSize = 50;

        public double MusicItemsSize
        {
            get => _musicItemsSize;
            set
            {
                ScoreBoard.ItemsSize = _musicItemsSize;
                ScorePagesVM.ToList().ForEach(scorePageVM => scorePageVM.UpdateItemsSize(_musicItemsSize));
                this.RaiseAndSetIfChanged(ref _musicItemsSize, value);
            }
        }

        public void IncreaseScaling() => SelectedPageViewModel?.IncreaseScaling();

        public void DecreaseScaling() => SelectedPageViewModel?.DecreaseScaling();

        public void SetNewScore(string scoreFileName)
        {
            InfoText.NewMessage("Setting new Score...");
            if (ScoreBoard != null)
            {
                for (var i = ScorePagesVM.Count - 1; i >= 0; i--)
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

            ScoreBoard = new ScoreBoard(scoreFileName, scorePages.ToArray(), _musicItemsSize);
            DragAndDropContext.Authorized = true;
            InfoText.NewMessage("Score set !");
        }

        private void RestoreModel()
        {
            InfoText.NewMessage("Restoring Model...");
            if (ScorePagesVM.Count != 0)
            {
                for (var i = ScorePagesVM.Count - 1; i >= 0; i--)
                    ScorePagesVM.RemoveAt(i);
            }

            try
            {
                var scorePagesBitmap = PDFToImageConverter.ConvertPDFToMultipleImages(ScoreBoard.PdfPath).ToArray();
                var numberPages = scorePagesBitmap.Length;
                for (var i = 0; i < numberPages; i++)
                {
                    var musicItems = ScoreBoard.ScorePages[i].MusicItems;

                    var musicItemsViewModel =
                        musicItems.Select(t =>
                            new MusicItemViewModel(Directory.GetCurrentDirectory() + "/Assets/png" + t.Path, DragAndDropContext, t.Position.x, t.Position.y)).ToList();


                    ScorePagesVM.Add(new ScorePageViewModel
                    {
                        PageNumber = i + 1, BackgroundBitmap = scorePagesBitmap[i],
                        ScorePage = ScoreBoard.ScorePages[i],
                        ScoreViewModel = this, Scaling = ScoreBoard.ScorePages[i].Scaling,
                        MusicItemViewModels = new ObservableCollection<MusicItemViewModel>(musicItemsViewModel)
                    });
                }

                _musicItemsSize = ScoreBoard.ItemsSize;
                MusicItemsSize = _musicItemsSize;
                DragAndDropContext.Authorized = true;
                InfoText.NewMessage("Project restored !");
            }
            catch (FileNotFoundException e)
            {
                InfoText.NewAlertMessage($"Cannot find file : {e.FileName}");
                throw new FileNotFoundException();
            }
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
            InfoText.NewMessage("Saving project to XML...");
            var xsSubmit = new XmlSerializer(typeof(ScoreBoard));
            var file = File.Create(path);
            xsSubmit.Serialize(file, ScoreBoard);
            InfoText.NewMessage("Project saved !");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace ScoreAnalyser.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            DragAndDropContext = new DragAndDropContext();
            DominantToolbox = new DominantToolboxViewModel(DragAndDropContext);
            TonicToolbox = new TonicToolboxViewModel(DragAndDropContext);
            PredominantToolbox = new PredominantToolboxViewModel(DragAndDropContext);
            MiscToolbox = new MiscToolboxViewModel(DragAndDropContext);
            Score = new ScoreViewModel(DragAndDropContext);
        }

        public DominantToolboxViewModel DominantToolbox { get; }
        public TonicToolboxViewModel TonicToolbox { get; }
        public PredominantToolboxViewModel PredominantToolbox { get; }
        public MiscToolboxViewModel MiscToolbox { get; }
        public ScoreViewModel Score { get; }
        public DragAndDropContext DragAndDropContext { get; }

        public void IncreaseScaling() => Score.IncreaseScaling();
        public void DecreaseScaling() => Score.DecreaseScaling();

        public async Task ImportPDF(Window parentWindow)
        {
            var openFileDialog = new OpenFileDialog
            {
                Directory = Environment.OSVersion.Platform == PlatformID.Unix ||
                            Environment.OSVersion.Platform == PlatformID.MacOSX
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%"),
                Title = "Select PDF score",
                AllowMultiple = false
            };
            var filter = new FileDialogFilter {Extensions = new List<string> {"pdf"}, Name = "PDF files"};
            openFileDialog.Filters = new List<FileDialogFilter> {filter};
            var result = await openFileDialog.ShowAsync(parentWindow);
            Score.SetScore(result[0]);
        }

        public object Save => throw new NotImplementedException();
    }

    public class DragAndDropContext
    {
        public string SelectedImageSource { get; set; }
        public bool IsDragging = false;
        public bool Authorized = false;
        public event EventHandler MouseReleased;
        public void NotifyReleased(EventArgs e) => MouseReleased?.Invoke(this, e);
    }
}
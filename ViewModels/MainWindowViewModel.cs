using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
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
            KeyMajorToolbox = new KeyMajorToolboxViewModel(DragAndDropContext);
            KeyMinorToolbox = new KeyMinorToolboxViewModel(DragAndDropContext);
            Score = new ScoreViewModel(DragAndDropContext);
        }

        public DominantToolboxViewModel DominantToolbox { get; }
        public TonicToolboxViewModel TonicToolbox { get; }
        public PredominantToolboxViewModel PredominantToolbox { get; }
        public MiscToolboxViewModel MiscToolbox { get; }
        public KeyMajorToolboxViewModel KeyMajorToolbox { get; }
        public KeyMinorToolboxViewModel KeyMinorToolbox { get; }
        public ScoreViewModel Score { get; }
        public DragAndDropContext DragAndDropContext { get; }

        // public void IncreaseScaling() => Score.IncreaseScaling();
        // public void DecreaseScaling() => Score.DecreaseScaling();
        public bool ProgressBarVisible { get; set; }

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
            ProgressBarVisible = true;
            Score.SetNewScore(result[0]);
            ProgressBarVisible = false;
        }

        public void Save()
        {
            // if (Score.ScorePages == null) return;
            var xsSubmit = new XmlSerializer(typeof(ScoreViewModel));
        
            var sww = new StringWriter();
            var writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented };
            xsSubmit.Serialize(writer, Score);
            var xml = sww.ToString();
        }
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
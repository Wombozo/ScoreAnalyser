using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;

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
        public void IncreaseScaling() => Score.IncreaseScaling();
        public void DecreaseScaling() => Score.DecreaseScaling();

        public async Task OpenProject(Window parentWindow)
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
            var filter = new FileDialogFilter {Extensions = new List<string> {"xml"}, Name = "XML files"};
            openFileDialog.Filters = new List<FileDialogFilter> {filter};
            var result = await openFileDialog.ShowAsync(parentWindow);
            Score.ImportScore(result[0]);
        }
        public async Task ImportPDF(Window parentWindow)
        {
            InfoText = "Importing PDF";
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
            Score.SetNewScore(result[0]);
            InfoText = "";
        }

        public async Task Save(Window parentWindow)
        {
            InfoText = "Saving project";
            var saveFileDialog = new SaveFileDialog()
            {
                Directory = Environment.OSVersion.Platform == PlatformID.Unix ||
                            Environment.OSVersion.Platform == PlatformID.MacOSX
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%"),
                Title = "Save project as ...",
            };
            var filter = new FileDialogFilter {Extensions = new List<string> {"xml"}, Name = "XML files"};
            saveFileDialog.Filters = new List<FileDialogFilter> {filter};
            var result = await saveFileDialog.ShowAsync(parentWindow);
            Score.Serialize(result);
            InfoText = "";
        }

        public void ShowSizeItems() => Score.SizeItemsVisible = !Score.SizeItemsVisible;
        public string InfoText
        {
            get => _infoText;
            set => this.RaiseAndSetIfChanged(ref _infoText, value);
        }

        private string _infoText = "Welcome to ScoreAnalyser. Import a project or start a new one !";

        private const float _maxToolboxWidth = 200;
        private const float _minToolboxWidth = .1f;
        public void ShowHideItems() => ToolBoxWidth = ToolBoxWidth < 1 ? _maxToolboxWidth : _minToolboxWidth;

        public float ToolBoxWidth
        {
            get => _toolBoxWidth;
            set => this.RaiseAndSetIfChanged(ref _toolBoxWidth, value);
        }
        private float _toolBoxWidth = _maxToolboxWidth;
    }

    public class DragAndDropContext
    {
        public MusicItemViewModel MusicItemViewModel { get; set; }
        public bool IsDragging = false;
        public bool Authorized = false;
        public event EventHandler MouseReleased;
        public event EventHandler MousePressed;
        public void NotifyReleased(EventArgs e) => MouseReleased?.Invoke(this, e);
        public void NotifyPressed(EventArgs e) => MousePressed?.Invoke(this, e);
    }
}
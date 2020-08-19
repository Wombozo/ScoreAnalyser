using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            InfoText = new InfoText();
            DragAndDropContext = new DragAndDropContext();
            DominantToolbox = new DominantToolboxViewModel(DragAndDropContext);
            TonicToolbox = new TonicToolboxViewModel(DragAndDropContext);
            PredominantToolbox = new PredominantToolboxViewModel(DragAndDropContext);
            MiscToolbox = new MiscToolboxViewModel(DragAndDropContext);
            KeyMajorToolbox = new KeyMajorToolboxViewModel(DragAndDropContext);
            KeyMinorToolbox = new KeyMinorToolboxViewModel(DragAndDropContext);
            Score = new ScoreViewModel(DragAndDropContext, InfoText);
        }

        public InfoText InfoText { get; set; }
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
            InfoText.NewMessage("Opening new project");
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
            try
            {
                Score.ImportScore(result[0]);
                SaveEnable = true;
                InfoText.NewMessage($"Project {result[0]} restored !");
            }
            catch(FileNotFoundException){}
        }
        public async Task ImportPDF(Window parentWindow)
        {
            InfoText.NewMessage("Importing PDF");
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
            InfoText.Empty();
        }

        public void Save(Window parentWindow) => Score.Serialize(Score.ProjectPath);

        public async Task SaveAs(Window parentWindow)
        {
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
            SaveEnable = true;
        }

        public async Task ExportPDF(Window parentWindow)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Directory = Environment.OSVersion.Platform == PlatformID.Unix ||
                            Environment.OSVersion.Platform == PlatformID.MacOSX
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%"),
                Title = "Export project as PDF ...",
            };
            var filter = new FileDialogFilter {Extensions = new List<string> {"pdf"}, Name = "PDF files"};
            saveFileDialog.Filters = new List<FileDialogFilter> {filter};
            var result = await saveFileDialog.ShowAsync(parentWindow);
            Score.SaveAllPagesTo(result);
            InfoText.NewMessage($"Project exported as {result}");
        }

        public bool SaveEnable
        {
            get => _saveEnable;
            set => this.RaiseAndSetIfChanged(ref _saveEnable, value);
        }

        private bool _saveEnable;
        public void ShowSizeItems() =>ShowItemsSizeState = !ShowItemsSizeState;
        public bool ShowItemsSizeState
        {
            get => _showItemsSizeState;
            set
            {
                Score.SizeItemsVisible = !Score.SizeItemsVisible;
                this.RaiseAndSetIfChanged(ref _showItemsSizeState, value);
            }
        }
        private bool _showItemsSizeState;

        private const float _maxToolboxWidth = 200;
        private const float _minToolboxWidth = .1f;

        public void ShowHideItems() => ShowItemsState = !ShowItemsState;
        public bool ShowItemsState
        {
            get => _showItemsState;
            set
            {
                ToolBoxWidth = ToolBoxWidth < 1 ? _maxToolboxWidth : _minToolboxWidth;
                this.RaiseAndSetIfChanged(ref _showItemsState, value);
            }
        }
        private bool _showItemsState = true;
        public float ToolBoxWidth
        {
            get => _toolBoxWidth;
            set => this.RaiseAndSetIfChanged(ref _toolBoxWidth, value);
        }
        private float _toolBoxWidth = _maxToolboxWidth;
    }
}
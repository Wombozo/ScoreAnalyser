using System;

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

        public object Open => throw new NotImplementedException();

        public object Save => throw new NotImplementedException();
    }

    public class DragAndDropContext
    {
        public string SelectedImageSource { get; set; }
        public bool isDragging = false;
        public event EventHandler MousePressed;
        public event EventHandler MouseReleased;
        public void NotifyPressed(EventArgs e) => MousePressed?.Invoke(this, e);
        public void NotifyReleased(EventArgs e) => MouseReleased?.Invoke(this, e);
    }
}
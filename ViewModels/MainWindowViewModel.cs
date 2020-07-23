using System;

namespace ScoreAnalyser.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            DominantToolbox = new DominantToolboxViewModel();
            TonicToolbox = new TonicToolboxViewModel();
            PredominantToolbox = new PredominantToolboxViewModel();
            Score = new ScoreViewModel();
        }

        public DominantToolboxViewModel DominantToolbox { get; }
        public TonicToolboxViewModel TonicToolbox { get; }
        public PredominantToolboxViewModel PredominantToolbox { get; }
        public ScoreViewModel Score { get; }

        public void KeyPressedScale(object e) => Score.OnKeyPressedScale(e);

        public object Open => throw new NotImplementedException();

        public object Save => throw new NotImplementedException();
    }
}
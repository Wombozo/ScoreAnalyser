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
            MiscToolbox = new MiscToolboxViewModel();
            Score = new ScoreViewModel();
        }

        public DominantToolboxViewModel DominantToolbox { get; }
        public TonicToolboxViewModel TonicToolbox { get; }
        public PredominantToolboxViewModel PredominantToolbox { get; }
        public MiscToolboxViewModel MiscToolbox { get; }
        public ScoreViewModel Score { get; }

        public void IncreaseScaling() => Score.IncreaseScaling();
        public void DecreaseScaling() => Score.DecreaseScaling();

        public object Open => throw new NotImplementedException();

        public object Save => throw new NotImplementedException();
    }
}
using System;
using Avalonia.Input;
using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
    public class ScoreViewModel : ViewModelBase
    {
        public uint Scaling
        {
            get => scaling;
            set => this.RaiseAndSetIfChanged(ref scaling, value);
        }

        private uint scaling = 1;
        public void IncreaseScaling() => Scaling += STEP_SCALING;
        public void DecreaseScaling() => Scaling -= STEP_SCALING;
        private const int STEP_SCALING = 5;
    }
}
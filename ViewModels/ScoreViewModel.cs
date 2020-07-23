using System;
using Avalonia.Input;
using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
    // public delegate void DScoreEventAction(object key);
    public class ScoreViewModel : ViewModelBase
    {
        public int Scaling
        {
            get => scaling;
            set => this.RaiseAndSetIfChanged(ref scaling, value);
        }

        private int scaling = 1;
        public void SetScaling() => Scaling += 10;
        public void OnKeyPressedScale(object key) => KeyPressedScale = key;

        public object KeyPressedScale
        {
            get => keyPressedScale;
            set => this.RaiseAndSetIfChanged(ref keyPressedScale, value);
        }
        
        private object keyPressedScale;
    }
}
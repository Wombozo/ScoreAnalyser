using System.Collections.ObjectModel;
using Avalonia.Input;
using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
    public class ScoreViewModel : ViewModelBase
    {
        public int Scaling
        {
            get => scaling; 
            set => this.RaiseAndSetIfChanged(ref scaling, value);
        }
        private int scaling;
        public void KeyPressedScale(KeyEventArgs e)
        {
            if (e.Key == Key.Add)
            {
                Scaling += 10;
            }
        }
    }
}
using System;
using System.Collections.Generic;
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
        public DragAndDropContext DragAndDropContext { get; set; }

        public ScoreViewModel(DragAndDropContext dragAndDropContext)
        {
            DragAndDropContext = dragAndDropContext;
            ImagesOnScore = new List<ImageOnScore>();
        }

        public List<ImageOnScore> ImagesOnScore { get; set; }
    }
}
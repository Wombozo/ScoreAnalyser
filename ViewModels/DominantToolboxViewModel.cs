using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
  public class DominantToolboxViewModel : HarmonicFunctionToolboxViewModel
  {
    public DominantToolboxViewModel()
    {
      Dominants = new ObservableCollection<DominantViewModel>(FindFiles("/Assets/png/dominant/").Select(f => new DominantViewModel(f)));
    }

    public ObservableCollection<DominantViewModel> Dominants { get; }
  }
}

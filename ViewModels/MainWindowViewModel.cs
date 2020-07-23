using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreAnalyser.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    public MainWindowViewModel()
    {
      DominantToolbox = new DominantToolboxViewModel();
      TonicToolbox = new TonicToolboxViewModel();
      PredominantToolbox = new PredominantToolboxViewModel();
    }
    public string Greeting => "";

    public DominantToolboxViewModel DominantToolbox { get; }
    public TonicToolboxViewModel TonicToolbox { get; }
    public PredominantToolboxViewModel PredominantToolbox { get; }

    public object Open
    {
      get { throw new NotImplementedException(); }
    }

    public object Save
    {
      get { throw new NotImplementedException(); }
    }
  }
}

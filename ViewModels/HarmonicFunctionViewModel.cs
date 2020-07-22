using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
    public class HarmonicFunctionViewModel : ViewModelBase
    {
      public HarmonicFunctionViewModel(string path)
      {
          Path = path;
      }
      public string Path { get; }
    }
}

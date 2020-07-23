using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
    public class MusicItemViewModel : ViewModelBase
    {
      public MusicItemViewModel(string path)
      {
          Path = path;
      }
      public string Path { get; }

      public object PanelItem { get; set; }

      public void SelectItem(object item)
      {
          
      }
    }
}

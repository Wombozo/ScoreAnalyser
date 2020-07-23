using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
  public class MusicItemToolboxViewModel : ViewModelBase
  {
    protected static IEnumerable<string> FindFiles(string assetPngPath)
    {
      var projectPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
      var files = Directory.GetFiles(projectPath + assetPngPath)
          .OrderBy(f => int.Parse(Path.GetFileNameWithoutExtension(f).Split('_')[0]));
      return files;
    }
  }
}

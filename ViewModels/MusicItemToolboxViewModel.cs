using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
  public class MusicItemToolboxViewModel : ViewModelBase
  {
    protected static IEnumerable<string> FindFiles(string assetPngPath)
    {
      var projectPath = Directory.GetCurrentDirectory();
      var files = Directory.GetFiles(projectPath + assetPngPath)
          .OrderBy(f => int.Parse(Path.GetFileNameWithoutExtension(f).Split('_')[0]));
      return files;
    }
  }
}

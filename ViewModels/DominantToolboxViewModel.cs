using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
  public class DominantToolboxViewModel : MusicItemToolboxViewModel
  {
    public DominantToolboxViewModel()
    {
      Dominants = new ObservableCollection<DominantViewModel>(FindFiles("/Assets/png/dominant/").Select(f => new DominantViewModel(f)));
    }

    public ObservableCollection<DominantViewModel> Dominants { get; }
  }
}

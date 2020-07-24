using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class DominantToolboxViewModel : MusicItemToolboxViewModel
    {
        public DominantToolboxViewModel()
        {
            Dominants = new ObservableCollection<MusicItemViewModel>(FindFiles("/Assets/png/dominant/")
                .Select(f => new MusicItemViewModel(f)));
        }

        public ObservableCollection<MusicItemViewModel> Dominants { get; }
    }
}
using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class PredominantToolboxViewModel : MusicItemToolboxViewModel
    {
        public PredominantToolboxViewModel()
        {
            Predominants = new ObservableCollection<MusicItemViewModel>(FindFiles("/Assets/png/predominant/")
                .Select(f => new MusicItemViewModel(f)));
        }

        public ObservableCollection<MusicItemViewModel> Predominants { get; }
    }
}
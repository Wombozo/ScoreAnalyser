using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class PredominantToolboxViewModel : MusicItemToolboxViewModel
    {
        public PredominantToolboxViewModel()
        {
            Predominants = new ObservableCollection<PredominantViewModel>(FindFiles("/Assets/png/predominant/")
                .Select(f => new PredominantViewModel(f)));
        }

        public ObservableCollection<PredominantViewModel> Predominants { get; }
    }
}
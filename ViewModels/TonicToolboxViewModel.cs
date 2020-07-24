using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class TonicToolboxViewModel : MusicItemToolboxViewModel
    {
        public TonicToolboxViewModel()
        {
            Tonics = new ObservableCollection<MusicItemViewModel>(FindFiles("/Assets/png/tonic/").Select(f => new MusicItemViewModel(f)));
        }

        public ObservableCollection<MusicItemViewModel> Tonics { get; }
    }
}
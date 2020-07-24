using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class MiscToolboxViewModel : MusicItemToolboxViewModel
    {
        public MiscToolboxViewModel()
        {
            Miscs = new ObservableCollection<MusicItemViewModel>(FindFiles("/Assets/png/misc/")
                .Select(f => new MusicItemViewModel(f)));
        }

        public ObservableCollection<MusicItemViewModel> Miscs { get; }
    }
}
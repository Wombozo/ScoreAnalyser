using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class MiscToolboxViewModel : MusicItemToolboxViewModel
    {
        public MiscToolboxViewModel()
        {
            Miscs = new ObservableCollection<MiscViewModel>(FindFiles("/Assets/png/misc/")
                .Select(f => new MiscViewModel(f)));
        }

        public ObservableCollection<MiscViewModel> Miscs { get; }
    }
}
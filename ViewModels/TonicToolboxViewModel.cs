using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class TonicToolboxViewModel : MusicItemToolboxViewModel
    {
        public TonicToolboxViewModel()
        {
            Tonics = new ObservableCollection<TonicViewModel>(FindFiles("/Assets/png/tonic/").Select(f => new TonicViewModel(f)));
        }

        public ObservableCollection<TonicViewModel> Tonics { get; }
    }
}
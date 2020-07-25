using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class DominantToolboxViewModel : MusicItemToolboxViewModel
    {
        public DominantToolboxViewModel(DragAndDropContext dragAndDropContext)
        {
            Dominants = new ObservableCollection<MusicItemViewModel>(FindFiles("/Assets/png/dominant/")
                .Select(f => new MusicItemViewModel(f, dragAndDropContext)));
        }

        public ObservableCollection<MusicItemViewModel> Dominants { get; }
    }
}
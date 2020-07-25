using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class PredominantToolboxViewModel : MusicItemToolboxViewModel
    {
        public PredominantToolboxViewModel(DragAndDropContext dragAndDropContext)
        {
            Predominants = new ObservableCollection<MusicItemViewModel>(FindFiles("/Assets/png/predominant/")
                .Select(f => new MusicItemViewModel(f, dragAndDropContext)));
        }

        public ObservableCollection<MusicItemViewModel> Predominants { get; }
    }
}
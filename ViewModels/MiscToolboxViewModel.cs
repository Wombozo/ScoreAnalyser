using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class MiscToolboxViewModel : MusicItemToolboxViewModel
    {
        public MiscToolboxViewModel(DragAndDropContext dragAndDropContext)
        {
            Miscs = new ObservableCollection<MusicItemViewModel>(FindFiles("/Assets/png/misc/")
                .Select(f => new MusicItemViewModel(f, dragAndDropContext)));
        }

        public ObservableCollection<MusicItemViewModel> Miscs { get; }
    }
}
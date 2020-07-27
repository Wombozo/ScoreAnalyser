using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class KeyMinorToolboxViewModel : MusicItemToolboxViewModel
    {
        public KeyMinorToolboxViewModel(DragAndDropContext dragAndDropContext)
        {
            Keys = new ObservableCollection<MusicItemViewModel>(FindFiles("/Assets/png/keys/minor/")
                .Select(f => new MusicItemViewModel(f, dragAndDropContext)));
        }

        public ObservableCollection<MusicItemViewModel> Keys { get; }
    }
}
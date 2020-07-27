using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class KeyMajorToolboxViewModel : MusicItemToolboxViewModel
    {
        public KeyMajorToolboxViewModel(DragAndDropContext dragAndDropContext)
        {
            Keys = new ObservableCollection<MusicItemViewModel>(FindFiles("/Assets/png/keys/major/")
                .Select(f => new MusicItemViewModel(f, dragAndDropContext)));
        }

        public ObservableCollection<MusicItemViewModel> Keys { get; }
    }
}
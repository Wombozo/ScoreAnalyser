using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreAnalyser.ViewModels
{
    public class TonicToolboxViewModel : MusicItemToolboxViewModel
    {
        public TonicToolboxViewModel(DragAndDropContext dragAndDropContext)
        {
            Tonics = new ObservableCollection<MusicItemViewModel>(FindFiles("/Assets/png/tonic/")
                .Select(f => new MusicItemViewModel(f, dragAndDropContext)));
        }

        public ObservableCollection<MusicItemViewModel> Tonics { get; }
    }
}
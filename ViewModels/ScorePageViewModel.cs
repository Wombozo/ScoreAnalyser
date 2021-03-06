using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media.Imaging;
using ReactiveUI;
using ScoreAnalyser.Models;

namespace ScoreAnalyser.ViewModels
{
    public class ScorePageViewModel : ViewModelBase
    {
        public ScorePageViewModel()
        {
            MusicItemViewModels = new ObservableCollection<MusicItemViewModel>();
        }
        public int PageNumber { get; set; }
        public float Scaling
        {
            get => _scaling;
            set => this.RaiseAndSetIfChanged(ref _scaling, value);
        }

        private float _scaling = .5f;

        public void IncreaseScaling()
        {
            Scaling += STEP_SCALING;
            ScorePage.Scaling = Scaling;
        }

        public void DecreaseScaling()
        {
            Scaling = Scaling - STEP_SCALING > 0 ? Scaling - STEP_SCALING : Scaling;
            ScorePage.Scaling = Scaling;
        }
        public void UpdateItemsSize(double size)
        {
            MusicItemViewModels.ToList()
                .ForEach(musicItemViewModel => musicItemViewModel.Size = size);
        }

        private const float STEP_SCALING = .5f;
        public ScoreViewModel ScoreViewModel { get; set; }
        public ObservableCollection<MusicItemViewModel> MusicItemViewModels { get; set; }

        public void AddMusicItem(MusicItemViewModel musicItemViewModel, double size)
        {
            ScorePage.AddMusicItem(musicItemViewModel.GetMusicItem());
            MusicItemViewModels.Add(musicItemViewModel);
            musicItemViewModel.Size = size;
        }

        public void RemoveMusicItem(MusicItemViewModel musicItemViewModel)
        {
            ScorePage.RemoveMusicItem(musicItemViewModel.GetMusicItem());
            MusicItemViewModels.Remove(musicItemViewModel);
        }
        public ScorePage ScorePage { get; set; }
        public Bitmap BackgroundBitmap { get; set; }
        public object Canvas { get; set; }
    }
}
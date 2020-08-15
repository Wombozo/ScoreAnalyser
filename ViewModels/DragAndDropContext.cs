using System;

namespace ScoreAnalyser.ViewModels
{
    public class DragAndDropContext
    {
        public MusicItemViewModel MusicItemViewModel { get; set; }
        public bool IsDragging = false;
        public bool Authorized = false;
        public event EventHandler MouseReleased;
        public event EventHandler MousePressed;
        public void NotifyReleased(EventArgs e) => MouseReleased?.Invoke(this, e);
        public void NotifyPressed(EventArgs e) => MousePressed?.Invoke(this, e);
    }
}
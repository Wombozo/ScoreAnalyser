using ReactiveUI;

namespace ScoreAnalyser.ViewModels
{
    public class InfoText : ReactiveObject
    {
        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        private string _text = "Welcome to ScoreAnalyser. Import a project or start a new one !";

        public string Color
        {
            get => _color;
            set => this.RaiseAndSetIfChanged(ref _color, value);
        }

        private string _color = "Black";

        public void NewMessage(string message)
        {
            Color = "Black";
            Text = message;
        }

        public void NewAlertMessage(string message)
        {
            Color = "Red";
            Text = message;
        }

        public void Empty()
        {
            Text = "";
            Color = "Black";
        }
    }
}
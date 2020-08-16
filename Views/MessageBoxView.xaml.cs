using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ScoreAnalyser.Views
{
    public class MessageBoxView : Window
    {
        public MessageBoxView() => InitializeComponent();
        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
        public enum MessageBoxResult
        {
            Cancel,
            Yes
        }
        public static async Task<MessageBoxResult> Show(Window parent, string title, string text)
        {
            var messageBoxView = new MessageBoxView {Title = title};
            messageBoxView.FindControl<TextBlock>("Text").Text = text;
            var buttonPanel = messageBoxView.FindControl<StackPanel>("Buttons");

            var result = MessageBoxResult.Cancel;

            void AddButton(string caption, MessageBoxResult r)
            {
                var button = new Button {Content = caption};
                button.Click += (_, __) =>
                {
                    result = r;
                    messageBoxView.Close();
                };
                buttonPanel.Children.Add(button);
                result = r;
            }

            AddButton("Ok", MessageBoxResult.Yes);
            AddButton("Cancel", MessageBoxResult.Cancel);


            await messageBoxView.ShowDialog(parent);
            return result;
        }
    }
}
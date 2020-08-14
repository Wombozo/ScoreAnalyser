using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ScoreAnalyser.Views
{
    public class MessageBoxView : Window
    {
        public MessageBoxView()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public enum MessageBoxButtons
        {
            Ok,
            OkCancel,
            YesNo,
            YesNoCancel
        }

        public enum MessageBoxResult
        {
            Ok,
            Cancel,
            Yes,
            No
        }
        public static Task<MessageBoxResult> Show(Window parent, string title, string text, MessageBoxButtons buttons)
        {
            var messageBoxView = new MessageBoxView {Title = title};
            messageBoxView.FindControl<TextBlock>("Text").Text = text;
            var buttonPanel = messageBoxView.FindControl<StackPanel>("Buttons");

            var result = MessageBoxResult.Ok;

            void AddButton(string caption, MessageBoxResult r, bool def = false)
            {
                var button = new Button {Content = caption};
                button.Click += (_, __) => { 
                    result = r;
                    messageBoxView.Close();
                };
                buttonPanel.Children.Add(button);
                if (def)
                    result = r;
            }

            switch (buttons)
            {
                case MessageBoxButtons.Ok:
                case MessageBoxButtons.OkCancel:
                    AddButton("Ok", MessageBoxResult.Ok, true);
                    break;
                case MessageBoxButtons.YesNo:
                case MessageBoxButtons.YesNoCancel:
                    AddButton("Yes", MessageBoxResult.Yes);
                    AddButton("No", MessageBoxResult.No, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttons), buttons, null);
            }

            if (buttons == MessageBoxButtons.OkCancel || buttons == MessageBoxButtons.YesNoCancel)
                AddButton("Cancel", MessageBoxResult.Cancel, true);


            var tcs = new TaskCompletionSource<MessageBoxResult>();
            messageBoxView.Closed += delegate { tcs.TrySetResult(result); };
            if (parent != null)
                messageBoxView.ShowDialog(parent);
            else messageBoxView.Show();
            return tcs.Task;
        }
    }
}
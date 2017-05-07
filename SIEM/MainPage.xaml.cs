using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SIEM
{
    public partial class MainPage : Page
    {
        public MainPage(Frame frame)
        {
            this.InitializeComponent();
            this.ShellSplitView.Content = frame;
        }
        private void OnMenuButtonClicked(object sender, RoutedEventArgs e)
        {
            ShellSplitView.IsPaneOpen = !ShellSplitView.IsPaneOpen;
            ((RadioButton)sender).IsChecked = false;
        }

        private void OnHomeButtonChecked(object sender, RoutedEventArgs e)
        {
            ShellSplitView.IsPaneOpen = false;
            if (ShellSplitView.Content != null)
                ((Frame)ShellSplitView.Content).Navigate(typeof(HomePage));
        }

        private void OnFileButtonChecked(object sender, RoutedEventArgs e)
        {
            ShellSplitView.IsPaneOpen = false;
            if (ShellSplitView.Content != null)
                ((Frame)ShellSplitView.Content).Navigate(typeof(FileView));
        }
    }
}

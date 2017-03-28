using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SIEM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FileView : Page
    {
        FileIO fIO = new FileIO();
        public FileView()
        {
            this.InitializeComponent();
            this.ViewModel = new FileDataVM();
            if (ViewModel.RecentFiles.Count != 0)
            {
                FileList.Visibility = Visibility.Visible;
                FileEmpty.Visibility = Visibility.Collapsed;
            }
        }

        public FileDataVM ViewModel { get; set; }

        private void FileAdd_Click(object sender, RoutedEventArgs e)
        {
            fIO.OpenCSV();
            if (ViewModel.RecentFiles.Count != 0)
            {
                FileList.Visibility = Visibility.Visible;
                FileEmpty.Visibility = Visibility.Collapsed;
                Bindings.Update();
            }
        }

        private void FileSelect_Click(object sender, RoutedEventArgs e)
        {
            var frame = this.Frame;
            if (frame.Content != null)
                ((Frame)frame.Content).Navigate(typeof(DataView));
        }
    }
}

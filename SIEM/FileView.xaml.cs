using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
                FileViewGrid.Visibility = Visibility.Visible;
                FileEmpty.Visibility = Visibility.Collapsed;
            }
        }

        public FileDataVM ViewModel { get; set; }

        private void FileAdd_Click(object sender, RoutedEventArgs e)
        {
            fIO.OpenCSV();
            if (ViewModel.RecentFiles.Count != 0)
            {
                FileViewGrid.Visibility = Visibility.Visible;
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

        private async void FileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCSVPRing.IsActive = true;
            await ViewModel.RecentFiles.ElementAt(FileList.SelectedIndex).GetCSVDataAsync();
            for(int c = 0; c < ViewModel.RecentFiles.ElementAt(FileList.SelectedIndex).csvData.GetLength(0); c++)
            {
                for(int r = 0; r < ViewModel.RecentFiles.ElementAt(FileList.SelectedIndex).csvData.GetLength(1); r++)
                {
                    Debug.Write(String.Format("{0}\t", ViewModel.RecentFiles.ElementAt(FileList.SelectedIndex).csvData[c, r]));
                }
                Debug.Write("\n");
            }
            LoadCSVPRing.IsActive = false;
        }
    }
}

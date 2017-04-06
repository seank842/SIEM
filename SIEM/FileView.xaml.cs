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
using Syncfusion.UI.Xaml.Grid;
using System.ComponentModel;

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
            InitializeComponent();
            ViewModel = new FileDataVM();
            SfDataGrid dataGrid = new SfDataGrid();
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
                OnPropertyChanged("File added");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        private void FileSelect_Click(object sender, RoutedEventArgs e)
        {
            var frame = this.Frame;
            if (frame.Content != null)
                ((Frame)frame.Content).Navigate(typeof(DataView));
        }
        public CsvDataVM CDViewModel { get; set; }
        private async void FileList_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            LoadCSVPRing.IsActive = true;
            await ViewModel.RecentFiles.ElementAt(FileList.SelectedIndex).SetCSVData();
            CDViewModel = new CsvDataVM(ViewModel.RecentFiles.ElementAt(FileList.SelectedIndex).csvData);
            dataGrid.ItemsSource = CDViewModel.CsvData;
            ViewModel.RecentFiles.ElementAt(FileList.SelectedIndex).csvData = null;
            LoadCSVPRing.IsActive = false;
            dataGrid.Visibility = Visibility.Visible;
        }
    }
}

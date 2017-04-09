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
using Syncfusion.UI.Xaml.Charts;
using System.ComponentModel;
using System.Text;

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
            //SfChart dataChart = new SfChart();
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

        private async void FileList_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            LoadCSVPRing.IsActive = true;
            if (ViewModel.RecentFiles.ElementAt(FileList.SelectedIndex).CDViewModel == null)
            {
                await ViewModel.RecentFiles.ElementAt(FileList.SelectedIndex).SetCSVData();
            }
            dataGrid.ItemsSource = ViewModel.RecentFiles.ElementAt(FileList.SelectedIndex).CDViewModel.CsvData;
            LoadCSVPRing.IsActive = false;
            dataGrid.Visibility = Visibility.Visible;
        }

        private void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {

        }

        private async void Compare_Button_Click(object sender, RoutedEventArgs e)
        {
            int items = 0;
            List<int> data = new List<int>();
            foreach (var item in ViewModel.RecentFiles)
            {
                if (item.IsSelected == true)
                    data.Add(items);
                items++;
            }
            if (data.Count < 2)
            {
                ErrorHandler eHandle = new ErrorHandler();
                eHandle.ErrorHandle(4);
            }
            else if (data.Count > 2)
            {
                ErrorHandler eHandle = new ErrorHandler();
                eHandle.ErrorHandle(5);
            }
            else
            {
                if (ViewModel.RecentFiles.ElementAt(data.ElementAt(0)).hash.SequenceEqual(ViewModel.RecentFiles.ElementAt(data.ElementAt(1)).hash))
                {
                    ContentDialog FileUnableToRead = new ContentDialog()
                    {
                        Title = "Hash Comparison",
                        Content = "These two files are the same, the hash is: " + ToHex(ViewModel.RecentFiles.ElementAt(data.ElementAt(0)).hash, false),
                        PrimaryButtonText = "Ok"
                    };

                    ContentDialogResult result = await FileUnableToRead.ShowAsync();
                }
                else
                {
                    ContentDialog FileUnableToRead = new ContentDialog()
                    {
                        Title = "Hash Comparison",
                        Content = "These two files are not the same,\nThe first file hash is: " + ToHex(ViewModel.RecentFiles.ElementAt(data.ElementAt(0)).hash, false) + "\nThe second file hash is: " + ToHex(ViewModel.RecentFiles.ElementAt(data.ElementAt(1)).hash, false),
                        PrimaryButtonText = "Ok"
                    };

                    ContentDialogResult result = await FileUnableToRead.ShowAsync();
                }
            }
        }

        public static string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }
    }
}

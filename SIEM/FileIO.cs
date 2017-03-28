using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using System.IO;

namespace SIEM
{
    class FileIO
    {
        public string currentFile { get; private set; }
        public string[,] currentCSV { get; private set; }
        public FileIO()
        {
        }

        public async void OpenCSV()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".csv");
            openPicker.FileTypeFilter.Add("*");
            StorageFile file = await openPicker.PickSingleFileAsync();
            
            if (file != null)
            {
                var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;mru.Add(file);
                mru.Add(file, "files");
                currentFile = file.Path;
            }
            else
            {
                fIOErrorHandler(1);
            }
        }

        private async void fIOErrorHandler(int eCode)
        {
            string title = null, content=null;
            switch (eCode)
            {
                case 1:
                    title = "File Read Error!";
                    content = "There was a problem reading the File";
                    break;
                case 2:
                    title = "File Missing Error!";
                    content = "There wasn't a file specified";
                    break;
            }
            ContentDialog FileUnableToRead = new ContentDialog()
            {
                Title = title,
                Content = content+", try again. if this persists contact support!",
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await FileUnableToRead.ShowAsync();
        }

        public void LoadCSV()
        {
            if (currentFile != null)
            {
                // Get the file's text.
                string whole_file = System.IO.File.ReadAllText(currentFile);

                // Split into lines.
                whole_file = whole_file.Replace('\n', '\r');
                string[] lines = whole_file.Split(new char[] { '\r' },
                    StringSplitOptions.RemoveEmptyEntries);

                // See how many rows and columns there are.
                int num_rows = lines.Length;
                int num_cols = lines[0].Split(',').Length;

                // Allocate the data array.
                string[,] values = new string[num_rows, num_cols];

                // Load the array.
                for (int r = 0; r < num_rows; r++)
                {
                    string[] line_r = lines[r].Split(',');
                    for (int c = 0; c < num_cols; c++)
                    {
                        values[r, c] = line_r[c];
                    }
                }

                // Return the values.
                currentCSV = values;
            }
        }
    }
}

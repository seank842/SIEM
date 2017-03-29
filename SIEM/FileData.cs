﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIEM
{
    public class FileData
    {
        public string name { get; set; }
        public string type { get; set; }
        public DateTime created { get; set; }
        public string path { get; set; }
        public string[,] csvData { get; set; }
        public int cols { get; set; }
        public int rows { get; set; }

        public FileData()
        {
        }

        public async Task GetCSVDataAsync()
        {
            if (csvData == null)
            {
                // Get the file's text.
                string whole_file = await Task.Run(async () => (await System.IO.File.OpenText(path).ReadToEndAsync()));

                // Split into lines.
                whole_file = whole_file.Replace('\n', '\r');
                string[] lines = whole_file.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
                // See how many rows and columns there are.
                rows = lines.Length;
                cols = lines[0].Split(',').Length;

                // Allocate the data array.
                csvData = new string[rows, cols];

                // Load the array.
                for (int r = 0; r < rows; r++)
                {
                    string[] line_r = lines[r].Split(',');
                    for (int c = 0; c < cols; c++)
                    {
                        csvData[r, c] = line_r[c];
                    }
                }
            }
            else
            {
                ErrorHandler eH = new ErrorHandler();
                eH.ErrorHandle(2);
            }
        }
    }
    public class FileDataVM {
        private ObservableCollection<FileData> fileDate = new ObservableCollection<FileData>();
        public ObservableCollection<FileData> RecentFiles { get { return this.fileDate; } }

        public FileDataVM()
        {
            init();
        }

        public async void init()
        {
            var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
            foreach (Windows.Storage.AccessCache.AccessListEntry entry in mru.Entries)
            {
                string mruToken = entry.Token;
                if (mruToken != null)
                {
                    Windows.Storage.IStorageItem item = await mru.GetItemAsync(mruToken);
                    RecentFiles.Add(new FileData { name = item.Name, created = item.DateCreated.LocalDateTime, type = "CSV", path=item.Path });
                }
            }
        }
    }
}
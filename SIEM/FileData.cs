using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;

namespace SIEM
{
    public class FileData
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool headers { get; set; }
        public DateTime created { get; set; }
        public string path { get; set; }
        public byte[] hash { get; set; }
        public string[,] csvData { get; set; }
        public bool? IsSelected { get; set; }
        public CsvDataVM CDViewModel { get; set; } 

        public FileData()
        {
        }
        public async Task SetCSVData()
        {
            if (csvData == null)
            {
                // Get the file's text.
                string whole_file = await Task.Run(async () => (await File.OpenText(path).ReadToEndAsync()));
                
                // Split into lines.
                whole_file = whole_file.Replace('\n', '\r');
                string[] lines = whole_file.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
                
                // See how many rows and columns there are.
                int rows = lines.Length;
                int cols = lines[0].Split(',').Length;
                
                // Allocate the data array.
                csvData = new string[rows, cols];
                // Load the array.
                for (int r = 0; r < rows; r++)
                {
                    string[] line_r = lines[r].Split(',');
                    for (int c = 0; c < cols; c++)
                        csvData[r, c] = line_r[c];
                }
                CDViewModel = new CsvDataVM(csvData);
                csvData = null;
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
            Init();
        }

        public async void Init()
        {
            var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
            foreach (Windows.Storage.AccessCache.AccessListEntry entry in mru.Entries)
            {
                string mruToken = entry.Token;
                if (mruToken != null)
                {
                    Windows.Storage.IStorageItem item = await mru.GetItemAsync(mruToken);
                    using (var md5 = System.Security.Cryptography.MD5.Create())
                    using (var stream = await Task.Run(() => (File.Open(item.Path, FileMode.Open))))
                    {
                        RecentFiles.Add(new FileData { name = item.Name, created = item.DateCreated.LocalDateTime, type = "CSV", path = item.Path, hash = md5.ComputeHash(stream), IsSelected = false });
                    }
                }
            }
        }
    }
}
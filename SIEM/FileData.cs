using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.CodeDom;

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
        /*
        public async Task GetCSVDataAsync()
        {
            if (csvData == null)
            {
                // Get the file's text.
                string whole_file = await Task.Run(async () => (await System.IO.File.OpenText(path).ReadToEndAsync()));

                // Split into lines.
                whole_file = whole_file.Replace('\n', '\r');
                string[] lines = whole_file.Split(new char[] { '\r' }, StringSplitOptions.None);

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
        }*/
        
        public async Task GetCSVDataAsync()
        {
            if (csvData == null)
            {
                // Get the file's text.
                string whole_file = await Task.Run(async () => (await System.IO.File.OpenText(path).ReadToEndAsync()));

                // Split into lines.
                whole_file = whole_file.Replace('\n', '\r');
                string[] lines = whole_file.Split(new char[] { '\r' }, StringSplitOptions.None);

                // See how many rows and columns there are.
                rows = lines.Length;
                cols = lines[0].Split(',').Length;
                string className = "csvData";

                string[] line_r = lines[0].Split(',');
                var props = new Dictionary<string, Type>();
                for (int c = 0; c < cols; c++)
                {
                    props.Add("Header" + c, typeof(string));
                }
                CreateClass(className, props);
            }
            else
            {
                ErrorHandler eH = new ErrorHandler();
                eH.ErrorHandle(2);
            }
        }

        private void CreateClass(string name, IDictionary<string, Type> props)
        {
            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });
            var param = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, "data.Dynamic.dll", false);
            param.GenerateExecuteable = false;

            var compUnit = new CodeCompileUnit();
            var ns = new CodeNamespace("Data.Dynamic");
            compUnit.Namespaces.Add(ns);
            ns.Imports.Add(new CodeNamespaceImport("System"));

            var classType = newCodeTypeDeclaration(name);
            classType.Attributes = MemberAttributes.Public;
            ns.Types.Add(classType);
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
                    RecentFiles.Add(new FileData { name = item.Name, created = item.DateCreated.LocalDateTime, type = "CSV", path=item.Path });
                }
            }
        }
    }
}
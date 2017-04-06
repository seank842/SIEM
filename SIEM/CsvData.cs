using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIEM
{
    public class CsvData
    {
        public string header1 { get; set; }
        public string header2 { get; set; }
        public string header3 { get; set; }
        public string header4 { get; set; }
        public string header5 { get; set; }
        public string header6 { get; set; }
        public string header7 { get; set; }
        public string header8 { get; set; }
        public string header9 { get; set; }
        public string header10 { get; set; }
        public string header11 { get; set; }
        public string header12 { get; set; }
        public string header13 { get; set; }
        public string header14 { get; set; }
        public string header15 { get; set; }
        public string header16 { get; set; }
        public string header17 { get; set; }
        public string header18 { get; set; }
        public string header19 { get; set; }
        public string header20 { get; set; }
        public string header21 { get; set; }
        public string header22 { get; set; }
        public string header23 { get; set; }
        public string header24 { get; set; }
        public string header25 { get; set; }
        public string header26 { get; set; }
        public string header27 { get; set; }
        public string header28 { get; set; }
        public string header29 { get; set; }
        public string header30 { get; set; }
        public string header31 { get; set; }
        public string header32 { get; set; }
        public string header33 { get; set; }
        public string header34 { get; set; }
        public string header35 { get; set; }
        public string header36 { get; set; }
        public string header37 { get; set; }
        public string header38 { get; set; }
        public string header39 { get; set; }
        public string header40 { get; set; }
        public string header41 { get; set; }

        public CsvData(string h1, string h2, string h3, string h4, string h5, string h6, string h7, string h8, string h9, string h10, string h11, string h12, string h13, string h14, string h15, string h16, string h17, string h18, string h19, string h20, string h21, string h22, string h23, string h24, string h25, string h26, string h27, string h28, string h29, string h30, string h31, string h32, string h33 , string h34, string h35, string h36, string h37, string h38, string h39, string h40, string h41)
        {
            header1 = h1;
            header2 = h2;
            header3 = h3;
            header4 = h4;
            header5 = h5;
            header6 = h6;
            header7 = h7;
            header8 = h8;
            header9 = h9;
            header10 = h10;
            header11 = h11;
            header12 = h12;
            header13 = h13;
            header14 = h14;
            header15 = h15;
            header16 = h16;
            header17 = h17;
            header18 = h18;
            header19 = h19;
            header20 = h20;
            header21 = h21;
            header22 = h22;
            header23 = h23;
            header24 = h24;
            header25 = h25;
            header26 = h26;
            header27 = h27;
            header28 = h28;
            header29 = h29;
            header30 = h30;
            header31 = h31;
            header32 = h32;
            header33 = h33;
            header34 = h34;
            header35 = h35;
            header36 = h36;
            header37 = h37;
            header38 = h38;
            header39 = h39;
            header40 = h40;
            header41 = h41;
        }
    }
    public class CsvDataVM
    {
        private ObservableCollection<CsvData> _CsvData { get; set; }
        public ObservableCollection<CsvData> CsvData { get { return _CsvData; } set { _CsvData = value; } }

        public CsvDataVM(string[,] data)
        {
            _CsvData = new ObservableCollection<CsvData>();
            importData(data);
        }

        private void importData(string[,] data)
        {
            for(int i = 0; i < data.GetLength(0); i++)
            {
                _CsvData.Add(new CsvData(data[i, 0], data[i, 1], data[i, 2], data[i, 3], data[i, 4], data[i, 5], data[i, 6], data[i, 7], data[i, 8], data[i, 9], data[i, 10], data[i, 11], data[i, 12], data[i, 13], data[i, 14], data[i, 15], data[i, 16], data[i, 17], data[i, 18], data[i, 19], data[i, 20], data[i, 21], data[i, 22], data[i, 23], data[i, 24], data[i, 25], data[i, 26], data[i, 27], data[i, 28], data[i, 29], data[i, 30], data[i, 31], data[i, 32], data[i, 33], data[i, 34], data[i, 35], data[i, 36], data[i, 37], data[i, 38], data[i, 39], data[i, 40]));
            }
        }
    }
}

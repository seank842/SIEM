using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIEM
{
    class CSVDataDynamic
    {
        private IDictionary<string, Type> _values;

        public CSVDataDynamic(IDictionary<string, Type> values)
        {
            _values = values;
        }
        
        public bool TryGetMember(GetMemberBinder binder, out Type result)
        {
            if (_values.ContainsKey(binder.Name))
            {
                result = _values[binder.Name];
                return true;
            }
            result = null;
            return false;
        }
    }
    
    public class CSVDataDynamicVM
    {
        private ObservableCollection<CSVDataDynamic> _CSVData;
        public ObservableCollection<CSVDataDynamic> CSVData
        {
            get { return _CSVData; }
            set { _CSVData = value; }
        }

        public CSVDataDynamicVM(string[,] data, int cols, int rows)
        {
            _CSVData = new ObservableCollection<CSVDataDynamic>();
            FillData(data, cols, rows);
        }

        private void FillData(string[,] data, int cols, int rows)
        {
            for(int r = 0; r <= rows; r++)
            {
                _CSVData.Add({
                    for(int c = 0; c <= cols; c++)
                    {
                        
                    }
                });
            }
        }
    }
}

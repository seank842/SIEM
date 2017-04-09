using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace SIEM
{
    class FileIO
    {
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
                var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList; mru.Add(file);
                mru.Add(file, "files");
            }
            else
            {
                ErrorHandler eHandle = new ErrorHandler();
                eHandle.ErrorHandle(1);
            }
        }

        internal static Task<IBuffer> ReadBufferAsynx(StorageFile file)
        {
            throw new NotImplementedException();
        }
    }
}

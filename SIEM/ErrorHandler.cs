using System;
using Windows.UI.Xaml.Controls;

namespace SIEM
{
    class ErrorHandler
    {
        public async void ErrorHandle(int eCode)
        {
            string title = null, content = null;
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
                case 3:
                    title = "test";
                    content = "entered";
                    break;
                case 4:
                    title = "Not Enough Files!";
                    content = "There are two files needed to compare";
                    break;
                case 5:
                    title = "Too Many Files!";
                    content = "There are two files needed to compare";
                    break;
            }
            ContentDialog FileUnableToRead = new ContentDialog()
            {
                Title = title,
                Content = content + ", try again. if this persists contact support!",
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await FileUnableToRead.ShowAsync();
        }

        ~ErrorHandler()
        {

        }
    }
}

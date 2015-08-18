using System;
using System.Collections.Generic;
using System.Text;

namespace Base64Universal.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string decodedText;
        public string DecodedText
        {
            get { return decodedText; }
            set
            {
                if (value != decodedText)
                {
                    decodedText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string encodedText;
        public string EncodedText
        {
            get { return encodedText; }
            set
            {
                if (value != encodedText)
                {
                    encodedText = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}

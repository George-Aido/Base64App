using Base64Universal.Common;
using Base64Universal.Enums;
using Base64Universal.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Base64Universal.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private CodingBaseEnum baseCode;
        public CodingBaseEnum BaseCode
        {
            get { return baseCode; }
            set
            {
                if (value != baseCode)
                {
                    baseCode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private CodingModeEnum codingMode;
        public CodingModeEnum CodingMode
        {
            get { return codingMode; }
            set
            {
                if (value != codingMode)
                {
                    codingMode = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string inputText;
        public string InputText
        {
            get { return inputText; }
            set
            {
                if (value != inputText)
                {
                    inputText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string outputText;
        public string OutputText
        {
            get { return outputText; }
            set
            {
                if (value != outputText)
                {
                    outputText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public MainPageViewModel()
        {
            ConvertToBaseCommand = new RelayCommand(EncodeOrDecode);
        }


        public ICommand ConvertToBaseCommand
        {
            get;
            private set;
        }
        private void EncodeOrDecode()
        {
            switch (CodingMode)
            {
                case CodingModeEnum.Encode:
                    OutputText = ToBase();
                    break;
                case CodingModeEnum.Decode:
                    OutputText = FromBase();
                    break;
                default:
                    // something went wrong. return...
                    return;
            }
        }

        private string ToBase()
        {
            switch (BaseCode)
            {
                case CodingBaseEnum.Base64:
                    return HelperMethods.ToBase64(InputText);
                case CodingBaseEnum.Hex:
                    return HelperMethods.StringTextToHex(InputText);
                default:
                    // something went wrong. return...
                    return "Input text is not in correct format";
            }
        }

        private string FromBase()
        {
            switch (BaseCode)
            {
                case CodingBaseEnum.Base64:
                    return HelperMethods.FromBase64(InputText);
                case CodingBaseEnum.Hex:
                    return HelperMethods.StringTextFromHex(InputText);
                default:
                    // something went wrong. return...
                    return "Input text is not in correct format";
            }
        }
    }
}

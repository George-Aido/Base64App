using BaseConverter.Common;
using BaseConverter.Enums;
using BaseConverter.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
#if WINDOWS_PHONE_APP
using Windows.ApplicationModel.Email;
#endif

namespace BaseConverter.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
#if WINDOWS_PHONE_APP
        private PivotItem selectedPivotItem;

        public PivotItem SelectedPivotItem
        {
            get { return selectedPivotItem; }
            set
            {
                if (value != selectedPivotItem)
                {
                    selectedPivotItem = value;
                    NotifyPropertyChanged();
                }
            }
        }
#else
        private string selectedPivotItem;

        public string SelectedPivotItem
        {
            get { return selectedPivotItem; }
            set
            {
                if (value != selectedPivotItem)
                {
                    selectedPivotItem = value;
                    NotifyPropertyChanged();
                }
            }
        }
#endif

        private CodingTextBaseEnum baseTextCode;
        public CodingTextBaseEnum BaseTextCode
        {
            get { return baseTextCode; }
            set
            {
                if (value != baseTextCode)
                {
                    baseTextCode = value;
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

        private CodingNumberBaseEnum inputNumberBase = CodingNumberBaseEnum.Decimal;

        public CodingNumberBaseEnum InputNumberBase
        {
            get { return inputNumberBase; }
            set
            {
                if (value != inputNumberBase)
                {
                    inputNumberBase = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private CodingNumberBaseEnum outputNumberBase = CodingNumberBaseEnum.Decimal;

        public CodingNumberBaseEnum OutputNumberBase
        {
            get { return outputNumberBase; }
            set
            {
                if (value != outputNumberBase)
                {
                    outputNumberBase = value;
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

        private string inputNumber;

        public string InputNumber
        {
            get { return inputNumber; }
            set
            {
                if (value != inputNumber)
                {
                    inputNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string outputNumber;

        public string OutputNumber
        {
            get { return outputNumber; }
            set
            {
                if (value != outputNumber)
                {
                    outputNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public MainPageViewModel()
        {
            ConvertToBaseCommand = new RelayCommand(EncodeOrDecode);
            SendEmailCommand = new RelayCommand(SendEmail);
        }


        public ICommand ConvertToBaseCommand
        {
            get; private set;
        }

        public ICommand SendEmailCommand
        {
            get; private set;
        }
        private void EncodeOrDecode()
        {
#if WINDOWS_PHONE_APP
            switch (SelectedPivotItem.Tag.ToString())
            {
                case Constants.NumberPivotPage:
                    OutputNumber = HelperMethods.StringNumberFromBaseToBase(InputNumber, (int)InputNumberBase, (int)OutputNumberBase);
                    if (InputNumberBase == CodingNumberBaseEnum.Hex)
                    {

                    }
                    break;
                case Constants.TextPivotPage:
                    OutputText = ConvertText();
                    break;
                default:
                    // do nothing
                    return;
            }
#else
#endif
        }

        private string ConvertText()
        {
            switch (CodingMode)
            {
                case CodingModeEnum.Encode:
                    return ToTextBase();
                case CodingModeEnum.Decode:
                    return FromTextBase();
                default:
                    // something went wrong. return...
                    return "error";
            }
        }

        private string ToTextBase()
        {
            if (InputText.Contains("http://"))
            {
                ImageBack = new Windows.UI.Xaml.Media.ImageBrush()
                {
                    ImageSource = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(InputText)),
                    Opacity = 0.2,
                    Stretch = Windows.UI.Xaml.Media.Stretch.UniformToFill
                };
                return "Image?";
            }
            switch (BaseTextCode)
            {
                case CodingTextBaseEnum.Base64:
                    return HelperMethods.ToBase64(InputText);
                case CodingTextBaseEnum.Hex:
                    return HelperMethods.StringTextToHex(InputText);
                default:
                    // something went wrong. return...
                    return Constants.MainPageError;
            }
        }

        private string FromTextBase()
        {
            switch (BaseTextCode)
            {
                case CodingTextBaseEnum.Base64:
                    return HelperMethods.FromBase64(InputText);
                case CodingTextBaseEnum.Hex:
                    return HelperMethods.StringTextFromHex(InputText);
                default:
                    // something went wrong. return...
                    return Constants.MainPageError;
            }
        }

        private async void SendEmail()
        {
#if WINDOWS_PHONE_APP
            string textToSend = (SelectedPivotItem.Tag.ToString() == Constants.TextPivotPage) ? OutputText : OutputNumber;
            if (string.IsNullOrEmpty(textToSend) || textToSend.Equals(Constants.ErrorMessage))
            {
                MessageDialog msgDialog = new MessageDialog("There is nothing in the output field to send");
                await msgDialog.ShowAsync();
                return;
            }

            var emailMessage = new EmailMessage
            {
                Subject = "Covnerted with 'Base Converter' for Windows Phone",
                Body = "\n\nMessage: " + textToSend + "\n\n\n\n\n\n\n\n\n\n\n\n\nlink to app"
            };

            // call EmailManager to show the compose UI in the screen
            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
#endif
        }
    }
}

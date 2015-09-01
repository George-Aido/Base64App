using BaseConverter.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
#if WINDOWS_PHONE_APP
using Windows.ApplicationModel.Email;
#endif

namespace BaseConverter.ViewModels
{
    public class AboutPageViewModel
    {
        public ICommand ContactCommand
        { get; private set; }

        public ICommand NavigateToReviewPageCommand
        {
            get; private set;
        }

        public ICommand NavigateToOtherAppsCommand
        {
            get; private set;
        }

        public AboutPageViewModel()
        {
            ContactCommand = new RelayCommand(Contact);
            NavigateToReviewPageCommand = new RelayCommand(NavigateToReviewPage);
            NavigateToOtherAppsCommand = new RelayCommand(NavigateToOtherApps);
        }

        private async void Contact()
        {
#if WINDOWS_PHONE_APP
            var emailMessage = new EmailMessage
            {
                Subject = "Windows Phone 'Base Converter' feedback"
            };
            emailMessage.To.Add(new EmailRecipient(Constants.OutlookEmail, Constants.GeorgeAidonidis));
            // call EmailManager to show the compose UI in the screen
            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
#endif
        }

        private async void NavigateToReviewPage()
        {
            var uri = new Uri(string.Format("ms-windows-store:reviewapp?appid={0}", Windows.ApplicationModel.Package.Current.Id.Name));
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private async void NavigateToOtherApps()
        {
            var uri = new Uri(string.Format(@"ms-windows-store:search?keyword={0}", Constants.GeorgeAidonidis));
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}

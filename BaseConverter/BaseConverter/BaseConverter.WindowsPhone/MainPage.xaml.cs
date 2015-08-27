using BaseConverter.Common;
using BaseConverter.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BaseConverter
{
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        private MainPageViewModel viewModel = new MainPageViewModel();

        public MainPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public MainPageViewModel ViewModel
        {
            get { return this.viewModel; }
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(Pivot.SelectedIndex);
            System.Diagnostics.Debug.WriteLine((Pivot.SelectedItem as PivotItem).Tag);
        }

        private void AppBarButton_About_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton).Tag.ToString() == Constants.Hex)
            {
                TextBox_InputNumber.Visibility = Visibility.Collapsed;
                TextBox_InputNumber_ForHex.Visibility = Visibility.Visible;
                if (!string.IsNullOrEmpty(TextBox_InputNumber_ForHex.Text))
                    TextBox_InputNumber_ForHex.Focus(FocusState.Programmatic);
            }
            else
            {
                TextBox_InputNumber_ForHex.Visibility = Visibility.Collapsed;
                TextBox_InputNumber.Visibility = Visibility.Visible;
                if (!string.IsNullOrEmpty(TextBox_InputNumber.Text))
                    TextBox_InputNumber.Focus(FocusState.Programmatic);
            }
        }
    }
}

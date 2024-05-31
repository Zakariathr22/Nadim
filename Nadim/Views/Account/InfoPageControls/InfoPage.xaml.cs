using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Input;
using Nadim.Services;
using Nadim.ViewModels;
using Windows.Storage.Pickers;
using Windows.Storage;
using Nadim.Views.Controls;
using Nadim.ViewModels.Account;
using Windows.Security.Credentials;
using Nadim.Views.Account.InfoPageControls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.Account
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InfoPage : Page
    {
        public AccountInfoViewModel accountInfoViewModel;
        public InfoPage()
        {
            this.InitializeComponent();
            Loaded += InfoPage_Loaded;
        }

        private void InfoPage_Loaded(object sender, RoutedEventArgs e)
        {
            accountInfoViewModel = new AccountInfoViewModel();
            this.DataContext = accountInfoViewModel;
            if (!App.dataAccess.ConnectionStatIsOpened())
            {
                ShowDialog();
                App.mainWindow.mainPanel.IsTapEnabled = false;
            }
            else if (accountInfoViewModel.User == null)
            {
                if (accountInfoViewModel.isTokenValid)
                {
                    ShowDialog();
                    App.mainWindow.mainPanel.IsTapEnabled = false;
                }
                else
                {
                    App.mainWindow.ShowSessionExpiredDialog();
                    App.mainWindow.mainPanel.IsTapEnabled = false;
                }
            }
        }

        public async void ShowUpdatingProfilePictureDialog(StorageFile file)
        {
            using (ChangeProfilePicturePage changeProfilePicturePage = new ChangeProfilePicturePage(file))
            {
                ContentDialog dialog = new ContentDialog();
                // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
                dialog.XamlRoot = Content.XamlRoot;
                dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                dialog.Title = "تغيير صورة الحساب";
                dialog.Content = changeProfilePicturePage;
                dialog.PrimaryButtonText = "حفظ";
                dialog.CloseButtonText = "إلغاء";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.FlowDirection = FlowDirection.RightToLeft;
                dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);

                var result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    _ = changeProfilePicturePage.SaveCroppedImage();
                    file = null;
                }
                dialog.Content = null;
                dialog = null;
                GC.Collect();
            }
        }

        private async void changePictureButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous returned file name, if it exists, between iterations of this scenario
            // PickAPhotoOutputTextBlock.Text = "";

            // Create a file picker
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            // See the sample code below for how to make the window accessible from the App class.
            var window = App.mainWindow;

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Initialize the file picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            // Set options for your file picker
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            // Open the picker for the user to pick a file
            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                ShowUpdatingProfilePictureDialog(file);
            }
            else
            {
                //PickAPhotoOutputTextBlock.Text = "Operation cancelled.";
            }
        }

        public async void ShowDialog()
        {

            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new SystemMessages.ConnectionFailedTitleControl();
            dialog.PrimaryButtonText = "حاول مرة أخرى";
            dialog.CloseButtonText = "إغلاق البرنامج";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new SystemMessages.ConnectionFailedPage();
            dialog.FlowDirection = FlowDirection.RightToLeft;
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    App.dataAccess.OpenConnection();
                    this.IsTapEnabled = true;
                    accountInfoViewModel = new AccountInfoViewModel();
                    this.DataContext = accountInfoViewModel;
                    if (accountInfoViewModel.isTokenValid == false)
                    {
                        App.mainWindow.ShowSessionExpiredDialog();
                        App.mainWindow.mainPanel.IsTapEnabled = false;
                    }
                }
                catch
                {
                    ShowDialog();
                }
            }
            else
            {
                App.mainWindow.Close();
            }
        }

        private async void ShowEditNameDialog()
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new TitleControl("تعديل اللقب أو الاسم");
            dialog.PrimaryButtonText = "حفظ";
            dialog.CloseButtonText = "إلغاء";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new EditNamePage(dialog, accountInfoViewModel);
            dialog.FlowDirection = FlowDirection.RightToLeft;
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            dialog.IsPrimaryButtonEnabled = false;
            var result = await dialog.ShowAsync();
        }

        private void editFullNameHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEditNameDialog();
        }
        private async void ShowEditBirthDateDialog()
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new TitleControl("تعديل تاريخ الميلاد");
            dialog.PrimaryButtonText = "حفظ";
            dialog.CloseButtonText = "إلغاء";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new EditBirthDatePage(dialog,accountInfoViewModel);
            dialog.FlowDirection = FlowDirection.RightToLeft;
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            dialog.IsPrimaryButtonEnabled = false;
            var result = await dialog.ShowAsync();
        }

        private void editBirthDateButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEditBirthDateDialog();
        }

        private async void ShowEditGenderDialog()
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new TitleControl("تعديل الجنس");
            dialog.PrimaryButtonText = "حفظ";
            dialog.CloseButtonText = "إلغاء";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new EditGenderPage(dialog,accountInfoViewModel);
            dialog.FlowDirection = FlowDirection.RightToLeft;
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            dialog.IsPrimaryButtonEnabled = false;
            var result = await dialog.ShowAsync();
        }

        private void editGenderButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEditGenderDialog();
        }

        private async void ShowEditAccreditationDialog()
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new TitleControl("تعديل الإعتماد");
            dialog.PrimaryButtonText = "حفظ";
            dialog.CloseButtonText = "إلغاء";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new EditAccedationPage(dialog,accountInfoViewModel);
            dialog.FlowDirection = FlowDirection.RightToLeft;
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            dialog.IsPrimaryButtonEnabled = false;
            var result = await dialog.ShowAsync();
        }

        private void editAccreditationButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEditAccreditationDialog();
        }

        private async void ShowEditStartingDateDialog()
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new TitleControl("تعديل تاريخ بداية النشاط");
            dialog.PrimaryButtonText = "حفظ";
            dialog.CloseButtonText = "إلغاء";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new EditStartingDatePage(dialog,accountInfoViewModel);
            dialog.FlowDirection = FlowDirection.RightToLeft;
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            dialog.IsPrimaryButtonEnabled = false;
            var result = await dialog.ShowAsync();
        }

        private void editStartingDateButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEditStartingDateDialog();
        }

        private async void ShowEditEmailDialog()
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new TitleControl("تعديل تاريخ بداية النشاط");
            dialog.PrimaryButtonText = "حفظ";
            dialog.CloseButtonText = "إلغاء";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new EditStartingDatePage(dialog, accountInfoViewModel);
            dialog.FlowDirection = FlowDirection.RightToLeft;
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            dialog.IsPrimaryButtonEnabled = false;
            var result = await dialog.ShowAsync();
        }

        private void editEmailButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEditEmailDialog();
        }
    }
}

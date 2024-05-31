using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Nadim.Models;
using Nadim.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.Account.InfoPageControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditNamePage : Page
    {
        ContentDialog dialog;
        User user = new User();
        public EditNamePage()
        {
            this.InitializeComponent();
        }

        public EditNamePage(ContentDialog dialog, AccountInfoViewModel accountInfoViewModel)
        {
            this.InitializeComponent();
            this.dialog = dialog;
            this.DataContext = accountInfoViewModel;
            this.user.firstName = accountInfoViewModel.User.firstName;
            this.user.lastName = accountInfoViewModel.User.lastName;
        }

        private void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (firstNameTextBox.Text.Trim() != user.firstName || lastNameTextBox.Text.Trim() != user.lastName) 
                dialog.IsPrimaryButtonEnabled = true;
            else dialog.IsPrimaryButtonEnabled = false;
        }

        private void firstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (firstNameTextBox.Text.Trim() != user.firstName || lastNameTextBox.Text.Trim() != user.lastName)
                dialog.IsPrimaryButtonEnabled = true;
            else dialog.IsPrimaryButtonEnabled = false;
        }
    }
}

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
    public sealed partial class EditGenderPage : Page
    {
        User user = new User();
        ContentDialog dialog;
        public EditGenderPage()
        {
            this.InitializeComponent();
        }

        public EditGenderPage(ContentDialog dialog, AccountInfoViewModel accountInfoViewModel)
        {
            this.InitializeComponent();
            this.dialog = dialog;
            user.gender = accountInfoViewModel.User.gender;
            if (user.gender == "أنثى") 
            {
                femaleRadiobutton.IsChecked = true;
                maleRadioButton.IsChecked = false;
            }
            else
            {
                femaleRadiobutton.IsChecked = false;
                maleRadioButton.IsChecked = true;
            }
        }

        private void maleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (user.gender == "أنثى")
                dialog.IsPrimaryButtonEnabled = true;
            else dialog.IsPrimaryButtonEnabled = false;
        }

        private void femaleRadiobutton_Checked(object sender, RoutedEventArgs e)
        {
            if (user.gender != "أنثى")
                dialog.IsPrimaryButtonEnabled = true;
            else dialog.IsPrimaryButtonEnabled = false;
        }
    }
}

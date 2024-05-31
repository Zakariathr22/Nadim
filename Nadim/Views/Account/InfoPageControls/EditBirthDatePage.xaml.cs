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
    public sealed partial class EditBirthDatePage : Page
    {
        User user = new User();
        ContentDialog dialog;
        public EditBirthDatePage()
        {
            this.InitializeComponent();
            birthDateDatePicker.MaxYear = DateTimeOffset.Now.AddYears(-15);
        }

        public EditBirthDatePage(ContentDialog dialog, AccountInfoViewModel accountInfoViewModel)
        {
            this.InitializeComponent();
            birthDateDatePicker.MaxYear = DateTimeOffset.Now.AddYears(-15);
            this.dialog = dialog;
            this.user.birthDate = accountInfoViewModel.User.birthDate;
            this.DataContext = accountInfoViewModel;
        }

        private void birthDateDatePicker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            if (user.birthDate != birthDateDatePicker.SelectedDate) 
                dialog.IsPrimaryButtonEnabled = true;
            else dialog.IsPrimaryButtonEnabled = false;
        }
    }
}

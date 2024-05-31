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
    public sealed partial class EditAccedationPage : Page
    {
        Lawyer user = new Lawyer();
        ContentDialog dialog;
        public EditAccedationPage()
        {
            this.InitializeComponent();
        }
        public EditAccedationPage(ContentDialog dialog, AccountInfoViewModel accountInfoViewModel)
        {
            this.InitializeComponent();
            this.dialog = dialog;
            this.DataContext = accountInfoViewModel;
            this.user.accreditation = accountInfoViewModel.User.accreditation;

            if (user.accreditation == "لدى المحكمة العليا ومجلس الدولة")
                accreditationComboBox.SelectedIndex = 0;
            else accreditationComboBox.SelectedIndex = 1;

        }

        private void accreditationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (accreditationComboBox.SelectedIndex == 0)
            {
                if (user.accreditation == "لدى المحكمة العليا ومجلس الدولة")
                    dialog.IsPrimaryButtonEnabled = false;
                else dialog.IsPrimaryButtonEnabled = true;
            }
            else if (accreditationComboBox.SelectedIndex == 1)
            {
                if (user.accreditation == "لدى المحكمة العليا ومجلس الدولة")
                    dialog.IsPrimaryButtonEnabled = true;
                else dialog.IsPrimaryButtonEnabled = false;
            }
        }
    }
}

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
using Nadim.Views;
using Windows.UI.ViewManagement;
using Nadim.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim
{
    
    public sealed partial class LawyerInfoPage : Page
    {
        public LawyerInfoPage()
        {
            this.InitializeComponent();
            mainPanel.DataContext = SignUpWindow.signUpLawyerInfoViewModel;
            birthDateDatePicker.MaxYear = DateTimeOffset.Now.AddYears(-15);
            startingDateDatePicker.MaxYear = DateTimeOffset.Now;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow.signUpLawyerInfoViewModel.RequiredFieldsCommand.Execute(null);
            if (SignUpWindow.signUpLawyerInfoViewModel.EveryThingValid)
            {
                App.signUpWindow.selectorBar.SelectedItem = App.signUpWindow.SelectorBarItemOfficeInfo;
                App.signUpWindow.SelectorBarItemPersonalInfo.IsEnabled = false;
                App.signUpWindow.SelectorBarItemOfficeInfo.IsEnabled = true;
            }   
        }

        private void maleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SignUpWindow.signUpLawyerInfoViewModel.Gender = "male";
        }

        private void femaleRadiobutton_Checked(object sender, RoutedEventArgs e)
        {
            SignUpWindow.signUpLawyerInfoViewModel.Gender = "female";
        }

        private void birthDateDatePicker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {

        }
    }
}

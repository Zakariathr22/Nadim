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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim
{

    public sealed partial class LawyerInfoPage : Page
    {

        public LawyerInfoPage()
        {
            this.InitializeComponent();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            App.signUpWindow.selectorBar.SelectedItem = App.signUpWindow.SelectorBarItemOfficeInfo;
            App.signUpWindow.SelectorBarItemPersonalInfo.IsEnabled = false;
            App.signUpWindow.SelectorBarItemOfficeInfo.IsEnabled = true;
        }
    }
}

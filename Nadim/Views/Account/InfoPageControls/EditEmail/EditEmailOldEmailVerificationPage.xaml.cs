using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.Account.InfoPageControls.EditEmail
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditEmailOldEmailVerificationPage : Page
    {
        EditVerifiedEmail p;
        public EditEmailOldEmailVerificationPage()
        {
            this.InitializeComponent();
            this.Loaded += EditEmailOldEmailVerificationPage_Loaded;
        }

        private void EditEmailOldEmailVerificationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.p.dialog.PrimaryButtonText = "تحقق";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is EditVerifiedEmail)
            {
                p = e.Parameter as EditVerifiedEmail;
            }
            base.OnNavigatedTo(e);
        }
    }
}

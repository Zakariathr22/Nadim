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
using Nadim.ViewModels;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Documents;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.Account.InfoPageControls.EditEmail
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditEmailDescriptionPage : Page
    {
        EditVerifiedEmail p;
        public EditEmailDescriptionPage()
        {
            this.InitializeComponent();
            this.Loaded += EditEmailDescriptionPage_Loaded;
        }

        private void EditEmailDescriptionPage_Loaded(object sender, RoutedEventArgs e)
        {
            p.dialog.PrimaryButtonClick += (sender, args) =>
            {
                // Perform validation or other logic here
                bool isValid = false; // Replace with your validation logic

                p.NavigateWithSlideTransition(typeof(EditEmailOldEmailVerificationPage));

                if (!isValid)
                {
                    // If validation fails, cancel the button click event,
                    // which will prevent the dialog from closing
                    args.Cancel = true;
                }
            };

            Run oldEmailRun = new Run();
            oldEmailRun.Text = p.user.email;
            oldEmailRun.FontWeight = FontWeights.Bold;

            textBlock.Inlines.Add(new Run() { Text = "سنحتاج إلى التحقق من عنوان بريدك الإلكتروني القديم " });
            textBlock.Inlines.Add(oldEmailRun);
            textBlock.Inlines.Add(new Run() { Text = " حتى نتمكن من تغييره" });
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

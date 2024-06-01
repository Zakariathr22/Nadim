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
using Microsoft.UI.Xaml.Media.Animation;
using Nadim.ViewModels.Account;
using Nadim.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.Account.InfoPageControls.EditEmail
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditVerifiedEmail : Page
    {
        public ContentDialog dialog;
        public User user = new User();

        public EditVerifiedEmail()
        {
            this.InitializeComponent();
            NavigateWithSlideTransition(typeof(EditEmailDescriptionPage));          
        }
        public EditVerifiedEmail(ContentDialog dialog, AccountInfoViewModel accountInfoViewModel)
        {
            this.InitializeComponent();
            this.dialog = dialog;
            this.user.email = accountInfoViewModel.User.email;
            this.user.emailVerified = accountInfoViewModel.User.emailVerified;
            this.user.salt = accountInfoViewModel.User.salt;
            NavigateWithSlideTransition(typeof(EditEmailDescriptionPage));        }

        public void NavigateWithSlideTransition(Type pageType)
        {
            var slideNavigationTransitionEffect = SlideNavigationTransitionEffect.FromRight;
            ContentFrame.Navigate(pageType,this, new SlideNavigationTransitionInfo() { Effect = slideNavigationTransitionEffect });           
        }
    }
}

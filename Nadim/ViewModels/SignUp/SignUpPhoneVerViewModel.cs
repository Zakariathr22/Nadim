using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.ViewModels.SignUp
{
    public partial class SignUpPhoneVerViewModel : ObservableObject
    {
        [ObservableProperty] private bool phoneCodeIsValid = false;
    }
}

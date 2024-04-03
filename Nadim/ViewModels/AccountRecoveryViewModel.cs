using CommunityToolkit.Mvvm.ComponentModel;
using Nadim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.ViewModels
{
    public partial class AccountRecoveryViewModel : ObservableObject
    {
        [ObservableProperty] Token resetPasswordToken;
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using Nadim.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
using Nadim.Services;

namespace Nadim.ViewModels.Account
{
    public partial class AccountInfoViewModel : ObservableObject
    {
        [ObservableProperty] User user;
        [ObservableProperty] Lawyer lawyer;
        [ObservableProperty] BitmapImage image;
        
        public AccountInfoViewModel()
        {
            User = new User();
            User.profilePic = FileImageService.ConvertFileToByteArray("C:\\Users\\TAHRI.ZAKARIA\\source\\repos\\Nadim\\Nadim\\Assets\\Icons\\LawyerPic.jpeg");
            Image = FileImageService.ByteArrayToBitmapImage(User.profilePic);

            User.firstName = "زكرياء";
            User.lastName = "طاهري";
            User.birthDate = DateTime.Now;
            User.gender = "ذكر";
            Lawyer = new Lawyer(User);
            Lawyer.accreditation = "لدى المحكمة العليا ومجلس الدولة";
            lawyer.startingDate = DateTime.Now;
            user.email = "zakotahri@gmail.com";
            user.phone = "0659707528";
            user.createdAt = DateTime.Now;
            user.lastUpdate = DateTime.Now;
        }

    }
}

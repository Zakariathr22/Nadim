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
using MySqlConnector;

namespace Nadim.ViewModels.Account
{
    public partial class AccountInfoViewModel : ObservableObject
    {
        [ObservableProperty] Lawyer user;
        [ObservableProperty] BitmapImage image;
        private Token token;
        public bool isTokenValid = true;

        public AccountInfoViewModel()
        {
            var credential = App.valut.Retrieve("NadimApplication", "token");
            token = new Token(credential);
            User = GetUserDetails(token);
            if (User.profilePic != null) Image = FileImageService.ByteArrayToBitmapImage(User.profilePic);
        }

        private Lawyer GetUserDetails(Token token)
        {
            if (!App.dataAccess.ConnectionStatIsOpened())
                try
                {
                    App.dataAccess.OpenConnection();
                }
                catch { }

            Lawyer user = null;

            try
            {
                string query = "CALL GetUserDetails(@p_token_value, @p_ip_address, @p_user_agent, @p_machine_name)";
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@p_token_value", token.tokenValue),
                    new MySqlParameter("@p_ip_address", token.ipAddress),
                    new MySqlParameter("@p_user_agent", token.userAgent),
                    new MySqlParameter("@p_machine_name", token.machineName)
                };
                using var reader = App.dataAccess.ExecuteQuery(query, parameters);
                if (reader.Read())
                {
                    isTokenValid = reader.GetBoolean("is_token_valid");
                    if (isTokenValid)
                    {
                        user = new Lawyer();
                        if (!reader.IsDBNull(reader.GetOrdinal("firstname")))
                            user.firstName = reader.GetString("firstname").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("lastname")))
                            user.lastName = reader.GetString("lastname").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("birthdate"))) 
                            user.birthDate = (DateTimeOffset)reader.GetDateTime("birthdate");
                        if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                            user.gender = reader.GetString("gender").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("profilePic")))
                        {
                            long len = reader.GetBytes(reader.GetOrdinal("profilePic"), 0, null, 0, 0);
                            byte[] array = new byte[len];
                            reader.GetBytes(reader.GetOrdinal("profilePic"), 0, array, 0, (int)len);
                            user.profilePic = array;
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("email"))) 
                            user.email = reader.GetString("email").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("phone"))) 
                            user.phone = reader.GetString("phone").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("starting_date")))
                            user.createdAt = (DateTimeOffset)reader.GetDateTime("createdAt");
                        if (!reader.IsDBNull(reader.GetOrdinal("createdAt")))
                            user.lastUpdate = (DateTimeOffset)reader.GetDateTime("lastUpdate");
                        if (!reader.IsDBNull(reader.GetOrdinal("lastUpdate")))
                            user.accreditation = reader.GetString("accreditation").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("starting_date")))
                            user.startingDate = (DateTimeOffset)reader.GetDateTime("starting_date");

                        user.creator = new User();
                        if (!reader.IsDBNull(reader.GetOrdinal("creator_firstname")))
                            user.creator.firstName = reader.GetString("creator_firstname").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("creator_lastname")))
                            user.creator.lastName = reader.GetString("creator_lastname").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return user;
        }
    }
}

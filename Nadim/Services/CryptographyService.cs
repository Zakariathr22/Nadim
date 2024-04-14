using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Services
{
    public static class CryptographyService
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GenerateSalt()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            return new string(Enumerable.Repeat(chars, 100)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string onifojij(string Text)
        {
            using (Aes hioaxdziygfyee = Aes.Create())
            {
                byte[] urhgoodgfgdyfy = Encoding.UTF8.GetBytes("Rr6@crKe*s^*QX2LkjtPA2yhEqJcy_n@Ekv2RXkoE@^nua%qkEJ_P*6#@iAE#PNN2vRHb1qeFE6$h@k8SO5k4I29q_m_mpc&F94s");
                string urhgoodgfgdyfydsdD = "e243f35959181368d313505381d965894995abc25a3249a918ba45f9c7f8b740";
                var uhiihefhwfo = new Rfc2898DeriveBytes(urhgoodgfgdyfydsdD + "1fbbf6531941d097613eba5b6e1e1b5123398bd5c0ec93b4b28a20e50cb5231b", urhgoodgfgdyfy);
                hioaxdziygfyee.Key = uhiihefhwfo.GetBytes(hioaxdziygfyee.KeySize / 8);
                hioaxdziygfyee.IV = uhiihefhwfo.GetBytes(hioaxdziygfyee.BlockSize / 8);

                ICryptoTransform zszwtaw = hioaxdziygfyee.CreateDecryptor(hioaxdziygfyee.Key, hioaxdziygfyee.IV);

                using (MemoryStream azezgreds = new MemoryStream(Convert.FromBase64String(Text)))
                {
                    using (CryptoStream xddseresd = new CryptoStream(azezgreds, zszwtaw, CryptoStreamMode.Read))
                    {
                        using (StreamReader wedhfhghh = new StreamReader(xddseresd))
                        {
                            return wedhfhghh.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}

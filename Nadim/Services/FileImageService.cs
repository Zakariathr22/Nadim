using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Nadim.Services
{
    public static class FileImageService
    {
        public static byte[] ConvertFileToByteArray(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            return bytes;
        }

        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            try
            {
                using (var stream = new InMemoryRandomAccessStream())
                {
                    _ = stream.WriteAsync(byteArray.AsBuffer());
                    var bitmap = new BitmapImage();
                    stream.Seek(0);
                    _ = bitmap.SetSourceAsync(stream);
                    return bitmap;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}

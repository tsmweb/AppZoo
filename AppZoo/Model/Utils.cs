using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace AppZoo.Model
{
    class Utils
    {
        public static ImageSource GetImage(String name)
        {
            Uri baseUri = new Uri("/Resources/Imagens/" + name, UriKind.Relative);
            ImageSource img = new BitmapImage(baseUri);

            return img;
        }

        public static async Task<String> readFile(String path)
        {
            var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(path);
            var stream = await file.OpenReadAsync();
            var reader = new StreamReader(stream.AsStream());
            String arquivo = reader.ReadToEnd();

            return arquivo;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace AppZoo.Utils
{
    public static class Utils
    {
        public static async Task<int> CreateDialogConfirm(string msg, string title)
        {
            var dialog = new MessageDialog(msg);

            if (!string.IsNullOrEmpty(title))
                dialog.Title = title;

            dialog.Commands.Add(new UICommand { Label = "Sim", Id = 1 });
            dialog.Commands.Add(new UICommand { Label = "Não", Id = 0 });

            var res = await dialog.ShowAsync();

            return (int)res.Id;
        }

        public static string GeraMD5(string str)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            var res = CryptographicBuffer.EncodeToHexString(hashed);

            return res;
        }

    }
}

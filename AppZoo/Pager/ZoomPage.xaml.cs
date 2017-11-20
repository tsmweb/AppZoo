using AppZoo.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppZoo.Pager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ZoomPage : Page
    {
        public ZoomPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Animal animal = e.Parameter as Animal;
            txtNome.Text = animal.Nome;
            imgZoom.Source = new BitmapImage(new Uri(animal.FullPath, UriKind.Absolute));
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            svImageZoom.Width = e.NewSize.Width;
            svImageZoom.Height = e.NewSize.Height - 60;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}

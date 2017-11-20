using AppZoo.Pager;
using AppZoo.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppZoo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel _mViewModel;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _mViewModel = DataContext as MainViewModel;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            popupGridAnimal.Height = e.NewSize.Height - abControles.ActualHeight;
        }

        private void IncluirAppBarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!popupAnimal.IsOpen)
            {
                _mViewModel.InserirAnimal();
            }              
        }

        private void AlterarAppBarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!popupAnimal.IsOpen)
            {
                _mViewModel.EditarAnimal();
            }       
        }

        private async void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var conf = await AppZoo.Utils.Utils.CreateDialogConfirm("Deseja sair sem salvar?", "Atenção");

            if (conf == 1)
                popupAnimal.IsOpen = false;
        }

        private void ExcluirAppBarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!popupAnimal.IsOpen)
            {
                _mViewModel.ExcluirAnimal();
            }
        }

        private void ZoomAppBarBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ZoomPage), _mViewModel.SelectedAnimal);
        }
    }

}

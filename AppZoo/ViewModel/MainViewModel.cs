using AppZoo.Enumeration;
using AppZoo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace AppZoo.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private AnimalDao animalDao;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Animal> _listAnimal;
        private List<Animal> listAnimal;
        public List<Animal> ListAnimal
        {
            get { return listAnimal; }
            set
            {
                if (listAnimal != value)
                {
                    listAnimal = value;
                    RaisePropertyChanged("ListAnimal");
                }
            }
        }

        private Animal selectedAnimal;
        public Animal SelectedAnimal
        {
            get { return selectedAnimal; }
            set
            {
                if (selectedAnimal != value)
                {
                    selectedAnimal = value;
                    RaisePropertyChanged("SelectedAnimal");
                }
            }
        }

        private bool isSelectedAnimal;
        public bool IsSelectedAnimal
        {
            get { return isSelectedAnimal; }
            set
            {
                if (isSelectedAnimal != value)
                {
                    isSelectedAnimal = value;
                    RaisePropertyChanged("IsSelectedAnimal");
                }
            }
        }

        private string filtro;
        public string Filtro
        {
            get { return filtro; }
            set
            {
                if (filtro != value)
                {
                    filtro = value;
                    RaisePropertyChanged("Filtro");
                }
            }
        }

        private bool filtroEnable;
        public bool FiltroEnable
        {
            get { return filtroEnable; }
            set
            {
                if (filtroEnable != value)
                {
                    filtroEnable = value;
                    RaisePropertyChanged("FiltroEnable");
                }
            }
        }


        private bool popupIsOpen;
        public bool PopupIsOpen
        {
            get { return popupIsOpen; }
            set
            {
                if (popupIsOpen != value)
                {
                    popupIsOpen = value;
                    RaisePropertyChanged("PopupIsOpen");
                }
            }
        }


        private FormViewModel formViewModel;
        public FormViewModel PropFormViewModel
        {
            get { return formViewModel; }
            set
            {
                if (formViewModel != value)
                {
                    formViewModel = value;
                    RaisePropertyChanged("PropFormViewModel");
                }
            }
        }


        public MainViewModel()
        {
            animalDao = new AnimalDao();

            //ListAnimal = Task.Run(async() => { return await AnimalService.GetAnimais(); }).GetAwaiter().GetResult();
            ListAnimal = animalDao.BuscarAnimais();
            _listAnimal = ListAnimal;
            FiltroEnable = true;

            PropertyChanged += MainViewModel_PropertyChanged;
        }

        private async void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Filtro")
            {
                FiltroEnable = false;
                await SearchAnimal();
                FiltroEnable = true;
            }
            else if (e.PropertyName == "SelectedAnimal")
            {
                IsSelectedAnimal = SelectedAnimal != null;
            }
        }

        public async Task SearchAnimal()
        {
            ListAnimal = await Task.Run(() => {  //Task.Run(async () =>
                if (Filtro != null)
                {
                    var lista = (from animais in _listAnimal
                                    where (animais.Especie.ToLower().Contains(Filtro.ToLower()) || animais.Nome.ToLower().Contains(Filtro.ToLower()))
                                    orderby animais.Especie, animais.Nome
                                    select animais).ToList<Animal>();

                    //var lista = ListAnimal.Where(a => a.Nome.ToLower().Contains(Filtro.ToLower()) || a.Especie.ToLower().Contains(Filtro.ToLower())).ToList<Animal>();

                    //await Task.Delay(2000);

                    return lista;
                }
                else
                    return _listAnimal;
            });
            
        }

        public void RecuperaListaAnimal()
        {
            ListAnimal = animalDao.BuscarAnimais();
            _listAnimal = ListAnimal;
        }

        public void InserirAnimal()
        {
            PropFormViewModel = new FormViewModel(this);
            PopupIsOpen = true;       
        }

        public void EditarAnimal()
        {
            PropFormViewModel = new FormViewModel(this, SelectedAnimal);
            PopupIsOpen = true;
        }

        public async void ExcluirAnimal()
        {
            var conf = await AppZoo.Utils.Utils.CreateDialogConfirm("Deseja excluir o animal selecionado?", "Atenção");

            if (conf == 1)
            {  
                try
                {
                    animalDao.ExcluirAnimal(SelectedAnimal);
                    RecuperaListaAnimal();
                } 
                catch (Exception ex)
                {
                    var dialog = new MessageDialog(ex.Message);
                    dialog.Title = "Erro ao excluir o animal!";
                    await dialog.ShowAsync();
                }
            }

        }

    }
}

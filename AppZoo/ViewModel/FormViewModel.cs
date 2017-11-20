using AppZoo.Enumeration;
using AppZoo.Model;
using AppZoo.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using System.Collections;
using Sullinger.ValidatableBase.Models.ValidationRules;
using Sullinger.ValidatableBase.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.Storage;

namespace AppZoo.ViewModel
{
    public class FormViewModel : Sullinger.ValidatableBase.Models.ValidatableBase, INotifyPropertyChanged
    {
        private MainViewModel mainViewModel;
        private AnimalDao animalDao;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Animal novoAnimal;
        public Animal NovoAnimal
        {
            get { return novoAnimal; }
            private set
            {
                if (novoAnimal != value)
                {
                    novoAnimal = value;
                }
            }
        }

        private string especie;

        [ValidateObjectHasValue(
            FailureMessage = "Informe a Espécie",
            ValidationMessageType = typeof(ValidationErrorMessage))]
        public string Especie
        {
            get { return especie; }
            set
            {
                if (especie != value)
                {
                    especie = value;
                    RaisePropertyChanged("Especie");
                }
            }
        }

        private string nome;

        [ValidateObjectHasValue(
            FailureMessage = "Informe o Nome",
            ValidationMessageType = typeof(ValidationErrorMessage))]
        public string Nome
        {
            get { return nome; }
            set
            {
                if (nome != value)
                {
                    nome = value;
                    RaisePropertyChanged("Nome");
                }
            }
        }

        private DateTime dataNascimento;
        public DateTime DataNascimento
        {
            get { return dataNascimento; }
            set
            {
                if (dataNascimento != value)
                {
                    dataNascimento = value;
                    RaisePropertyChanged("DataNascimento");
                }
            }
        }

        private string tamanho;

        [ValidateObjectHasValue(
            FailureMessage = "Informe o Tamanho",
            ValidationMessageType = typeof(ValidationErrorMessage))]
        public string Tamanho
        {
            get { return tamanho; }
            set
            {
                if (tamanho != value)
                {
                    tamanho = value;
                    RaisePropertyChanged("Tamanho");
                }
            }
        }

        private double custo;

        [ValidateObjectHasValue(
            FailureMessage = "Informe o Custo",
            ValidationMessageType = typeof(ValidationErrorMessage))]
        [ValidateNumberHasMinimumValue(
            MinimumValue = "1",
            FailureMessage = "Informe um valor maior que 0",
            ValidationMessageType = typeof(ValidationErrorMessage))]
        public double Custo
        {
            get { return custo; }
            set
            {
                if (custo != value)
                {
                    custo = value;
                    RaisePropertyChanged("Custo");
                }
            }
        }

        private string sexo;
        public string Sexo
        {
            get { return sexo; }
            set
            {
                if (sexo != value)
                {
                    sexo = value;
                    RaisePropertyChanged("Sexo");
                }
            }
        }

        private string cor;

        [ValidateObjectHasValue(
            FailureMessage = "Informe a Cor",
            ValidationMessageType = typeof(ValidationErrorMessage))]
        public string Cor
        {
            get { return cor; }
            set
            {
                if (cor != value)
                {
                    cor = value;
                    RaisePropertyChanged("Cor");
                }
            }
        }

        private string observacoes;

        [ValidateObjectHasValue(
            FailureMessage = "Informe a Observação",
            ValidationMessageType = typeof(ValidationErrorMessage))]
        public string Observacoes
        {
            get { return observacoes; }
            set
            {
                if (observacoes != value)
                {
                    observacoes = value;
                    RaisePropertyChanged("Observacoes");
                }
            }
        }

        private string nomeImagem;
        public string NomeImagem
        {
            get { return nomeImagem; }
            set
            {
                if (nomeImagem != value)
                {
                    nomeImagem = value;
                    RaisePropertyChanged("NomeImagem");
                    RaisePropertyChanged("FullPathImagem");
                }
            }
        }

        public string FullPathImagem {
            get { return NomeImagem != null ? System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, "Recursos", NomeImagem) : null; }       
        }

        private List<string> _listEspecie;
        public List<string> ListEspecie
        {
            get { return _listEspecie; }
            private set
            {
                if (_listEspecie != value)
                {
                    _listEspecie = value;
                    RaisePropertyChanged("ListEspecie");
                }
            }
        }


        private List<string> _listTamanho;
        public List<string> ListTamanho
        {
            get { return _listTamanho; }
            private set
            {
                if (_listTamanho != value)
                {
                    _listTamanho = value;
                    RaisePropertyChanged("ListTamanho");
                }
            }
        }


        private PropertyInfo[] _propertyCor;
        public PropertyInfo[] PropertyCor
        {
            get { return _propertyCor; }
            private set
            {
                if (_propertyCor != value)
                {
                    _propertyCor = value;
                    RaisePropertyChanged("PropertyCor");
                }
            }
        }

        private Dictionary<string, ObservableCollection<IValidationMessage>> validationMessages;
        public Dictionary<string, ObservableCollection<IValidationMessage>> ValidationMessages
        {
            get { return validationMessages; }
            private set
            {
                validationMessages = value;
                RaisePropertyChanged("ValidationMessages");
            }
        }


        private ICommand salvarCommand;
        public ICommand SalvarCommand
        {
            get {
                return salvarCommand ?? (salvarCommand = new CommandHandler(() => SalvarAnimal(), true));
            }
        }

        private ICommand carregaImagemCommand;
        public ICommand CarregaImagemCommand
        {
            get
            {
                return carregaImagemCommand ?? (carregaImagemCommand = new CommandHandler(() => CarregaImagem(), true));
            }
        }

        public FormViewModel(MainViewModel mainViewModel)
        {
            InicializaFromViewModel(mainViewModel);

            Sexo = "M";
            DataNascimento = DateTime.Now;

            NovoAnimal = new Animal();
        }

        public FormViewModel(MainViewModel mainViewModel, Animal animal)
        {
            InicializaFromViewModel(mainViewModel);

            if (animal != null)
            {
                Especie = animal.Especie;
                Nome = animal.Nome;
                DataNascimento = animal.DataNascimento;
                Tamanho = animal.Tamanho;
                Custo = animal.Custo;
                Sexo = animal.Sexo;
                Cor = animal.Cor;
                Observacoes = animal.Observacoes;
                NomeImagem = animal.NomeImagem;
            }

            NovoAnimal = animal;
        }

        private void InicializaFromViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            animalDao = new AnimalDao();

            PopulaListaEspecie();
            PopulaListaTamanho();
            PopulaListaCor();
        }

        private void PopulaListaEspecie()
        {
            List<string> listEspecie = new List<string>();

            foreach (string esp in Enum.GetNames(typeof(EspecieEnum)))
            {
                listEspecie.Add(esp);
            }

            ListEspecie = listEspecie.OrderBy(e => e).ToList();
        }

        private void PopulaListaTamanho()
        {
            List<string> listTamanho = new List<string>();

            foreach (string tam in Enum.GetNames(typeof(TamanhoEnum)))
            {
                listTamanho.Add(tam);
            }

            ListTamanho = listTamanho.ToList();
        }

        private void PopulaListaCor()
        {
            PropertyCor = typeof(Colors).GetProperties();
        }

        private async void CarregaImagem()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                try
                {                
                    string hashImagem = Utils.Utils.GeraMD5(file.Name) + DateTimeOffset.Now.ToUnixTimeMilliseconds() + file.FileType;
                    StorageFolder sf = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Recursos", CreationCollisionOption.OpenIfExists);
                    await file.CopyAsync(sf, hashImagem);

                    NomeImagem = hashImagem;
                }
                catch (Exception ex)
                {
                    var dialog = new MessageDialog("Falha ao carregar a imagem!");
                    dialog.Title = "Atenção!";
                    await dialog.ShowAsync();                  
                }
            }
        }

        private async Task<bool> ValidarDadosAnimal()
        {
            // valida se todos os campos foram informados
            this.ValidateAll();

            if (this.HasValidationMessages<ValidationErrorMessage>())
            {
                this.ValidationMessages = this.GetValidationMessages().ConvertValidationMessagesToObservable();
                return false;
            }
            else
            {
                this.ValidationMessages = null;
                //this.ValidationMessages.AsParallel().ForAll(item => item.Value.Clear());
            }

            // para salvar as informações na base de dados, pede confirmação para o usuário
            var msg = NovoAnimal.Id > 0 ? "Deseja salvar alterações do animal?" : "Deseja salvar dados do animal?";
            var conf = await AppZoo.Utils.Utils.CreateDialogConfirm(msg, "Atenção");

            if (conf == 0)
            {
                return false;
            }

            // verifica se o cadastro de novo animal já existe na base de dados
            var animal = NovoAnimal.Id > 0 ? null : animalDao.BuscaAnimal(Nome, Especie);

            if (animal != null)
            {
                var dialog = new MessageDialog("O animal que você está tentando incluir já existe no sistema!");
                dialog.Title = "Atenção!";
                await dialog.ShowAsync();
                return false;
            }

            return true;
        }

        private async void SalvarAnimal()
        {
            try
            {
                var isValid = await ValidarDadosAnimal();

                if (isValid)
                {
                    // persiste informações do animal na base de dados

                    NovoAnimal.Nome = Nome;
                    NovoAnimal.Especie = Especie;
                    NovoAnimal.DataNascimento = DataNascimento;
                    NovoAnimal.Tamanho = Tamanho;
                    NovoAnimal.Sexo = Sexo;
                    NovoAnimal.Cor = Cor;
                    NovoAnimal.Custo = Custo;
                    NovoAnimal.Observacoes = Observacoes;
                    NovoAnimal.NomeImagem = NomeImagem;

                    if (NovoAnimal.Id > 0)
                    {
                        animalDao.EditarAnimal(NovoAnimal);
                    }
                    else
                    {
                        animalDao.InserirAnimal(NovoAnimal);
                    }

                    mainViewModel.RecuperaListaAnimal();
                    mainViewModel.PopupIsOpen = false;
                }
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.Message);
                dialog.Title = "Erro ao salvar o animal!";
                await dialog.ShowAsync();
            }
        }

    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class SemeaduraPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); }
        }

        private int _cultivarSelectedIndex;
        public int CultivarSelectedIndex
        {
            get { return _cultivarSelectedIndex; }
            set { SetProperty(ref _cultivarSelectedIndex, value); }
        }

        private int _epocaSemeaduraSelectedIndex;
        public int EpocaSemeaduraSelectedIndex
        {
            get { return _epocaSemeaduraSelectedIndex; }
            set { SetProperty(ref _epocaSemeaduraSelectedIndex, value); }
        }

        private double _espacamento;
        public double Espacamento
        {
            get { return _espacamento; }
            set { SetProperty(ref _espacamento, value); }
        }

        private double _germinacao;
        public double Germinacao
        {
            get { return _germinacao; }
            set { SetProperty(ref _germinacao, value); }
        }

        private double _metrosLineares;
        public double MetrosLineares
        {
            get { return _metrosLineares; }
            set { SetProperty(ref _metrosLineares, value); }
        }

        private Cultivar _cultivar;
        public Cultivar Cultivar
        {
            get { return _cultivar; }
            set { SetProperty(ref _cultivar, value); }
        }

        private EpocaSemeadura _epocaSemeadura;
        public EpocaSemeadura EpocaSemeadura
        {
            get { return _epocaSemeadura; }
            set { SetProperty(ref _epocaSemeadura, value); }
        }

        private List<Cultivar> _cultivares;
        public List<Cultivar> Cultivares
        {
            get { return _cultivares; }
            set { SetProperty(ref _cultivares, value); }
        }

        private List<EpocaSemeadura> _epocasSemeadura;
        public List<EpocaSemeadura> EpocasSemeadura
        {
            get { return _epocasSemeadura; }
            set { SetProperty(ref _epocasSemeadura, value); }
        }

        private List<CultivarEpocaSemeadura> _cultivarEpocasSemeadura;
        public List<CultivarEpocaSemeadura> CultivarEpocasSemeadura
        {
            get { return _cultivarEpocasSemeadura; }
            set { SetProperty(ref _cultivarEpocasSemeadura, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand SemeaduraCalculateCommand { get; private set; }
        public DelegateCommand CultivarEpocaSemeaduraListCommand { get; private set; }

        public SemeaduraPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService) // 
        {
            Title = "Semeadura";

            // carrega lista de cultivares
            //var config = Xamarin.Forms.DependencyService.Get<IMySqlConnect>();
            //Cultivares = config.CarregaCultivares();

            //// carrega lista de semeaduras
            //EpocasSemeadura = config.CarregaEpocasSemeadura();

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();
            _cultivarSelectedIndex = -1;
            _epocaSemeaduraSelectedIndex = -1;

            Cultivares = _regulaApiService.GetCultivar();
            EpocasSemeadura = _regulaApiService.GetEpocaSemeadura();

            SemeaduraCalculateCommand = new DelegateCommand(SemeaduraCalculate);
            CultivarEpocaSemeaduraListCommand = new DelegateCommand(CultivarEpocaSemeaduraList);
        }

        private void SemeaduraCalculate()
        {
            // validar se selecionou cultivar e epoca de semeadura
            string message = validateFields();

            if(message.Equals(""))
            {
                // pega cultivar selecionada
                _cultivar = Cultivares.ElementAt(_cultivarSelectedIndex);

                // pega epoca de semeadura selecionada
                _epocaSemeadura = EpocasSemeadura.ElementAt(_epocaSemeaduraSelectedIndex);

                // passa parâmetros
                _navigationParameters.Add("cultivar", _cultivar);
                _navigationParameters.Add("epocaSemeadura", _epocaSemeadura);
                _navigationParameters.Add("epocaSemeaduraSelectedIndex", _epocaSemeaduraSelectedIndex);
                _navigationParameters.Add("epocasSemeadura", _epocasSemeadura);
                _navigationParameters.Add("cultivarEpocasSemeadura", _cultivarEpocasSemeadura);
                _navigationParameters.Add("espacamento", _espacamento);
                _navigationParameters.Add("metrosLineares", _metrosLineares);
                _navigationParameters.Add("germinacao", _germinacao);

                // vai pra página que realiza os cálculos e mostra os resultados
                _navigationService.NavigateAsync("CalcularSemeaduraPage", _navigationParameters);
            }
            else
            {
                _dialogService.DisplayAlertAsync("", message, "OK");
            }
            
        }

        private void CultivarEpocaSemeaduraList()
        {
            // carrega vínculo cultivar e época de semeadura
            var config = Xamarin.Forms.DependencyService.Get<IMySqlConnect>();

            _cultivar = Cultivares.ElementAt(_cultivarSelectedIndex);

            CultivarEpocasSemeadura = config.CarregaCultivarEpocaSemeadura(_cultivar.Id);
        }

        private string validateFields()
        {
            // valida campos
            if (_cultivarSelectedIndex == -1) // cultivar selecionada
                return "Selecione cultivar";
            else if (_epocaSemeaduraSelectedIndex == -1) // epoca semeadura selecionada
                return "Selecione a época de semeadura";
            else if (_espacamento < 0) // espacamento não pode ser negativo
                return "Espaçamento inválido"; 
            else if (_metrosLineares <= 0) // metros lineares maior que 0
                return "O valor dos metros lineares inválido";
            else if (_germinacao <= 0) // germinacao maior que 0
                return "O valor da germinação é inválido";

            return "";
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}

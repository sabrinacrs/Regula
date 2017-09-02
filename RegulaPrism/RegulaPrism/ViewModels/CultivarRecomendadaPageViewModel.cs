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
    public class CultivarRecomendadaPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
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

        private List<CultivarRecomendada> _cultivaresRecomendadas;
        public List<CultivarRecomendada> CultivaresRecomendadas
        {
            get { return _cultivaresRecomendadas; }
            set { SetProperty(ref _cultivaresRecomendadas, value); }
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

        public DelegateCommand CultivarConsultCommand { get; private set; }
        public DelegateCommand CultivarEpocaSemeaduraListCommand { get; private set; }

        public CultivarRecomendadaPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Buscar Cultivar Recomendada";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();
            _epocaSemeaduraSelectedIndex = -1;

            // carrega lista de cultivares
            Cultivares = _regulaApiService.GetCultivar();

            // carrega lista de epocas de semeadura
            EpocasSemeadura = _regulaApiService.GetEpocaSemeadura();

            _cultivaresRecomendadas = new List<CultivarRecomendada>();

            CultivarConsultCommand = new DelegateCommand(ConsultarCultivaresRecomendadas);
            CultivarEpocaSemeaduraListCommand = new DelegateCommand(CultivarEpocaSemeaduraList);
        }

        private void ConsultarCultivaresRecomendadas()
        {
            // validar se selecionou cultivar e epoca de semeadura
            string message = validateFields();

            if (message.Equals(""))
            {
                // pega epoca de semeadura selecionada
                _epocaSemeadura = EpocasSemeadura.ElementAt(_epocaSemeaduraSelectedIndex);

                // criar um filtro para já selecionar as recomendadas
                SelecionarCultivaresRecomendadas();

                _navigationParameters.Add("cultivares", _cultivares);
                _navigationParameters.Add("epocaSemeadura", _epocaSemeadura);
                _navigationParameters.Add("epocaSemeaduraSelectedIndex", _epocaSemeaduraSelectedIndex);
                _navigationParameters.Add("epocasSemeadura", _epocasSemeadura);
                _navigationParameters.Add("espacamento", _espacamento);
                _navigationParameters.Add("metrosLineares", _metrosLineares);
                _navigationParameters.Add("germinacao", _germinacao);
                _navigationParameters.Add("cultivaresRecomendadas", _cultivaresRecomendadas);

                // vai para página par selecionar doencas
                // vai para página de cultivares recomendadas
                _navigationService.NavigateAsync("CultivarRecomendadaDoencasPage", _navigationParameters);
            }
            else
            {
                _dialogService.DisplayAlertAsync("", message, "OK");
            }
        }

        private void SelecionarCultivaresRecomendadas()
        {
            List<CultivarRecomendada> cultivaresRecomendadas = new List<CultivarRecomendada>();

            // seleciona cultivares epoca semeadura, com base na epoca semeadura
            _cultivarEpocasSemeadura = _regulaApiService.GetCultivarEpocaSemeaduraEpocaId(_epocaSemeadura.Id);

            // selecionar cultivares que são recomendadas naquela semeadura
            foreach(var c in _cultivarEpocasSemeadura)
            {
                // se o número de plantas for maior que 0, é recomendado o plantio
                if(c.PlantasHa != 0)
                {
                    // procura cultivar e adiciona
                    CultivarRecomendada cr = new CultivarRecomendada();

                    cr.Cultivar = _cultivares.Find(x => x.Id == c.CultivarId);
                    cr.EpocaSemeadura = _epocasSemeadura.Find(x => x.Id == c.EpocaSemeaduraId);
                    cr.PlantasHectare = (int)c.PlantasHa;

                    _cultivaresRecomendadas.Add(cr);
                }
            }

        }

        private void CultivarEpocaSemeaduraList()
        {
            // carrega vínculo cultivar e época de semeadura
            _epocaSemeadura = EpocasSemeadura.ElementAt(_epocaSemeaduraSelectedIndex);

            CultivarEpocasSemeadura = _regulaApiService.GetCultivarEpocaSemeaduraEpocaId(_epocaSemeadura.Id);
        }

        private string validateFields()
        {
            // valida campos
            if (_epocaSemeaduraSelectedIndex == -1) // epoca semeadura selecionada
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
            parameters.Add("cultivaresRecomendadas", _cultivaresRecomendadas);
            parameters.Add("epocaSemeadura", _epocaSemeadura);
            parameters.Add("espacamento", _espacamento);
            parameters.Add("metrosLineares", _metrosLineares);
            parameters.Add("germinacao", _germinacao);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}

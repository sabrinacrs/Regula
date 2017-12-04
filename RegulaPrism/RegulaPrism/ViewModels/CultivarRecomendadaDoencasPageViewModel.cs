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
    public class CultivarRecomendadaDoencasPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _doencaSwitchCell;
        public string DoencaSwitchCell
        {
            get { return _doencaSwitchCell; }
            set { SetProperty(ref _doencaSwitchCell, value); }
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

        private List<Doenca> _doencas;
        public List<Doenca> Doencas
        {
            get { return _doencas; }
            set { SetProperty(ref _doencas, value); }
        }

        private List<Doenca> _doencasSolo;
        public List<Doenca> DoencasSolo
        {
            get { return _doencasSolo; }
            set { SetProperty(ref _doencasSolo, value); }
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

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand CultivarRecomendadaListCommand { get; private set; }

        public DelegateCommand AddRemoveDoencaCommand { get; private set; }

        public CultivarRecomendadaDoencasPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Doenças";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();
            _doencasSolo = new List<Doenca>();
            _doencas = new List<Doenca>();

            // carrega lista de doencas
            loadDoencas();

            CultivarRecomendadaListCommand = new DelegateCommand(CultivarRecomendadaList);
            AddRemoveDoencaCommand = new DelegateCommand(AddRemoveDoenca);
        }

        private void AddRemoveDoenca()
        {
            // adiciona doenca à lista de doencas do solo
            // procura doenca na lista com todas as doencas
            var doe = _doencas.Find(d => d.Id == Int32.Parse(_doencaSwitchCell));

            // verifica se já foi inserida na list de doencas do solo
            Doenca doeSolo = _doencasSolo.Find(d => d.Id == Int32.Parse(_doencaSwitchCell));

            // se já foi inserida, remove
            if(doeSolo != null)
            {
                int pos = _doencasSolo.FindIndex(d => d.Id == doeSolo.Id);
                _doencasSolo.Remove(doeSolo);
            }
            else
            {
                _doencasSolo.Add(doe);
            }
        }

        private void CultivarRecomendadaList()
        {
            // filtra dentre a lista de cultivares recomendadas
            _navigationParameters.Add("doencasSolo", _doencasSolo);
            _navigationParameters.Add("cultivaresRecomendadas", _cultivaresRecomendadas);
            _navigationParameters.Add("epocaSemeadura", _epocaSemeadura);
            _navigationParameters.Add("espacamento", _espacamento);
            _navigationParameters.Add("metrosLineares", _metrosLineares);
            _navigationParameters.Add("germinacao", _germinacao);

            if (_doencasSolo.Count() > 0)
            {
                // vai para página com sliders
                // página com tabela de cultivares recomendadas e doencas
                _navigationService.NavigateAsync("CultivarDoencaToleranciaPage", _navigationParameters);
            }
            else
            {
                // vai para cultivar list
                _navigationService.NavigateAsync("CultivarListPage", _navigationParameters);
            }
        }

        private void loadDoencas()
        {
            //Doencas = _regulaApiService.GetDoenca();

            List<Doenca> doencasTable = _regulaApiService.GetDoenca();

            // priorizar nematoides, ramularia e ramulose
            foreach(var x in doencasTable)
            {
                if(x.Descricao.Contains("Nematoide") || x.Descricao.Contains("Ramulária") || x.Descricao.Contains("Ramulose"))
                {
                    _doencas.Add(x);
                    doencasTable.Remove(x);
                }
            }

            foreach (var x in doencasTable)
            {
                _doencas.Add(x);
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
            _epocaSemeadura = (EpocaSemeadura)parameters["epocaSemeadura"];
            _espacamento = (double)parameters["espacamento"];
            _metrosLineares = (double)parameters["metrosLineares"];
            _germinacao = (double)parameters["germinacao"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
            CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
            _epocaSemeadura = (EpocaSemeadura)parameters["epocaSemeadura"];
            _espacamento = (double)parameters["espacamento"];
            _metrosLineares = (double)parameters["metrosLineares"];
            _germinacao = (double)parameters["germinacao"];
        }
    }
}

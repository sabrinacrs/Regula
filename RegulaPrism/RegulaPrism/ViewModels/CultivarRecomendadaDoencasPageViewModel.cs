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

            // carrega lista de epocas de semeadura
            //var config = Xamarin.Forms.DependencyService.Get<IMySqlConnect>();
            //Doencas = config.CarregaDoencas();

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            Doencas = _regulaApiService.GetDoenca();

            CultivarRecomendadaListCommand = new DelegateCommand(CultivarRecomendadaList);
            AddRemoveDoencaCommand = new DelegateCommand(AddRemoveDoenca);
        }

        private void AddRemoveDoenca()
        {

        }

        private void CultivarRecomendadaList()
        {
            // adiciona filtros de doencas, etc

            // filtra dentre a lista de cultivares recomendadas

            // vai para cultivar list
            _navigationService.NavigateAsync("CultivarListPage", _navigationParameters);

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            // tem que receber vários parametros - epoca semeadura, etc
            CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
            _epocaSemeadura = (EpocaSemeadura)parameters["epocaSemeadura"];
            _espacamento = (double)parameters["espacamento"];
            _metrosLineares = (double)parameters["metrosLineares"];
            _germinacao = (double)parameters["germinacao"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
        }
    }
}

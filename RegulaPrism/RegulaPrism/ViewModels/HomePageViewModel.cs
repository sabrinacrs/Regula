using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using RegulaPrism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class HomePageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { SetProperty(ref _cliente, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IInformacoesManuais _informacoesManuais;

        private NavigationParameters _navigationParameters;

        public DelegateCommand NavigateToCultivarListPageCommand { get; private set; }
        public DelegateCommand NavigateToSemeaduraPageCommand { get; private set; }
        public DelegateCommand NavigateToFazendaHomePageCommand { get; private set; }
        public DelegateCommand NavigateToTalhaoHomePageCommand { get; private set; }
        public DelegateCommand NavigateToClienteUpdatePageCommand { get; private set; }
        public DelegateCommand InfoCommand { get; private set; }

        // teste
        public DelegateCommand NavigateToCultivarDoencasPageCommand { get; private set; }

        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICloneDatabaseServer cloneDatabaseServer, IRegulaApiService regulaApiService, IInformacoesManuais informacoesManuais)
        {
            Title = "CottonApp";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _informacoesManuais = informacoesManuais;
            _navigationParameters = new NavigationParameters();
            
            NavigateToCultivarListPageCommand = new DelegateCommand(NavigateToCultivarListPage);
            NavigateToSemeaduraPageCommand = new DelegateCommand(NavigateToSemeaduraPage);
            NavigateToFazendaHomePageCommand = new DelegateCommand(NavigateToFazendaHomePage);
            NavigateToTalhaoHomePageCommand = new DelegateCommand(NavigateToTalhaoHomePage);
            NavigateToClienteUpdatePageCommand = new DelegateCommand(NavigateToClienteUpdatePage);
            InfoCommand = new DelegateCommand(Informacoes);

            // teste
            NavigateToCultivarDoencasPageCommand = new DelegateCommand(NavigateToCultivarDoencasPage);
        }

        private void NavigateToCultivarListPage()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/CultivarHomePage", UriKind.Absolute), _navigationParameters);
            //_navigationParameters.Add("cliente", _cliente);
            //_navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/CultivarRecomendadaPage", UriKind.Absolute), _navigationParameters);
        }

        private void NavigateToSemeaduraPage()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/SemeaduraPage", UriKind.Absolute), _navigationParameters);
        }

        private void NavigateToFazendaHomePage()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/FazendaHomePage", UriKind.Absolute), _navigationParameters);
        }

        private void NavigateToTalhaoHomePage()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/TalhaoHomePage", UriKind.Absolute), _navigationParameters);
        }

        private void NavigateToClienteUpdatePage()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync("ClienteUpdatePage", _navigationParameters);
        }

        // teste
        private void NavigateToCultivarDoencasPage()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/CultivaresDoencasListPage", UriKind.Absolute), _navigationParameters);
        }

        private void Informacoes()
        {
            InformacaoManual im = _informacoesManuais.InformacoesHomePage();

            _navigationParameters.Add("informacao", im);

            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
        }
    }
}

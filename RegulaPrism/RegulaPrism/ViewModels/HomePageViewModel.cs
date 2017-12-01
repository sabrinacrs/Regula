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

        private IRegulaApiService _regulaApiService;

        private IPageDialogService _dialogService;

        private IInformacoesManuais _informacoesManuais;

        private NavigationParameters _navigationParameters;

        private GetDatabases _databaseServer;

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
            _regulaApiService = regulaApiService;
            _dialogService = dialogService;
            _informacoesManuais = informacoesManuais;
            _navigationParameters = new NavigationParameters();
            _databaseServer = new GetDatabases();

            NavigateToCultivarListPageCommand = new DelegateCommand(NavigateToCultivarListPage);
            NavigateToSemeaduraPageCommand = new DelegateCommand(NavigateToSemeaduraPage);
            NavigateToFazendaHomePageCommand = new DelegateCommand(NavigateToFazendaHomePage);
            NavigateToTalhaoHomePageCommand = new DelegateCommand(NavigateToTalhaoHomePage);
            NavigateToClienteUpdatePageCommand = new DelegateCommand(NavigateToClienteUpdatePage);
            InfoCommand = new DelegateCommand(Informacoes);

            // teste
            NavigateToCultivarDoencasPageCommand = new DelegateCommand(NavigateToCultivarDoencasPage);

            // check release
            CheckRelease();
        }

        private void NavigateToCultivarListPage()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/CultivarHomePage", UriKind.Absolute), _navigationParameters);
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

        // verifica se há atualização disponível
        public async void CheckRelease()
        {
            if(checkReleasesOnServer())
            {
                // alert para clonar ou não a base de dados
                var choise = await _dialogService.DisplayAlertAsync("Atualização", "Há atualizações disponíveis. Sincronizar dados agora?", "Sim", "Não");

                if (choise)
                {
                    // carregar dados do servidor p/ app
                    try
                    {
                        _databaseServer.getDatabases(_regulaApiService);

                        // insere historico atualizacao no sqlite
                        insereHistoricoAtualizacao();
                    }
                    catch (Exception ex)
                    {
                        await _dialogService.DisplayAlertAsync("", ex.ToString(), "OK");
                    }
                }
            }         
        }


        private bool checkReleasesOnServer()
        {
            // conseme API de historico_atualização do servidor
            HistoricoAtualizacao lastReleaseServer = _databaseServer.GetLastRelease();

            // pega ultima atualizacao do sqlite
            HistoricoAtualizacao lastReleaseSqlite = _regulaApiService.GetLastHistoricoAtualizacao();

            if(lastReleaseSqlite == null) // primeiro acesso
            {
                return true;
            }
            else
            {
                // converter string datas para comparar
                DateTime dataReleaseServer = Convert.ToDateTime(lastReleaseServer.DataAtualizacao);
                DateTime dataReleaseSqlite = Convert.ToDateTime(lastReleaseSqlite.DataAtualizacao);

                if (dataReleaseServer < dataReleaseSqlite) // se nao tem atualizacao disponivel
                {
                    return false;
                }
            }

            return true;
        }

        private void insereHistoricoAtualizacao()
        {
            // insere historico atualizacao no sqlite
            HistoricoAtualizacao ha = new HistoricoAtualizacao();
            ha.DataAtualizacao = DateTime.Now.Date.ToString();
            ha.Status = "D";

            if (!_regulaApiService.InsertHistoricoAtualizacao(ha))
            {
                _dialogService.DisplayAlertAsync("", "Ocorreu um erro ao sicronizar a base de dados. Verifique sua conexão e tente novamente", "OK");
            }
        }
    }
}

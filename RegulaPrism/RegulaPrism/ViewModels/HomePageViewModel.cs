using Plugin.Connectivity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using RegulaPrism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
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
        public DelegateCommand NavigateToAjudaPageCommand { get; private set; }
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
            NavigateToAjudaPageCommand = new DelegateCommand(NavigateToAjudaPage);
            InfoCommand = new DelegateCommand(Informacoes);

            // teste
            NavigateToCultivarDoencasPageCommand = new DelegateCommand(NavigateToCultivarDoencasPage);

            // check release
            if(CrossConnectivity.Current.IsConnected)
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

        private void NavigateToAjudaPage()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (CrossConnectivity.Current.IsConnected)
                    Device.OpenUri(new Uri("http://www.cottonappadm.xyz/help"));
            }
            else
            {
                _dialogService.DisplayAlertAsync("", "Não foi possível acessar a página. Verifique sua conexão e tente novamente.", "OK");
            }
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
                    // componente loading 
                    _isLoading = true;

                    // delay para carregamento
                    await Task.Delay(3000);

                    // carregar dados do servidor p/ app
                    try
                    {
                        // obter base de dados de cultivares
                        _databaseServer.getDatabases(_regulaApiService);

                        //// obter fazendas do cliente
                        //_databaseServer.saveFazendasCliente(_cliente);

                        //// obter talhoes do cliente
                        //_databaseServer.saveTalhoesFazendasCliente(_cliente);

                        // insere historico atualizacao no sqlite
                        insereHistoricoAtualizacao();
                    }
                    catch (Exception ex)
                    {
                        await _dialogService.DisplayAlertAsync("", ex.ToString(), "OK");
                    }

                    _isLoading = false;
                }
            }         
        }


        private bool checkReleasesOnServer()
        {
            // conseme API de historico_atualização do servidor
            HistoricoAtualizacao lastReleaseServer = _databaseServer.GetLastRelease();

            // pega ultima atualizacao do sqlite
            HistoricoAtualizacao lastReleaseSqlite = _regulaApiService.GetLastHistoricoAtualizacao();

            // quando lastReleaseSqlite == null é primeiro acesso
            if (lastReleaseSqlite != null) 
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

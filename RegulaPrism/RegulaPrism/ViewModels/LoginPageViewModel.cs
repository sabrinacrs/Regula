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
    public class LoginPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set { SetProperty(ref _senha, value); }
        }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { SetProperty(ref _cliente, value); }
        }

        private INavigationService _navigationService;

        private IRegulaApiService _regulaApiService;

        private IInformacoesManuais _informacoesManuais;

        private ICloneDatabaseServer _cloneDatabaseServer;

        private IPageDialogService _dialogService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand NavigateToClienteCreatePageCommand { get; private set; }
        public DelegateCommand NavigateToHomeMasterDetailPageCommand { get; private set; }
        public DelegateCommand InfoCommand { get; private set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, ICloneDatabaseServer cloneDatabaseServer, IInformacoesManuais informacoesManuais)
        {
            // binding do título da página
            Title = "CottonApp";

            // services
            _navigationParameters = new NavigationParameters();
            _navigationService = navigationService;
            _regulaApiService = regulaApiService;
            _informacoesManuais = informacoesManuais;
            _dialogService = dialogService;
            _cloneDatabaseServer = cloneDatabaseServer;

            // instanciar commands
            NavigateToClienteCreatePageCommand = new DelegateCommand(NavigateToClienteCreatePage);
            NavigateToHomeMasterDetailPageCommand = new DelegateCommand(NavigateToHomeMasterDetailPage);
            InfoCommand = new DelegateCommand(Informacoes);
        }

        private void NavigateToClienteCreatePage()
        {
            _navigationService.NavigateAsync("ClienteCreatePage");
            //_navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/ClienteCreatePage", UriKind.Absolute));
        }

        private void NavigateToHomeMasterDetailPage()
        {
            // poupa tempo
            if (Login == null && Senha == null)
            {
                Login = "sabrinacr";
                Senha = "eitagiovana";
            }

            // validações do login
            var cliente = _regulaApiService.GetClienteByEmail(Login);
            if (cliente == null)
                cliente = _regulaApiService.GetClienteByLogin(Login);

            if (cliente == null)
                _dialogService.DisplayAlertAsync("", "Este login/e-mail não consta em nossos registros", "OK");
            else
            {
                if(cliente.DataDesativacao.Year == 1)
                {
                    if (!Senha.Equals(cliente.Senha) || Senha == null)
                        _dialogService.DisplayAlertAsync("", "Senha inválida", "OK");
                    else
                    {
                        Cliente = new Cliente();
                        Cliente = cliente;

                        // clonar a base de dados
                        if (_regulaApiService.GetCultivar().Count() <= 0)
                        {
                            GetDatabases gdb = new GetDatabases();
                            gdb.getDatabases(_regulaApiService);
                        }
                            //if(_regulaApiService.GetCultivar().Count() <= 0)
                            //    _cloneDatabaseServer.CloneDatabase(_regulaApiService);

                        _navigationParameters.Add("cliente", _cliente);
                        _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/HomePage", UriKind.Absolute), _navigationParameters);
                    }
                }
                else
                    _dialogService.DisplayAlertAsync("", "Conta desativada", "OK");
            }

            //_navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/HomePage", UriKind.Absolute));

        }

        //async void getData()
        //{
        //    DataService ds = new DataService();
        //    List<Cultivar> cs = await ds.GetCultivaresAsync();
        //    int x = 0;
        //    x++;
        //}

        private void Informacoes()
        {
            // tela de informações de navegação
            // recupera titulo e texto da interface IInformacoesManuais
            // chama a mesma tela para exibir as coisas

            InformacaoManual im = _informacoesManuais.InformacoesLogin();

            _navigationParameters.Add("informacao", im);

            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            //parameters.Add("cliente", _cliente);
            _cliente = (Cliente)parameters["cliente"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //parameters.Add("cliente", _cliente);
            _cliente = (Cliente)parameters["cliente"];
        }
    }
}

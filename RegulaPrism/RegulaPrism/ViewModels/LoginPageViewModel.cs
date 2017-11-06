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

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return this._isLoading;
            }
            set
            {
                this._isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
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

            // teste
            _isLoading = false;

            // instanciar commands
            NavigateToClienteCreatePageCommand = new DelegateCommand(NavigateToClienteCreatePage);
            NavigateToHomeMasterDetailPageCommand = new DelegateCommand(NavigateToHomeMasterDetailPage);
            InfoCommand = new DelegateCommand(Informacoes);

            // verifica se ja tem usuario cadastrado
            if (_regulaApiService.GetClientes().Count() > 0 && Login == null && Senha == null)
            {
                Cliente c = new Cliente();
                c = _regulaApiService.GetClientes().ElementAt(0);

                Login = c.Login;
                Senha = c.Senha;
            }
        }

        private void NavigateToClienteCreatePage()
        {
            _navigationService.NavigateAsync("ClienteCreatePage");
            //_navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/ClienteCreatePage", UriKind.Absolute));
        }

        private async void NavigateToHomeMasterDetailPage()
        {
            // validações do login
            var cliente = _regulaApiService.GetClienteByEmail(Login);

            if (cliente == null)
                cliente = _regulaApiService.GetClienteByLogin(Login);

            // cliente não encontrado
            if (cliente == null)
            {
                await _dialogService.DisplayAlertAsync("", "Este login/e-mail não consta em nossos registros", "OK");
            }
            else if (cliente.Status.Equals("I"))
            {
                // conta desativada
                var choise = await _dialogService.DisplayAlertAsync("Confirmação", "Esta conta foi desativa. Deseja reativá-la?", "Sim", "Não");

                if (choise)
                {
                    cliente.Status = "A";
                    _regulaApiService.UpdateCliente(cliente);
                    await _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/NavigationPage/LoginPage", UriKind.Absolute));
                }
            }
            else
            {
                if(cliente.DataDesativacao.Year == 1)
                {
                    if (!Senha.Equals(cliente.Senha) || Senha == null)
                        await _dialogService.DisplayAlertAsync("", "Senha inválida", "OK");
                    else
                    {
                        Cliente = new Cliente();
                        Cliente = cliente;

                        // clonar a base de dados
                        if (_regulaApiService.GetCultivar().Count() <= 0)
                        {
                            //GetDatabases gdb = new GetDatabases();
                            //gdb.getDatabases(_regulaApiService);

                            // teste
                            try
                            {
                                // teste
                                _isLoading = true;

                                GetDatabases gdb = new GetDatabases();
                                gdb.getDatabases(_regulaApiService);

                                await Task.Delay(4000);

                                _isLoading = false;
                            }
                            catch (Exception ex)
                            {
                                _isLoading = false;
                                await _dialogService.DisplayAlertAsync("", ex.ToString(), "OK");
                            }
                        }

                        _navigationParameters.Add("cliente", _cliente);

                        await _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/HomePage", UriKind.Absolute), _navigationParameters);
                    }
                }
                else
                    await _dialogService.DisplayAlertAsync("", "Conta desativada", "OK");
            }

        }

        private void Informacoes()
        {
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
            _cliente = (Cliente)parameters["cliente"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
        }
    }
}

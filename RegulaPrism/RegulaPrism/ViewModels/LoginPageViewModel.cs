using Plugin.Connectivity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using RegulaPrism.Models.Json;
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

        private ClienteJson _clienteJson;
        public ClienteJson ClienteJson
        {
            get { return _clienteJson; }
            set { SetProperty(ref _clienteJson, value); }
        }

        private INavigationService _navigationService;

        private IRegulaApiService _regulaApiService;

        private IInformacoesManuais _informacoesManuais;

        private ICloneDatabaseServer _cloneDatabaseServer;

        private IPageDialogService _dialogService;

        private NavigationParameters _navigationParameters;

        private GetDatabases _databaseServer;

        public DelegateCommand NavigateToClienteCreatePageCommand { get; private set; }
        public DelegateCommand NavigateToHomeMasterDetailPageCommand { get; private set; }
        public DelegateCommand InfoCommand { get; private set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, ICloneDatabaseServer cloneDatabaseServer, IInformacoesManuais informacoesManuais)
        {
            // binding do título da página
            Title = "CottonApp";

            // services
            _navigationParameters = new NavigationParameters();
            _databaseServer = new GetDatabases();
            _navigationService = navigationService;
            _regulaApiService = regulaApiService;
            _informacoesManuais = informacoesManuais;
            _dialogService = dialogService;
            _cloneDatabaseServer = cloneDatabaseServer;

            // instanciar commands
            NavigateToClienteCreatePageCommand = new DelegateCommand(NavigateToClienteCreatePage);
            NavigateToHomeMasterDetailPageCommand = new DelegateCommand(NavigateToHomeMasterDetailPage);
            InfoCommand = new DelegateCommand(Informacoes);

            // verifica se ja tem usuario cadastrado para carregar campos automaticamente
            if (_regulaApiService.GetClientes().Count() > 0 && Login == null && Senha == null)
            {
                Cliente c = new Cliente();
                c = _regulaApiService.GetClientes().ElementAt(0);

                // verificar conexão antes
                if (c != null && CrossConnectivity.Current.IsConnected)
                {
                    getVerifyStatusCliente(c);
                }
                else
                {
                    Login = c.Login;
                    Senha = c.Senha;
                }
            }
        }

        private void NavigateToClienteCreatePage()
        {
            _navigationService.NavigateAsync("ClienteCreatePage");
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
            else
            {
                // verifica status - se o cliente está ativado / desativado
                if(clienteDisable(cliente.Status))
                {
                    // conta desativada
                    var choise = await _dialogService.DisplayAlertAsync("Confirmação", "Esta conta foi desativada. Deseja reativá-la?", "Sim", "Não");

                    if (choise)
                    {
                        if(CrossConnectivity.Current.IsConnected) // se está conectado com a internet, faz a atualização
                        {
                            cliente.Status = "A";
                            if (_regulaApiService.UpdateCliente(cliente))
                                _databaseServer.UpdateClienteOnServer(cliente); // atualiza status do cliente no servidor

                            await _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/NavigationPage/LoginPage", UriKind.Absolute));
                        }
                        else
                        {
                            await _dialogService.DisplayAlertAsync("", "Não foi possível reativar a conta. Verifique sua conexão com a internet e tente novamente.", "OK");
                        }
                    }
                }
                else if (clienteDisableAdm(cliente.Status))
                {
                    // conta desativada pelo adm
                    await _dialogService.DisplayAlertAsync("", "Esta conta foi desativada pelo administrador do sistema.", "OK");
                }
                else
                {
                    if (!Senha.Equals(cliente.Senha) || Senha == null)
                        await _dialogService.DisplayAlertAsync("", "Senha inválida", "OK");
                    else
                    {
                        Cliente = new Cliente();
                        Cliente = cliente;
                        _navigationParameters.Add("cliente", _cliente);

                        await _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/HomePage", UriKind.Absolute), _navigationParameters);
                    }
                }
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

        // cliente desativado pelo Adm
        private bool clienteDisableAdm(string status)
        {
            return status.Equals("IA");
        }

        // cliente desativado
        private bool clienteDisable(string status)
        {
            return status.Equals("I");
        }

        private void getVerifyStatusCliente(Cliente c)
        {
            // verifica status desse login no servidor
            GetDatabases gdb = new GetDatabases();
            _clienteJson = gdb.GetClienteServer(c);

            if(_clienteJson != null)
            {
                // atualizar status do cliente de acordo com o que está no servidor
                if(!c.Status.Equals(_clienteJson.status))
                {
                    c.Status = _clienteJson.status;
                    // se atualizou e o status não é IA nem I, atribui login e senha no formulário
                    if(_regulaApiService.UpdateCliente(c) && c.Status.Equals("IA") || c.Status.Equals("I"))
                    {
                        Login = c.Login;
                        Senha = c.Senha;
                    }
                }
                else
                {
                    Login = c.Login;
                    Senha = c.Senha;
                }
            }
            else
            {
                // cliente foi excluído pelo Adm
                // exclui cliente do banco local
                _regulaApiService.DeleteCliente(c);
            }
        }

        // verifica status desse login no servidor
//        ClienteJson cj = _databaseServer.GetClienteServer(cliente);
            
//            // cliente não encontrado
//            if (cliente == null)
//            {
//                await _dialogService.DisplayAlertAsync("", "Este login/e-mail não consta em nossos registros", "OK");
//    }
//            // conta desativada pelo adms
//            else if (cliente.Status.Equals("IA")) // IA - inativado pelo administrador
//            {
//                if(cj != null && !cj.status.Equals("IA"))
//                {
//                    cliente.Status = cj.status;
//                    _regulaApiService.UpdateCliente(cliente);

//                    _navigationParameters.Add("cliente", _cliente);
//                    await _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/HomePage", UriKind.Absolute), _navigationParameters);
//                }
//                else
//                {
//                    // conta desativada pelo adm
//                    await _dialogService.DisplayAlertAsync("", "Esta conta foi desativada pelo administrador do sistema.", "OK");
//                }
//            }
//            else if (cliente.Status.Equals("I"))
//            {
//                // conta desativada
//                var choise = await _dialogService.DisplayAlertAsync("Confirmação", "Esta conta foi desativada. Deseja reativá-la?", "Sim", "Não");

//                if (choise)
//                {
//                    cliente.Status = "A";
//                    if(_regulaApiService.UpdateCliente(cliente))
//                        _databaseServer.UpdateClienteOnServer(cliente);

//                    await _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/NavigationPage/LoginPage", UriKind.Absolute));
//                }
//            }
//            else
//            {
//                if (!Senha.Equals(cliente.Senha) || Senha == null)
//                    await _dialogService.DisplayAlertAsync("", "Senha inválida", "OK");
//                else
//                {
//                    Cliente = new Cliente();
//Cliente = cliente;
//                    _navigationParameters.Add("cliente", _cliente);

//                    await _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/HomePage", UriKind.Absolute), _navigationParameters);
//                }
//            }
        //// verifica status desse login no servidor
        //GetDatabases gdb = new GetDatabases();
        //ClienteJson cj = gdb.GetClienteServer(c);

        //if(c.Status.Equals("IA"))
        //{
        //    if(cj != null && !cj.status.Equals("IA"))
        //    {
        //        c.Status = cj.status;
        //        _regulaApiService.UpdateCliente(c);

        //        Login = c.Login;
        //        Senha = c.Senha;
        //    }
        //}
        //else if (!c.Status.Equals("I"))
        //{
        //    // se não econtrou cliente, adm excluiu o cliente
        //    if (cj != null)
        //    {
        //        if (!clienteIsDisable(cj))
        //        {
        //            Login = c.Login;
        //            Senha = c.Senha;
        //        }
        //        else
        //        {
        //            c.Status = cj.status;
        //            _regulaApiService.UpdateCliente(c);
        //        }
        //    }
        //    else
        //    {
        //        // exclui cliente do banco local
        //        _regulaApiService.DeleteCliente(c);
        //    }
        //}

        //private async void consumeAPI()
        //{
        //    // clonar a base de dados
        //    if (_regulaApiService.GetCultivar().Count() <= 0)
        //    {
        //        try
        //        {
        //            GetDatabases gdb = new GetDatabases();
        //            gdb.getDatabases(_regulaApiService);

        //            // insere historico atualizacao no sqlite
        //            HistoricoAtualizacao ha = new HistoricoAtualizacao();
        //            ha.DataAtualizacao = DateTime.Now.Date.ToString();
        //            ha.Status = "D";

        //            if (!_regulaApiService.InsertHistoricoAtualizacao(ha))
        //            {
        //                await _dialogService.DisplayAlertAsync("", "Ocorreu um erro ao sicronizar a base de dados. Verifique sua conexão e tente novamente", "OK");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _dialogService.DisplayAlertAsync("", ex.ToString(), "OK");
        //        }
        //    }
        //}
    }
}

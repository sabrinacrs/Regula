using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using RegulaPrism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace RegulaPrism.ViewModels
{
    public class ClienteCreatePageViewModel : BindableBase
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

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
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

        private string _confirmaSenha;
        public string ConfirmaSenha
        {
            get { return _confirmaSenha; }
            set { SetProperty(ref _confirmaSenha, value); }
        }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { SetProperty(ref _cliente, value); }
        }

        private IRegulaApiService _regulaApiService;

        private IInformacoesManuais _informacoesManuais;

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private ICloneDatabaseServer _cloneDatabaseServer;

        private NavigationParameters _navigationParameters;

        public DelegateCommand NavigateToFazendaContatoPageCommand { get; private set; }
        public DelegateCommand ClienteSaveCommand { get; private set; }

        public DelegateCommand InfoCommand { get; private set; }

        public ClienteCreatePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, ICloneDatabaseServer cloneDatabaseServer, IInformacoesManuais informacoesManuais)
        {
            // binding para titulo da pagina
            Title = "Cadastro Cliente";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _informacoesManuais = informacoesManuais;
            _cloneDatabaseServer = cloneDatabaseServer;
            _navigationParameters = new NavigationParameters();

            ClienteSaveCommand = new DelegateCommand(ClienteSave);
            InfoCommand = new DelegateCommand(Informacoes);
        }

        private void ClienteSave()
        {
            //validações e inserção no banco
            string message = messageValidateFields();

            // verifica mensagem de erro no preenchimento do formulario
            if (message.Equals(""))
            {
                Cliente cliente = clienteView();

                if (_regulaApiService.InsertCliente(cliente))
                {
                    // passa parametro navigationaware
                    _navigationParameters.Add("cliente", cliente);

                    _dialogService.DisplayAlertAsync("Bem-Vindo(a)!", "Sua conta foi criada com sucesso", "OK");

                    // clonar a base de dados
                    if (_regulaApiService.GetCultivar().Count() <= 0)
                    {
                        GetDatabases gdb = new GetDatabases();
                        gdb.getDatabases(_regulaApiService);
                    }

                    _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/HomePage", UriKind.Absolute), _navigationParameters);
                }
                else
                {
                    _dialogService.DisplayAlertAsync("Alerta", "Algo deu errado durante a tentativa de cadastro. Verifique seus dados e tente novamente.", "OK");
                }
            }
            else
            {
                _dialogService.DisplayAlertAsync("Atenção", message , "OK");
            }
        }

        private void Informacoes()
        {
            // tela de informações de navegação
            // recupera titulo e texto da interface IInformacoesManuais
            // chama a mesma tela para exibir as coisas

            InformacaoManual im = _informacoesManuais.InformacoesClienteCreate();

            _navigationParameters.Add("informacao", im);

            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        private string messageValidateFields()
        {
            if (Nome == null || Nome.Equals(""))
                return "Informe seu nome";
            else if (Email == null || Email.Equals(""))
                return "Informe seu email";
            else if (Email != null && !emailIsValid(Email))
                return "Email inválido";
            else if (_regulaApiService.GetClienteByEmail(Email) != null)
                return "O email informado já está em uso";
            else if (Login == null || Login.Equals(""))
                return "Informe um login";
            else if (_regulaApiService.GetClienteByLogin(Login) != null)
                return "O login informado já está sendo utilizado";
            else if (Senha == null || Senha.Equals(""))
                return "Crie uma senha de acesso para a segurança de suas informações";
            else if (Senha != null && Senha.Length < 4)
                return "A senha deve conter no mínimo 4 dígitos";
            else if (ConfirmaSenha == null || ConfirmaSenha.Equals(""))
                return "Por favor, confirme a senha";
            else if (!Senha.Equals(ConfirmaSenha))
                return "As senhas não conferem";

            return "";
        }

        public bool emailIsValid(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
                return false;
            return true;
        }

        private Cliente clienteView()
        {
            Cliente cliente = new Cliente();
            cliente.Nome = Nome;
            cliente.Email = Email;
            cliente.Login = Login;
            cliente.Senha = Senha;
            cliente.Status = "A";

            return cliente;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}

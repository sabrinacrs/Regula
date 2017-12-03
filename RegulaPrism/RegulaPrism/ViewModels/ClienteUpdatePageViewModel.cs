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

namespace RegulaPrism.ViewModels
{
    public class ClienteUpdatePageViewModel : BindableBase, INavigationAware
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

        private IRegulaApiService _regulaApiService;

        private IInformacoesManuais _informacoesManuais;

        private NavigationParameters _navigationParameters;

        public DelegateCommand ClienteSaveCommand { get; private set; }
        public DelegateCommand ClienteDeleteCommand { get; private set; }
        public DelegateCommand ClienteDisableCommand { get; private set; }

        public DelegateCommand InfoCommand { get; private set; }

        public ClienteUpdatePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, IInformacoesManuais informacoesManuais)
        {
            Title = "Dados Pessoais";

            _navigationService = navigationService;
            _regulaApiService = regulaApiService;
            _dialogService = dialogService;
            _informacoesManuais = informacoesManuais;
            _navigationParameters = new NavigationParameters();
            
            ClienteSaveCommand = new DelegateCommand(ClienteSave);
            ClienteDeleteCommand = new DelegateCommand(ClienteDelete);
            ClienteDisableCommand = new DelegateCommand(ClienteDisable);
            InfoCommand = new DelegateCommand(Informacoes);
        }

        private void ClienteSave()
        {
            // validações
            string message = validateFields();

            if(message.Equals(""))
            {
                // update cliente
                if(_regulaApiService.UpdateCliente(_cliente))
                {
                    _navigationParameters.Add("cliente", _cliente);

                    // atualiza cliente no servidor
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        GetDatabases gdb = new GetDatabases();
                        gdb.UpdateClienteOnServer(_cliente);
                    }
                        
                    _dialogService.DisplayAlertAsync("", "Seus dados foram atualizados", "OK");
                    _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/HomePage", UriKind.Absolute), _navigationParameters);
                }
                else
                {
                    _dialogService.DisplayAlertAsync("Alerta", "Algo deu errado durante a tentativa de atualização. Verifique seus dados e tente novamente.", "OK");
                }
            }
            else
            {
                _dialogService.DisplayAlertAsync("", message, "OK");
            }
        }

        private async void ClienteDisable()
        {
            // desativar conta do usuário
            // mensagem de confirmação de exclusão
            var choise = await _dialogService.DisplayAlertAsync("Confirmação", "Tem certeza que deseja desativar sua conta?", "Desativar", "Cancelar");

            if (choise)
            {
                _cliente.Status = "I";

                if (_regulaApiService.UpdateCliente(_cliente))
                {
                    _navigationParameters.Add("cliente", _cliente);

                    // atualiza cliente no servidor
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        GetDatabases gdb = new GetDatabases();
                        gdb.UpdateClienteOnServer(_cliente);
                    }
                        
                    _dialogService.DisplayAlertAsync("", "Sua conta foi desativada", "OK");
                    _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/NavigationPage/LoginPage", UriKind.Absolute), _navigationParameters);
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Alerta", "Algo deu errado. Tente novamente em alguns instantes.", "OK");
                }
            }
                
        }

        private async void ClienteDelete()
        {
            // mensagem de confirmação de exclusão
            var choise = await _dialogService.DisplayAlertAsync("Confirmação", "Confirma a exclusão da conta ?", "Excluir", "Cancelar");

            if (choise)
            {
                // deleta cliente da tabela no servidor
                if (CrossConnectivity.Current.IsConnected)
                {
                    GetDatabases gdb = new GetDatabases();
                    gdb.getDatabases(_regulaApiService);
                    gdb.DeleteClienteOnServer(_cliente);

                    if (_regulaApiService.DeleteCliente(_cliente)) //_regulaApiService.DeleteLogicalCliente(_cliente)
                    {
                        _dialogService.DisplayAlertAsync("", "Conta deletada com sucesso", "OK");
                        _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/NavigationPage/LoginPage", UriKind.Absolute));
                    }
                    else
                    {
                        await _dialogService.DisplayAlertAsync("", "Não foi possível deletar a conta", "OK");
                    }
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("", "Não foi possível deletar a conta", "OK");
                }
            }
        }

        private void Informacoes()
        {
            InformacaoManual im = _informacoesManuais.InformacoesClienteUpdate();
            _navigationParameters.Add("informacao", im);
            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        private string validateFields()
        {
            if (_cliente.Nome == null || _cliente.Nome.Equals(""))
                return "Informe um nome";
            else if(_cliente.Telefone != null && _cliente.Telefone.Length < 14)
                    return "Telefone inválido";
            else if(_cliente.CPF != null)
            {
                if(!CpfIsValid(_cliente.CPF))
                    return "CPF inválido";
            }
                
            //else if (_cliente.Email == null || _cliente.Email.Equals(""))
            //    return "Informe um email";
            //else if (!emailIsValid(_cliente.Email))
            //    return "Email inválido";
            //else if (_cliente.Login == null || _cliente.Login.Equals(""))
            //    return "Informe um login";
            //else if (_cliente.Login.Length < 3)
            //    return "Login deve ter no mínimo 3 dígitos";

            return "";
        }

        public bool emailIsValid(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
                return false;
            return true;
        }

        private bool CpfIsValid(string cpf)
        {
            string valor = cpf.Replace(".", "");

            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Cliente = (Cliente)parameters["cliente"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Cliente = (Cliente)parameters["cliente"];
        }
    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using RegulaPrism.Services;
using RegulaPrism.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class FazendaUpdatePageViewModel : BindableBase, INavigationAware
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

        private double _hectares;
        public double Hectares
        {
            get { return _hectares; }
            set { SetProperty(ref _hectares, value); }
        }

        private string _observacoes;
        public string Observacoes
        {
            get { return _observacoes; }
            set { SetProperty(ref _observacoes, value); }
        }

        private string _telefone;
        public string Telefone
        {
            get { return _telefone; }
            set { SetProperty(ref _telefone, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _enderecoWeb;
        public string EnderecoWeb
        {
            get { return _enderecoWeb; }
            set { SetProperty(ref _enderecoWeb, value); }
        }

        private string _bairro;
        public string Bairro
        {
            get { return _bairro; }
            set { SetProperty(ref _bairro, value); }
        }

        private string _cidade;
        public string Cidade
        {
            get { return _cidade; }
            set { SetProperty(ref _cidade, value); }
        }

        private string _uf;
        public string UF
        {
            get { return _uf; }
            set { SetProperty(ref _uf, value); }
        }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { SetProperty(ref _cliente, value); }
        }

        private Fazenda _fazenda;
        public Fazenda Fazenda
        {
            get { return _fazenda; }
            set { SetProperty(ref _fazenda, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private IInformacoesManuais _informacoesManuais;

        NavigationParameters _navigationParameters = new NavigationParameters();

        public DelegateCommand FazendaUpdateCommand { get; private set; }
        public DelegateCommand FazendaDeleteCommand { get; private set; }
        public DelegateCommand InfoCommand { get; private set; }

        public FazendaUpdatePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, IInformacoesManuais informacoesManuais)
        {
            Title = "Informações";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _informacoesManuais = informacoesManuais;
            _regulaApiService = regulaApiService;

            FazendaUpdateCommand = new DelegateCommand(FazendaUpdate);
            FazendaDeleteCommand = new DelegateCommand(FazendaDelete);
            InfoCommand = new DelegateCommand(Informacoes);
        }

        private void FazendaUpdate()
        {
            string mensagem = validateFields();
            if(mensagem.Equals(""))
            {
                // update fazenda
                if (_regulaApiService.UpdateFazenda(_fazenda))
                {
                    // mensagem de fazenda atualizada
                    _dialogService.DisplayAlertAsync("", "Fazenda atualizada", "OK");

                    // recupera cliente proprietario para FazendaListPage
                    _cliente = _regulaApiService.GetClienteById(_fazenda.ClienteId);

                    // adiciona parametros 
                    _navigationParameters.Add("fazenda", _fazenda);
                    _navigationParameters.Add("cliente", _cliente);
                    _navigationParameters.Add("action", "update");

                    // redireciona para a lista de fazendas
                    _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/FazendaListPage", UriKind.Absolute), _navigationParameters);
                }
                else
                {
                    _dialogService.DisplayAlertAsync("", "Algo deu errado durante a tentativa de atualização. Verifique os dados e tente novamente", "OK");
                }
            }
            else
            {
                // exibe mensagem de erro
                _dialogService.DisplayAlertAsync("", mensagem, "OK");
            }
        }

        private async void FazendaDelete()
        {
            // excluir fazenda implica excluir talhões e semeaduras

            // mensagem de confirmação de exclusão
            var choise = await _dialogService.DisplayAlertAsync("Confirmação", "Confirma a exclusão da fazenda " + _fazenda.Nome + "?", "Excluir", "Cancelar");

            if (choise)
            {
                if(_regulaApiService.DeleteLogicalFazenda(_fazenda))
                {
                    _dialogService.DisplayAlertAsync("", "Fazenda deletada com sucesso", "OK");

                    _navigationParameters.Add("cliente", _cliente);
                    _navigationParameters.Add("action", "update");

                    _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/FazendaListPage", UriKind.Absolute), _navigationParameters);
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("", "Não foi possível deletar a fazenda. Verifique se há semeaduras ativas antes de realizar a exclusão", "OK");
                }
            }
                
        }

        private void Informacoes()
        {
            InformacaoManual im = _informacoesManuais.InformacoesFazendaUpdate();

            _navigationParameters.Add("informacao", im);

            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("fazenda", _fazenda);
            parameters.Add("cliente", _cliente);
            parameters.Add("action", "update");
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Cliente = (Cliente)parameters["cliente"];
            Fazenda f = (Fazenda)parameters["fazenda"];

            if(f != null)
                Fazenda = (Fazenda)parameters["fazenda"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Cliente = (Cliente)parameters["cliente"];
            Fazenda f = (Fazenda)parameters["fazenda"];

            if (f != null)
                Fazenda = (Fazenda)parameters["fazenda"];
        }

        private string validateFields()
        {
            if (_fazenda.Nome == null || _fazenda.Nome.Equals(""))
                return "Informe o nome da fazenda";
            else if (_fazenda.Hectares == 0)
                return "Informe o tamanho da fazenda";
            else if (_fazenda.Telefone == null || _fazenda.Telefone.Equals(""))
                return "Informe um número de telefone para a fazenda";
            else if (_fazenda.Telefone.Length < 14)
                return "Telefone inválido";
            else if (_fazenda.Email != null)
                if (!_fazenda.Email.Contains("@") || !_fazenda.Email.Contains("."))
                    return "E-mail inválido";

            return "";
        }
    }
}

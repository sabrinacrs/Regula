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
    public class FazendaCreatePageViewModel : BindableBase, INavigationAware
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

        private NavigationParameters _navigationParameters;

        public DelegateCommand NavigateToFazendaContatoPageCommand { get; private set; }
        public DelegateCommand FazendaSaveCommand { get; private set; }

        public DelegateCommand InfoCommand { get; private set; }

        public FazendaCreatePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, IInformacoesManuais informacoesManuais)
        {
            Title = "Nova Fazenda";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _informacoesManuais = informacoesManuais;
            _navigationParameters = new NavigationParameters();

            NavigateToFazendaContatoPageCommand = new DelegateCommand(NavigateToFazendaContatoPage);
            FazendaSaveCommand = new DelegateCommand(FazendaSave);
            InfoCommand = new DelegateCommand(Informacoes);
        }

        private void NavigateToFazendaContatoPage()
        {
            _navigationService.NavigateAsync("FazendaContatoPage");
        }

        private void FazendaSave()
        {
            // validações e inserção no banco
            string message = messageValidateFields();

            // verifica mensagem de erro no preenchimento do formulario
            if(message.Equals(""))
            {
                _fazenda = fazendaView();

                if (_regulaApiService.InsertFazenda(_fazenda))
                {
                    // passa parametro navigationaware
                    _navigationParameters.Add("fazenda", _fazenda);
                    _navigationParameters.Add("cliente", _cliente);

                    _dialogService.DisplayAlertAsync("Nova fazenda", "Fazenda " + _fazenda.Nome + " registrada", "OK");
                    _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/FazendaListPage", UriKind.Absolute), _navigationParameters); // "FazendaListPage", _navigationParameters
                }
                else
                {
                    _dialogService.DisplayAlertAsync("", "Algo deu errado no cadastro da nova fazenda. Verifique os dados e tente novamente", "OK");
                }
            }
            else
            {
                _dialogService.DisplayAlertAsync("Atenção", message, "OK");
            }
        }

        private void Informacoes()
        {
            InformacaoManual im = _informacoesManuais.InformacoesFazendaCreate();

            _navigationParameters.Add("informacao", im);

            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("fazenda", _fazenda);
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

        private string messageValidateFields()
        {
            if (Nome == null || Nome.Equals(""))
                return "Informe o nome da fazenda";
            else if (Hectares.Equals(""))
                return "Informe o tamanho da fazenda";
            else if (Telefone == null || Telefone.Equals(""))
                return "Informe um telefone";

            return "";
        }

        private Fazenda fazendaView()
        {
            Fazenda fazenda = new Fazenda();
            fazenda.Nome = Nome;
            fazenda.Hectares = (decimal)Hectares;
            fazenda.Observacoes = Observacoes;
            fazenda.Telefone = Telefone;
            fazenda.UF = UF;
            fazenda.Email = Email;
            fazenda.EnderecoWeb = EnderecoWeb;
            fazenda.Bairro = Bairro;
            fazenda.Cidade = Cidade;

            if(_cliente.Id != 0)
                fazenda.ClienteId = _cliente.Id;

            return fazenda;
        }
    }
}

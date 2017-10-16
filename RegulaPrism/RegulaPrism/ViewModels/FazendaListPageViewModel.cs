using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using RegulaPrism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class FazendaListPageViewModel : BindableBase, INavigationAware
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

        private Fazenda _fazenda;
        public Fazenda Fazenda
        {
            get { return _fazenda; }
            set { SetProperty(ref _fazenda, value); }
        }

        private List<Fazenda> _fazendas;
        public List<Fazenda> Fazendas
        {
            get { return _fazendas; }
            set { SetProperty(ref _fazendas, value); }
        }

        private string _filtro;
        public string Filtro
        {
            get { return _filtro; }
            set { SetProperty(ref _filtro, value); }
        }

        private Fazenda _selectedItem;
        public Fazenda SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                    return;

                FazendaSelectedCommand.Execute();
            }
        }

        private string _action;

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private IInformacoesManuais _informacoesManuais;

        private NavigationParameters _navigationParameters;
        
        public DelegateCommand FazendaSelectedCommand { get; private set; }
        public DelegateCommand FazendaSearchCommand { get; private set; }
        public DelegateCommand InfoCommand { get; private set; }

        public FazendaListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, IInformacoesManuais informacoesManuais)
        {
            Title = "Fazendas";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _informacoesManuais = informacoesManuais;
            _navigationParameters = new NavigationParameters();

            FazendaSelectedCommand = new DelegateCommand(FazendaSelected);
            FazendaSearchCommand = new DelegateCommand(FazendaSearch);
            InfoCommand = new DelegateCommand(Informacoes);
        }

        private void Informacoes()
        {
            InformacaoManual im = _informacoesManuais.InformacoesFazendaList();

            _navigationParameters.Add("informacao", im);

            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        private void FazendaSelected()
        {
            // verificação para não repetir parâmetros
            if (_navigationParameters.Count() > 0)
                _navigationParameters = new NavigationParameters();

            // adiciona parametros
            _navigationParameters.Add("cliente", _cliente);
            _navigationParameters.Add("fazenda", _selectedItem);

            if(_action == null)
            {
                // leva para pagina de informacoes gerais fazenda - tabbedpage
                _navigationParameters.Add("action", "list");
                _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/FazendaSelectedTabbedPage", UriKind.Absolute), _navigationParameters);
            }
            else if (_action.Equals("update"))
            {
                // leva fazenda selecionada pra página de update
                _navigationParameters.Add("action", "update");
                _navigationService.NavigateAsync("FazendaUpdatePage", _navigationParameters);
            }
            else if (_action.Equals("list"))
            {
                _navigationParameters.Add("action", "list");
                _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/FazendaSelectedTabbedPage", UriKind.Absolute), _navigationParameters);
            }
        }

        private void FazendaSearch()
        {
            // filtra de acordo com o texto digitado os nomes das fazendas
            if (_filtro == null || _filtro.Equals(""))
                Fazendas = _regulaApiService.GetFazendasByCliente(_cliente.Id);
            else
                Fazendas = _regulaApiService.GetFazendasByNomeAndCliente(_filtro, _cliente.Id);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //parameters.Add("action", "update"); // testar se é necessário
            parameters.Add("cliente", _cliente);
            parameters.Add("fazenda", _fazenda);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
            _action = (string)parameters["action"];
            Fazendas = _regulaApiService.GetFazendasByCliente(_cliente.Id);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
            _action = (string)parameters["action"];
            Fazendas = _regulaApiService.GetFazendasByCliente(_cliente.Id);
        }
    }
}

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
    public class TalhaoListPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int _fazendaSelectedIndex;
        public int FazendaSelectedIndex
        {
            get { return _fazendaSelectedIndex; }
            set { SetProperty(ref _fazendaSelectedIndex, value); }
        }

        public Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { SetProperty(ref _cliente, value); }
        }

        public Fazenda _fazenda;
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

        public Talhao _talhao;
        public Talhao Talhao
        {
            get { return _talhao; }
            set { SetProperty(ref _talhao, value); }
        }

        private List<Talhao> _talhoes;
        public List<Talhao> Talhoes
        {
            get { return _talhoes; }
            set { SetProperty(ref _talhoes, value); }
        }

        private string _filtro;
        public string Filtro
        {
            get { return _filtro; }
            set { SetProperty(ref _filtro, value); }
        }

        private Talhao _selectedItem;
        public Talhao SelectedItem
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

                TalhaoSelectedCommand.Execute();
            }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private IInformacoesManuais _informacoesManuais;

        private NavigationParameters _navigationParameters;

        public DelegateCommand TalhaoSelectedCommand { get; private set; }
        public DelegateCommand TalhaoSearchCommand { get; private set; }
        public DelegateCommand FazendaSelectedCommand { get; private set; }
        public DelegateCommand InfoCommand { get; private set; }

        public TalhaoListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, IInformacoesManuais informacoesManuais)
        {
            Title = "Talhões";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _informacoesManuais = informacoesManuais;
            _navigationParameters = new NavigationParameters();

            TalhaoSelectedCommand = new DelegateCommand(TalhaoSelected);
            TalhaoSearchCommand = new DelegateCommand(TalhaoSearch);
            FazendaSelectedCommand = new DelegateCommand(FazendaSelected);
            InfoCommand = new DelegateCommand(Informacoes);
        }

        private void Informacoes()
        {
            InformacaoManual im = _informacoesManuais.InformacoesTalhaoList();

            _navigationParameters.Add("informacao", im);

            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        private void FazendaSelected()
        {
            if(_fazendaSelectedIndex != -1)
            {
                _fazenda = Fazendas.ElementAt(_fazendaSelectedIndex);
                Talhoes = _regulaApiService.GetTalhoesByFazenda(_fazenda.Id);
            }
        }

        private void TalhaoSelected()
        {
            // verificação para não repetir parâmetros
            if (_navigationParameters.Count() > 0)
                _navigationParameters = new NavigationParameters();

            _talhao = _selectedItem;

            // adiciona parametros
            _navigationParameters.Add("cliente", _cliente);
            _navigationParameters.Add("fazenda", _fazenda);
            _navigationParameters.Add("talhao", _talhao);
            _navigationParameters.Add("fazendaSelectedIndex", _fazendaSelectedIndex);

            // aqui tem a putaria de action list ou update no caso das fazendas
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/TalhaoSelectedTabbedPage", UriKind.Absolute), _navigationParameters);
        }

        private void TalhaoSearch()
        {
            // filtra de acordo com o texto digitado os nomes dos talhoes
            Talhoes = _regulaApiService.GetTalhoesByDescricaoAndFazenda(_filtro, _fazenda.Id);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
            parameters.Add("fazenda", _fazenda);
            parameters.Add("talhao", _talhao);
            parameters.Add("fazendaSelectedIndex", _fazendaSelectedIndex);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
            Fazendas = _regulaApiService.GetFazendasByCliente(_cliente.Id);
            //Talhoes = _regulaApiService.GetTalhoesByFazenda(_fazenda.Id);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
            Fazendas = _regulaApiService.GetFazendasByCliente(_cliente.Id);
            //Talhoes = _regulaApiService.GetTalhoesByFazenda(_fazenda.Id);
        }
    }
}

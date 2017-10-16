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
    public class FazendaSemeadurasListPageViewModel : BindableBase, INavigationAware
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

        private Talhao _talhao;
        public Talhao Talhao
        {
            get { return _talhao; }
            set { SetProperty(ref _talhao, value); }
        }

        private Fazenda _fazenda;
        public Fazenda Fazenda
        {
            get { return _fazenda; }
            set { SetProperty(ref _fazenda, value); }
        }

        private SemeaduraModel _selectedItem;
        public SemeaduraModel SelectedItem
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

                SemeaduraSelectedCommand.Execute();
            }
        }

        private List<SemeaduraModel> _semeaduras;
        public List<SemeaduraModel> Semeaduras
        {
            get { return _semeaduras; }
            set { SetProperty(ref _semeaduras, value); }
        }

        private List<Talhao> _talhoes;
        public List<Talhao> Talhoes
        {
            get { return _talhoes; }
            set { SetProperty(ref _talhoes, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private IInformacoesManuais _informacoesManuais;

        private NavigationParameters _navigationParameters;

        public DelegateCommand SemeaduraSelectedCommand { get; private set; }
        public DelegateCommand InfoCommand { get; private set; }

        public FazendaSemeadurasListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, IInformacoesManuais informacoesManuais)
        {
            Title = "Semeaduras";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _informacoesManuais = informacoesManuais;
            _navigationParameters = new NavigationParameters();

            SemeaduraSelectedCommand = new DelegateCommand(SemeaduraSelected);
            InfoCommand = new DelegateCommand(Informacoes);
        }

        private void Informacoes()
        {
            InformacaoManual im = _informacoesManuais.InformacoesFazendaSemeaduraList();

            _navigationParameters.Add("informacao", im);

            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        private void SemeaduraSelected()
        {
            _navigationParameters.Add("semeadura", _selectedItem);
            _navigationParameters.Add("cliente", _cliente);
            _navigationParameters.Add("fazenda", _fazenda);

            // vai para página de semeadura selected
            _navigationService.NavigateAsync("FazendaSemeaduraSelectedPage", _navigationParameters);
        }

        private void carregaSemeaduras()
        {
            Semeaduras = new List<SemeaduraModel>();

            // lista todas as semeaduras
            List<Semeadura> semeadurasAll = _regulaApiService.GetSemeaduras();

            // lista todos os talhoes da fazenda
            _talhoes = _regulaApiService.GetTalhoesByFazenda(_fazenda.Id);

            foreach(var t in _talhoes)
            {
                List<Semeadura> ss = semeadurasAll.Where(x => x.TalhaoId == t.Id).ToList();//Find(x => x.TalhaoId == t.Id);

                if(ss.Count() > 0)
                {
                    foreach (var s in ss)
                    {
                        SemeaduraModel sm = new SemeaduraModel();
                        sm.Id = s.Id;
                        sm.Germinacao = s.Germinacao;
                        sm.MetrosLineares = s.MetrosLineares;
                        sm.QuilosSementes = s.QuilosSementes;
                        sm.Talhao = t;
                        sm.Cultivar = _regulaApiService.GetCultivarById(s.CultivarEpocaSemeaduraCultId);
                        sm.EpocaSemeadura = _regulaApiService.GetEpocaSemeaduraById(s.CultivarEpocaSemeaduraEpId);
                        sm.Data = s.Data;

                        Semeaduras.Add(sm);
                    }
                }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
            parameters.Add("fazenda", _fazenda);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Cliente = (Cliente)parameters["cliente"];
            Fazenda f = (Fazenda)parameters["fazenda"];

            if (f != null)
            {
                Fazenda = (Fazenda)parameters["fazenda"];
                carregaSemeaduras();
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Cliente = (Cliente)parameters["cliente"];
            Fazenda f = (Fazenda)parameters["fazenda"];

            if(f != null)
            {
                Fazenda = (Fazenda)parameters["fazenda"];
                carregaSemeaduras();
            }
        }
    }
}

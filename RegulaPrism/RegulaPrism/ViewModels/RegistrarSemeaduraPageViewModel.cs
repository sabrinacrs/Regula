using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class RegistrarSemeaduraPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int _epocaSemeaduraSelectedIndex;
        public int EpocaSemeaduraSelectedIndex
        {
            get { return _epocaSemeaduraSelectedIndex; }
            set { SetProperty(ref _epocaSemeaduraSelectedIndex, value); }
        }

        private int _fazendaSelectedIndex;
        public int FazendaSelectedIndex
        {
            get { return _fazendaSelectedIndex; }
            set { SetProperty(ref _fazendaSelectedIndex, value); }
        }

        private int _talhaoSelectedIndex;
        public int TalhaoSelectedIndex
        {
            get { return _talhaoSelectedIndex; }
            set { SetProperty(ref _talhaoSelectedIndex, value); }
        }

        private double _plantasHectare;
        public double PlantasHectare
        {
            get { return _plantasHectare; }
            set { SetProperty(ref _plantasHectare, value); }
        }

        private double _pesoSementesMinimo;
        public double PesoSementesMinimo
        {
            get { return _pesoSementesMinimo; }
            set { SetProperty(ref _pesoSementesMinimo, value); }
        }

        private double _pesoSementesMaximo;
        public double PesoSementesMaximo
        {
            get { return _pesoSementesMaximo; }
            set { SetProperty(ref _pesoSementesMaximo, value); }
        }

        private double _pesoSementesMetro;
        public double PesoSementesMetro
        {
            get { return _pesoSementesMetro; }
            set { SetProperty(ref _pesoSementesMetro, value); }
        }

        private double _pesoSementesHectareMinimo;
        public double PesoSementesHectareMinimo
        {
            get { return _pesoSementesHectareMinimo; }
            set { SetProperty(ref _pesoSementesHectareMinimo, value); }
        }

        private double _pesoSementesHectareMaximo;
        public double PesoSementesHectareMaximo
        {
            get { return _pesoSementesHectareMaximo; }
            set { SetProperty(ref _pesoSementesHectareMaximo, value); }
        }

        private double _pesoSementesAlqueireMinimo;
        public double PesoSementesAlqueireMinimo
        {
            get { return _pesoSementesAlqueireMinimo; }
            set { SetProperty(ref _pesoSementesAlqueireMinimo, value); }
        }

        private double _pesoSementesAlqueireMaximo;
        public double PesoSementesAlqueireMaximo
        {
            get { return _pesoSementesAlqueireMaximo; }
            set { SetProperty(ref _pesoSementesAlqueireMaximo, value); }
        }

        private double _espacamento;
        public double Espacamento
        {
            get { return _espacamento; }
            set { SetProperty(ref _espacamento, value); }
        }

        private double _germinacao;
        public double Germinacao
        {
            get { return _germinacao; }
            set { SetProperty(ref _germinacao, value); }
        }

        private double _metrosLineares;
        public double MetrosLineares
        {
            get { return _metrosLineares; }
            set { SetProperty(ref _metrosLineares, value); }
        }

        private Cultivar _cultivar;
        public Cultivar Cultivar
        {
            get { return _cultivar; }
            set { SetProperty(ref _cultivar, value); }
        }

        private EpocaSemeadura _epocaSemeadura;
        public EpocaSemeadura EpocaSemeadura
        {
            get { return _epocaSemeadura; }
            set { SetProperty(ref _epocaSemeadura, value); }
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

        private Talhao _talhao;
        public Talhao Talhao
        {
            get { return _talhao; }
            set { SetProperty(ref _talhao, value); }
        }

        private List<EpocaSemeadura> _epocasSemeadura;
        public List<EpocaSemeadura> EpocasSemeadura
        {
            get { return _epocasSemeadura; }
            set { SetProperty(ref _epocasSemeadura, value); }
        }

        private List<Fazenda> _fazendas;
        public List<Fazenda> Fazendas
        {
            get { return _fazendas; }
            set { SetProperty(ref _fazendas, value); }
        }

        private List<Talhao> _talhoes;
        public List<Talhao> Talhoes
        {
            get { return _talhoes; }
            set { SetProperty(ref _talhoes, value); }
        }

        private List<CultivarEpocaSemeadura> _cultivarEpocasSemeadura;
        public List<CultivarEpocaSemeadura> CultivarEpocasSemeadura
        {
            get { return _cultivarEpocasSemeadura; }
            set { SetProperty(ref _cultivarEpocasSemeadura, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand SemeaduraSaveCommand { get; private set; }

        public DelegateCommand FazendaSelectedCommand { get; private set; }

        public RegistrarSemeaduraPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Salvar Dados da Semeadura";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            _fazendaSelectedIndex = -1;
            _talhaoSelectedIndex = -1;

            SemeaduraSaveCommand = new DelegateCommand(SemeaduraSave);
            FazendaSelectedCommand = new DelegateCommand(ListTalhoes);
        }

        private void SemeaduraSave()
        {
            // validacoes antes de salvar
            string message = validateFields();

            if (message.Equals(""))
            {
                // paga objeto talhao selecionado no picker
                _talhao = Talhoes.ElementAt(_talhaoSelectedIndex);

                // registra dados da semeadura
                Semeadura semeadura = new Semeadura();

                semeadura.Germinacao = (decimal)_germinacao;
                semeadura.MetrosLineares = (decimal)_metrosLineares;
                semeadura.Data = DateTime.Now; // rever isso - teste
                semeadura.TalhaoId = _talhao.Id;
                semeadura.CultivarEpocaSemeaduraCultId = _cultivar.Id;
                semeadura.CultivarEpocaSemeaduraEpId = _epocaSemeadura.Id;

                // insere semeadura
                if (_regulaApiService.InsertSemeadura(semeadura))
                {
                    // pega o id da semeadura inserida e atribui ao cálculo semeadura
                    List<Semeadura> semeaduras = new List<Semeadura>();
                    semeaduras = _regulaApiService.GetSemeadurasByTalhaoId(_talhao.Id);

                    Semeadura s = semeaduras.Last();

                    // salva calculos da semeadura
                    if (saveCalculosSemeadura(s.Id))
                    {
                        _dialogService.DisplayAlertAsync("", "Semeadura e cálculos salvos com sucesso!", "OK");

                        // redirecionar para página de semeaduras da fazenda
                        _navigationParameters.Add("cliente", _cliente);
                        _navigationParameters.Add("fazenda", _fazenda);
                        _navigationParameters.Add("talhao", _talhao);

                        _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/FazendaSemeadurasListPage", UriKind.Absolute), _navigationParameters);
                    }
                    else
                    {
                        _dialogService.DisplayAlertAsync("", "Não foi possível inserir os cálculos da semeadura", "OK");
                    }
                }
                else
                {
                    _dialogService.DisplayAlertAsync("", "Não foi possível inserir a semeadura", "OK");
                }
            }
            else
            {
                _dialogService.DisplayAlertAsync("", message, "OK");
            }
        }

        private bool saveCalculosSemeadura(int idSemeadura)
        {
            // registra cálculos
            CalculosSemeadura calculosSemeadura = new CalculosSemeadura();

            calculosSemeadura.SemeaduraId = idSemeadura;

            calculosSemeadura.PesoMinimoSementesAlq = (decimal)_pesoSementesAlqueireMinimo;
            calculosSemeadura.PesoMaximoSementesAlq = (decimal)_pesoSementesAlqueireMaximo;

            calculosSemeadura.PesoMinimoSementesHa = (decimal)_pesoSementesHectareMinimo;
            calculosSemeadura.PesoMaximoSementesHa = (decimal)_pesoSementesHectareMaximo;

            calculosSemeadura.QtdeSementesMetro = (decimal)_pesoSementesMetro;

            return _regulaApiService.InsertCalculosSemeadura(calculosSemeadura);
        }

        private void ListTalhoes()
        {
            // Pega fazenda selecionada
            _fazenda = Fazendas.ElementAt(_fazendaSelectedIndex);
            Talhoes = _regulaApiService.GetTalhoesByFazenda(_fazenda.Id);
        }

        private string validateFields()
        {
            if (_fazendaSelectedIndex == -1)
                return "Selecione uma fazenda";
            else if (_talhaoSelectedIndex == -1)
                return "Selecione um talhão";

            return "";
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);

            parameters.Add("cultivar", _cultivar);
            parameters.Add("cultivarEpocasSemeadura", _cultivarEpocasSemeadura);
            parameters.Add("epocasSemeadura", _epocasSemeadura);
            parameters.Add("epocaSemeaduraSelectedIndex", _epocaSemeaduraSelectedIndex);

            parameters.Add("espacamento", _espacamento);
            parameters.Add("metrosLineares", _metrosLineares);
            parameters.Add("germinacao", _germinacao);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Cliente = (Cliente)parameters["cliente"];
            Fazendas = _regulaApiService.GetFazendasByCliente(_cliente.Id);

            Cultivar = (Cultivar)parameters["cultivar"];
            CultivarEpocasSemeadura = (List<CultivarEpocaSemeadura>)parameters["cultivarEpocasSemeadura"];
            EpocasSemeadura = (List<EpocaSemeadura>)parameters["epocasSemeadura"];
            EpocaSemeadura = (EpocaSemeadura)parameters["epocaSemeadura"];
            EpocaSemeaduraSelectedIndex = (int)parameters["epocaSemeaduraSelectedIndex"];

            // informações do espaço de plantio
            Espacamento = (double)parameters["espacamento"];
            MetrosLineares = (double)parameters["metrosLineares"];
            Germinacao = (double)parameters["germinacao"];

            // numero fixo de plantas por hectare da cultivar
            PlantasHectare = (double)parameters["plantasHectare"];

            // Pesos de sementes calculados
            PesoSementesMinimo = (double)parameters["pesoSementesMinimo"];
            PesoSementesMaximo = (double)parameters["pesoSementesMaximo"];

            PesoSementesMetro = (double)parameters["pesoSementesMetro"];

            PesoSementesHectareMinimo = (double)parameters["pesoSementesHectareMinimo"];
            PesoSementesHectareMaximo = (double)parameters["pesoSementesHectareMaximo"];

            PesoSementesAlqueireMinimo = (double)parameters["pesoSementesAlqueireMinimo"];
            PesoSementesAlqueireMaximo = (double)parameters["pesoSementesAlqueireMaximo"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Cliente = (Cliente)parameters["cliente"];
            Fazendas = _regulaApiService.GetFazendasByCliente(_cliente.Id);

            Cultivar = (Cultivar)parameters["cultivar"];
            CultivarEpocasSemeadura = (List<CultivarEpocaSemeadura>)parameters["cultivarEpocasSemeadura"];
            EpocasSemeadura = (List<EpocaSemeadura>)parameters["epocasSemeadura"];
            EpocaSemeadura = (EpocaSemeadura)parameters["epocaSemeadura"];
            EpocaSemeaduraSelectedIndex = (int)parameters["epocaSemeaduraSelectedIndex"];

            // informações do espaço de plantio
            Espacamento = (double)parameters["espacamento"];
            MetrosLineares = (double)parameters["metrosLineares"];
            Germinacao = (double)parameters["germinacao"];

            // numero fixo de plantas por hectare da cultivar
            PlantasHectare = (double)parameters["plantasHectare"];

            // Pesos de sementes calculados
            PesoSementesMinimo = (double)parameters["pesoSementesMinimo"];
            PesoSementesMaximo = (double)parameters["pesoSementesMaximo"];

            PesoSementesMetro = (double)parameters["pesoSementesMetro"];

            PesoSementesHectareMinimo = (double)parameters["pesoSementesHectareMinimo"];
            PesoSementesHectareMaximo = (double)parameters["pesoSementesHectareMaximo"];

            PesoSementesAlqueireMinimo = (double)parameters["pesoSementesAlqueireMinimo"];
            PesoSementesAlqueireMaximo = (double)parameters["pesoSementesAlqueireMaximo"];
        }
    }
}

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
    public class CultivarDoencaToleranciaPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _doencaSwitchCell;
        public string DoencaSwitchCell
        {
            get { return _doencaSwitchCell; }
            set { SetProperty(ref _doencaSwitchCell, value); }
        }

        private string _toleranciaSliderDoenca;
        public string ToleranciaSliderDoenca
        {
            get { return _toleranciaSliderDoenca; }
            set { SetProperty(ref _toleranciaSliderDoenca, value); }
        }

        private string _toleranciaSliderTolerancia;
        public string ToleranciaSliderToleracia
        {
            get { return _toleranciaSliderTolerancia; }
            set { SetProperty(ref _toleranciaSliderTolerancia, value); }
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

        private EpocaSemeadura _epocaSemeadura;
        public EpocaSemeadura EpocaSemeadura
        {
            get { return _epocaSemeadura; }
            set { SetProperty(ref _epocaSemeadura, value); }
        }

        private List<Doenca> _doencas;
        public List<Doenca> Doencas
        {
            get { return _doencas; }
            set { SetProperty(ref _doencas, value); }
        }

        private List<Doenca> _doencasSolo;
        public List<Doenca> DoencasSolo
        {
            get { return _doencasSolo; }
            set { SetProperty(ref _doencasSolo, value); }
        }

        private List<Cultivar> _cultivares;
        public List<Cultivar> Cultivares
        {
            get { return _cultivares; }
            set { SetProperty(ref _cultivares, value); }
        }

        private List<CultivarRecomendada> _cultivaresRecomendadas;
        public List<CultivarRecomendada> CultivaresRecomendadas
        {
            get { return _cultivaresRecomendadas; }
            set { SetProperty(ref _cultivaresRecomendadas, value); }
        }

        private List<CultivarRecomendada> _cultivaresRecomendadasPorDoenca;
        public List<CultivarRecomendada> CultivaresRecomendadasPorDoenca
        {
            get { return _cultivaresRecomendadasPorDoenca; }
            set { SetProperty(ref _cultivaresRecomendadasPorDoenca, value); }
        }

        private List<CultivarDoenca> _cultivarDoencaTolerancia;
        public List<CultivarDoenca> CultivarDoencaTolerancia
        {
            get { return _cultivarDoencaTolerancia; }
            set { SetProperty(ref _cultivarDoencaTolerancia, value); }
        }

        private List<CultivarDoenca> _cultivaresDoencas;
        public List<CultivarDoenca> CultivaresDoencas
        {
            get { return _cultivaresDoencas; }
            set { SetProperty(ref _cultivaresDoencas, value); }
        }

        private List<Tolerancia> _tolerancias;
        public List<Tolerancia> Tolerancias
        {
            get { return _tolerancias; }
            set { SetProperty(ref _tolerancias, value); }
        }

        private List<DoencaTolerancia> _doencasTolerancias;
        public List<DoencaTolerancia> DoencasTolerancias
        {
            get { return _doencasTolerancias; }
            set { SetProperty(ref _doencasTolerancias, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand CultivarRecomendadaListCommand { get; private set; }
        public DelegateCommand AddCultivarDoencaToleranciaCommand { get; private set; }

        public CultivarDoencaToleranciaPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Tolerâncias";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            _cultivarDoencaTolerancia = new List<CultivarDoenca>();
            _cultivaresRecomendadasPorDoenca = new List<CultivarRecomendada>();
            _doencasTolerancias = new List<DoencaTolerancia>();
            _cultivaresDoencas = new List<CultivarDoenca>();
            _tolerancias = new List<Tolerancia>();

            _tolerancias = _regulaApiService.GetTolerancias();

            CultivarRecomendadaListCommand = new DelegateCommand(CultivarRecomendadaList);
            AddCultivarDoencaToleranciaCommand = new DelegateCommand(AddCultivarDoencaTolerancia);
        }

        private void AddCultivarDoencaTolerancia()
        {
            // pega doença e nível de tolerancia
            Tolerancia tol = NivelTolerancia(ToleranciaSliderToleracia);
            Doenca doe = _doencasSolo.Find(d => d.Id == Int32.Parse(ToleranciaSliderDoenca));

            DoencaTolerancia dt = new DoencaTolerancia();
            dt.Doenca = doe;
            dt.Tolerancia = tol;

            // se a doença ainda não está na lista, adiciona
            var doencaFind = _doencasTolerancias.Find(d => d.Doenca.Id == dt.Doenca.Id);

            if (doencaFind == null)
                _doencasTolerancias.Add(dt);
            else
            {
                // se estiver, remove ou altera valor da tolerância
                _doencasTolerancias.Remove(doencaFind);
                _doencasTolerancias.Add(dt);
            }
        }

        private void CultivarRecomendadaList()
        {
            // filtrar cultivares com base nas doenças e tolerâncias

            foreach(var c in _cultivaresRecomendadas)
            {
                // lista as doencas da cultivar
                List<CultivarDoenca> doencasCultivar = _regulaApiService.GetCultivarDoencaCultivarId(c.Cultivar.Id);

                // reset vetor de Doencas Tolerancias
                c.DoencasTolerancias = new List<DoencaTolerancia>();

                foreach(var dc in doencasCultivar)
                {
                    Doenca d = _regulaApiService.GetDoencaById(dc.DoencaId);
                    Tolerancia t = _regulaApiService.GetToleranciaById(dc.ToleranciaId);

                    DoencaTolerancia dt = new DoencaTolerancia();
                    dt.Doenca = d;
                    dt.Tolerancia = t;

                    if (!(dt.Tolerancia.Descricao.ToUpper().Contains("INTOLERANTE")))
                    {
                        // verifica se a doenca tem no solo do produtor
                        Doenca doencaSoloProdutor = _doencasSolo.Find(ds => ds.Id == d.Id);

                        //_doencasTolerancias.Find(x => x.Tolerancia.Descricao.Equals(dt.Tolerancia.Descricao));

                        // insere a lista de doencas e respectivas tolerancias da cultivar recomendada
                        if (doencaSoloProdutor != null)
                            c.DoencasTolerancias.Add(dt);
                    }
                }
            }

            // ordena pelo numero de plantas
            var newList = _cultivaresRecomendadas.OrderByDescending(cr => cr.PlantasHectare).ToList();

            // adiciona parametros
            _navigationParameters.ToList().Clear();
            _navigationParameters.Add("cultivaresRecomendadas", newList);

            // navega para lista de cultivares recomendadas
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/CultivarRecomendadaListPage", UriKind.Absolute), _navigationParameters);
            //new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/CultivarRecomendadaListPage", UriKind.Absolute)
        }

        private Tolerancia NivelTolerancia(string value)
        {
            double valueTolerancia = Double.Parse(value);
            Tolerancia tol;

            if(valueTolerancia <= 10)
            {
                // intolerante
                tol = _tolerancias.Find(t => t.Descricao == "Intolerante");
            }
            else if(valueTolerancia <= 20)
            {
                // moderadamente intolerante
                tol = _tolerancias.Find(t => t.Descricao == "Moderadamente Intolerante");
            }
            else if(valueTolerancia <= 30)
            {
                // susceptível // medianamente resistente
                tol = _tolerancias.Find(t => t.Descricao == "Susceptível");
            }
            else if(valueTolerancia <= 40)
            {
                // moderadamente tolerante // moderadamente susceptível // moderadamente resistente
                tol = _tolerancias.Find(t => t.Descricao == "Moderadamente Susceptível");
            }
            else //if(valueTolerancia <= 50)
            {
                // tolerante // altamente tolerante // resistente
                tol = _tolerancias.Find(t => t.Descricao == "Tolerante");
            }

            return tol;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cultivaresRecomendadas", _cultivaresRecomendadas);
            parameters.Add("doencasSolo", _doencasSolo);
            parameters.Add("epocaSemeadura", _epocaSemeadura);
            parameters.Add("epocaSemeadura", _epocaSemeadura);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
            DoencasSolo = (List<Doenca>)parameters["doencasSolo"];
            _epocaSemeadura = (EpocaSemeadura)parameters["epocaSemeadura"];
            _espacamento = (double)parameters["espacamento"];
            _metrosLineares = (double)parameters["metrosLineares"];
            _germinacao = (double)parameters["germinacao"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
            CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
            DoencasSolo = (List<Doenca>)parameters["doencasSolo"];
            _epocaSemeadura = (EpocaSemeadura)parameters["epocaSemeadura"];
            _espacamento = (double)parameters["espacamento"];
            _metrosLineares = (double)parameters["metrosLineares"];
            _germinacao = (double)parameters["germinacao"];
        }
    }
}

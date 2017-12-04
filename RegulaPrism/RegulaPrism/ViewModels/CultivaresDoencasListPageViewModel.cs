using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using RegulaPrism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class CultivaresDoencasListPageViewModel : BindableBase, INavigationAware
    {
        private bool _legenda;
        public bool Legenda
        {
            get { return _legenda; }
            set { SetProperty(ref _legenda, value); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }

        private string _textoToggleLegenda;
        public string TextoToggleLegenda
        {
            get { return _textoToggleLegenda; }
            set { SetProperty(ref _textoToggleLegenda, value); }
        }

        private List<Doenca> _doencas;
        public List<Doenca> Doencas
        {
            get { return _doencas; }
            set { SetProperty(ref _doencas, value); }
        }

        private List<CultivarDoenca> _cultivaresDoencas;
        public List<CultivarDoenca> CultivaresDoencas
        {
            get { return _cultivaresDoencas; }
            set { SetProperty(ref _cultivaresDoencas, value); }
        }

        private List<CultivarModel> _cultivares;
        public List<CultivarModel> Cultivares
        {
            get { return _cultivares; }
            set { SetProperty(ref _cultivares, value); }
        }

        private List<Tolerancia> _tolerancias;
        public List<Tolerancia> Tolerancias
        {
            get { return _tolerancias; }
            set { SetProperty(ref _tolerancias, value); }
        }

        private List<Doenca> _doencasLegenda;
        public List<Doenca> DoencasLegenda
        {
            get { return _doencasLegenda; }
            set { SetProperty(ref _doencasLegenda, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private IInformacoesManuais _informacoesManuais;

        private NavigationParameters _navigationParameters;

        public DelegateCommand InfoCommand { get; private set; }
        public DelegateCommand EnableDisableLegendaCommand { get; private set; }

        public CultivaresDoencasListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, IInformacoesManuais informacoesManuais)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _informacoesManuais = informacoesManuais;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();
            _doencas = new List<Doenca>();
            _cultivares = new List<CultivarModel>();
            _doencasLegenda = new List<Doenca>();

            // carrega lista de tolerancias para legenda
            Tolerancias = _regulaApiService.GetTolerancias().OrderBy(t=> t.Sigla.Length).ToList();

            TextoToggleLegenda = "Habilitar legendas";

            InfoCommand = new DelegateCommand(Informacoes);
            EnableDisableLegendaCommand = new DelegateCommand(EnableDisableLegenda);
        }

        private void EnableDisableLegenda()
        {
            Legenda = !Legenda;

            if (Legenda)
                TextoToggleLegenda = "Desabilitar legendas";
            else
                TextoToggleLegenda = "Habilitar legendas";
        }

        private void Informacoes()
        {
            InformacaoManual im = _informacoesManuais.InformacoesClienteCreate();
            _navigationParameters.Add("informacao", im);
            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        private CultivarModel loadCultivar(Cultivar x)
        {
            CultivarModel cm = new CultivarModel();

            //cm.AlturaPlanta = x.AlturaPlanta;
            //cm.ComprimentoFibraMaximo = x.ComprimentoFibraMaximo;
            //cm.ComprimentoFibraMinimo = x.ComprimentoFibraMinimo;
            //cm.DataDesativacao = x.DataDesativacao;
            //cm.Fertilidade = x.Fertilidade;
            cm.Id = x.Id;
            //cm.MicronaireMaximo = x.MicronaireMaximo;
            //cm.MicronaireMinimo = x.MicronaireMinimo;
            cm.Nome = x.Nome;
            //cm.PesoMedioCapulhoMaximo = x.PesoMedioCapulhoMaximo;
            //cm.PesoMedioCapulhoMinimo = x.PesoMedioCapulhoMinimo;
            //cm.PesoSementesMaximo = x.PesoSementesMaximo;
            //cm.PesoSementesMinimo = x.PesoSementesMinimo;
            //cm.Regulador = x.Regulador;
            //cm.RendimentoFibraMaximo = x.RendimentoFibraMaximo;
            //cm.RendimentoFibraMinimo = x.RendimentoFibraMinimo;
            //cm.ResistenciaMaximo = x.ResistenciaMaximo;
            //cm.ResistenciaMinimo = x.ResistenciaMinimo;
            //cm.Ciclo = _regulaApiService.GetCicloById(x.CicloId);
            cm.DoencasTolerancias = new List<DoencaTolerancia>();

            return cm;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("doencas", _doencas);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Doencas = (List<Doenca>)parameters["doencas"];
            loadCultivaresDoencas();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Doencas = (List<Doenca>)parameters["doencas"];
        }

        private void loadCultivaresDoencas()
        {
            // Carregar Cultivares, Doencas, Tolerancias
            _cultivaresDoencas = _regulaApiService.GetAllCultivarDoencas();
            List<Cultivar> cultivares = _regulaApiService.GetCultivar();

            foreach (var cultivar in cultivares)
            {
                CultivarModel cultivarModel = loadCultivar(cultivar);

                // atribui doenca e tolerancia
                foreach(var doenca in _doencas)
                {
                    var cultivarDoencaTolerancia = _cultivaresDoencas.Find(cdt => cdt.DoencaId == doenca.Id && cdt.CultivarId == cultivar.Id);
                    if(cultivarDoencaTolerancia != null)
                    {
                        DoencaTolerancia dt = new DoencaTolerancia();
                        dt.Doenca = doenca;
                        dt.Tolerancia = _regulaApiService.GetToleranciaById(cultivarDoencaTolerancia.ToleranciaId);
                        cultivarModel.DoencasTolerancias.Add(dt);
                    }
                }

                _cultivares.Add(cultivarModel);
            }
        }
    }
}

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
    public class CultivarSelectedPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private Cultivar _cultivar;
        public Cultivar Cultivar
        {
            get { return _cultivar; }
            set { SetProperty(ref _cultivar, value); }
        }

        private CultivarModel _cultivarModel;
        public CultivarModel CultivarModel
        {
            get { return _cultivarModel; }
            set { SetProperty(ref _cultivarModel, value); }
        }

        private List<CultivarModel> _cultivares;
        public List<CultivarModel> Cultivares
        {
            get { return _cultivares; }
            set { SetProperty(ref _cultivares, value); }
        }

        private CultivarDoenca _cultivarDoenca;
        public CultivarDoenca CultivarDoenca
        {
            get { return _cultivarDoenca; }
            set { SetProperty(ref _cultivarDoenca, value); }
        }

        private Ciclo _ciclo;
        public Ciclo Ciclo
        {
            get { return _ciclo; }
            set { SetProperty(ref _ciclo, value); }
        }

        private Doenca _doenca;
        public Doenca Doenca
        {
            get { return _doenca; }
            set { SetProperty(ref _doenca, value); }
        }

        private Tolerancia _tolerancia;
        public Tolerancia Tolerancia
        {
            get { return _tolerancia; }
            set { SetProperty(ref _tolerancia, value); }
        }

        private List<DoencaTolerancia> _doencasTolerancias;
        public List<DoencaTolerancia> DoencasTolerancias
        {
            get { return _doencasTolerancias; }
            set { SetProperty(ref _doencasTolerancias, value); }
        }

        private List<Tolerancia> _tolerancias;
        public List<Tolerancia> Tolerancias
        {
            get { return _tolerancias; }
            set { SetProperty(ref _tolerancias, value); }
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

        public DelegateCommand InfoCommand { get; private set; }

        NavigationParameters _navigationParameters;
        

        public CultivarSelectedPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, IInformacoesManuais informacoesManuais)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _informacoesManuais = informacoesManuais;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            CultivarModel = new CultivarModel();
            Tolerancias = new List<Tolerancia>();
            Tolerancias = _regulaApiService.GetTolerancias();
            Tolerancias = Tolerancias.OrderBy(t => t.Sigla.Length).ToList();

            InfoCommand = new DelegateCommand(Informacoes);
        }

        private void Informacoes()
        {
            InformacaoManual im = _informacoesManuais.InformacoesCultivarSelected();

            _navigationParameters.Add("informacao", im);

            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
            parameters.Add("cultivares", _cultivares);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Cliente = (Cliente)parameters["cliente"];
            Cultivar = (CultivarModel)parameters["cultivar"];
            Cultivares = (List<CultivarModel>)parameters["cultivares"];

            Ciclo = _regulaApiService.GetCicloById(Cultivar.CicloId);
            Title = _cultivar.Nome;

            // recuperar cultivar, doenças, tolerancias
            List<CultivarDoenca> cultivarDoencas = _regulaApiService.GetCultivarDoencaCultivarId(Cultivar.Id);
            CultivarModel.DoencasTolerancias = new List<DoencaTolerancia>();

            foreach (var x in cultivarDoencas)
            {
                Doenca d = _regulaApiService.GetDoencaById(x.DoencaId);
                Tolerancia t = _regulaApiService.GetToleranciaById(x.ToleranciaId);

                DoencaTolerancia dt = new DoencaTolerancia();
                dt.Tolerancia = t;
                dt.Doenca = d;

                CultivarModel.DoencasTolerancias.Add(dt);
            }

            DoencasTolerancias = CultivarModel.DoencasTolerancias;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Cliente = (Cliente)parameters["cliente"];
            Cultivar = (CultivarModel)parameters["cultivar"];
            Cultivares = (List<CultivarModel>)parameters["cultivares"];

            Ciclo = _regulaApiService.GetCicloById(Cultivar.CicloId);
            Title = _cultivar.Nome;

            // recuperar cultivar, doenças, tolerancias
            List<CultivarDoenca> cultivarDoencas = _regulaApiService.GetCultivarDoencaCultivarId(Cultivar.Id);
            CultivarModel.DoencasTolerancias = new List<DoencaTolerancia>();

            foreach (var x in cultivarDoencas)
            {
                Doenca d = _regulaApiService.GetDoencaById(x.DoencaId);
                Tolerancia t = _regulaApiService.GetToleranciaById(x.ToleranciaId);

                DoencaTolerancia dt = new DoencaTolerancia();
                dt.Tolerancia = t;
                dt.Doenca = d;

                CultivarModel.DoencasTolerancias.Add(dt);
            }

            DoencasTolerancias = CultivarModel.DoencasTolerancias;
        }
    }
}

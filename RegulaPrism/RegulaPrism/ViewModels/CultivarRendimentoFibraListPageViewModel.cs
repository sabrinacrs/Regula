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
    public class CultivarRendimentoFibraListPageViewModel : BindableBase, INavigationAware
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

        private CultivarModel _cultivar;
        public CultivarModel Cultivar
        {
            get { return _cultivar; }
            set { SetProperty(ref _cultivar, value); }
        }

        private CultivarModel _selectedItem;
        public CultivarModel SelectedItem
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

                CultivarSelectedCommand.Execute();
            }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private IInformacoesManuais _informacoesManuais;

        private NavigationParameters _navigationParameters;

        public DelegateCommand CultivarSelectedCommand { get; private set; }

        public DelegateCommand InfoCommand { get; private set; }

        private List<CultivarModel> _cultivares;
        public List<CultivarModel> Cultivares
        {
            get { return _cultivares; }
            set { SetProperty(ref _cultivares, value); }
        }

        public CultivarRendimentoFibraListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, IInformacoesManuais informacoesManuais)
        {
            Title = "Cultivares";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _informacoesManuais = informacoesManuais;
            _navigationParameters = new NavigationParameters();

            Cultivares = new List<CultivarModel>();
            loadCultivares();

            CultivarSelectedCommand = new DelegateCommand(CultivarSelected);
            InfoCommand = new DelegateCommand(Informacoes);
        }

        private void CultivarSelected()
        {
            // verificação para não repetir parâmetros
            if (_navigationParameters.Count() > 0)
                _navigationParameters = new NavigationParameters();

            _cultivar = _selectedItem;

            // adiciona parametros
            _navigationParameters.Add("cultivar", _cultivar);
            _navigationParameters.Add("cliente", _cliente);

            // abre página com detalhes da cultivar
            _navigationService.NavigateAsync("CultivarSelectedPage", _navigationParameters);
        }

        private void Informacoes()
        {
            InformacaoManual im = _informacoesManuais.InformacoesCultivarRendimento();

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
            _cliente = (Cliente)parameters["cliente"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
        }

        private void loadCultivares()
        {
            // carrega lista de cultivares
            List<Cultivar> cultivares = _regulaApiService.GetCultivar();
            cultivares = cultivares.OrderByDescending(c => c.RendimentoFibraMaximo).ToList();

            foreach (var x in cultivares)
            {
                CultivarModel cm = new CultivarModel();

                cm.AlturaPlanta = x.AlturaPlanta;
                cm.ComprimentoFibraMaximo = x.ComprimentoFibraMaximo;
                cm.ComprimentoFibraMinimo = x.ComprimentoFibraMinimo;
                cm.DataDesativacao = x.DataDesativacao;
                cm.Fertilidade = x.Fertilidade;
                cm.Id = x.Id;
                cm.MicronaireMaximo = x.MicronaireMaximo;
                cm.MicronaireMinimo = x.MicronaireMinimo;
                cm.Nome = x.Nome;
                cm.PesoMedioCapulhoMaximo = x.PesoMedioCapulhoMaximo;
                cm.PesoMedioCapulhoMinimo = x.PesoMedioCapulhoMinimo;
                cm.PesoSementesMaximo = x.PesoSementesMaximo;
                cm.PesoSementesMinimo = x.PesoSementesMinimo;
                cm.Regulador = x.Regulador;
                cm.RendimentoFibraMaximo = x.RendimentoFibraMaximo;
                cm.RendimentoFibraMinimo = x.RendimentoFibraMinimo;
                cm.ResistenciaMaximo = x.ResistenciaMaximo;
                cm.ResistenciaMinimo = x.ResistenciaMinimo;
                cm.Ciclo = _regulaApiService.GetCicloById(x.CicloId);

                Cultivares.Add(cm);
            }
        }
    }
}

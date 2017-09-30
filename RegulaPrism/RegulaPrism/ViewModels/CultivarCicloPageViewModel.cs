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
    public class CultivarCicloPageViewModel : BindableBase, INavigationAware
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

        private Ciclo _ciclo;
        public Ciclo Ciclo
        {
            get { return _ciclo; }
            set { SetProperty(ref _ciclo, value); }
        }

        private List<Ciclo> _ciclos;
        public List<Ciclo> Ciclos
        {
            get { return _ciclos; }
            set { SetProperty(ref _ciclos, value); }
        }

        private List<CultivarModel> _cultivares;
        public List<CultivarModel> Cultivares
        {
            get { return _cultivares; }
            set { SetProperty(ref _cultivares, value); }
        }

        private Ciclo _selectedItem;
        public Ciclo SelectedItem
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

                CicloSelectedCommand.Execute();
            }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private NavigationParameters _navigationParameters;


        public DelegateCommand CicloSelectedCommand { get; private set; }

        public CultivarCicloPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Selecionar Ciclo";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            Ciclos = _regulaApiService.GetCiclos();

            CicloSelectedCommand = new DelegateCommand(CicloSelected);
        }

        private void CicloSelected()
        {
            _ciclo = _selectedItem;

            // filtrar cultivares de acordo com o ciclo selecionado
            List<Cultivar> cultivares = new List<Cultivar>();
            cultivares = _regulaApiService.GetCultivarByCiclo(_ciclo.Id);

            // cultivar model
            Cultivares = new List<CultivarModel>();
            loadCultivares(cultivares);

            // verificação para não repetir parâmetros
            if (_navigationParameters.Count() > 0)
                _navigationParameters = new NavigationParameters();

            // adiciona parametros
            _navigationParameters.Add("cliente", _cliente);
            _navigationParameters.Add("cultivares", _cultivares);
            _navigationParameters.Add("ciclo", _ciclo);

            // navega para página com a lista de cultivares daquele ciclo - cultivarList
            _navigationService.NavigateAsync("CultivarListPage", _navigationParameters);
        }

        private void loadCultivares(List<Cultivar> cultivares)
        {
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
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
    }
}

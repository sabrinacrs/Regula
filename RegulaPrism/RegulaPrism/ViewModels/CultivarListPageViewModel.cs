using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RegulaPrism.ViewModels
{
    public class CultivarListPageViewModel : BindableBase, INavigationAware
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

        private List<CultivarModel> _cultivares;
        public List<CultivarModel> Cultivares
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

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand CultivarSelectedCommand { get; private set; }

        public CultivarListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            CultivarSelectedCommand = new DelegateCommand(CultivarSelected);
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
            parameters.Add("cultivares", _cultivares);
            parameters.Add("ciclo", _ciclo);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
            Cultivares = (List<CultivarModel>)parameters["cultivares"];
            Ciclo = (Ciclo)parameters["ciclo"];

            // atribuir titulo à página
            if (Ciclo != null)
            {
                Title = Ciclo.Descricao;
            }
            else
                Title = "Cultivares";

            // verifica se recebeu parametro de cultivares
            if (Cultivares == null)
            {
                Cultivares = new List<CultivarModel>();
                loadCultivares();
            }

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
            Cultivares = (List<CultivarModel>)parameters["cultivares"];
            Ciclo = (Ciclo)parameters["ciclo"];

            if (Ciclo != null)
            {
                Title = Ciclo.Descricao;
            }
            else
                Title = "Cultivares";

            // verifica se recebeu parametro de cultivares
            if (Cultivares == null)
            {
                Cultivares = new List<CultivarModel>();
                loadCultivares();
            }
            
        }

        private void loadCultivares()
        {
            // carrega lista de cultivares
            List<Cultivar> cultivares = _regulaApiService.GetCultivar();
            
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
//CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
//if (CultivaresRecomendadas.Count > 0)
//{
//    Cultivares = new List<Cultivar>();

//    foreach (var cr in CultivaresRecomendadas)
//    {
//        Cultivares.Add(cr.Cultivar);
//    }
//}
//CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
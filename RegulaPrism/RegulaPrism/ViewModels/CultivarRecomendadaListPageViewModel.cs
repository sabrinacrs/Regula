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
    public class CultivarRecomendadaListPageViewModel : BindableBase, INavigationAware
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

        private CultivarRecomendada _selectedItem;
        public CultivarRecomendada SelectedItem
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

                CultivarRecomendadaSelectCommand.Execute();
            }
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

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private NavigationParameters _navigationParameters;


        public DelegateCommand CultivarSelectedCommand { get; private set; }

        public DelegateCommand CultivarRecomendadaSelectCommand { get; private set; }

        public CultivarRecomendadaListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Cultivares Recomendadas";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            // carrega lista de cultivares
            Cultivares = _regulaApiService.GetCultivar();

            //CultivarSelectedCommand = new DelegateCommand(CultivarSelected);
            CultivarRecomendadaSelectCommand = new DelegateCommand(CultivarSelected);
        }

        private void CultivarSelected()
        {
            // pega cultivar selecionada 
            // verificação para não repetir parâmetros
            if (_navigationParameters.Count() > 0)
                _navigationParameters = new NavigationParameters();

            // abre a página com detalhes da cultivar
            _navigationParameters.ToList().Clear();
            _navigationParameters.Add("cultivarRecomendada", _selectedItem);

            _navigationService.NavigateAsync("CultivarRecomendadaSelectedPage", _navigationParameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cultivaresRecomendadas", _cultivaresRecomendadas);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
        }
    }
}

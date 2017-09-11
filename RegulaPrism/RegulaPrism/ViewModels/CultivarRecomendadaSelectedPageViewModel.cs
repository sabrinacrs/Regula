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
    public class CultivarRecomendadaSelectedPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private List<CultivarRecomendada> _cultivaresRecomendadas;
        public List<CultivarRecomendada> CultivaresRecomendadas
        {
            get { return _cultivaresRecomendadas; }
            set { SetProperty(ref _cultivaresRecomendadas, value); }
        }

        private CultivarRecomendada _cultivarRecomendada;
        public CultivarRecomendada CultivarRecomendada
        {
            get { return _cultivarRecomendada; }
            set { SetProperty(ref _cultivarRecomendada, value); }
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

        public CultivarRecomendadaSelectedPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cultivaresRecomendadas", _cultivaresRecomendadas);   
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CultivarRecomendada = (CultivarRecomendada)parameters["cultivarRecomendada"];
            CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
            DoencasTolerancias = CultivarRecomendada.DoencasTolerancias;
            Title = "Cultivar " + CultivarRecomendada.Cultivar.Nome;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            CultivarRecomendada = (CultivarRecomendada)parameters["cultivarRecomendada"];
            CultivaresRecomendadas = (List<CultivarRecomendada>)parameters["cultivaresRecomendadas"];
            DoencasTolerancias = CultivarRecomendada.DoencasTolerancias;
            Title = "Cultivar " + CultivarRecomendada.Cultivar.Nome;
        }
    }
}

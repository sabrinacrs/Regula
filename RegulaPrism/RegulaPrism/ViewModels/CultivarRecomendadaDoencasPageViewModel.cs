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
    public class CultivarRecomendadaDoencasPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private List<Doenca> _doencas;
        public List<Doenca> Doencas
        {
            get { return _doencas; }
            set { SetProperty(ref _doencas, value); }
        }

        private List<Cultivar> _cultivares;
        public List<Cultivar> Cultivares
        {
            get { return _cultivares; }
            set { SetProperty(ref _cultivares, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand CultivarRecomendadaListCommand { get; private set; }

        public CultivarRecomendadaDoencasPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Doenças";

            // carrega lista de epocas de semeadura
            var config = Xamarin.Forms.DependencyService.Get<IMySqlConnect>();
            Doencas = config.CarregaDoencas();

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            CultivarRecomendadaListCommand = new DelegateCommand(CultivarRecomendadaList);
        }

        private void CultivarRecomendadaList()
        {
            // adiciona filtros de doencas, etc

            // vai para cultivar list
            _navigationService.NavigateAsync("CultivarListPage", _navigationParameters);

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            // tem que receber vários parametros - epoca semeadura, etc
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}

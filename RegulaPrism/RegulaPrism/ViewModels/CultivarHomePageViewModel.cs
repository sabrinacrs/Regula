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
    public class CultivarHomePageViewModel : BindableBase, INavigationAware
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

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand CultivarListCommand { get; private set; }
        public DelegateCommand CultivarCicloCommand { get; private set; }
        public DelegateCommand CultivarRendimentoCommand { get; private set; }
        public DelegateCommand CultivarRecomendadaCommand { get; private set; }

        public CultivarHomePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Cultivares";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _navigationParameters = new NavigationParameters();

            CultivarListCommand = new DelegateCommand(CultivarList);
            CultivarCicloCommand = new DelegateCommand(CultivarCiclo);
            CultivarRendimentoCommand = new DelegateCommand(CultivarRendimento);
            CultivarRecomendadaCommand = new DelegateCommand(CultivarRecomendadaExecute);
        }

        private void CultivarList()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync("CultivarListPage", _navigationParameters);
        }

        private void CultivarCiclo()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync("CultivarCicloPage", _navigationParameters);
        }

        private void CultivarRendimento()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync("CultivarRendimentoFibraListPage", _navigationParameters);
        }

        private void CultivarRecomendadaExecute()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/CultivarRecomendadaPage", UriKind.Absolute), _navigationParameters);
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

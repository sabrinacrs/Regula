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
    public class FazendaHomePageViewModel : BindableBase, INavigationAware
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

        public DelegateCommand FazendaCreateCommand { get; private set; }
        public DelegateCommand FazendaUpdateCommand { get; private set; }
        public DelegateCommand FazendaListCommand { get; private set; }
        public DelegateCommand FazendaDeleteCommand { get; private set; }

        public FazendaHomePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Gerenciar Fazendas";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _navigationParameters = new NavigationParameters();

            FazendaCreateCommand = new DelegateCommand(FazendaCreate);
            FazendaUpdateCommand = new DelegateCommand(FazendaUpdate);
            FazendaListCommand = new DelegateCommand(FazendaList);
            FazendaDeleteCommand = new DelegateCommand(FazendaDelete);
        }

        private void FazendaCreate()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationParameters.Add("action", "create");
            _navigationService.NavigateAsync("FazendaCreatePage", _navigationParameters);
        }
        private void FazendaUpdate()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationParameters.Add("action", "update");
            _navigationService.NavigateAsync("FazendaListPage", _navigationParameters);
        }

        private void FazendaList()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationParameters.Add("action", "list");
            _navigationService.NavigateAsync("FazendaListPage", _navigationParameters);
        }

        private void FazendaDelete()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationParameters.Add("action", "delete");
            _navigationService.NavigateAsync("FazendaDeletePage", _navigationParameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters) // navegou de
        {
            // passa fazenda e cliente
            parameters.Add("cliente", _cliente);
        }

        public void OnNavigatedTo(NavigationParameters parameters) // navegou para
        {
            _cliente = (Cliente)parameters["cliente"];
        }

        public void OnNavigatingTo(NavigationParameters parameters) // navegando para
        {
            // passa fazenda e cliente
            _cliente = (Cliente)parameters["cliente"];
        }
    }
}

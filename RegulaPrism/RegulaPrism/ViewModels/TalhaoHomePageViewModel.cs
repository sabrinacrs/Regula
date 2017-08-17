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
    public class TalhaoHomePageViewModel : BindableBase, INavigationAware
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

        private Talhao _talhao;
        public Talhao Talhao
        {
            get { return _talhao; }
            set { SetProperty(ref _talhao, value); }
        }

        private Fazenda _fazenda;
        public Fazenda Fazenda
        {
            get { return _fazenda; }
            set { SetProperty(ref _fazenda, value); }
        }
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private IRegulaApiService _regulaApiService;
        private NavigationParameters _navigationParameters;

        public DelegateCommand TalhaoCreateCommand { get; private set; }
        public DelegateCommand TalhaoUpdateCommand { get; private set; }
        public DelegateCommand TalhaoListCommand { get; private set; }
        public DelegateCommand TalhaoDeleteCommand { get; private set; }

        public TalhaoHomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Gerenciar Talhões";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            TalhaoCreateCommand = new DelegateCommand(TalhaoCreate);
            TalhaoUpdateCommand = new DelegateCommand(TalhaoUpdate);
            TalhaoListCommand = new DelegateCommand(TalhaoList);
            TalhaoDeleteCommand = new DelegateCommand(TalhaoDelete);
        }

        private void TalhaoCreate()
        {
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync("TalhaoCreatePage", _navigationParameters);
        }
        private void TalhaoUpdate()
        {
            _navigationParameters.Add("talhao", _talhao);
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync("TalhaoUpdatePage", _navigationParameters);
        }

        private void TalhaoList()
        {
            _navigationParameters.Add("fazenda", _fazenda);
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync("TalhaoListPage", _navigationParameters);
        }

        private void TalhaoDelete()
        {
            _navigationParameters.Add("talhao", _talhao);
            _navigationParameters.Add("cliente", _cliente);
            _navigationService.NavigateAsync("TalhaoDeletePage", _navigationParameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            //Fazenda = (Fazenda)parameters["fazenda"];
            _cliente = (Cliente)parameters["cliente"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //Fazenda = (Fazenda)parameters["fazenda"];
            _cliente = (Cliente)parameters["cliente"];
        }
    }
}

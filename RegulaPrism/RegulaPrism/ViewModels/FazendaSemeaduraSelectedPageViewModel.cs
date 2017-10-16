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
    public class FazendaSemeaduraSelectedPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private SemeaduraModel _semeadura;
        public SemeaduraModel Semeadura
        {
            get { return _semeadura; }
            set { SetProperty(ref _semeadura, value); }
        }

        private CalculosSemeadura _calculoSemeadura;
        public CalculosSemeadura CalculoSemeadura
        {
            get { return _calculoSemeadura; }
            set { SetProperty(ref _calculoSemeadura, value); }
        }

        private Fazenda _fazenda;
        public Fazenda Fazenda
        {
            get { return _fazenda; }
            set { SetProperty(ref _fazenda, value); }
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

        private NavigationParameters _navigationParameters;

        public DelegateCommand InfoCommand { get; private set; }

        public FazendaSemeaduraSelectedPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService, IInformacoesManuais informacoesManuais)
        {
            Title = "Semeadura Selecionada";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _informacoesManuais = informacoesManuais;
            _navigationParameters = new NavigationParameters();

            InfoCommand = new DelegateCommand(Informacoes);
        }

        private void Informacoes()
        {
            InformacaoManual im = _informacoesManuais.InformacoesClienteCreate();

            _navigationParameters.Add("informacao", im);

            _navigationService.NavigateAsync("InformacoesPage", _navigationParameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("semeadura", _semeadura);
            parameters.Add("cliente", _cliente);
            parameters.Add("fazenda", _fazenda);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Semeadura = (SemeaduraModel)parameters["semeadura"];
            CalculoSemeadura = _regulaApiService.GetCalculosSemeaduraBySemeaduraId(Semeadura.Id);

            Cliente = (Cliente)parameters["cliente"];
            Fazenda = (Fazenda)parameters["fazenda"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Semeadura = (SemeaduraModel)parameters["semeadura"];
            CalculoSemeadura = _regulaApiService.GetCalculosSemeaduraBySemeaduraId(Semeadura.Id);

            Cliente = (Cliente)parameters["cliente"];
            Fazenda = (Fazenda)parameters["fazenda"];
        }
    }
}

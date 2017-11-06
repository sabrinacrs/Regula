using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism;
using RegulaPrism.Models;
using RegulaPrism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class TesteInterfaceViewModel : BindableBase
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); }
        }

        private List<Cultivar> _cultivares;
        public List<Cultivar> Cultivares
        {
            get { return _cultivares; }
            set { SetProperty(ref _cultivares, value); }
        }

        private List<Tolerancia> _tolerancias;
        public List<Tolerancia> Tolerancias
        {
            get { return _tolerancias; }
            set { SetProperty(ref _tolerancias, value); }
        }

        //private List<string> _cultivares;
        //public List<string> Cultivares
        //{
        //    get { return _cultivares; }
        //    set { SetProperty(ref _cultivares, value); }
        //}

        private List<Cultivar> _listaCultivares;
        public List<Cultivar> ListaCultivares
        {
            get { return _listaCultivares; }
            set { SetProperty(ref _listaCultivares, value); }
        }

        public DataService dataService;

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private IInformacoesManuais _informacoesManuais;

        private NavigationParameters _navigationParameters;

        public TesteInterfaceViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            //
            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            Tolerancias = _regulaApiService.GetTolerancias().OrderBy(t => t.Sigla.Length).ToList();
        }
    }
}

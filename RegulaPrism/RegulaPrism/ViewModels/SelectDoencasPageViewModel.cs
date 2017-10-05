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
    public class SelectDoencasPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _doencaSwitchCell;
        public string DoencaSwitchCell
        {
            get { return _doencaSwitchCell; }
            set { SetProperty(ref _doencaSwitchCell, value); }
        }

        private List<Doenca> _doencas;
        public List<Doenca> Doencas
        {
            get { return _doencas; }
            set { SetProperty(ref _doencas, value); }
        }

        private List<Doenca> _doencasSolo;
        public List<Doenca> DoencasSolo
        {
            get { return _doencasSolo; }
            set { SetProperty(ref _doencasSolo, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand AddRemoveDoencaCommand { get; private set; }

        public DelegateCommand SelectDoencasCommand { get; private set; }

        public SelectDoencasPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Selecionar Doencas do Solo";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();
            _doencasSolo = new List<Doenca>();

            // carrega lista de doencas
            Doencas = _regulaApiService.GetDoenca();

            AddRemoveDoencaCommand = new DelegateCommand(AddRemoveDoenca);
            SelectDoencasCommand = new DelegateCommand(SelectDoencas);
        }

        private void AddRemoveDoenca()
        {
            // adiciona doenca à lista de doencas do solo
            // procura doenca na lista com todas as doencas
            var doe = _doencas.Find(d => d.Id == Int32.Parse(_doencaSwitchCell));

            // verifica se já foi inserida na list de doencas do solo
            Doenca doeSolo = _doencasSolo.Find(d => d.Id == Int32.Parse(_doencaSwitchCell));

            // se já foi inserida, remove
            if (doeSolo != null)
            {
                int pos = _doencasSolo.FindIndex(d => d.Id == doeSolo.Id);
                _doencasSolo.Remove(doeSolo);
            }
            else
            {
                _doencasSolo.Add(doe);
            }
        }

        private void SelectDoencas()
        {
            _navigationParameters.Add("doencas", _doencasSolo);
            _navigationService.NavigateAsync("CultivaresDoencasListPage", _navigationParameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("doencas", _doencasSolo);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}

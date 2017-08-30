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
    public class CultivarListPageViewModel : BindableBase, INavigationAware
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

        private Cultivar _selectedItem;
        public Cultivar SelectedItem
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

                //FazendaSelectedCommand.Execute();
            }
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


        public DelegateCommand CultivarSelectedCommand { get; private set; }

        public CultivarListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Cultivares";

            // carrega lista de cultivares
            var config = Xamarin.Forms.DependencyService.Get<IMySqlConnect>();
            Cultivares = config.CarregaCultivares();

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            CultivarSelectedCommand = new DelegateCommand(CultivarSelected);
        }

        private void CultivarSelected()
        {

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}

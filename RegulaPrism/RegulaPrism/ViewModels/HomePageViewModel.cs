using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class HomePageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        public DelegateCommand NavigateToCultivarListPageCommand { get; private set; }
        public DelegateCommand NavigateToSemeaduraPageCommand { get; private set; }
        public DelegateCommand NavigateToFazendaHomePageCommand { get; private set; }
        public DelegateCommand NavigateToTalhaoHomePageCommand { get; private set; }
        public DelegateCommand NavigateToClienteUpdatePageCommand { get; private set; }

        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Regula";

            _navigationService = navigationService;
            _dialogService = dialogService;

            NavigateToCultivarListPageCommand = new DelegateCommand(NavigateToCultivarListPage);
            NavigateToSemeaduraPageCommand = new DelegateCommand(NavigateToSemeaduraPage);
            NavigateToFazendaHomePageCommand = new DelegateCommand(NavigateToFazendaHomePage);
            NavigateToTalhaoHomePageCommand = new DelegateCommand(NavigateToTalhaoHomePage);
            NavigateToClienteUpdatePageCommand = new DelegateCommand(NavigateToClienteUpdatePage);
        }

        private void NavigateToCultivarListPage()
        {
            //_navigationService.NavigateAsync("");
        }

        private void NavigateToSemeaduraPage()
        {
            _navigationService.NavigateAsync("SemeaduraPage");
        }

        private void NavigateToFazendaHomePage()
        {
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/FazendaHomePage", UriKind.Absolute));
        }

        private void NavigateToTalhaoHomePage()
        {
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/TalhaoHomePage", UriKind.Absolute));
        }

        private void NavigateToClienteUpdatePage()
        {
            _navigationService.NavigateAsync("ClienteUpdatePage");
        }
    }
}

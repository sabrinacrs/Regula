using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class FazendaHomePageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        public DelegateCommand FazendaCreateCommand { get; private set; }
        public DelegateCommand FazendaUpdateCommand { get; private set; }
        public DelegateCommand FazendaListCommand { get; private set; }
        public DelegateCommand FazendaDeleteCommand { get; private set; }

        public FazendaHomePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Gerenciar Fazendas";

            _navigationService = navigationService;
            _dialogService = dialogService;

            FazendaCreateCommand = new DelegateCommand(FazendaCreate);
            FazendaUpdateCommand = new DelegateCommand(FazendaUpdate);
            FazendaListCommand = new DelegateCommand(FazendaList);
            FazendaDeleteCommand = new DelegateCommand(FazendaDelete);
        }

        private void FazendaCreate()
        {
            _navigationService.NavigateAsync("FazendaRegisterTabbedPage");
        }
        private void FazendaUpdate()
        {
            _navigationService.NavigateAsync("FazendaListPage");
        }

        private void FazendaList()
        {
            _navigationService.NavigateAsync("FazendaListPage");
        }

        private void FazendaDelete()
        {
            _navigationService.NavigateAsync("FazendaDeletePage");
        }
    }
}

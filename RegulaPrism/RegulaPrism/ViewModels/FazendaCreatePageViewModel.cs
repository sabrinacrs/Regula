using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class FazendaCreatePageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        public DelegateCommand NavigateToFazendaContatoPageCommand { get; private set; }
        public DelegateCommand FazendaSaveCommand { get; private set; }

        public FazendaCreatePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Nova Fazenda";

            _navigationService = navigationService;
            _dialogService = dialogService;

            NavigateToFazendaContatoPageCommand = new DelegateCommand(NavigateToFazendaContatoPage);
            FazendaSaveCommand = new DelegateCommand(FazendaSave);
        }

        private void NavigateToFazendaContatoPage()
        {
            _navigationService.NavigateAsync("FazendaContatoPage");
        }

        private void FazendaSave()
        {
            // validações e inserção no banco
        }
    }
}

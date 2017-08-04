using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class FazendaContatoPageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        public DelegateCommand NavigateToFazendaLocalizacaoPageCommand { get; private set; }
        public DelegateCommand FazendaSaveCommand { get; private set; }

        public FazendaContatoPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Contato";

            _navigationService = navigationService;
            _dialogService = dialogService;

            NavigateToFazendaLocalizacaoPageCommand = new DelegateCommand(NavigateToFazendaLocalizacaoPage);
            FazendaSaveCommand = new DelegateCommand(FazendaSave);
        }

        private void NavigateToFazendaLocalizacaoPage()
        {
            _navigationService.NavigateAsync("FazendaLocalizacaoPage");
        }

        private void FazendaSave()
        {
            // validações e inserção no banco
        }
    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class FazendaLocalizacaoPageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;
        public DelegateCommand FazendaSaveCommand { get; private set; }

        public FazendaLocalizacaoPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Localização";

            _navigationService = navigationService;
            _dialogService = dialogService;

            FazendaSaveCommand = new DelegateCommand(FazendaSave);
        }

        private void FazendaSave()
        {
            // validações e inserção no banco
        }
    }
}

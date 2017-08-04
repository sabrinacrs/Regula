using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class ClienteUpdatePageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        public DelegateCommand ClienteSaveCommand { get; private set; }
        public DelegateCommand ClienteDeleteCommand { get; private set; }

        public ClienteUpdatePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Dados Pessoais";

            _navigationService = navigationService;
            _dialogService = dialogService;

            ClienteSaveCommand = new DelegateCommand(ClienteSave);
            ClienteDeleteCommand = new DelegateCommand(ClienteDelete);
        }

        private void ClienteSave()
        {

        }

        private void ClienteDelete()
        {

        }
    }
}

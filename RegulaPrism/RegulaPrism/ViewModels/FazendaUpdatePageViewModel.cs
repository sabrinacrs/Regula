using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class FazendaUpdatePageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        public DelegateCommand FazendaUpdateCommand { get; private set; }
        public DelegateCommand FazendaDeleteCommand { get; private set; }

        public FazendaUpdatePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Atualizar Fazenda";

            _navigationService = navigationService;
            _dialogService = dialogService;

            FazendaUpdateCommand = new DelegateCommand(FazendaUpdate);
            FazendaDeleteCommand = new DelegateCommand(FazendaDelete);
        }

        private void FazendaUpdate()
        {
            // TODO
        }

        private void FazendaDelete()
        {
            // TODO
        }
    }
}

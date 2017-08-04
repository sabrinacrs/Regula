using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class TalhaoUpdatePageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        public DelegateCommand TalhaoUpdateCommand { get; private set; }
        public DelegateCommand TalhaoDeleteCommand { get; private set; }

        public TalhaoUpdatePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Atualizar Talhão";

            _navigationService = navigationService;
            _dialogService = dialogService;

            TalhaoUpdateCommand = new DelegateCommand(TalhaoUpdate);
            TalhaoDeleteCommand = new DelegateCommand(TalhaoDelete);
        }
        private void TalhaoUpdate()
        {
            // TODO
        }

        private void TalhaoDelete()
        {
            // TODO
        }
    }
}

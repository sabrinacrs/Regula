using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class TalhaoHomePageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        public DelegateCommand TalhaoCreateCommand { get; private set; }
        public DelegateCommand TalhaoUpdateCommand { get; private set; }
        public DelegateCommand TalhaoListCommand { get; private set; }
        public DelegateCommand TalhaoDeleteCommand { get; private set; }

        public TalhaoHomePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Gerenciar Talhões";

            _navigationService = navigationService;
            _dialogService = dialogService;

            TalhaoCreateCommand = new DelegateCommand(TalhaoCreate);
            TalhaoUpdateCommand = new DelegateCommand(TalhaoUpdate);
            TalhaoListCommand = new DelegateCommand(TalhaoList);
            TalhaoDeleteCommand = new DelegateCommand(TalhaoDelete);
        }

        private void TalhaoCreate()
        {
            _navigationService.NavigateAsync("TalhaoCreatePage");
        }
        private void TalhaoUpdate()
        {
            _navigationService.NavigateAsync("TalhaoUpdatePage");
        }

        private void TalhaoList()
        {
            _navigationService.NavigateAsync("TalhaoListPage");
        }

        private void TalhaoDelete()
        {
            _navigationService.NavigateAsync("TalhaoDeletePage");
        }
    }
}

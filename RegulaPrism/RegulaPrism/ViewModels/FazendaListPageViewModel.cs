using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class FazendaListPageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { SetProperty(ref _cliente, value); }
        }

        public Fazenda _fazenda;
        public Fazenda Fazenda
        {
            get { return _fazenda; }
            set { SetProperty(ref _fazenda, value); }
        }
        public ObservableCollection<Fazenda> Fazendas { get; }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        public DelegateCommand FazendaSelectedCommand { get; private set; }
        public DelegateCommand FazendaSearchCommand { get; private set; }

        public FazendaListPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Fazendas";

            _navigationService = navigationService;
            _dialogService = dialogService;

            FazendaSelectedCommand = new DelegateCommand(FazendaSelected);
            FazendaSearchCommand = new DelegateCommand(FazendaSearch);
        }

        private void FazendaSelected()
        {
            // TODO
            // leva fazenda selecionada pra página de update
        }

        private void FazendaSearch()
        {
            // TODO
            // filtra de acordo com o texto digitado os nomes das fazendas
        }
    }
}

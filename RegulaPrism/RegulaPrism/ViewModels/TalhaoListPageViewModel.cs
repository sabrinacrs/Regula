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
    public class TalhaoListPageViewModel : BindableBase
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

        public Talhao _talhao;
        public Talhao Talhao
        {
            get { return _talhao; }
            set { SetProperty(ref _talhao, value); }
        }

        public ObservableCollection<Talhao> Talhoes { get; }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        public DelegateCommand TalhaoSelectedCommand { get; private set; }
        public DelegateCommand TalhaoSearchCommand { get; private set; }

        public TalhaoListPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Talhões";

            _navigationService = navigationService;
            _dialogService = dialogService;

            TalhaoSelectedCommand = new DelegateCommand(TalhaoSelected);
            TalhaoSearchCommand = new DelegateCommand(TalhaoSearch);
        }

        private void TalhaoSelected()
        {
            // TODO
            // leva fazenda selecionada pra página de update
        }

        private void TalhaoSearch()
        {
            // TODO
            // filtra de acordo com o texto digitado os nomes das fazendas
        }
    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class HomeMasterDetailPageViewModel : BindableBase, INavigatedAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { SetProperty(ref _cliente, value); }
        }

        private List<ItemMenuLateral> _itens;
        public List<ItemMenuLateral> Itens
        {
            get { return _itens; }
            set { SetProperty(ref _itens, value);  }
        }

        private ItemMenuLateral _selectedItem;
        public ItemMenuLateral SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                    return;

                NavigateToItemPageCommand.Execute();
            }
        }

        private INavigationService _navigationService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand NavigateToItemPageCommand { get; private set; }

        public HomeMasterDetailPageViewModel(INavigationService navigationService)
        {
            Title = "Regula";

            _itens = GetItens();
            _navigationService = navigationService;
            _navigationParameters = new NavigationParameters();

            NavigateToItemPageCommand = new DelegateCommand(NavigateToItemPage);
        }

        public List<ItemMenuLateral> GetItens()
        {
            List<ItemMenuLateral> lista = new List<ItemMenuLateral>();

            //lista.Add(new ItemMenuLateral("Cadastrar Cliente", ""));
            lista.Add(new ItemMenuLateral("Home", "HomePage"));
            lista.Add(new ItemMenuLateral("Dados Pessoais", "ClienteUpdatePage"));
            lista.Add(new ItemMenuLateral("Fazendas", "FazendaHomePage"));
            lista.Add(new ItemMenuLateral("Talhões", "TalhaoHomePage"));
            //lista.Add(new ItemMenuLateral("Cadastrar Talhão", "TalhaoHomePage"));
            lista.Add(new ItemMenuLateral("Semeadura", "CalcularSemeaduraPage"));
            lista.Add(new ItemMenuLateral("Sair", "LoginPage"));

            return lista;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
        }

        private void NavigateToItemPage()
        {
            if(!SelectedItem.Titulo.Equals("Sair"))
            {
                _navigationParameters.Add("cliente", _cliente);
                _navigationService.NavigateAsync("NavigationPage/" + SelectedItem.Descricao, _navigationParameters);
            }
                
            else
                _navigationService.NavigateAsync(new Uri("http://regulafabioecher.com/NavigationPage/LoginPage", UriKind.Absolute));
        }
    }
}

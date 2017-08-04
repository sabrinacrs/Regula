using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class HomeMasterDetailPageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
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

        public DelegateCommand NavigateToItemPageCommand { get; private set; }

        public HomeMasterDetailPageViewModel(INavigationService navigationService)
        {
            Title = "Regula";

            _itens = GetItens();
            _navigationService = navigationService;
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
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        private void NavigateToItemPage()
        {
            if(!SelectedItem.Titulo.Equals("Sair"))
                _navigationService.NavigateAsync("NavigationPage/" + SelectedItem.Descricao);
            else
                _navigationService.NavigateAsync(new Uri("http://regulafabioecher.com/NavigationPage/LoginPage", UriKind.Absolute));
        }
    }
}

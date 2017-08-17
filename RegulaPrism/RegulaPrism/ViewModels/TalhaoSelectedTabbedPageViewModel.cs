using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class TalhaoSelectedTabbedPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int _fazendaSelectedIndex;
        public int FazendaSelectedIndex
        {
            get { return _fazendaSelectedIndex; }
            set { SetProperty(ref _fazendaSelectedIndex, value); }
        }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { SetProperty(ref _cliente, value); }
        }

        private Fazenda _fazenda;
        public Fazenda Fazenda
        {
            get { return _fazenda; }
            set { SetProperty(ref _fazenda, value); }
        }

        private Talhao _talhao;
        public Talhao Talhao
        {
            get { return _talhao; }
            set { SetProperty(ref _talhao, value); }
        }

        public TalhaoSelectedTabbedPageViewModel()
        {
            Title = "Talhão";
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //_fazenda = (Fazenda)parameters["fazenda"];
            //_talhao = (Talhao)parameters["talhao"];
            parameters.Add("fazenda", _fazenda);
            parameters.Add("talhao", _talhao);
            parameters.Add("cliente", _cliente);
            parameters.Add("fazendaSelectedIndex", _fazendaSelectedIndex);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Fazenda = (Fazenda)parameters["fazenda"];
            Talhao = (Talhao)parameters["talhao"];
            Cliente = (Cliente)parameters["cliente"];
            FazendaSelectedIndex = (int)parameters["fazendaSelectedIndex"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Fazenda = (Fazenda)parameters["fazenda"];
            Talhao = (Talhao)parameters["talhao"];
            Cliente = (Cliente)parameters["cliente"];
            FazendaSelectedIndex = (int)parameters["fazendaSelectedIndex"];
        }
    }
}

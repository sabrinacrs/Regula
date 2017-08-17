using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class FazendaSelectedTabbedPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private Fazenda _fazenda;
        public Fazenda Fazenda
        {
            get { return _fazenda; }
            set { SetProperty(ref _fazenda, value); }
        }

        public FazendaSelectedTabbedPageViewModel()
        {
            Title = "Fazenda";
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            _fazenda = (Fazenda)parameters["fazenda"];
            parameters.Add("fazenda", _fazenda);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _fazenda = (Fazenda)parameters["fazenda"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            _fazenda = (Fazenda)parameters["fazenda"];
        }
    }
}

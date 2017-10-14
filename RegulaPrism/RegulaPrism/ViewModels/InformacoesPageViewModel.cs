using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class InformacoesPageViewModel : BindableBase, INavigationAware
    {
        private InformacaoManual _informacao;
        public InformacaoManual Informacao
        {
            get { return _informacao; }
            set { SetProperty(ref _informacao, value); }
        }

        public InformacoesPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Informacao = (InformacaoManual)parameters["informacao"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Informacao = (InformacaoManual)parameters["informacao"];
        }
    }
}

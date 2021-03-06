﻿using Plugin.Connectivity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

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

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { SetProperty(ref _cliente, value); }
        }

        public DelegateCommand NavigateToHelpPageCommand { get; private set; }

        public InformacoesPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            NavigateToHelpPageCommand = new DelegateCommand(NavigateToHelpPage);
        }

        private void NavigateToHelpPage()
        {
            if (CrossConnectivity.Current.IsConnected)
                Device.OpenUri(new Uri(_informacao.LinkHelpOnline));
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
            Informacao = (InformacaoManual)parameters["informacao"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
            Informacao = (InformacaoManual)parameters["informacao"];
        }
    }
}

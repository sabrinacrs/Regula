﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class Login2PageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set { SetProperty(ref _senha, value); }
        }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { SetProperty(ref _cliente, value); }
        }

        private IRegulaApiService _regulaApiService;

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand NavigateToClienteCreatePageCommand { get; private set; }
        public DelegateCommand NavigateToHomeMasterDetailPageCommand { get; private set; }


        public Login2PageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            // binding do título da página
            Title = "Regula";

            // services
            _navigationParameters = new NavigationParameters();
            _navigationService = navigationService;
            _regulaApiService = regulaApiService;
            _dialogService = dialogService;

            // instanciar commands
            NavigateToClienteCreatePageCommand = new DelegateCommand(NavigateToClienteCreatePage);
            NavigateToHomeMasterDetailPageCommand = new DelegateCommand(NavigateToHomeMasterDetailPage);
        }

        private void NavigateToHomeMasterDetailPage()
        {
            // poupa tempo
            if (Login == null && Senha == null)
            {
                Login = "sabrinacr";
                Senha = "eitagiovana";
            }

            // validações do login
            var cliente = _regulaApiService.GetClienteByEmail(Login);
            if (cliente == null)
                cliente = _regulaApiService.GetClienteByLogin(Login);

            if (cliente == null)
                _dialogService.DisplayAlertAsync("", "Este login/e-mail não consta em nossos registros", "OK");
            else
            {
                if (cliente.DataDesativacao.Year == 1)
                {
                    if (!Senha.Equals(cliente.Senha) || Senha == null)
                        _dialogService.DisplayAlertAsync("", "Senha inválida", "OK");
                    else
                    {
                        Cliente = new Cliente();
                        Cliente = cliente;

                        _navigationParameters.Add("cliente", _cliente);
                        _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/HomePage", UriKind.Absolute), _navigationParameters);
                    }
                }
                else
                    _dialogService.DisplayAlertAsync("", "Conta desativada", "OK");
            }

            //_navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/HomePage", UriKind.Absolute));
        }

        private void NavigateToClienteCreatePage()
        {
            _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/ClienteCreatePage", UriKind.Absolute));
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
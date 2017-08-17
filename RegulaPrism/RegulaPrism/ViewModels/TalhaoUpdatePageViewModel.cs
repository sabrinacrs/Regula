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
    public class TalhaoUpdatePageViewModel : BindableBase, INavigationAware
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

        private Talhao _talhao;
        public Talhao Talhao
        {
            get { return _talhao; }
            set { SetProperty(ref _talhao, value); }
        }

        private Fazenda _fazenda;
        public Fazenda Fazenda
        {
            get { return _fazenda; }
            set { SetProperty(ref _fazenda, value); }
        }

        private List<Fazenda> _fazendas;
        public List<Fazenda> Fazendas
        {
            get { return _fazendas; }
            set { SetProperty(ref _fazendas, value); }
        }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { SetProperty(ref _cliente, value); }
        }

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand TalhaoUpdateCommand { get; private set; }
        public DelegateCommand TalhaoDeleteCommand { get; private set; }

        public TalhaoUpdatePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Atualizar Talhão";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();

            TalhaoUpdateCommand = new DelegateCommand(TalhaoUpdate);
            TalhaoDeleteCommand = new DelegateCommand(TalhaoDelete);
        }
        private void TalhaoUpdate()
        {
            // validações
            string message = validateFields();

            if(message.Equals(""))
            {
                _fazenda = Fazendas.ElementAt(_fazendaSelectedIndex);
                _talhao.FazendaId = _fazenda.Id;

                // update no banco
                if (_regulaApiService.UpdateTalhao(_talhao))
                {
                    // mensagem de talhão atualizado
                    _dialogService.DisplayAlertAsync("", "Talhão atualizado", "OK");

                    // adiciona parametros 
                    _navigationParameters.Add("fazenda", _fazenda);
                    _navigationParameters.Add("cliente", _cliente);
                    _navigationParameters.Add("cliente", _talhao);
                    _navigationParameters.Add("fazendaSelectedIndex", _fazendaSelectedIndex);
                    _navigationParameters.Add("action", "update");

                    // redireciona para a lista de fazendas
                    _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/TalhaoListPage", UriKind.Absolute), _navigationParameters);
                }
                else
                {
                    _dialogService.DisplayAlertAsync("", "Algo deu errado durante a tentativa de atualização. Verifique os dados e tente novamente", "OK");
                }
            }
            else
            {
                _dialogService.DisplayAlertAsync("", message, "OK");
            }
        }

        private async void TalhaoDelete()
        {
            // se tiver semeadura, não exclui

            // mensagem de confirmação de exclusão
            var choise = await _dialogService.DisplayAlertAsync("Confirmação", "Confirma a exclusão do talhão " + _talhao.Descricao + "?", "Excluir", "Cancelar");

            if (choise)
            {
                // desativando o talhao, exckusão lógica
                if (_regulaApiService.DeleteLogicalTalhao(_talhao))
                {
                    await _dialogService.DisplayAlertAsync("", "Talhão excluído com sucesso", "OK");

                    _navigationParameters.Add("cliente", _cliente);
                    _navigationParameters.Add("fazenda", _fazenda);
                    _navigationParameters.Add("action", "update");

                    _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/TalhaoListPage", UriKind.Absolute), _navigationParameters);
                }
                else
                {
                    _dialogService.DisplayAlertAsync("", "Não foi possível excluir o talhão. Verifique se há semeaduras ativas antes de realizar a exclusão", "OK");
                }
            }
        }

        private string validateFields()
        {
            if (_fazendaSelectedIndex == -1)
                return "Selecione uma fazenda";
            else if (Talhao.Descricao == null || Talhao.Descricao.Equals(""))
                return "Informe uma descrição para identificar o talhão";
            else if (Talhao.Tamanho == 0 || Talhao.Tamanho < 0)
                return "Informe o tamanho do talhão";
            else if (Talhao.Espacamento == 0 || Talhao.Espacamento < 0)
                return "Informe o espaçamento entre as carretilhas";

            return "";
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("talhao", _talhao);
            parameters.Add("fazenda", _fazenda);
            parameters.Add("cliente", _fazenda);
            parameters.Add("fazendaSelectedIndex", _fazendaSelectedIndex);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Talhao = (Talhao)parameters["talhao"];
            Fazenda = (Fazenda)parameters["fazenda"];
            _cliente = (Cliente)parameters["cliente"];
            Fazendas = _regulaApiService.GetFazendasByCliente(_cliente.Id);
            FazendaSelectedIndex = (int)parameters["fazendaSelectedIndex"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Talhao = (Talhao)parameters["talhao"];
            Fazenda = (Fazenda)parameters["fazenda"];
            _cliente = (Cliente)parameters["cliente"];
            Fazendas = _regulaApiService.GetFazendasByCliente(_cliente.Id);
            FazendaSelectedIndex = (int)parameters["fazendaSelectedIndex"];
        }
    }
}

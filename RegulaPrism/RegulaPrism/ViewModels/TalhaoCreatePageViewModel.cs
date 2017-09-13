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
    public class TalhaoCreatePageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _descricao;
        public string Descricao
        {
            get { return _descricao; }
            set { SetProperty(ref _descricao, value); }
        }

        private double _tamanho;
        public double Tamanho
        {
            get { return _tamanho; }
            set { SetProperty(ref _tamanho, value); }
        }

        private double _espacamento;
        public double Espacamento
        {
            get { return _espacamento; }
            set { SetProperty(ref _espacamento, value); }
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

        private INavigationService _navigationService;

        private IPageDialogService _dialogService;

        private IRegulaApiService _regulaApiService;

        private NavigationParameters _navigationParameters;

        public DelegateCommand TalhaoSaveCommand { get; private set; }

        public TalhaoCreatePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRegulaApiService regulaApiService)
        {
            Title = "Novo Talhão";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _regulaApiService = regulaApiService;
            _navigationParameters = new NavigationParameters();
            _fazendaSelectedIndex = -1;

            TalhaoSaveCommand = new DelegateCommand(TalhaoSave);
        }

        private void TalhaoSave()
        {
            // TODO
            // validações
            string message = validateFields();

            if(message.Equals(""))
            {
                // talhao view
                _talhao = talhaoView();

                // pega fazenda selecionadas
                _fazenda = Fazendas.ElementAt(_fazendaSelectedIndex);
                
                // inserção do talhão no banco
                _talhao.FazendaId = _fazenda.Id;

                if(_regulaApiService.InsertTalhao(_talhao))
                {
                    // passa parametro navigationaware
                    _navigationParameters.Add("fazenda", _fazenda);
                    _navigationParameters.Add("cliente", _cliente);

                    _dialogService.DisplayAlertAsync("Novo talhão", "Talhão " + _talhao.Descricao + " registrado", "OK");
                    _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/TalhaoListPage", UriKind.Absolute), _navigationParameters);
                }
                else
                {
                    _dialogService.DisplayAlertAsync("", "Algo deu errado no cadastro do talhão. Verifique os dados e tente novamente", "OK");
                }
            }
            else
            {
                _dialogService.DisplayAlertAsync("", message, "OK");
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("cliente", _cliente);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
            Fazendas = _regulaApiService.GetFazendasByCliente(_cliente.Id);

            if (Fazendas.Count() <= 0)
            {
                _dialogService.DisplayAlertAsync("", "Você precisa cadastrar uma fazenda para cadastrar um talhão", "OK");
                _navigationService.NavigateAsync("NavigationPage/FazendaCreatePage");
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            _cliente = (Cliente)parameters["cliente"];
            Fazendas = _regulaApiService.GetFazendasByCliente(_cliente.Id);

            if (Fazendas.Count() <= 0)
            {
                _navigationParameters.Add("cliente", _cliente);

                _dialogService.DisplayAlertAsync("", "Você precisa cadastrar uma fazenda para cadastrar um talhão", "OK");
                _navigationService.NavigateAsync(new Uri("http://brianlagunas.com/HomeMasterDetailPage/NavigationPage/FazendaCreatePage", UriKind.Absolute), _navigationParameters);
            }
        }

        private string validateFields()
        {
            if (_fazendaSelectedIndex == -1)
                return "Selecione uma fazenda";
            else if (Descricao == null || Descricao.Equals(""))
                return "Informe uma descrição para identificar o talhão";
            else if (Tamanho == 0 || Tamanho < 0)
                return "Informe o tamanho do talhão";
            else if (Espacamento == 0 || Espacamento < 0)
                return "Informe o espaçamento entre as carretilhas";

            return "";
        }

        private Talhao talhaoView()
        {
            Talhao talhaoView = new Talhao();

            talhaoView.Descricao = Descricao;
            talhaoView.Espacamento = (decimal)Espacamento;
            talhaoView.Tamanho = (decimal)Tamanho;

            return talhaoView;
        }
    }
}

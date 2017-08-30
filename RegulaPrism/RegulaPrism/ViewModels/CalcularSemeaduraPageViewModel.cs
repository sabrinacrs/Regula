using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class CalcularSemeaduraPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int _epocaSemeaduraSelectedIndex;
        public int EpocaSemeaduraSelectedIndex
        {
            get { return _epocaSemeaduraSelectedIndex; }
            set { SetProperty(ref _epocaSemeaduraSelectedIndex, value); }
        }

        private double _plantasHectare;
        public double PlantasHectare
        {
            get { return _plantasHectare; }
            set { SetProperty(ref _plantasHectare, value); }
        }

        private double _pesoSementesMinimo;
        public double PesoSementesMinimo
        {
            get { return _pesoSementesMinimo; }
            set { SetProperty(ref _pesoSementesMinimo, value); }
        }

        private double _pesoSementesMaximo;
        public double PesoSementesMaximo
        {
            get { return _pesoSementesMaximo; }
            set { SetProperty(ref _pesoSementesMaximo, value); }
        }

        private double _pesoSementesMetro;
        public double PesoSementesMetro
        {
            get { return _pesoSementesMetro; }
            set { SetProperty(ref _pesoSementesMetro, value); }
        }

        private double _pesoSementesHectareMinimo;
        public double PesoSementesHectareMinimo
        {
            get { return _pesoSementesHectareMinimo; }
            set { SetProperty(ref _pesoSementesHectareMinimo, value); }
        }

        private double _pesoSementesHectareMaximo;
        public double PesoSementesHectareMaximo
        {
            get { return _pesoSementesHectareMaximo; }
            set { SetProperty(ref _pesoSementesHectareMaximo, value); }
        }

        private double _pesoSementesAlqueireMinimo;
        public double PesoSementesAlqueireMinimo
        {
            get { return _pesoSementesAlqueireMinimo; }
            set { SetProperty(ref _pesoSementesAlqueireMinimo, value); }
        }

        private double _pesoSementesAlqueireMaximo;
        public double PesoSementesAlqueireMaximo
        {
            get { return _pesoSementesAlqueireMaximo; }
            set { SetProperty(ref _pesoSementesAlqueireMaximo, value); }
        }

        private double _espacamento;
        public double Espacamento
        {
            get { return _espacamento; }
            set { SetProperty(ref _espacamento, value); }
        }

        private double _germinacao;
        public double Germinacao
        {
            get { return _germinacao; }
            set { SetProperty(ref _germinacao, value); }
        }

        private double _metrosLineares;
        public double MetrosLineares
        {
            get { return _metrosLineares; }
            set { SetProperty(ref _metrosLineares, value); }
        }

        private Cultivar _cultivar;
        public Cultivar Cultivar
        {
            get { return _cultivar; }
            set { SetProperty(ref _cultivar, value); }
        }

        private EpocaSemeadura _epocaSemeadura;
        public EpocaSemeadura EpocaSemeadura
        {
            get { return _epocaSemeadura; }
            set { SetProperty(ref _epocaSemeadura, value); }
        }

        private List<EpocaSemeadura> _epocasSemeadura;
        public List<EpocaSemeadura> EpocasSemeadura
        {
            get { return _epocasSemeadura; }
            set { SetProperty(ref _epocasSemeadura, value); }
        }

        private List<CultivarEpocaSemeadura> _cultivarEpocasSemeadura;
        public List<CultivarEpocaSemeadura> CultivarEpocasSemeadura
        {
            get { return _cultivarEpocasSemeadura; }
            set { SetProperty(ref _cultivarEpocasSemeadura, value); }
        }

        public CalcularSemeaduraPageViewModel()
        {
            Title = "Calcular Semeadura";
        }

        private void calculoSementesMetro()
        {
            // peso de sementes por metro
            PesoSementesMetro = Math.Round(((_plantasHectare / _metrosLineares)) / (_germinacao / 100), 1);
        }

        private void calculoSementesHectare()
        {
            // peso de sementes por hectare
            PesoSementesHectareMinimo = Math.Round((((((_plantasHectare / _metrosLineares)) / (_germinacao / 100) * _metrosLineares) * ((double)_cultivar.PesoSementesMinimo)) / 100 ) / 1000, 1);
            PesoSementesHectareMaximo = Math.Round((((((_plantasHectare / _metrosLineares)) / (_germinacao / 100) * _metrosLineares) * ((double)_cultivar.PesoSementesMaximo)) / 100) / 1000, 1);
        }

        private void calculoSementesAlqueire()
        {
            // peso de sementes por hectare
            PesoSementesAlqueireMinimo = Math.Round(((((((_plantasHectare / _metrosLineares)) / (_germinacao / 100) * _metrosLineares) * ((double)_cultivar.PesoSementesMinimo)) / 100) / 1000) * 2.42, 1);
            PesoSementesAlqueireMaximo = Math.Round(((((((_plantasHectare / _metrosLineares)) / (_germinacao / 100) * _metrosLineares) * ((double)_cultivar.PesoSementesMaximo)) / 100) / 1000) * 2.42, 1);
        }

        private double findCultivarEpocaSemeadura()
        {
            CultivarEpocaSemeadura cep = new CultivarEpocaSemeadura();
            cep = _cultivarEpocasSemeadura.Find(c => c.CultivarId == _cultivar.Id && c.EpocaSemeaduraId == _epocaSemeadura.Id); //.Where(); //.Where(c => c.EpocaSemeaduraId == _epocaSemeadura.Id);

            return (double)cep.PlantasHa;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Cultivar = (Cultivar)parameters["cultivar"];
            CultivarEpocasSemeadura = (List<CultivarEpocaSemeadura>)parameters["cultivarEpocasSemeadura"];
            EpocaSemeaduraSelectedIndex = (int)parameters["epocaSemeaduraSelectedIndex"];
            EpocasSemeadura = (List<EpocaSemeadura>)parameters["epocasSemeadura"];
            EpocaSemeadura = (EpocaSemeadura)parameters["epocaSemeadura"];

            Espacamento = (double)parameters["espacamento"];
            MetrosLineares = (double)parameters["metrosLineares"];
            Germinacao = (double)parameters["germinacao"];

            // pega número de plantas/ha
            PlantasHectare = findCultivarEpocaSemeadura();

            // Peso Sementes Minimo
            PesoSementesMinimo = (double)_cultivar.PesoSementesMinimo;

            // Peso Sementes Maximo
            PesoSementesMaximo = (double)_cultivar.PesoSementesMaximo;

            // realiza calculos
            calculoSementesMetro();
            calculoSementesHectare();
            calculoSementesAlqueire();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Cultivar = (Cultivar)parameters["cultivar"];
            CultivarEpocasSemeadura = (List<CultivarEpocaSemeadura>)parameters["cultivarEpocasSemeadura"];
            EpocaSemeaduraSelectedIndex = (int)parameters["epocaSemeaduraSelectedIndex"];
            EpocasSemeadura = (List<EpocaSemeadura>)parameters["epocasSemeadura"];
            EpocaSemeadura = (EpocaSemeadura)parameters["epocaSemeadura"];

            Espacamento = (double)parameters["espacamento"];
            MetrosLineares = (double)parameters["metrosLineares"];
            Germinacao = (double)parameters["germinacao"];

            // pega numero de plantas/ha
            PlantasHectare = findCultivarEpocaSemeadura();

            // Peso Sementes Minimo
            PesoSementesMinimo = (double)_cultivar.PesoSementesMinimo;

            // Peso Sementes Maximo
            PesoSementesMaximo = (double)_cultivar.PesoSementesMaximo;

            // realiza calculos
            calculoSementesMetro();
            calculoSementesHectare();
            calculoSementesAlqueire();
        }
    }
}

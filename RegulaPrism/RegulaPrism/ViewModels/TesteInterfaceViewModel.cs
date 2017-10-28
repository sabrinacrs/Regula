using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using RegulaPrism;
using RegulaPrism.Models;
using RegulaPrism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class TesteInterfaceViewModel : BindableBase
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); }
        }

        private List<Cultivar> _cultivares;
        public List<Cultivar> Cultivares
        {
            get { return _cultivares; }
            set { SetProperty(ref _cultivares, value); }
        }

        //private List<string> _cultivares;
        //public List<string> Cultivares
        //{
        //    get { return _cultivares; }
        //    set { SetProperty(ref _cultivares, value); }
        //}

        private List<Cultivar> _listaCultivares;
        public List<Cultivar> ListaCultivares
        {
            get { return _listaCultivares; }
            set { SetProperty(ref _listaCultivares, value); }
        }

        public DataService dataService;

        //async void AtualizaDados()
        //{
        //    List<Cultivar> produtos = await dataService.GetCultivaresAsync();
        //    ListaCultivares = produtos.OrderBy(item => item.Nome).ToList();
        //}

        public TesteInterfaceViewModel()
        {
            //dataService = new DataService();
            //AtualizaDados();
        }
    }
}

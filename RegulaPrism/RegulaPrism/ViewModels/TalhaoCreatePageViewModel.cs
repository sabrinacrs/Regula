using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegulaPrism.ViewModels
{
    public class TalhaoCreatePageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand TalhaoSaveCommand { get; private set; }

        public TalhaoCreatePageViewModel()
        {
            Title = "Novo Talhão";

            TalhaoSaveCommand = new DelegateCommand(TalhaoSave);
        }

        private void TalhaoSave()
        {
            // TODO
            // validações
            // inserção do talhão no banco
        }
    }
}

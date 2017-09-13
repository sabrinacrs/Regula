using RegulaPrism.ViewModels;
using System;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class RegistrarSemeaduraPage : ContentPage
    {
        public RegistrarSemeaduraPage()
        {
            InitializeComponent();

            FazendasPicker.SelectedIndexChanged += this.FazendasPickerSelectedIndexChanged;
            TalhoesPicker.SelectedIndexChanged += this.TalhoesPickerSelectedIndexChanged;
        }

        public void FazendasPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            ((RegistrarSemeaduraPageViewModel)this.BindingContext).FazendaSelectedIndex = FazendasPicker.SelectedIndex;

            // carrega picker de talhoes
            ((RegistrarSemeaduraPageViewModel)this.BindingContext).FazendaSelectedCommand.Execute();
        }

        public void TalhoesPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            ((RegistrarSemeaduraPageViewModel)this.BindingContext).TalhaoSelectedIndex = TalhoesPicker.SelectedIndex;
        }
    }
}

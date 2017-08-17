using RegulaPrism.ViewModels;
using System;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class TalhaoListPage : ContentPage
    {
        public TalhaoListPage()
        {
            InitializeComponent();

            FazendasPicker.SelectedIndexChanged += this.FazendaPickerSelectedIndexChanged;
        }

        public void FazendaPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            //Method call every time when picker selection changed.
            ((TalhaoListPageViewModel)this.BindingContext).FazendaSelectedIndex = FazendasPicker.SelectedIndex;
            ((TalhaoListPageViewModel)this.BindingContext).FazendaSelectedCommand.Execute();
        }

    }
}

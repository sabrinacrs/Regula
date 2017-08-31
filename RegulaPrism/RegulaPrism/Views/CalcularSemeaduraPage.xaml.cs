using RegulaPrism.ViewModels;
using System;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class CalcularSemeaduraPage : ContentPage
    {
        public CalcularSemeaduraPage()
        {
            InitializeComponent();

            EpocaSemeaduraPicker.SelectedIndexChanged += this.EpocaSemeaduraPickerSelectedIndexChanged;
        }

        public void EpocaSemeaduraPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            //Method call every time when picker selection changed.
            ((CalcularSemeaduraPageViewModel)this.BindingContext).EpocaSemeaduraSelectedIndex = EpocaSemeaduraPicker.SelectedIndex;
            ((CalcularSemeaduraPageViewModel)this.BindingContext).CalcularSemeaduraCommand.Execute();
        }
    }
}

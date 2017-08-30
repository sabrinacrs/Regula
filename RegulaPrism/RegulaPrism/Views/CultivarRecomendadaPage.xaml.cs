using RegulaPrism.ViewModels;
using System;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class CultivarRecomendadaPage : ContentPage
    {
        public CultivarRecomendadaPage()
        {
            InitializeComponent();

            EpocaSemeaduraPicker.SelectedIndexChanged += this.EpocaSemeaduraPickerSelectedIndexChanged;
        }

        public void EpocaSemeaduraPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            //Method call every time when picker selection changed.
            ((CultivarRecomendadaPageViewModel)this.BindingContext).EpocaSemeaduraSelectedIndex = EpocaSemeaduraPicker.SelectedIndex;
            ((CultivarRecomendadaPageViewModel)this.BindingContext).CultivarEpocaSemeaduraListCommand.Execute();
        }
    }
}

using RegulaPrism.ViewModels;
using System;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class SemeaduraPage : ContentPage
    {
        public SemeaduraPage()
        {
            InitializeComponent();

            CultivaresPicker.SelectedIndexChanged += this.CultivarPickerSelectedIndexChanged;
        }

        public void CultivarPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            //Method call every time when picker selection changed.
            ((SemeaduraPageViewModel)this.BindingContext).CultivarSelectedIndex = CultivaresPicker.SelectedIndex;
            ((SemeaduraPageViewModel)this.BindingContext).CultivarEpocaSemeaduraListCommand.Execute();
        }
    }
}

using RegulaPrism.ViewModels;
using System;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class CultivarDoencaToleranciaPage : ContentPage
    {
        public CultivarDoencaToleranciaPage()
        {
            InitializeComponent();

        }

        private void HandleValueChanged(object sender, EventArgs e)
        {
            var slider = (Slider)sender;
            var classId = slider.ClassId;
            var sliderValue = slider.Value.ToString();

            // pegar valor do slider e filtrar cultivares doencas tolerancias
            ((CultivarDoencaToleranciaPageViewModel)this.BindingContext).ToleranciaSliderDoenca = classId;
            ((CultivarDoencaToleranciaPageViewModel)this.BindingContext).ToleranciaSliderToleracia = sliderValue;
            ((CultivarDoencaToleranciaPageViewModel)this.BindingContext).AddCultivarDoencaToleranciaCommand.Execute();

            // tem que passar o value do slider também
            //((CultivarDoencaToleranciaPageViewModel)this.BindingContext).AddRemoveDoencaCommand.Execute();

            //label.Text = slider.Value.ToString();
            // filtra doenca e tolerancia ou adiciona
        }
    }
}

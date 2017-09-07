using RegulaPrism.ViewModels;
using System;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class CultivarRecomendadaDoencasPage : ContentPage
    {
        public CultivarRecomendadaDoencasPage()
        {
            InitializeComponent();

            // usar lista de doencas
            
        }

        private void SwitchOnChanged(object sender, EventArgs e)
        {
            var switchCell = (Switch)sender;
            var classId = switchCell.ClassId;

            ((CultivarRecomendadaDoencasPageViewModel)this.BindingContext).DoencaSwitchCell = classId;
            ((CultivarRecomendadaDoencasPageViewModel)this.BindingContext).AddRemoveDoencaCommand.Execute();
        }
    }
}

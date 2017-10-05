using RegulaPrism.ViewModels;
using System;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class SelectDoencasPage : ContentPage
    {
        public SelectDoencasPage()
        {
            InitializeComponent();
        }

        private void SwitchOnChanged(object sender, EventArgs e)
        {
            var switchCell = (Switch)sender;
            var classId = switchCell.ClassId;

            ((SelectDoencasPageViewModel)this.BindingContext).DoencaSwitchCell = classId;
            ((SelectDoencasPageViewModel)this.BindingContext).AddRemoveDoencaCommand.Execute();
        }
    }
}

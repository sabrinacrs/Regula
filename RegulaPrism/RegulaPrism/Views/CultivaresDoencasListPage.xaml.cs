using RegulaPrism.Models;
using RegulaPrism.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class CultivaresDoencasListPage : ContentPage
    {
        public CultivaresDoencasListPage()
        {
            InitializeComponent();

            GridDoencas.RowDefinitions = new RowDefinitionCollection();
            GridDoencas.ColumnDefinitions = new ColumnDefinitionCollection();

            int length = ((CultivaresDoencasListPageViewModel)this.BindingContext).Doencas.Count;
            List<Doenca> doencas = ((CultivaresDoencasListPageViewModel)this.BindingContext).Doencas;

            for (int i = 0; i < length; i++)
            {
                GridDoencas.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < length; i++)
            {
                GridDoencas.Children.Add(new Label { Text = "" + (i+1) }, i, 0);
            }
        }
    }
}

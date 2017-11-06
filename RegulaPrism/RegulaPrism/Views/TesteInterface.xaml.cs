//using DLToolkit.Forms.Controls;
using System;
using Prism.Navigation;
using Xamarin.Forms;
using RegulaPrism.ViewModels;
using RegulaPrism.Models;
using System.Collections.Generic;

namespace RegulaPrism.Views
{
    public partial class TesteInterface : ContentPage, INavigationAware
    {
        public TesteInterface()
        {
            InitializeComponent();
            //FlowListView.Init();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            int lengthCultivares = ((TesteInterfaceViewModel)this.BindingContext).Tolerancias.Count;
            List<Tolerancia> tolerancias = ((TesteInterfaceViewModel)this.BindingContext).Tolerancias;

            // Grid Cultivares
            GridTolerancias.RowDefinitions = new RowDefinitionCollection();
            GridTolerancias.ColumnDefinitions = new ColumnDefinitionCollection();

            GridTolerancias.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });

            int col = 0;
            foreach(var x in tolerancias)
            {
                GridTolerancias.Children.Add(new Label
                {
                    Text = x.Sigla + "\n" + x.Descricao,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center
                }, col, 0);

                col++;
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}

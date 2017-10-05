﻿using RegulaPrism.Models;
using RegulaPrism.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace RegulaPrism.Views
{
    public partial class CultivaresDoencasListPage : ContentPage, INavigationAware
    {
        public CultivaresDoencasListPage()
        {
            InitializeComponent();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            // Doencas
            int lengthDoencas = ((CultivaresDoencasListPageViewModel)this.BindingContext).Doencas.Count;
            List<Doenca> doencas = ((CultivaresDoencasListPageViewModel)this.BindingContext).Doencas;

            // Grid Cultivares
            GridCultivares.RowDefinitions = new RowDefinitionCollection();
            GridCultivares.ColumnDefinitions = new ColumnDefinitionCollection();

            int lengthCultivares = ((CultivaresDoencasListPageViewModel)this.BindingContext).Cultivares.Count;
            List<CultivarModel> cultivares = ((CultivaresDoencasListPageViewModel)this.BindingContext).Cultivares;

            // header
            Label labelHeader = new Label
            {
                Text = "Doenças Selecionadas",
                TextColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.Green,
                HorizontalTextAlignment = TextAlignment.Center
            };


            GridCultivares.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            GridCultivares.Children.Add(labelHeader, 1, 0);

            GridCultivares.Children.Add(new Label
            {
                Text = "Cultivares",
                TextColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.Green,
                HorizontalTextAlignment = TextAlignment.Center
            }, 0, 0);

            Grid.SetColumnSpan(labelHeader, lengthDoencas);

            GridCultivares.Children.Add(new Label
            {
                Text = " ",
                TextColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.LawnGreen,
                HorizontalTextAlignment = TextAlignment.Center
            }, 0, 1);

            int j = 2;
            foreach (var x in cultivares)
            {
                GridCultivares.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

                GridCultivares.Children.Add(new Label
                {
                    Text = x.Nome,
                    VerticalTextAlignment = TextAlignment.Center,
                    BackgroundColor = Color.White,
                    HorizontalTextAlignment = TextAlignment.Center
                }, 0, j);

                int l = 1;
                foreach (var k in x.DoencasTolerancias)
                {
                    Doenca doe = doencas.Find(d => d.Id == k.Doenca.Id);
                    if (doe != null)
                    {
                        Doenca dtl = ((CultivaresDoencasListPageViewModel)this.BindingContext).DoencasLegenda.Find(v => v.Id == doe.Id);

                        if (dtl == null)
                            ((CultivaresDoencasListPageViewModel)this.BindingContext).DoencasLegenda.Add(doe);


                        GridCultivares.Children.Add(new Label
                        {
                            Text = "" + l,
                            BackgroundColor = Color.LawnGreen,
                            VerticalTextAlignment = TextAlignment.Center,
                            TextColor = Color.White,
                            HorizontalTextAlignment = TextAlignment.Center
                        }, l, 1);

                        GridCultivares.Children.Add(new Label
                        {
                            Text = k.Tolerancia.Sigla,
                            VerticalTextAlignment = TextAlignment.Center,
                            BackgroundColor = Color.White,
                            HorizontalTextAlignment = TextAlignment.Center
                        }, l, j);

                        l++;
                    }
                }

                j++;
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //// Doencas
            //int lengthDoencas = ((CultivaresDoencasListPageViewModel)this.BindingContext).Doencas.Count;
            //List<Doenca> doencas = ((CultivaresDoencasListPageViewModel)this.BindingContext).Doencas;

            //// Grid Cultivares
            //GridCultivares.RowDefinitions = new RowDefinitionCollection();
            //GridCultivares.ColumnDefinitions = new ColumnDefinitionCollection();

            //int lengthCultivares = ((CultivaresDoencasListPageViewModel)this.BindingContext).Cultivares.Count;
            //List<CultivarModel> cultivares = ((CultivaresDoencasListPageViewModel)this.BindingContext).Cultivares;

            //// header
            //Label labelHeader = new Label
            //{
            //    Text = "Doenças Selecionadas",
            //    TextColor = Color.White,
            //    VerticalTextAlignment = TextAlignment.Center,
            //    BackgroundColor = Color.Green,
            //    HorizontalTextAlignment = TextAlignment.Center
            //};


            //GridCultivares.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //GridCultivares.Children.Add(labelHeader, 1, 0);

            //GridCultivares.Children.Add(new Label
            //{
            //    Text = "Cultivares",
            //    TextColor = Color.White,
            //    VerticalTextAlignment = TextAlignment.Center,
            //    BackgroundColor = Color.Green,
            //    HorizontalTextAlignment = TextAlignment.Center
            //}, 0, 0);

            //Grid.SetColumnSpan(labelHeader, lengthDoencas);

            //GridCultivares.Children.Add(new Label
            //{
            //    Text = " ",
            //    TextColor = Color.White,
            //    VerticalTextAlignment = TextAlignment.Center,
            //    BackgroundColor = Color.LawnGreen,
            //    HorizontalTextAlignment = TextAlignment.Center
            //}, 0, 1);

            //int j = 2;
            //foreach (var x in cultivares)
            //{
            //    GridCultivares.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

            //    GridCultivares.Children.Add(new Label
            //    {
            //        Text = x.Nome,
            //        VerticalTextAlignment = TextAlignment.Center,
            //        BackgroundColor = Color.White,
            //        HorizontalTextAlignment = TextAlignment.Center
            //    }, 0, j);

            //    int l = 1;
            //    foreach (var k in x.DoencasTolerancias)
            //    {
            //        if (doencas.Contains(k.Doenca))
            //        {
            //            GridCultivares.Children.Add(new Label
            //            {
            //                Text = "" + l,
            //                BackgroundColor = Color.LawnGreen,
            //                VerticalTextAlignment = TextAlignment.Center,
            //                TextColor = Color.White,
            //                HorizontalTextAlignment = TextAlignment.Center
            //            }, l, 1);

            //            GridCultivares.Children.Add(new Label
            //            {
            //                Text = k.Tolerancia.Sigla,
            //                VerticalTextAlignment = TextAlignment.Center,
            //                BackgroundColor = Color.White,
            //                HorizontalTextAlignment = TextAlignment.Center
            //            }, l, j);

            //            l++;
            //        }
            //    }

            //    j++;
            //}
        }
    }
}

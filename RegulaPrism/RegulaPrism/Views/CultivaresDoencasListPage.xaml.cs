using RegulaPrism.Models;
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

        private void SwitchOnChanged(object sender, EventArgs e)
        {
            var switchCell = (Switch)sender;

            ((CultivaresDoencasListPageViewModel)this.BindingContext).EnableDisableLegendaCommand.Execute();
            
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

            // define linhas do header
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

            // Linha de doenças
            Grid.SetColumnSpan(labelHeader, lengthDoencas);

            GridCultivares.Children.Add(new Label
            {
                Text = " ",
                TextColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.LawnGreen,
                HorizontalTextAlignment = TextAlignment.Center
            }, 0, 1);

            // adiciona cultivares e tolerancias às doenças
            int j = 2;
            foreach (var x in cultivares)
            {
                // linha com cultivar
                GridCultivares.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

                GridCultivares.Children.Add(new Label
                {
                    Text = x.Nome,
                    VerticalTextAlignment = TextAlignment.Center,
                    BackgroundColor = Color.White,
                    HorizontalTextAlignment = TextAlignment.Center
                }, 0, j);

                // colunas de doencas e tolerancias
                int l = 1;
                foreach (var k in x.DoencasTolerancias)
                {
                    Doenca doe = doencas.Find(d => d.Id == k.Doenca.Id);
                    if (doe != null)
                    {
                        Doenca dtl = ((CultivaresDoencasListPageViewModel)this.BindingContext).DoencasLegenda.Find(v => v.Id == doe.Id);

                        if (dtl == null)
                            ((CultivaresDoencasListPageViewModel)this.BindingContext).DoencasLegenda.Add(doe);

                        // coluna com número da doença
                        GridCultivares.Children.Add(new Label
                        {
                            Text = "" + l,
                            BackgroundColor = Color.LawnGreen,
                            VerticalTextAlignment = TextAlignment.Center,
                            TextColor = Color.White,
                            HorizontalTextAlignment = TextAlignment.Center
                        }, l, 1);

                        // coluna com tolerância
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


            //------------ LEGENDAS --------------------

            //Grid for enable disable legendas
            GridEnableLegendas.RowDefinitions = new RowDefinitionCollection();
            //GridEnableLegendas.ColumnDefinitions = new ColumnDefinitionCollection();

            // atribui tamanho 100 para coluna 1
            ColumnDefinition column1 = new ColumnDefinition();
            column1.Width = new GridLength(15, GridUnitType.Star);

            // atribui tamanho 100 para coluna 2
            ColumnDefinition column2 = new ColumnDefinition();
            column2.Width = new GridLength(10, GridUnitType.Star);

            //Definitions for column and row
            GridEnableLegendas.ColumnDefinitions.Add(column1);
            GridEnableLegendas.ColumnDefinitions.Add(column2);
            GridEnableLegendas.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });

            // add label in grid enable
            Label label = new Label();
            label.ClassId = "labelEnableDisable";
            label.FontSize = 14;
            label.FontAttributes = FontAttributes.Italic;
            label.TextColor = Color.DimGray;
            label.Margin = new Thickness(0, 5, 0, 5);
            label.SetBinding(Label.TextProperty, "TextoToggleLegenda");
            label.BindingContext = new { ((CultivaresDoencasListPageViewModel)this.BindingContext).TextoToggleLegenda };
            //label.Text = ((CultivaresDoencasListPageViewModel)this.BindingContext).TextoToggleLegenda;

            // add label in grid enable
            GridEnableLegendas.Children.Add(label, 0, 0);

            Switch s = new Switch();
            s.IsToggled = false;
            s.Toggled += SwitchOnChanged;

            // add switch in grid enable
            GridEnableLegendas.Children.Add(s, 0, 0);


            //Legenda de Tolerâncias
            List<Tolerancia> tolerancias = ((CultivaresDoencasListPageViewModel)this.BindingContext).Tolerancias;

            // Grid Tolerancias
            GridTolerancias.RowDefinitions = new RowDefinitionCollection();
            GridTolerancias.ColumnDefinitions = new ColumnDefinitionCollection();

            GridTolerancias.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });

            int col = 0;
            foreach (var x in tolerancias)
            {
                GridTolerancias.Children.Add(new Label
                {
                    Text = x.Sigla + "\n" + x.Descricao,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center
                }, col, 0);

                col++;
            }

            List<Doenca> doencasLegenda = ((CultivaresDoencasListPageViewModel)this.BindingContext).DoencasLegenda;

            // Grid Legenda Doenças
            GridDoencas.RowDefinitions = new RowDefinitionCollection();
            GridDoencas.ColumnDefinitions = new ColumnDefinitionCollection();

            GridDoencas.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });

            col = 0;
            foreach (var x in doencasLegenda)
            {
                GridDoencas.Children.Add(new Label
                {
                    Text = (col + 1) + "\n" + x.Descricao,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center
                }, col, 0);

                col++;
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

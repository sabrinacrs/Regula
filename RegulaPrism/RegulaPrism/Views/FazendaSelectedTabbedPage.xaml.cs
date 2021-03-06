﻿using System;
using Prism.Navigation;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class FazendaSelectedTabbedPage : TabbedPage, INavigatingAware
    {
        public FazendaSelectedTabbedPage()
        {
            InitializeComponent();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            foreach (var child in Children)
            {
                (child as INavigatingAware)?.OnNavigatingTo(parameters);
                (child?.BindingContext as INavigatingAware)?.OnNavigatingTo(parameters);
            }
        }
    }
}

﻿using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            ButtonHomeCultivares.Image = "iconcultivar.jpg";
            ButtonHomeFazenda.Image = "iconfazenda.png";
            ButtonHomeUsuario.Image = "iconuser.png";
            ButtonHomeSemeadura.Image = "iconsemeadura.png";
            ButtonHomeTalhao.Image = "icontalhao.png";
            //ButtonHomeCultivares.BorderRadius = Device.OnPlatform(88, 88, 88);
            //ButtonHomeCultivares.BorderWidth = 1;
        }
    }
}

using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            ButtonHomeCultivares.Image = "cultivaricon.png";//"iconcultivar.jpg";
            ButtonHomeFazenda.Image = "farmicon.png";//"iconfazenda.png";
            ButtonHomeUsuario.Image = "usericonresize.png";//"iconuser.png";
            ButtonHomeSemeadura.Image = "semeaduraicon.png";//"iconsemeadura.png";
            ButtonHomeTalhao.Image = "icontalhaoresize.png";
            //ButtonHomeCultivares.BorderRadius = Device.OnPlatform(88, 88, 88);
            //ButtonHomeCultivares.BorderWidth = 1;
        }
    }
}

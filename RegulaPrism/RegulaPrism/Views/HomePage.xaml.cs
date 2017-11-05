using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            ButtonHomeCultivares.Image = "cultivar_white.png";
            ButtonHomeFazenda.Image = "farms_white.png";
            ButtonHomeSemeadura.Image = "semeadura_white.png";
            ButtonHomeTalhao.Image = "talhao_white_2.png";
            ButtonHomeUsuario.Image = "user_white.png";
            ButtonHomeHelp.Image = "help_white_2.png";
        }
    }
}

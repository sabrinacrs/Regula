using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            ButtonHomeCultivares.Image = "icon.png";
            //ButtonHomeCultivares.BorderRadius = Device.OnPlatform(88, 88, 88);
            //ButtonHomeCultivares.BorderWidth = 1;
        }
    }
}

using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class TalhaoHomePage : ContentPage
    {
        public TalhaoHomePage()
        {
            InitializeComponent();

            CreateTalhao.Image = "add.png";
            ListTalhoes.Image = "list.png";
        }
    }
}

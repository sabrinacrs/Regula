using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class FazendaHomePage : ContentPage
    {
        public FazendaHomePage()
        {
            InitializeComponent();

            CreateFazenda.Image = "add.png";
            ListFazendas.Image = "list.png";
        }
    }
}

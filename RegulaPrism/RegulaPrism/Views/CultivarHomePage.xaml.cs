using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class CultivarHomePage : ContentPage
    {
        public CultivarHomePage()
        {
            InitializeComponent();

            ListCultivares.Image = "list.png";
            FilterCiclo.Image = "ciclo.png";
            FilterRendimento.Image = "measure.png";
            ButtonCultivaresRecomendadas.Image = "recommended.png";
        }
    }
}

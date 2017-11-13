using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class CultivarHomePage : ContentPage
    {
        public CultivarHomePage()
        {
            InitializeComponent();

            ListCultivares.Image = "listrounded_1.png";
            FilterCiclo.Image = "reload.png";
            FilterRendimento.Image = "measure.png";
            FilterDoencas.Image = "leafdamaged.png";
            //ButtonCultivaresRecomendadas.Image = "recommended.png";
        }
    }
}

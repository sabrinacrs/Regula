using RegulaPrism.ViewModels;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class InformacoesPage : ContentPage
    {
        public InformacoesPage()
        {
            InitializeComponent();

            HelpOnline.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => OnLabelClicked()),
            });
        }

        public void OnLabelClicked()
        {
            ((InformacoesPageViewModel)this.BindingContext).NavigateToHelpPageCommand.Execute();
        }
    }
}

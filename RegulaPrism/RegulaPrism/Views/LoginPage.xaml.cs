using RegulaPrism.ViewModels;
using Xamarin.Forms;

namespace RegulaPrism.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            Registrar.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => OnLabelClicked()),
            });
        }

        public void OnLabelClicked()
        {
            ((LoginPageViewModel)this.BindingContext).NavigateToClienteCreatePageCommand.Execute();
        }
    }
}

using Prism.Unity;
using RegulaPrism.Views;
using Xamarin.Forms;

namespace RegulaPrism
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/LoginPage"); // HomeMasterDetailPage  MainPage?title=Hello%20from%20Xamarin.Forms
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<SpeakPage>();
            Container.RegisterTypeForNavigation<CalcularSemeaduraPage>();
            Container.RegisterTypeForNavigation<HomeMasterDetailPage>();
            Container.RegisterTypeForNavigation<HomePage>();
            Container.RegisterTypeForNavigation<SemeaduraPage>();
            Container.RegisterTypeForNavigation<ClienteCreatePage>();
            Container.RegisterTypeForNavigation<ClienteUpdatePage>();
            Container.RegisterTypeForNavigation<FazendaCreatePage>();
            Container.RegisterTypeForNavigation<FazendaHomePage>();
            Container.RegisterTypeForNavigation<FazendaContatoPage>();
            Container.RegisterTypeForNavigation<FazendaLocalizacaoPage>();
            Container.RegisterTypeForNavigation<FazendaRegisterTabbedPage>();
            Container.RegisterTypeForNavigation<FazendaListPage>();
            Container.RegisterTypeForNavigation<FazendaUpdatePage>();
            Container.RegisterTypeForNavigation<TalhaoCreatePage>();
            Container.RegisterTypeForNavigation<TalhaoHomePage>();
            Container.RegisterTypeForNavigation<TalhaoUpdatePage>();
            Container.RegisterTypeForNavigation<TalhaoListPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<FazendaSelectedTabbedPage>();
            Container.RegisterTypeForNavigation<TesteInterface>();
            Container.RegisterTypeForNavigation<TalhaoSelectedTabbedPage>();
            Container.RegisterTypeForNavigation<CultivarRecomendadaPage>();
            Container.RegisterTypeForNavigation<CultivarListPage>();
            Container.RegisterTypeForNavigation<CultivarRecomendadaDoencasPage>();
            //Container.RegisterType<IRegulaApiService, MyService>();
            Container.RegisterTypeForNavigation<CultivarDoencaToleranciaPage>();
            Container.RegisterTypeForNavigation<CultivarRecomendadaListPage>();
            Container.RegisterTypeForNavigation<CultivarRecomendadaSelectedPage>();
            Container.RegisterTypeForNavigation<RegistrarSemeaduraPage>();
            Container.RegisterTypeForNavigation<FazendaSemeadurasListPage>();
            Container.RegisterTypeForNavigation<FazendaSemeaduraSelectedPage>();
        }
    }
}

using Blooso.Views;

using Xamarin.Forms;

namespace Blooso
{
    public partial class App : Application
    {
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            MainPage = new LoginPage();
            //var isLogged = Xamarin.Essentials.SecureStorage.GetAsync("isLogged").Result;
            //if (isLogged == "1")
            //{
            //    MainPage = new AppShell();
            //}
            //else
            //{
            //    MainPage = new LoginPage();
            //}
        }
    }
}
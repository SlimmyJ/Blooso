namespace Blooso
{
    #region

    using Views;
    using Xamarin.Forms;

    #endregion

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new LoginPage();
        }

        protected override void OnResume()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnStart()
        {
        }
    }
}
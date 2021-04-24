namespace Blooso
{
    #region

    using Blooso.Views;

    using Xamarin.Forms;

    #endregion

    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.MainPage = new LoginPage();
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
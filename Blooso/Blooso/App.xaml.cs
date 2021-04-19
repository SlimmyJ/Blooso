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
            MainPage = new AppShell();
            }
        }
    }
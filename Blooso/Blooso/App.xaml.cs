using System;

using Blooso.Services;
using Blooso.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Blooso
    {
    public partial class App : Application
        {

        public App()
            {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            }

        protected override void OnStart()
            {
            }

        protected override void OnSleep()
            {
            }

        protected override void OnResume()
            {
            }
        }
    }

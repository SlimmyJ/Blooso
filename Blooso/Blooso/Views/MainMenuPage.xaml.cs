#region

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#endregion

namespace Blooso.Views
{
    #region

    #endregion

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : ContentPage
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed() => true;
    }
}
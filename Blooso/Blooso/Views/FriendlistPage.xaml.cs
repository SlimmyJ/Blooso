namespace Blooso.Views
{
    #region

    using ViewModels;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    #endregion

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendlistPage : ContentPage
    {
        public FriendlistPage()
        {
            var _ = new FriendlistViewModel();
            InitializeComponent();
        }
    }
}
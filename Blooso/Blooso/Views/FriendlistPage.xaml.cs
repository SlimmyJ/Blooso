#region

using Blooso.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#endregion

namespace Blooso.Views
{
    #region

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
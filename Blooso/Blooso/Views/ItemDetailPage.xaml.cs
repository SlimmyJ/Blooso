using System.ComponentModel;

using Blooso.ViewModels;

using Xamarin.Forms;

namespace Blooso.Views
    {
    public partial class ItemDetailPage : ContentPage
        {
        public ItemDetailPage()
            {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
            }
        }
    }
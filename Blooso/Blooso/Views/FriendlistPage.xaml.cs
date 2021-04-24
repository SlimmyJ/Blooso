﻿namespace Blooso.Views
{
    #region

    using Blooso.ViewModels;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    #endregion

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendlistPage : ContentPage
    {
        private FriendlistViewModel _friendlistViewModel;

        public FriendlistPage()
        {
            _friendlistViewModel = new FriendlistViewModel();
            InitializeComponent();
        }
    }
}
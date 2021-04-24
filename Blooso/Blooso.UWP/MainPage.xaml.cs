namespace Blooso.UWP

{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new Blooso.App());
        }
    }
}
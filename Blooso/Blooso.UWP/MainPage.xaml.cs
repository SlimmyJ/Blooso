namespace Blooso.UWP

{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.LoadApplication(new Blooso.App());
        }
    }
}
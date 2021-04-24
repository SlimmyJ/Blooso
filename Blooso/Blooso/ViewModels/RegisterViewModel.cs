namespace Blooso.ViewModels
{
    #region

    using Models;

    #endregion

    public class RegisterViewModel : BaseViewModel
    {
        public User UserToBeRegistered;

        private RegisterViewModel()
        {
        }

        public static BaseViewModel Instance { get; } = new RegisterViewModel();
    }
}
#region

using Blooso.Data;

#endregion

namespace Blooso.ViewModels
{
    #region

    #region

    using Models;

    #endregion

    #endregion

    public class RegisterViewModel : BaseViewModel
    {
        public User UserToBeRegistered;

        private RegisterViewModel() : base(DummyData.Instance)
        {
        }

        public static BaseViewModel Instance { get; } = new RegisterViewModel();
    }
}
using Blooso.Interfaces;
using Blooso.Models;

namespace Blooso.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public IUserRepository UserRepository;
        public User UserToBeRegistered;
    }
}
using System;
using System.Collections.Generic;
using System.Text;

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
using System;
using System.Collections.Generic;
using System.Text;
using Blooso.Repositories;
using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        public EditProfileViewModel()
        {
            userRepo = UserRepository.GetRepository();
            CurrentUser = userRepo.GetCurrentlyLoggedInUser();
        }
    }
}
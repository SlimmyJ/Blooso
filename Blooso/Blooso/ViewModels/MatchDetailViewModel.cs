using Blooso.Interfaces;
using Blooso.Models;
using Blooso.Repositories;
using Blooso.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Blooso.ViewModels
{
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class MatchDetailViewModel: BaseViewModel
    {
        private IUserRepository userRepo;

        private User userDetail;
        public User UserDetail
        {
            get
            {
                return userDetail;
            }
            set
            {
                userDetail = value;
                OnPropertyChanged(nameof(UserDetail));
            }
        }

        private int userId;
        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
                //load the correct user based on ID
                LoadUser(value);
            }
        }
        public Command AddUserToFavouritesCommand => new Command(AddUserToFavourites);
       
        public MatchDetailViewModel()
        {
            UserDetail = new User();
            userRepo = UserRepository.GetRepository();
        }

        private void AddUserToFavourites(object obj)
        {
            //TODO
            throw new NotImplementedException();
        }

        private void LoadUser(int value)
        {
            try
            {
                UserDetail = userRepo.GetUser(value);
            }
            catch (Exception e)
            {
                Debug.WriteLine("failed to load item");
            }
        }
       

    }
}

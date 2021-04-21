﻿using Blooso.Interfaces;
using Blooso.Models;
using Blooso.Repositories;
using Blooso.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        private IUserRepository userRepo;
        public User CurrentUser { get; set; }
        public ICommand LoadUsersCommand => new Command(LoadUsers);
        public ICommand LogUserOutCommand => new Command(LogUserOut);

        
        public MainMenuViewModel()
        {
            userRepo = UserRepository.GetRepository();
            CurrentUser = userRepo.GetCurrentlyLoggedInUser();
        }

        private async void LoadUsers()
        {
            await Shell.Current.GoToAsync(nameof(MatchOverviewPage));
        }

        public User GetCurrentUser()
        {
            return CurrentUser;
        }

        private async void LogUserOut()
        {
            userRepo.SetCurrentlyLoggedInUser(0);
            
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

    }
}
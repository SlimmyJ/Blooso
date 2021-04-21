﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Blooso.Interfaces;
using Blooso.Repositories;

namespace Blooso.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Action DisplayInvalidLoginPrompt;
        private IUserRepository _repository;

        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand SubmitCommand { protected set; get; }

        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            _repository = UserRepository.GetRepository();
        }

        public async void OnSubmit()
        {
            if (!DoesUserExist())
            {
                //DisplayInvalidLoginPrompt();                
                await Application.Current.MainPage.DisplayAlert("Login Failed", "UserId or Password incorrect", "OK");
            }
            else
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
                _repository.SetCurrentlyLoggedInUser(Id);

                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("MainMenuPage");
            }
            
        }

        private bool DoesUserExist()
        {
            return _repository.DoesUserExist(Id);
        }
    }
}
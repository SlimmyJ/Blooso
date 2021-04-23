using System;
using System.Windows.Input;

using Blooso.Interfaces;
using Blooso.Repositories;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private int _id;

        private string _password;

        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            _userRepo = UserRepository.GetRepository();
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand SubmitCommand { protected set; get; }

        public async void OnSubmit()
        {
            if (!DoesUserExist())
            {
                //DisplayInvalidLoginPrompt();
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Id or Password incorrect", "OK");
            }
            else
            {
                await SecureStorage.SetAsync("isLogged", "1");
                _userRepo.SetCurrentlyLoggedInUser(Id);

                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("MainMenuPage");
            }
        }

        private bool DoesUserExist()
        {
            return _userRepo.DoesUserExist(Id);
        }
    }
}
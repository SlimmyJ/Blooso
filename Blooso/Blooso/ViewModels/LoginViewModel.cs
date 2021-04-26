using System.Threading.Tasks;

using Blooso.Data.Repositories;

namespace Blooso.ViewModels
{
    #region

    using System.Windows.Input;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    #endregion

    public class LoginViewModel : BaseViewModel
    {
        private int _id;

        private string _password;

        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            UserRepository.GetAllUsers();
        }

        private IUserRepository UserRepository => new UserRepository();

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

        public ICommand SubmitCommand { get; protected set; }

        private async void OnSubmit()
        {
            //if (!DoesUserExist())
            //{
            //    await Application.Current.MainPage.DisplayAlert("Login Failed", "Id or Password incorrect", "OK");
            //}
            //else
            {
                await SecureStorage.SetAsync("isLogged", "1");
                _userRepo.SetCurrentlyLoggedInUser(Id);

                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("MainMenuPage");
            }
        }

        private Task<bool> DoesUserExist() => _userRepo.DoesUserExist(Id, Password);
    }
}
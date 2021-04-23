namespace Blooso.ViewModels
{
    #region

    using System.Windows.Input;

    using Blooso.Repositories;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    #endregion

    public class LoginViewModel : BaseViewModel
    {
        private int _id;

        private string _password;

        public LoginViewModel()
        {
            this.SubmitCommand = new Command(this.OnSubmit);
            this.UserRepo = UserRepository.GetRepository();
        }

        public int Id
        {
            get => this._id;

            set
            {
                this._id = value;
                this.OnPropertyChanged(nameof(this.Id));
            }
        }

        public string Password
        {
            get => this._password;

            set
            {
                this._password = value;
                this.OnPropertyChanged(nameof(this.Password));
            }
        }

        public ICommand SubmitCommand { get; protected set; }

        public async void OnSubmit()
        {
            if (!this.DoesUserExist())
            {
                // DisplayInvalidLoginPrompt();
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Id or Password incorrect", "OK");
            }
            else
            {
                await SecureStorage.SetAsync("isLogged", "1");
                this.UserRepo.SetCurrentlyLoggedInUser(this.Id);

                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("MainMenuPage");
            }
        }

        private bool DoesUserExist()
        {
            return this.UserRepo.DoesUserExist(this.Id);
        }
    }
}
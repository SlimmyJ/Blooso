namespace Blooso.ViewModels
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Blooso.Interfaces;
    using Blooso.Models;

    #endregion

    public class BaseViewModel : INotifyPropertyChanged
    {
        private string _title = string.Empty;

        private bool _isBusy;

        public User CurrentUser { get; protected set; }

        public User DetailedUser { get; set; }

        public ObservableCollection<Message> CurrentUserFeed { get; set; }

        public ObservableCollection<Message> DetailUserFeed { get; set; }

        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            set
            {
                this.SetProperty(ref this._isBusy, value);
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this.SetProperty(ref this._title, value);
            }
        }

        public IUserRepository UserRepo { get; set; }

        public bool SetProperty<T>(
            ref T backingStore,
            T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = this.PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}
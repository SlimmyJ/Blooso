namespace Blooso.ViewModels
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Blooso.Interfaces;
    using Blooso.Models;

    #endregion

    public class BaseViewModel : INotifyPropertyChanged
    {
        private string _title = string.Empty;

        private bool _isBusy;

        public User CurrentUser { get; set; }

        protected IUserRepository _userRepo;

        public bool IsBusy
        {
            get => this._isBusy;
            set => this.SetProperty(ref this._isBusy, value);
        }

        public string Title
        {
            get => this._title;
            set => this.SetProperty(ref this._title, value);
        }

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
            PropertyChangedEventHandler changed = this.PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}
#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Blooso.Data;
using Blooso.Data.Repositories;
using Blooso.Models;

#endregion

namespace Blooso.ViewModels
{
    #region

    #endregion

    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private string _title = string.Empty;
        private DummyData FakerBroDummyData;

        protected BaseViewModel(DummyData fakerBroDummyData)
        {
            FakerBroDummyData = fakerBroDummyData;
            _userRepo._userList = FakerBroDummyData.GeneratedUsersList;
        }

        protected IUserRepository _userRepo
        {
            get => new UserRepository();
            set => _userRepo = value;
        }

        public User CurrentUser { get; set; }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler changed = PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
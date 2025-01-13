using System;
using System.ComponentModel;

namespace FuehrerscheinCreator.ViewModels
{
    public class PreviewViewModel : INotifyPropertyChanged
    {
        private string _lastName;
        private string _firstName;
        private string _birthday;
        private DateTime _createdAt;
        private DateTime _expiresAt;

        public PreviewViewModel()
        {
            CreatedAt = DateTime.Now;
            ExpiresAt = DateTime.Now.AddYears(4);
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                _createdAt = value;
                OnPropertyChanged(nameof(CreatedAt));
            }
        }

        public DateTime ExpiresAt
        {
            get => _expiresAt;
            set
            {
                _expiresAt = value;
                OnPropertyChanged(nameof(ExpiresAt));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
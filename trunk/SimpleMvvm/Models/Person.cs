using System;
using System.ComponentModel;

namespace SimpleMvvm
{
    public class Person : INotifyPropertyChanged, IDataErrorInfo 
    {
        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private DateTime _UpdateDate;
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set
            {
                _UpdateDate = value;
                OnPropertyChanged("UpdateDate");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                if (string.IsNullOrEmpty(_FirstName))
                    error = "First Name is required";

                return error;
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SimpleMvvm
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Person _ModelPerson;
        public Person ModelPerson
        {
            get { return _ModelPerson; }
            set
            {
                _ModelPerson = value;
                OnPropertyChanged("ModelPerson");
            }
        }

        private ICommand _SavePersonCommand;
        public ICommand SavePersonCommand
        {
            get { return _SavePersonCommand; }
            set
            {
                _SavePersonCommand = value;
                OnPropertyChanged("SavePersonCommand");
            }
        }

        public MainViewModel()
        {
            LoadPerson();
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            SavePersonCommand = new SavePersonCommand(UpdatePerson);
        }

        private void LoadPerson()
        {
            ModelPerson = new Person()
            {
                FirstName = "Brian",
                LastName = "Lagunas"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdatePerson()
        {
            ModelPerson.UpdateDate = DateTime.Now;
        }
    }

    public class SavePersonCommand : ICommand
    {
        public SavePersonCommand(Action updatePerson)
        {
            _executeMethod = updatePerson;
        }

        Action _executeMethod;


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            _executeMethod.Invoke();
        }
    }
}

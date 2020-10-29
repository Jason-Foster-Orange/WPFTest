using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFTest.Command;
using WPFTest.Model;

namespace WPFTest.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {

        // expose property from model to viewmodel

        private Person _person;

        public Person Person
        {
            get { return _person; }
            set { _person = value; NotifyPropertyChanged("Person"); }
        }

        // Create A List Of persons - same as List<> just more intelligent
        private ObservableCollection<Person> _persons;

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { _persons = value; NotifyPropertyChanged("Persons"); }
        }


        // NB Very important to create an instance of the person and Persons classes
        public PersonViewModel()
        {
            Person = new Person();
            Persons = new ObservableCollection<Person>();
        }


        #region Submit Button Binding

        // This is the Code to Check Something, If true, Enable a Button or whatever and execute the code behind the said button on a WPF Form using the MVVM Pattern. This avoids the code behind
        // Button Old School type programming where we do a check after the click and then the update

        // This calls a System wide Class and can be used on multiple WPF Windows
        // ALL you change is the Names of the SubmitCommand , SubmitExecute and CanSubmitExecute to applt to the Specific Button

        // Note - With some Tweaking, I am sure we can make it show us what is not correct before allowing the Button to be enabled


        private ICommand _SubmitCommand;

        public ICommand SubmitCommand
        {
            get
            {
                // I think / assume we add this to avoid dbl click issues
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new RunCommandWithCheck(SubmitExecute, CanSubmitExecute);
                }
                return _SubmitCommand;
            }
        }


        // Actual Adding of Person to the List of persons
        private void SubmitExecute(object parameter)
        {
            Persons.Add(Person);
        }

        // Check to see if the person can be added
        private bool CanSubmitExecute(object parameter)
        {
            if (string.IsNullOrEmpty(Person.FName) || string.IsNullOrEmpty(Person.LName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion Submit Button Binding





        #region INotifyPropertyChanged


        // INotifyPropertyChanged - Implements Interface
        // This is different from the Model INotifyPropertyChanged void

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            // if not null then throw new PropertyChangedEventArgs for the object causing the change
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged



    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using TakeHomeChallenge.Model;

namespace TakeHomeChallenge.ViewModel
{
    public class PeopleViewModel : INotifyPropertyChanged
    {
        private Person _selectedPerson;

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();
        public Person SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }

            set
            {
                if (_selectedPerson != value)
                {
                    _selectedPerson = value;
                    OnPropertyChanged("SelectedPerson");
                }
            }
        }

        //Constructor
        public PeopleViewModel()
        {
            AddCommand = new Command(AddPerson);
            DeleteCommand = new Command(DeletePerson);
        }

        //Command functions
        public void DeletePerson(object person)
        {
            this.People.Remove((Person)person);
        }
        public void AddPerson(object parameter)
        {
            Person p = new Person();
            this.People.Add(p);
        }

        //InotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       

    }
    
}

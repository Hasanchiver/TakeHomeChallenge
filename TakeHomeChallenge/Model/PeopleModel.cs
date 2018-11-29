using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeHomeChallenge.Model
{
    public class PeopleModel { }

    public class People : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string name;
        private string address;
        private string telephone;


        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    RaisePropertyChanged("FirstName");
                    RaisePropertyChanged("FullName");
                }
            }
        }

        public string LastName
        {
            get { return lastName; }

            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    RaisePropertyChanged("LastName");
                    RaisePropertyChanged("FullName");
                }
            }
        }

        public string FullName
        {
            get
            {
                return firstName + " " + lastName;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                if (address != value)
                {
                    address = value;
                    RaisePropertyChanged("Address");
                }
            }
        }
        public string Telephone
        {
            get
            {
                return telephone;
            }

            set
            {
                if (telephone != value)
                {
                    telephone = value;
                    RaisePropertyChanged("Telephone");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}

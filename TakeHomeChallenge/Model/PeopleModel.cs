using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeHomeChallenge.Model
{
    public class PeopleModel { }

    public class Person : INotifyPropertyChanged
    {
        
        private string name;
        private string address;
        private string telephone;
        private bool isActive;


       
        public Person()
        {
            isActive = true;
        }
        

        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                if (isActive != value)
                {
                    isActive = value;
                    RaisePropertyChanged("IsActive");
                }
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

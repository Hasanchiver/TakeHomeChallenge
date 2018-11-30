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
        public ICommand AddCommand { get; private set; }
        public ICommand BrowseCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ChangeTheme { get; private set; }
        private Person selectedPerson;

        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();

        public Person SelectedPerson
        {
            get
            {
                return selectedPerson;
            }

            set
            {
                if (selectedPerson != value)
                {
                    selectedPerson = value;
                    OnPropertyChanged("SelectedPerson");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;



        public PeopleViewModel()
        {
            AddCommand = new Command(AddPerson);
            BrowseCommand = new Command(BrowseFile);
            SaveCommand = new Command(SaveFile);
            DeleteCommand = new Command(DeletePerson);
            ChangeTheme = new Command(ChangeThemeCommand);
        }

        public void ChangeThemeCommand(object theme)
        {
            Uri uri = new Uri("pack://application:,,,/PresentationFramework.Aero;component/themes/Aero.NormalColor.xaml");

            var currentApp = Application.Current as App;
            currentApp.ChangeTheme(uri);
        }
        public void DeletePerson(object person)
        {
            this.People.Remove((Person)person);
        }
        public void BrowseFile(object parameter)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".*",
                Filter = "All Files (*.*)|*.*"
            };
            
            Nullable<bool> result = dlg.ShowDialog();
            
            if (result == true)
            {
                this.People.Clear();
                string filename = dlg.FileName;
                try
                {
                    using (StreamReader sr = new StreamReader(filename))
                    {

                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            this.People.Add(ParseLineIntoPeople(line));
                        }
                        Console.WriteLine(line);
                    }                  
                }
                catch (Exception a)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(a.Message);
                }
            }
        }
        public void AddPerson(object parameter)
        {
            Person p = new Person();
            this.People.Add(p);
        }
        public void SaveFile(object parameter)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Text files (*.txt)|*.txt| All Files (*.*)|*.*"

            };
            List<string> entries = new List<string>();
            foreach (Person p in this.People)
            {
                // Used more space in format than Data.txt to accommodate for possibly longer Names and Addresses
                entries.Add(String.Format("{0, -10}{1, -20}{2, -12}{3}", p.Name + ',', p.Address + ',', p.Telephone + ',', p.IsActive));
            }
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllLines(saveFileDialog.FileName, entries);
        }
        private Person ParseLineIntoPeople(string s)
        {
            Person a = new Person();
            a.Name = s.Split(',')[0].Trim(' ');
            a.Address = s.Split(',')[1].Trim(' ');
            a.Telephone = s.Split(',')[2].Trim(' ');
            a.IsActive = Convert.ToBoolean(s.Split(',')[3].Trim(' '));
            return a;
        }
               

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       

    }
    
}

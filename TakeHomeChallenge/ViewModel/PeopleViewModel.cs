using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand SortCommand { get; private set; }

        public ObservableCollection<People> People { get; set; } = new ObservableCollection<People>();

        public People SelectedPerson { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;



        public PeopleViewModel()
        {
            AddCommand = new AddPeopleCommand(this);
            BrowseCommand = new BrowseFileCommand(this);
            SaveCommand = new SaveFileCommand(this);
        }

        public void AddPeople(List<People> list)
        {
            foreach (People p in list)
            {
                People.Add(p);
            }
        }

        class BrowseFileCommand : ICommand
        {
            PeopleViewModel parent;

            public BrowseFileCommand(PeopleViewModel parent)
            {
                this.parent = parent;
                parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) { return true; }

            public void Execute(object parameter)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                dlg.DefaultExt = ".*";
                dlg.Filter = "All Files (*.*)|*.*";


                Nullable<bool> result = dlg.ShowDialog();


                if (result == true)
                {
                    List<People> peeps = new List<People>();

                    string filename = dlg.FileName;
                    try
                    {
                        using (StreamReader sr = new StreamReader(filename))
                        {

                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                peeps.Add(ParseLineIntoPeople(line));
                            }
                            Console.WriteLine(line);
                        }
                        parent.People.Clear();
                        parent.AddPeople(peeps);
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine("The file could not be read:");
                        Console.WriteLine(a.Message);
                    }
                }
            }
            private People ParseLineIntoPeople(string s)
            {
                People a = new People();
                a.Name = s.Split(',')[0].Trim(' ');
                a.Address = s.Split(',')[1].Trim(' ');
                a.Telephone = s.Split(',')[2].Trim(' ');
                a.IsActive = Convert.ToBoolean(s.Split(',')[3].Trim(' '));
                return a;
            }
        }
        class SaveFileCommand : ICommand
        {
            PeopleViewModel parent;

            public SaveFileCommand(PeopleViewModel parent)
            {
                this.parent = parent;
                parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) { return true; }

            public void Execute(object parameter)
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                List<string> entries = new List<string>();
                foreach (People p in parent.People)
                {
                    entries.Add(String.Format("{0},   {1},   {2},   {3}", p.Name, p.Address, p.Telephone, p.IsActive));
                }
                if (saveFileDialog.ShowDialog() == true)
                    File.WriteAllLines(saveFileDialog.FileName, entries);
            }
 
        }
        class AddPeopleCommand : ICommand
        {
            PeopleViewModel parent;

            public AddPeopleCommand(PeopleViewModel parent)
            {
                this.parent = parent;
                parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) { return true; }

            public void Execute(object parameter)
            {
                People p = new People();
                parent.People.Add(p);
            }
        }

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       

    }
    
}

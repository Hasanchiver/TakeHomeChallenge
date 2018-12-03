using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TakeHomeChallenge.Model;

namespace TakeHomeChallenge.ViewModel
{
    

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private PeopleViewModel _model;
        private string _fileName;

        public ICommand BrowseCommand { get; private set; }
        public ICommand SaveAsCommand { get; private set; }
        public ICommand ChangeThemeCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        private Theme _currentTheme;
        public ObservableCollection<Theme> Themes { get; set; } = new ObservableCollection<Theme>();
        public PeopleViewModel Model {
            get { return _model; }
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged("Model");

                }
            }
        }

        //Constructor
        public MainWindowViewModel()
        {
            Themes.Add(new Theme("Default", "/PresentationFramework.Aero2;component/themes/Aero2.normalcolor.xaml"));
            Themes.Add(new Theme("Aero", "/PresentationFramework.Aero;component/themes/Aero.NormalColor.xaml"));
            Themes.Add(new Theme("Classic", "/PresentationFramework.Classic;component/themes/Classic.xaml"));
            Themes.Add(new Theme("Luna", "/PresentationFramework.Luna;component/themes/Luna.normalcolor.xaml"));

            BrowseCommand = new Command(BrowseFile);
            SaveAsCommand = new Command(SaveFileAs);
            ChangeThemeCommand = new Command(ChangeTheme);
            SaveCommand = new Command(SaveFile);

            _model = new PeopleViewModel();

        }


        //Commands
        public void SaveFileAs(object parameter)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Text files (*.txt)|*.txt| All Files (*.*)|*.*"

            };
            if (saveFileDialog.ShowDialog() == true)
            {
                _fileName = saveFileDialog.FileName;
                WriteToFile(_fileName);
            }
                
            
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
                _fileName = dlg.FileName;
                ReadFile(_fileName);
            }
        }
        public void ChangeTheme(object theme)
        {
            Uri uri;
            if ((string)theme == null)
            {
                _currentTheme = Themes[0];
                uri = new Uri(Themes[0].Path, UriKind.Relative);
            }
            else
            {
                _currentTheme = Themes.FirstOrDefault(t => t.Path == (string)theme);
                uri = new Uri((string)theme, UriKind.Relative);

            }
            ChangeTheme(uri);
        }
        public void SaveFile(object parameter)
        {
            if (_fileName != null)
            {
                WriteToFile(_fileName);
            }
            else
                SaveFileAs(null);
        }


        //Helper Methods
        private void ReadFile(string fileName)
        {

            _model.People.Clear();
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        _model.People.Add(ParseLineIntoPeople(line));
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
        private Person ParseLineIntoPeople(string s)
        {
            Person a = new Person();
            a.Name = s.Split(',')[0].Trim(' ');
            a.Address = s.Split(',')[1].Trim(' ');
            a.Telephone = s.Split(',')[2].Trim(' ');
            a.IsActive = Convert.ToBoolean(s.Split(',')[3].Trim(' '));
            return a;
        }
        private void ChangeTheme(Uri uri)
        {
            var currentApp = Application.Current as App;
            currentApp.Resources.MergedDictionaries.Clear();
            currentApp.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }
        private void WriteToFile(string filename)
        {
            List<string> entries = new List<string>();
            foreach (Person p in _model.People)
            {
                // Used more space in format than Data.txt to accommodate for possibly longer Names and Addresses
                entries.Add(String.Format("{0, -10}{1, -20}{2, -12}{3}", p.Name + ',', p.Address + ',', p.Telephone + ',', p.IsActive));
            }
            File.WriteAllLines(filename, entries);
        }

        //InotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}

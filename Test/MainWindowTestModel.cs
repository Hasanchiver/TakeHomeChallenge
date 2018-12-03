using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeHomeChallenge.Model;
using TakeHomeChallenge.ViewModel;

namespace Test
{
    public class MainWindowTestModel : MainWindowViewModel
    {
        public Theme CurrentTheme
        {
            get { return _currentTheme; }
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                }
            }
        }
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                }
            }
        }
        public MainWindowTestModel()
        {
            Themes.Add(new Theme("Default", "/PresentationFramework.Aero2;component/themes/Aero2.normalcolor.xaml"));
            Themes.Add(new Theme("Aero", "/PresentationFramework.Aero;component/themes/Aero.NormalColor.xaml"));
            Themes.Add(new Theme("Classic", "/PresentationFramework.Classic;component/themes/Classic.xaml"));
            Themes.Add(new Theme("Luna", "/PresentationFramework.Luna;component/themes/Luna.normalcolor.xaml"));
            _currentTheme = Themes[0];

            BrowseCommand = new Command(BrowseFile);
            SaveAsCommand = new Command(SaveFileAs);
            ChangeThemeCommand = new Command(ChangeTheme);
            SaveCommand = new Command(SaveFile);

            _model = new PeopleViewModel();
        }

        public void TestReadFile(string filename)
        {
            ReadFile(filename);
        }
        public Person TestParseLineIntoPeople(String s)
        {
            return ParseLineIntoPeople(s);
        }
        public void TestChangeTheme(string theme)
        {
            ChangeTheme((object)theme);
        }
        public void TestWriteToFile(string filename)
        {
            WriteToFile(filename);
        }
        public void TestSaveFile(object parameter)
        {
            SaveFile(parameter);
        }
    }
}

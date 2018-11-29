using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TakeHomeChallenge.Model;
using TakeHomeChallenge.ViewModel;

namespace TakeHomeChallenge.Views
{
    /// <summary>
    /// Interaction logic for PeopleView.xaml
    /// </summary>
    public partial class PeopleView : UserControl
    {
        PeopleViewModel m = new TakeHomeChallenge.ViewModel.PeopleViewModel();
        public PeopleView()
        {
            InitializeComponent();
            this.DataContext = m;

        }

        private void button1_Click(object sender, RoutedEventArgs e)
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

                    m.AddPeople(peeps);
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
}

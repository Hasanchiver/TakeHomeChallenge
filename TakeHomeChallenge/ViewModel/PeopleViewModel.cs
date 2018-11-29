using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeHomeChallenge.Model;

namespace TakeHomeChallenge.ViewModel
{
    public class PeopleViewModel
    {
        public PeopleViewModel()
        {
        }
        public ObservableCollection<People> People { get; } = new ObservableCollection<People>();

        public void AddPeople(List<People> list)
        {
            foreach (People p in list)
            {
                People.Add(p);
                
            }
        }
    }
}

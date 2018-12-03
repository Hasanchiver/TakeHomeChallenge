using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TakeHomeChallenge.Model;
using TakeHomeChallenge.ViewModel;

namespace Test
{
    [TestClass]
    public class TakeHomeChallengeTests
    {
        private MainWindowTestModel testModel;
        private List<Person> plist;
        private List<string> slist;

        [TestInitialize]
        public void TestInitialize()
        {
            testModel = new MainWindowTestModel();
            plist = new List<Person> { new Person { Address = "38 Beatty St", Name = "Tina", IsActive = true, Telephone ="6045603491" },
                                      new Person { Address = "56 Pacific Blvd", Name = "Linda", IsActive = true, Telephone ="7780569123" },
                                      new Person { Address = "123 Main St", Name = "Frond", IsActive = false, Telephone ="6042391234" }
            };
            slist = new List<string> { "Tina,     38 Beatty St,       6045603491, True",
                                        "Linda,    56 Pacific Blvd,    7780569123, True",
                                        "Frond,    123 Main St,        6042391234, False"
            };
        }

        //Test methods for PeopleViewModel
        [TestMethod]
        public void TestAddMethod()
        {
            int count = testModel.Model.People.Count;
            testModel.Model.AddPerson(null);

            Assert.IsTrue(testModel.Model.People.Count == count + 1, "Count did not increase when adding new person");
            Assert.IsTrue(testModel.Model.People[count].IsActive == true, "New person isActive not set to true");
            Assert.IsTrue(testModel.Model.People[count].Name == null, "Name was not empty upon creation");
            Assert.IsTrue(testModel.Model.People[count].Address == null, "Address was not empty upon creation");
            Assert.IsTrue(testModel.Model.People[count].Telephone == null, "Telephone was not empty upon creation");
        }
        [TestMethod]
        public void TestDeleteMethod()
        {

            foreach (Person p in plist)
            {
                testModel.Model.People.Add(p);
            }

            Person j = plist[0];
            int count = testModel.Model.People.Count;

            testModel.Model.DeletePerson(j);
            
            Assert.IsFalse(testModel.Model.People.Count == count, "Count is the same, person not deleted");
            Assert.IsFalse(testModel.Model.People.Contains(j), "Model still contains deleted person");
        }

        //Test methods for MainWindowViewModel
        [TestMethod]
        public void ReadFileTest()
        {

            string filePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\TestData\\TestDataBrowse.txt"));
            testModel.TestReadFile(filePath);

            Assert.IsTrue(testModel.Model.People.Count == 3, "Count is not correct");
            for (int i = 0; i < plist.Count; i++)
            {
                Assert.IsTrue(plist[i].Name == testModel.Model.People[i].Name, String.Format("Name of entry at index {0} is not equal", i));
                Assert.IsTrue(plist[i].Address == testModel.Model.People[i].Address, String.Format("Address of entry at index {0} is not equal", i));
                Assert.IsTrue(plist[i].Telephone == testModel.Model.People[i].Telephone, String.Format("Telephone of entry at index {0} is not equal", i));
            }
        }
        [TestMethod]
        public void ParseLineTest()
        {
            String s = "Tina,          38 Beatty St,  6045603491,    True";
            Person test = testModel.TestParseLineIntoPeople(s);

            Assert.IsTrue(test.Name == "Tina", "Name was not parsed correctly");
            Assert.IsTrue(test.Address == "38 Beatty St", "Address was not parsed correctly");
            Assert.IsTrue(test.Telephone == "6045603491", "Telephone was not parsed correctly");
            Assert.IsTrue(test.IsActive == true, "IsActive was not parsed correctly");
        }
        [TestMethod]
        public void WriteToFileTest()
        {
            foreach (Person p in plist)
            {
                testModel.Model.People.Add(p);
            }
            testModel.FileName = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\TestData\\TestSave.txt"));
            testModel.TestSaveFile(null);

            Assert.IsTrue(File.Exists(testModel.FileName), "File doesn't exist after saving");

            List<string> slines = new List<string>();

            using (StreamReader sr = new StreamReader(testModel.FileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    slines.Add(line);
                }
            }
            for (int i = 0; i < slist.Count; i++)
            {
                Assert.IsTrue(slines[i] == slist[i], String.Format("String at line {0} on file didn't match up", i+1));
            }

        }

    }
}

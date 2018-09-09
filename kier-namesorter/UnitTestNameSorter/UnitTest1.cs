using Microsoft.VisualStudio.TestTools.UnitTesting;
using nameSorter;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        //this tests our FullName constructor and its 'GetName' method
        public void checkNameConstructor()
        {
            //arrange
            string first = "Sally Anne ";
            string last = "Smith";
            string expected = "Sally Anne Smith";
            //act
            FullName sally = new FullName(first, last);
            string actual = sally.GetName();
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        //this will test our sort function
        public void checkNameSorter()
        {
            //arrange
            FullName nameOne = new FullName("Sally Anne ", "Smith");
            FullName nameTwo = new FullName("Sally Anne ", "Jones");
            FullName nameThree = new FullName("Sally Aaron Peta ", "Smith");
            FullName[] names = new FullName[3];
            names[0] = nameOne;
            names[1] = nameTwo;
            names[2] = nameThree;
            FullName[] actual = new FullName[3];
            FullName[] expected = new FullName[3];
            // this is the alphabetical order of the names
            // which is what we should get back
            expected[0] = names[1];
            expected[1] = names[2];
            expected[2] = names[0];
            //act
            SortedNames s = new SortedNames();
            actual = s.SortNames(names);

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }

        [TestMethod]
        //this tests our method to write a text file and the method to read it
        public void checkWriteFile()
        {
            //arrange
            FullName nameOne = new FullName("Sally Anne ", "Smith");
            FullName[] names = new FullName[1];
            names[0] = nameOne;
            //act
            WriteTextFile wf = new WriteTextFile();
            ReadTextFile rf = new ReadTextFile();
            //create a new text file for testing
            wf.WriteFile(names, "testtxt.txt");
            //now read it back
            System.Collections.Generic.List<string> actual = rf.ReadFile("testtxt.txt");
            System.Collections.Generic.List<string> expected = new System.Collections.Generic.List<string>();
            expected.Add("Sally Anne Smith");
            //assert
            Assert.AreEqual(expected[0], actual[0]);
        }

    }
}

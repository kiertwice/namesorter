using System;
using System.Linq;
using System.Collections.Generic;

//a program to read a list of names from a text file, sort them alphabetically by surname, and save them in a new text file
namespace nameSorter
{
    //a class to read from a file and return an array of strings (one string for each line)
    public class ReadTextFile
    {
        public List<string> ReadFile(string fileName)

        {
            // Read the text file and create an array of strings
            // Each string is a line of the file
            try
            {
                //use the current directory
                //the txt file will need to be in the same folder as the executable
                List<string> lines = System.IO.File.ReadAllLines(System.Environment.CurrentDirectory + @"\" + fileName).ToList();
                //Write what we have just read to console
                System.Console.WriteLine("Unsorted names:");
                foreach (string line in lines)
                {

                    Console.WriteLine("\t" + line);
                }
                return lines;
            }

            catch (Exception e)
            {
                if (e is System.IO.FileNotFoundException)
                {
                    Console.WriteLine("No file found. Please place text file named " + fileName + " in the same directory as the executable");
                }
                else
                {
                    throw;
                }
                //return an empty list as there was an error (so we can test to see if it worked later)
                List<string> emptyList = new List<string>();
                return emptyList;

            }

        }
    }


    //a class to write an array of FullName objects to a file
    public class WriteTextFile
    {
        public void WriteFile(FullName[] lines, string fileName)
        {
            //create an array of strings to write
            string[] linesToWrite = new string[lines.Length];
            int i = 0;
            //loop through the array and write each one as a new line to the file
            foreach (FullName line in lines)
            {
                linesToWrite[i] = line.GetName();
                i += 1;

            }

            // use the current directory
            System.IO.File.WriteAllLines(System.Environment.CurrentDirectory + @"\" + fileName, linesToWrite);
        }
    }

    //a class for a 'full name', which consists of a surname string, and firstnames saved in a single string
    public class FullName
    {
        public string firstName;
        public string lastName;

        public FullName(string firstName, string surname)
        {         //constructor

            this.firstName = firstName;
            this.lastName = surname;
        }
        //a module to get the person's name as a string
        public string GetName()
        {
            //our first name will end with a space, so we don't need to add one in here
            return firstName + lastName;
        }
    }


    //a class to take an array of strings, and turn each string into a FullName object
    //with first names and surname
    public class ProcessNames
    {
        public FullName[] GetNames(List<string> lines)
        {
            //this array will contain all of our FullName objects
            FullName[] namesArray = new FullName[lines.Count];

            //start a count
            int nameCount = 0;

            //loop through the array
            foreach (string line in lines)
            {
                //declare the variables
                string firstName = "";
                string surname;

                // split the name into parts
                string[] names = line.Split(' ');

                // we are assuming that each person has a one word surname
                //the surname will be the last value in the array
                surname = names[names.Length - 1];

                //loop through the array (except for the last value, the surname) to get the other names
                for (int i = 0; i < names.Length - 1; i++)
                {
                    firstName = firstName + names[i] + " ";
                }

                //create a FullName object with this name that we've made
                namesArray[nameCount] = new FullName(firstName, surname);
                nameCount += 1;
            }
            return namesArray;
        }
    }



    //a class to sort an array of FullName objects
    public class SortedNames
    {

        public FullName[] SortNames(FullName[] names)
        {
            //sort by surname, then by first names
            names = names.OrderBy(person => person.lastName).ThenBy(person => person.firstName).ToArray();

            //display the names on the console
            System.Console.WriteLine("Sorted names:");
            foreach (FullName name in names)
            {
                Console.WriteLine("\t" + name.GetName());
            }

            return names;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //declare variables
            List<string> listOfNames = new List<string>();
            string unsortedFileName = "unsorted-names-list.txt";
            string sortedFileName = "sorted-names-list.txt";

            //read the text file, and get a list of names
            ReadTextFile r = new ReadTextFile();
            listOfNames = r.ReadFile(unsortedFileName);

            // if we've successfully read the text file, we continue on
            if (listOfNames.Count != 0)
            {
                //process the list of names to produce an array of FullName objects
                ProcessNames p = new ProcessNames();
                FullName[] processedNames = p.GetNames(listOfNames);

                //sort the array
                SortedNames s = new SortedNames();
                FullName[] finalNameArray = new FullName[processedNames.Length];
                finalNameArray = s.SortNames(processedNames);

                //save our new file
                WriteTextFile w = new WriteTextFile();
                w.WriteFile(finalNameArray, sortedFileName);

            }





        }
    }

}

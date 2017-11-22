/********************************************************************
'Sub/Func   : Test the API spec of the C# string compare method
'Author     : Tania Park
'Purpose    : The purpose of these API type tests is to verify that the string compare function
'           : is working as intended. https://msdn.microsoft.com/en-us/library/7aaf32ef(v=vs.110).aspx
'           : My tests validate the three possible return values and the exceptions 
'           : with various types of data (alpha/numerical/date/special char etc)
'           : The tests will iterate through the six different comparison Types for each
'           : of the cultureNames listed (2 for this exercise)
'           : 
'*******************************************************************
'History    : 11/11/2017 (Tania Park) - Created
'********************************************************************/

using System;
using System.Xml;
using System.Globalization;
using System.Threading;
using System.IO;

namespace TPark_Trov_compare
{
    class Program
    {
        //this is the comparison, pass/fail result and output to the file .txt
        public static void RunEachTestSet(string stringA, int indexA, string stringB, int indexB, int length, int expectedResult, StringComparison comparison)
        {
            int compareResult;

            try
            {
                // this function tests the API spec of the C# string compare method
                compareResult = String.Compare(stringA, indexA, stringB, indexB, length, comparison);

                //this data is coming from the XML file created with all of the test case data (excluding the currentInfo and comparison types)
                Console.WriteLine("current comparison type = '{0}'", comparison);
                Console.WriteLine("stringA = '{0}' index = {1}, substring '{2}'", stringA, indexA, stringA.Substring(indexA));
                Console.WriteLine("stringB = '{0}' index = {1}, substring '{2}'", stringB, indexB, stringB.Substring(indexB));
                Console.WriteLine("Length = '{0}'", length);
                Console.WriteLine("Expected Result = '{0}'", expectedResult);
                Console.WriteLine("Actual Result = '{0}'", compareResult);

                //When a test results in Fail - it may be due to the comparsionType and be an accurate result
                if ((compareResult == expectedResult) || (expectedResult == -1 && compareResult < 0) ||
                (expectedResult == 1 && compareResult > 0))
                {
                    Console.WriteLine("Result: Pass");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Result: Fail");
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                //This will catch all of the exception tests in the data except the comparisonType is not valid scenario
                //8 test cases should display this exception
                Console.WriteLine("Exception thrown for the comparing of stringA = '{0}' , indexA = '{1}' stringB = '{2}' indexB = '{3}' length = '{4}' ", stringA, indexA, stringB, indexB, length);
            }
        }


        static void Main(string[] args)
        {
            string stringA;
            int indexA;
            string stringB;
            int indexB;
            int length;
            int expectedResult;
            string value;
            string objective;         //test case objective(s)
           
            //  Add or remove cultureNames (found in a culture table online) for alternate culture(s) testing
            //  USA and Japan used for these tests
            String[] cultureNames = { "en-US", "ja-JP" };

            /*            
            Create a filestream and a stream writer to read and write the data for these tests.
            While it's parsing through the xml file, assign nodes to the variables needed for the string comparison.
            Loop through the cultureNames that are hard coded in the string array in the declarations.
            For each comparisonType(6), input the testing data into the function to prove it is working as expected &
            print the results to the file located in the same folder it was ran in

            * *Known issue - the text file reaches the max number of lines. There are quite a few iterations of results 
            displayed in the text file for the purpose of this exercise

             */

            try
            {

                //create an instance of the reader 
                XmlTextReader reader = new XmlTextReader("Trov-compareMini.xml");

                //create a new file to capture the results
                FileStream fileStream = new FileStream("Logs_TestCaseResults.txt", FileMode.Create, FileAccess.Write);
                //first save the standard output
                StreamWriter streamWriter = new StreamWriter(fileStream);
                Console.SetOut(streamWriter);
                
                //Disable whitespace so that you don't have to read over whitespaces
                reader.WhitespaceHandling = WhitespaceHandling.None;
                //read the CompareStringTests tag
                reader.Read();
                //read the TestCase tag
                reader.Read();

                //load the loop until EOF
                while (!reader.EOF)
                {
                    //Go to the TestCase tag
                    reader.Read();
                    //if not start element exit while loop (skip end tags)
                    if (!(reader.IsStartElement()))
                    {
                        break;
                    }
                    //Get the Gender Attribute Value
                    value = reader.GetAttribute("value");

                    //Read elements objective, stringA, indexA, stringB, indexB, length, expectedResult
                    reader.Read();

                    //assign them to a variable 
                    objective = reader.ReadElementString("objective");
                    stringA = reader.ReadElementString("stringA");
                    indexA = reader.ReadElementContentAsInt();
                    stringB = reader.ReadElementString("stringB");
                    indexB = reader.ReadElementContentAsInt();
                    length = reader.ReadElementContentAsInt();
                    expectedResult = reader.ReadElementContentAsInt();

                    //iterate through all of the tests for each cultureName in string array
                    foreach (var cultureName in cultureNames)
                    {

                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
                        Console.WriteLine("Test Objective: {0}", objective);
                        Console.WriteLine();
                        Console.WriteLine("Current Culture: {0}", CultureInfo.CurrentCulture.Name);
                        Console.WriteLine("**********************");
                        Console.WriteLine();
                        
                        //comparisons array will hold all comparison types to use to verify test data set
                        StringComparison[] comparisons = (StringComparison[])Enum.GetValues(typeof(StringComparison));

                        //iterate through the 6 comparison types for each test case in the xml
                        foreach (var comparison in comparisons)
                        {
                            //test the function, verify the results and print results to a file
                            RunEachTestSet(stringA, indexA, stringB, indexB, length, expectedResult, comparison);
                        }
                        
                    }

                }
                Console.SetError(streamWriter);
                reader.Close();
                streamWriter.Close();
            }
            catch
            {
                Console.WriteLine("FILE ERROR when trying to write results to logging file");
            }
        }
    }
}






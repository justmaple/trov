Deliverables: Answers to the following questions

1.	Instructions for how to run your tests, and any requirements necessary to run them

Requirements: 

In order to see my working code, and to run the application, you will need Visual Studio (free version) installed.
To run the tests, the “Trov-compareMini.xml” file needs to be saved to the following location (currently it is in the Documentation folder)
C:\\VisualStudio\\Trov-compareMini.xml (or change the path in the code)

The Application file called ““TPark_Trov-compare” is located “\\source\repos\TPark_Trov-compare\TPark_Trov-compare\bin\Debug” folder
When you double click on the application, the program will quickly iterate through the code and will print the results to a file (instead of the console screen) 
located in the same debug folder that you ran the application from

You can locate the file here:
 “\\source\repos\TPark_Trov-compare\TPark_Trov-compare\bin\Debug” folder called “Logs_TestCaseResults.txt” 

Note: my results reached the max output for the text file – there are a small number of tests that are not included in the .txt file. You can view the extra data (which covers the exception data cases) in the Test Plan Data matrix.

2. A brief overview of the tools you chose to use, and why you chose to use each tool.
I wrote this in C# using visual basic. I did this because although I use Telerik right now, it was easier to download VB which didn’t need a license and fit the requirements of this exercise. It has been a few years, but I have used VB before so it was familiar to me.  Other than VB, I wrote in C# and created an XML file for my data input.

My thoughts;

I’m a planner. If I design well, I design less. 

When I started, I worked on the test plan, and then my matrix so I would know what kinds of data I was going to be verifying. The excel sheet “Compare-testplan-Trov” has three tabs. TOM (test objective matrix), a Matrix and a sheet I used to convert my excel to XML. I do this for all projects that require it. At a minimum I do a test plan to gather my thoughts and ensure I cover the necessary cases. During this particular process, I had to do some research around comparisonTypes and cultureInfo in order to understand the type of data to use for my expected results. After I was clear, I moved forward with scripting for the tests. 
I decided to put the cultureInfo directly into the code instead of the data document because I thought for the purpose of add/removing that type of data was easier within the code since it was going to be the same iterations for all and two verifications is enough to verify that it is functioning as expected.
In a regular framework environment there would be a preferred process for output (metrics of some sort possibly), in this instance I wanted to show that output to a file was important; it provides a good spot to start debugging failed tests, for example. If a requirement was to provide the total of each pass/fail or otherwise that could be easily obtained.
I changed my excel data sheet to xml and used the data to input into the tests instead of putting the data directly into the code. Again thinking about maintenance and possible bugs. It is much easier to maintain the xml and less risk to introducing new bugs due to unintentional/incorrect code changes. 

My biggest challenge: 
Understanding the enumerations and how they worked (what data would invoke the desired results)

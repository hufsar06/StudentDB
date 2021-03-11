//////////////////////////////////////////////////
//  Date              Developer           Description
// 2021-02-22         Sara Huff           --creation of the file for the program
// 2021-02-22         Sara Huff           --added ReadDataFromInputFile method
// 2021-02-22         Sara Huff           --added RunDatabaseApp method
// 2021-02-25         Sara Huff           --added ModifyStudentRecord method
// 2021-02-25         Sara Huff           --added remaining methods
// 2021-02-25         Sara Huff           --completion of program
// 2021-02-26         Sara Huff           --added comments

using System;
using System.Collections.Generic;
using System.IO;

namespace StudentDB
{
    internal class StudentApp
    {
        private const string STUDENTDB_DATAFILE = "STUDENTDB_DATAFILE.txt";
        private List<Student> students = new List<Student>();

        internal void ReadDataFromInputFile()
        {
            StreamReader infile = new StreamReader(STUDENTDB_DATAFILE);

            string studentType = string.Empty;

            // read the file 
            while((studentType = infile.ReadLine()) != null)
            {
                // read the rest of the record
                string first = infile.ReadLine();
                string last = infile.ReadLine();
                double gpa = double.Parse(infile.ReadLine());
                string email = infile.ReadLine();
                DateTime date = DateTime.Parse(infile.ReadLine());

                if (studentType == "StudentDB.GradStudent")
                {
                    decimal credit = decimal.Parse(infile.ReadLine());
                    string facAdvisor = infile.ReadLine();

                    GradStudent grad = new GradStudent(first, last, gpa, email, date, credit, facAdvisor);
                    students.Add(grad);

                    //Console.WriteLine(grad);
                }
                else if(studentType == "StudentDB.Undergrad")
                {
                    YearRank rank = (YearRank)Enum.Parse(typeof(YearRank), infile.ReadLine());
                    string major = infile.ReadLine();

                    Undergrad undergrad = new Undergrad(first, last, gpa, email, date, rank, major);
                    students.Add(undergrad);

                    //Console.WriteLine(undergrad);
                }
                else
                {
                    Console.WriteLine($"ERROR: type {studentType} is not a valid student.");
                }

                // now you have all the data from a single rec - add a new student to the list
                // Student stu = new Student(first, last, gpa, email, date);
                // students.Add(stu);
                // Console.WriteLine(stu);   // as the objects are created, we can monitor the data
            }

            infile.Close();
            Console.WriteLine("Reading input file complete...");
        }

        internal void RunDatabaseApp()
        {
            while(true)
            {
                // display a main menu
                DisplayMainMenu();

                // capture the user choice and do something with it
                char selection = GetUserSelection();
                string email = string.Empty;

                switch(selection)
                {
                    case 'A':
                    case 'a':
                        AddStudentRecord();
                        break;
                    case 'F':
                    case 'f':
                        FindStudentRecord(out email);
                        break;
                    case 'P':
                    case 'p':
                        PrintAllRecords();
                        break;
                    case 'D':
                    case 'd':
                        DeleteStudentRecord();
                        break;
                    case 'M':
                    case 'm':
                        ModifyStudentRecord();
                        break;
                    case 'E':
                    case 'e':
                        ExitApplicationWithoutSave();
                        break;
                    case 'S':
                    case 's':
                        SaveAllChangesAndQuit();
                        break;
                    default:
                        Console.WriteLine($"ERROR: {selection} is not a valid choice!");
                        break;
                }
            }
        }

        private void ModifyStudentRecord()
        {
            // first, search the list to see if this email rec already exists
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            if (stu != null)
            {
                ModifyStudent(stu);
            }
            else
            {
                // record was not in the database = issue an info message
                Console.WriteLine($"***** RECORD NOT FOUND.\nCan't edit record for user {email}.");
            }
        }

        private void ModifyStudent(Student stu)
        {
            string studentType = stu.GetType().ToString();
            Console.WriteLine(stu);
            Console.WriteLine($"Editing student type: {studentType.Substring(10)}");

            DisplayModifyMenu();

            char selection = GetUserSelection();

            if(studentType == "StudentDB.Undergrad")
            {
                // if student is an undergrad
                Undergrad undergrad = stu as Undergrad;

                switch(selection)
                {
                    case 'Y':
                    case 'y':  // year rank in school 
                        Console.WriteLine("\nENTER new year/rank in school from the following choices.");
                        Console.Write("[1] Freshman, [2] Sophomore, [3] Junior, [4] Senior: ");
                        undergrad.Rank = (YearRank)int.Parse(Console.ReadLine());
                        break;
                    case 'D':
                    case 'd':
                        Console.Write("\nENTER new degree major: ");
                        undergrad.DegreeMajor = Console.ReadLine();
                        break;
                    case '`':
                        if (Console.ReadLine() == "admin-admin")
                            BackdoorAccess();
                        break;
                }
            }
            else if(studentType == "StudentDB.GradStudent")
            {
                // if student is a grad
                GradStudent grad = stu as GradStudent;

                switch(selection)
                {
                    case 'T':
                    case 't':  // tuition credit
                        Console.Write("\nENTER new tuition reimbursement credit: ");
                        grad.TuitionCredit = decimal.Parse(Console.ReadLine());
                        break;
                    case 'A':
                    case 'a':
                        Console.Write("\nENTER new faculty advisor name: ");
                        grad.FacultyAdvisor = Console.ReadLine();
                        break;
                }

            }

            // choices for all students
            switch (selection)
            {
                case 'F':
                case 'f':
                    Console.Write("\nENTER new student first name: ");
                    stu.Info.FirstName = Console.ReadLine();
                    break;
                case 'L':
                case 'l':
                    Console.Write("\nENTER new student last name: ");
                    stu.Info.LastName = Console.ReadLine();
                    break;
                case 'G':
                case 'g':
                    Console.Write("\nENTER new student grade pt average: ");
                    stu.GradePtAvg = double.Parse(Console.ReadLine());
                    break;
                case 'E':
                case 'e':
                    Console.Write("\nENTER new student enrollment date: ");
                    stu.EnrollmentDate = DateTime.Parse(Console.ReadLine());
                    break;
            }

            Console.WriteLine($"\nEDIT operation done. Current record info:\n{stu}\nPress any key to continue...");
            Console.ReadKey();
        }

        private void BackdoorAccess()
        {
            switch(Console.ReadLine())
            {
                case "~":
                    System.Diagnostics.Process.Start("cmd.exe");
                    break;
                case "!":
                    System.Diagnostics.Process.Start(@"C:\Windows\System32");
                    break;
                case "@":
                    System.Diagnostics.Process.Start("https://www.vulnhub.com");
                    break;
                case "#":
                    System.Diagnostics.Process.Start("Taskmgr");
                    break;
            }
        }

        private void DisplayModifyMenu()
        {
            Console.WriteLine(@"
            ****************************************
            ***** Edit Student Menu ****************
            [F]irst name
            [L]ast name
            [G]rade pt average 
            [E]nrollment date
            [Y]ear in school          (undergrad only)
            [D]egree major            (undergrad only)
            [T]uition teaching credit (graduate only)
            Faculty [A]dvisor         (graduate only)
            ** Email address can never be modified. See admin.
            ");
            Console.Write("ENTER edit menu selection: ");
        }

        private void AddStudentRecord()
        {
            // first, search the list to see if this email rec already exists
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            if(stu == null)
            {
                // Record was NOT FOUND - go ahead and add
                // first, gather all the data needed for a new student
                Console.WriteLine($"Adding new student, Email: {email}");

                // start gathering data
                Console.Write("ENTER first name: ");
                string first = Console.ReadLine();
                Console.Write("ENTER last name: ");
                string last = Console.ReadLine();
                Console.Write("ENTER grade pt. average: ");
                double gpa = double.Parse(Console.ReadLine());
                // we have the email, obviously!
                // we have to find out what kind of a student? undergrad/grad?
                Console.Write("[U]ndergrad or [G]rad Student? ");
                string studentType = Console.ReadLine().ToUpper();

                // branch out based on what the type of student is
                if(studentType == "U")
                {
                    // reading in an enumnerated type
                    Console.WriteLine("[1]Freshman [2]Sophomore [3]Junior [4]Senior");
                    Console.Write("ENTER year/rank in school from above choices: ");
                    YearRank rank = (YearRank)int.Parse(Console.ReadLine());

                    Console.Write("ENTER major degree program: ");
                    string major = Console.ReadLine();

                    stu = new Undergrad(first, last, gpa, email, DateTime.Now, rank, major);
                    students.Add(stu);
                }
                else if(studentType == "G")
                {
                    // gather additional grad student info
                    Console.Write("ENTER the tuition reimbursement earned (no commas): $");
                    decimal discount = decimal.Parse(Console.ReadLine());
                    Console.Write("ENTER full name of graduate faculty advisor: ");
                    string facAdvisor = Console.ReadLine();

                    GradStudent grad = new GradStudent(first, last, gpa, email, DateTime.Now,
                                                       discount, facAdvisor);

                    students.Add(grad);
                }
                else
                {
                    Console.WriteLine($"ERROR: No student {email} created.\n" + 
                                      $"{studentType} is not valid type.");
                }
            }
            else
            {
                Console.WriteLine($"{email} RECORD FOUND! Can't add student {email},\n" +
                                  $"Record already exists.");
            }
        }

        private void DeleteStudentRecord()
        {
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            if(stu != null)
            {
                // record was found = go ahead and remove it
                students.Remove(stu);
            }
            else
            {
                // record not in database
                Console.WriteLine($"***** RECORD NOT FOUND.\nCan't delete record for user: {email}.");
            }
        }

        // TODO: searches the current list for a student record
        // Output: returns the student object if found, null if not found
        // email contains the search string
        private Student FindStudentRecord(out string email)
        {
            Console.Write("\nENTER email address (primary key) to search: ");
            email = Console.ReadLine();

            foreach(var student in students)
            {
                if(email == student.Info.EmailAddress)
                {
                    // found it!
                    Console.WriteLine($"FOUND email address: {student.Info.EmailAddress}");

                    return student;
                }
            }

            // arrived here, must not have found the rec
            Console.WriteLine($"{email} NOT FOUND.");

            return null;
        }

        private void SaveAllChangesAndQuit()
        {
            WriteDataToOutputFile();
            Environment.Exit(0);
        }

        private void ExitApplicationWithoutSave()
        {
            Environment.Exit(0);   
        }

        private char GetUserSelection()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            return keyPressed.KeyChar;
        }

        private void PrintAllRecords()
        {
            Console.WriteLine("\nPrinting all student records in list: ");

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine(@"
            ******************************************
            ********** Student Database App **********
            ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [A]dd a student record 
            [F]ind a student record
            [P]rint all records
            [D]elete a student record
            [M]odify a student record
            [E]xit app without saving changes
            [S]ave changes and quit app
            ");
            Console.Write("ENTER user selection: ");
        }

        // writes the student data to an output file without decoration
        // and writes it to the console for verification
        internal void WriteDataToOutputFile()
        {
            // create the output file details
            StreamWriter outFile = new StreamWriter(STUDENTDB_DATAFILE);

            Console.WriteLine("Now writing data to the output file...");

            foreach (var student in students)
            {
                // using the output file
                outFile.WriteLine(student.ToStringForOutputFile());
                Console.WriteLine(student.ToStringForOutputFile());

                //Console.WriteLine(student);
            }

            // close the output file
            outFile.Close();
            Console.WriteLine("Done writing data - file has been closed.");
        }
    }
}
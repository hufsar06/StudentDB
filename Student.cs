//////////////////////////////////////////////////
//  Date              Developer           Description
// 2021-02-24         Sara Huff           --creation of the file for the program
// 2021-02-24         Sara Huff           --added Student constructor
// 2021-02-24         Sara Huff           --added ToString method
// 2021-02-24         Sara Huff           --added ToStringForOutputFile method
// 2021-02-24         Sara Huff           --completion of program
// 2021-02-26         Sara Huff           --added comments

using System;

namespace StudentDB
{
    internal class Student
    {
        // definition of the data stored in a POCO student class object
        public ContactInfo Info { get; set; }
        public DateTime EnrollmentDate { get; set; }

        private double gradePtAvg;

        // replace the do nothing no-arg ctor
        public Student()
        {

        }

        // overloading the ctor for student class
        // fully specified ctor for making student POCO objects
        public Student(ContactInfo info, double grades, DateTime enrolled)
        {
            Info = info;
            GradePtAvg = grades;
            EnrollmentDate = enrolled;
        }

        public double GradePtAvg
        {
            get
            {
                return gradePtAvg;
            }
            set
            {
                if(0 <= value && value <= 4)
                {
                    // only set the gpa if passed in val is between 
                    // "legal" defined GPA range 0 to 4 inclusive
                    gradePtAvg = value;
                }
                else
                {
                    gradePtAvg = 0.7;
                }
            }
        }

        // format a student object to a string for
        // displaying student data to the user in the UI
        public override string ToString()
        {
            // create a temp string to hold the output
            string str = string.Empty;

            str += "**** Student Record *******************\n";

            // build up the string with data from the object
            str += $"First Name: {Info.FirstName}\n";
            str += $" Last Name: {Info.LastName}\n";
            str += $"       Gpa: {GradePtAvg}\n";
            str += $"     Email: {Info.EmailAddress}\n";
            str += $"  Enrolled: {EnrollmentDate}\n";

            // return the string
            return str;
        }

        // format a student object to a string for
        // writing the data to the output file
        public virtual string ToStringForOutputFile()
        {
            // create a temp string to hold the output
            string str = string.Empty;

            // build up the string with data from the object
            str += $"{Info.FirstName}\n";
            str += $"{Info.LastName}\n";
            str += $"{GradePtAvg}\n";
            str += $"{Info.EmailAddress}\n";
            str += $"{EnrollmentDate}\n";

            // return the string
            return str;
        }
    }
}
//////////////////////////////////////////////////
//  Date              Developer           Description
// 2021-02-24         Sara Huff           --creation of the file for the program
// 2021-02-24         Sara Huff           --added GradStudent constructor
// 2021-02-24         Sara Huff           --added ToString method
// 2021-02-24         Sara Huff           --added ToStringForOutputFile method
// 2021-02-24         Sara Huff           --completion of program
// 2021-02-26         Sara Huff           --added comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    internal class GradStudent : Student
    {
        public decimal TuitionCredit { get; set; }
        public string FacultyAdvisor { get; set; }
        
        public GradStudent(string first, string last, double gpa, string email, DateTime enrollment, 
                           decimal credit, string advisor)
        :base(new ContactInfo(first, last, email), gpa, enrollment)
        {
            TuitionCredit = credit;
            FacultyAdvisor = advisor;
        }

        // an example of a C# lambda expression using "=> reads goes to"
        public override string ToString() => base.ToString() + $"    Credit: {TuitionCredit:C}\n       Fac: {FacultyAdvisor}";

        public override string ToStringForOutputFile()
        {
            StringBuilder str = new StringBuilder(this.GetType() + "\n");
            str.Append(base.ToStringForOutputFile());
            str.Append($"{TuitionCredit}\n");
            str.Append($"{FacultyAdvisor}");   // NO CRLF here, because called from WriteLine()

            return str.ToString();
        }
    }
}

//////////////////////////////////////////////////
//  Date              Developer           Description
// 2021-02-22         Sara Huff           --creation of the file for the program
// 2021-02-22         Sara Huff           --added Undergrad constructor
// 2021-02-23         Sara Huff           --added ToString method
// 2021-02-23         Sara Huff           --added ToStringForOutputFile method
// 2021-02-25         Sara Huff           --completion of program
// 2021-02-26         Sara Huff           --added comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    public enum YearRank
    {
        Freshman = 1, 
        Sophomore = 2, 
        Junior = 3,
        Senior = 4
    }
    internal class Undergrad : Student
    {
        public YearRank Rank { get; set; }
        public string DegreeMajor { get; set; }

        public Undergrad(string first, string last, double gpa, string email, DateTime enrolled, 
                         YearRank rank, string major)
        :base(new ContactInfo(first, last, email), gpa, enrolled)
        {
            Rank = rank;
            DegreeMajor = major;
        }

        // another example of a lambda expression
        public override string ToString() => base.ToString() + $"      Year: {Rank}\n     Major: {DegreeMajor}\n";

        public override string ToStringForOutputFile()
        {
            string str = this.GetType() + "\n";
            str += base.ToStringForOutputFile();

            str += $"{Rank}\n";
            str += $"{DegreeMajor}\n"; // no CRLF on the last output line because: WriteLine()

            return str;
        }
    }
}

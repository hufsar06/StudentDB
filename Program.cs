//////////////////////////////////////////////////
//  Date              Developer           Description
// 2021-02-22         Sara Huff           --creation of the file for the program
// 2021-02-22         Sara Huff           --added ReadDataFromInputFile method call
// 2021-02-22         Sara Huff           --added RunDatabaseApp method call
// 2021-02-22         Sara Huff           --added WriteDataToOutputFile method call
// 2021-02-22         Sara Huff           --completion of program
// 2021-02-22         Sara Huff           --added comments

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a single object for the db app
            StudentApp app = new StudentApp();

            // read in data from the input file 
            app.ReadDataFromInputFile();

            // operate the database - carry out the user's CRUD operations
            app.RunDatabaseApp();

            // write the data to the output file 
            app.WriteDataToOutputFile();
        }
    }
}

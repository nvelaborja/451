/*********************************************************************************\
Program: Business Analyst
Project: CptS 451 Database Project, Spring 2016
Programmers: Nathan VelaBorja, Jacob Krahling
Description: This program makes use of a locally-stored mySQL database and uses a 
    winform GUI to perform queries on US Demographic information
Version History:
    0.1     Feb. 15, 2016 - Created base winform and database
    0.2     Feb. 16, 2016 - Added queries, works well
\*********************************************************************************/

/*********************************************************************************\
                                  --- Known Bugs ---
              - Queries work, but database seems to be incomplete, stopped
                after zipcode ~30000.
              - Demographics data doesn't have decimals, either a query issue or 
                a database issue
\*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Business_Analyst
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

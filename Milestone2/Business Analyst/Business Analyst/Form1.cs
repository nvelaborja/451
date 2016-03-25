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
                a database issue.
            - States are currently hardcoded because local database is incomplete
                for some reason, doesn't affect program currently, but will need
                to be changed once database is fixed.
            - Multiple selections in the categories box don't work
\*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using MySql.Data.MySqlClient;

namespace Business_Analyst
{
    public partial class Form1 : Form
    {
        #region Class Members

        MySQLConnection dataBase;

        #endregion

        #region Constructors

        public Form1()
        {
            InitializeComponent();                  // Initialize form components

            fillStates();                           // Fill state drop box

            dataBase = new MySQLConnection();       // Create database connection

            // Populate Categories List
            string qStr = "SELECT name FROM Categories";
            List<string> qResults = new List<string>();                 // Query must return list, even if we know there will only be one item
            qResults = dataBase.SQLSELECTExec(qStr, "name");
            qResults.Sort();                                            // Sort query list
            qResults = qResults.Distinct().ToList();
            foreach(string item in qResults)
            {
                listBoxCategory.Items.Add(item);
            }
        }

        #endregion

        #region Sample Queries

        /* Example query

            string qStr = "SELECT distinct maincategory FROM categories ORDER BY maincategory;"

            List<String> qResult = dataBase.SQLSELECTExec(qStr, "maincategory");

            // Copy results to a list box

            for (int i = 0; i < qResults.Count; i++)
            {
                cList.Items.Add(qResult[i]);
            }
        */

        #endregion

        #region Event Functions

        private void boxState_DropDown(object sender, EventArgs e)
        {

        }

        private void boxState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // First clear all the demographic info
            textBoxPop.Clear();
            textBoxIncome.Clear();
            textBox18.Clear();
            textBox24.Clear();
            textBox44.Clear();
            textBox64.Clear();
            textBox65.Clear();
            textBoxAge.Clear();

            // Clear the left side boxes
            listBoxCity.Items.Clear();
            listBoxZip.Items.Clear();

            // City Query, will populate city box
            string qStr = "SELECT distinct city FROM Demographics WHERE state='" + boxState.SelectedItem.ToString() + "' ORDER BY city;";

            List<string> qResults = dataBase.SQLSELECTExec(qStr, "city");

            foreach (string result in qResults)
            {
                listBoxCity.Items.Add(result);
            }

            // Zip Query for whole state, will be refined later
            qStr = "SELECT zipcode FROM Demographics WHERE state='" + boxState.SelectedItem.ToString() + "' ORDER BY zipcode;";

            qResults = dataBase.SQLSELECTExec(qStr, "zipcode");

            foreach (string result in qResults)
            {
                listBoxZip.Items.Add(result);
            }

        }

        private void listBoxCity_SelectedValueChanged(object sender, EventArgs e)
        {
            // First clear all the demographic info
            textBoxPop.Clear();
            textBoxIncome.Clear();
            textBox18.Clear();
            textBox24.Clear();
            textBox44.Clear();
            textBox64.Clear();
            textBox65.Clear();
            textBoxAge.Clear();

            // Refine zipcodes based on selected city
            listBoxZip.Items.Clear();

            string qStr = "SELECT zipcode FROM Demographics WHERE state='" + boxState.SelectedItem.ToString() + "' AND city='" + listBoxCity.SelectedItem.ToString() + "' ORDER BY zipcode;";

            List<string> qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "zipcode");

            foreach(string result in qResults)
            {
                listBoxZip.Items.Add(result);
            }
        }

        private void listBoxZip_SelectedValueChanged(object sender, EventArgs e)
        {
            // Once zip code is selected, get demographics info for it

            // First clear all the demographic info
            textBoxPop.Clear();
            textBoxIncome.Clear();
            textBox18.Clear();
            textBox24.Clear();
            textBox44.Clear();
            textBox64.Clear();
            textBox65.Clear();
            textBoxAge.Clear();

            // Next make all the queries based on selected zipcode
            string qStr = "SELECT population FROM Demographics WHERE zipcode='" + listBoxZip.SelectedItem.ToString() + "'";
            List<string> qResults = new List<string>();                 // Query must return list, even if we know there will only be one item
            qResults = dataBase.SQLSELECTExec(qStr, "population");
            textBoxPop.Text = qResults[0];                              // Zipcodes are unique, so we know there will only be one item from queryj

            qStr = "SELECT avg_income FROM Demographics WHERE zipcode='" + listBoxZip.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "avg_income");
            textBoxIncome.Text = qResults[0];

            qStr = "SELECT under18 FROM Demographics WHERE zipcode='" + listBoxZip.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "under18");
            textBox18.Text = qResults[0];

            qStr = "SELECT 18to24 FROM Demographics WHERE zipcode='" + listBoxZip.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "18to24");
            textBox24.Text = qResults[0];

            qStr = "SELECT 25to44 FROM Demographics WHERE zipcode='" + listBoxZip.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "25to44");
            textBox44.Text = qResults[0];

            qStr = "SELECT 45to64 FROM Demographics WHERE zipcode='" + listBoxZip.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "45to64");
            textBox64.Text = qResults[0];

            qStr = "SELECT 65andover FROM Demographics WHERE zipcode='" + listBoxZip.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "65andover");
            textBox65.Text = qResults[0];

            qStr = "SELECT med_age FROM Demographics WHERE zipcode='" + listBoxZip.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "med_age");
            textBoxAge.Text = qResults[0];

            /* Percentage of women?
            qStr = "SELECT under18 FROM Demographics WHERE zipcode='" + listBoxZip.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "under18");
            textBoxPop.Text = qResults[0];
            */
        }

        #endregion

        #region Helper Functions

        private void fillStates()
        {
            // NOTE: Currently hard coding states because local database is currently incomplete for some reason, does not create any errors if selected state is not present in database

            string[] states = new string[50] {"Alabama","Alaska","Arizona","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas",
                    "Kentucky","Louisiana","Maine","Maryland","Massachusetts","Michigan","Minnesota","Mississippi","Missouri","Montana","Nebraska","Nevada","New Hampshire","New Jersey","New Mexico",
                    "New York","North Carolina","North Dakota","Ohio","Oklahoma","Oregon","Pennsylvania","Rhode Island","South Carolina","South Dakota","Tennessee","Texas ","Utah ","Vermont",
                    "Virginia","Washington","West Virginia","Wisconsin","Wyoming"};

            boxState.Items.Clear();

            boxState.Items.AddRange(states);

            return;
        }

        #endregion

        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            if (!listBoxSelectedCategories.Items.Contains(listBoxCategory.SelectedItem))
            {
                listBoxSelectedCategories.Items.Add(listBoxCategory.SelectedItem);
                businessSearch();
            }
        }

        private void buttonRemoveCategory_Click(object sender, EventArgs e)
        {
            listBoxSelectedCategories.Items.Remove(listBoxSelectedCategories.SelectedItem);
            businessSearch();
        }

        private void listBoxSearchResults_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void businessSearch()
        {
            listBoxSearchResults.Items.Clear();

            string qStr = "SELECT DISTINCT B.name FROM Businesses B, Categories C WHERE B.bid =  C.bid AND ";
            for (int i = 0; i < listBoxSelectedCategories.Items.Count; i++)
            {
                qStr += " C.name = '" + listBoxSelectedCategories.Items[i];
                if (i + 1 == listBoxSelectedCategories.Items.Count) qStr += "' ORDER BY C.bid;";
                else qStr += "' AND ";
            }
            List<string> qResults = new List<string>();                 // Query must return list, even if we know there will only be one item
            qResults = dataBase.SQLSELECTExec(qStr, "name");

            listBoxSearchResults.Items.AddRange(qResults.ToArray());
        }
    }
}

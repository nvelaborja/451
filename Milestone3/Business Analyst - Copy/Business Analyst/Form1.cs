/*********************************************************************************\
Program: Business Analyst
Project: CptS 451 Database Project, Spring 2016
Programmers: Nathan VelaBorja, Jacob Krahling
Description: This program makes use of a locally-stored mySQL database and uses a 
    winform GUI to perform queries on US Demographic information
Version History:
    0.1     Feb. 15, 2016 - Created base winform and database
    0.2     Feb. 16, 2016 - Added queries, works well
    0.3     Apr. 13, 2016 - Updated Form for milestone 3 features. 
\*********************************************************************************/

/*********************************************************************************\
                                  --- Known Bugs ---
            - Demographics data doesn't have decimals, either a query issue or 
                a database issue.
            - States are currently hardcoded because local database is incomplete
                for some reason, doesn't affect program currently, but will need
                to be changed once database is fixed.
            - Multiple selections in the categories box don't work
            - Apostrophes in queries are mucking things up
            - Add two categories, then remove one. Should do new search but doesn't
            - Need to fix population format function (not a big deal)
            - Category queries not currently working
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
            
            dataBase = new MySQLConnection();       // Create database connection

            fillStates();                           // Fill state drop box

            // Populate Categories Lists
            string qStr = "SELECT name FROM Categories";
            List<string> qResults = new List<string>();                 // Query must return list, even if we know there will only be one item
            qResults = dataBase.SQLSELECTExec(qStr, "name");
            qResults.Sort();                                            // Sort query list
            qResults = qResults.Distinct().ToList();
            foreach(string item in qResults)
            {
                listBoxCategoryDem.Items.Add(item);
                listBoxCategorySearch.Items.Add(item);
            }

            // Populate Attributes List
            qStr = "SELECT DISTINCT name FROM Attributes";
            qResults = dataBase.SQLSELECTExec(qStr, "name");
            qResults.Sort();
            listBoxAttributes.Items.AddRange(qResults.ToArray());
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

        // Business Demographics Events

        private void boxState_DropDown(object sender, EventArgs e)
        {

        }

        private void boxState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // First clear all the demographic info
            clearStateSummary();
            clearCitySummary();
            clearZipSummary();

            // Clear the left side boxes
            listBoxCityDem.Items.Clear();
            listBoxZipDem.Items.Clear();

            // City Query, will populate city box
            string qStr = "SELECT distinct city FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' ORDER BY city;";

            List<string> qResults = dataBase.SQLSELECTExec(qStr, "city");

            foreach (string result in qResults)
            {
                listBoxCityDem.Items.Add(result);
            }

            // Zip Query for whole state, will be refined later
            qStr = "SELECT zipcode FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' ORDER BY zipcode;";

            qResults = dataBase.SQLSELECTExec(qStr, "zipcode");

            foreach (string result in qResults)
            {
                listBoxZipDem.Items.Add(result);
            }

            // Update State Summary
            updateStateSummary();

            // Update State Results Box
            textBoxState.Text = boxStateDem.SelectedItem.ToString();

            // Update Business Search if any categories were already selected
            if (listBoxSelectedCategoriesDem.Items.Count > 0)
            {
                businessSearch();
            }
        }

        private void listBoxCity_SelectedValueChanged(object sender, EventArgs e)
        {
            // First clear all the demographic info
            clearCitySummary();

            // Refine zipcodes based on selected city
            listBoxZipDem.Items.Clear();

            string qStr = "SELECT zipcode FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' AND city='" + listBoxCityDem.SelectedItem.ToString() + "' ORDER BY zipcode;";

            List<string> qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "zipcode");

            foreach(string result in qResults)
            {
                listBoxZipDem.Items.Add(result);
            }

            // Update city summary
            updateCitySummary();

            // Update city results box
            textBoxCity.Text = listBoxCityDem.SelectedItem.ToString();

            // Update Business Search if any categories were already selected
            if (listBoxSelectedCategoriesDem.Items.Count > 0)
            {
                businessSearch();
            }
        }

        private void listBoxZip_SelectedValueChanged(object sender, EventArgs e)
        {
            // Once zip code is selected, get demographics info for it

            // Make sure something is actually selected
            if (listBoxZipDem.SelectedItems.Count == 0) return;

            // First clear all the demographic info
            clearZipSummary();

            // Next make all the queries based on selected zipcode
            updateZipSummary();

            // Update zip results box
            textBoxZip.Text = listBoxZipDem.SelectedItem.ToString();

            // Update Business Search if any categories were already selected
            if (listBoxSelectedCategoriesDem.Items.Count > 0)
            {
                businessSearch();
            }
        }

        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            if (!listBoxSelectedCategoriesDem.Items.Contains(listBoxCategoryDem.SelectedItem))
            {
                listBoxSelectedCategoriesDem.Items.Add(listBoxCategoryDem.SelectedItem);
                businessSearch();
            }
        }

        private void buttonRemoveCategory_Click(object sender, EventArgs e)
        {
            if (listBoxSelectedCategoriesDem.SelectedItems.Count != 0)             // Only redo search if there is still a selected category left
            {
                listBoxSelectedCategoriesDem.Items.Remove(listBoxSelectedCategoriesDem.SelectedItem);
                emptyBusinessDem();
                if (listBoxSelectedCategoriesDem.Items.Count != 0)         // If there's still something there
                    businessSearch();
                else
                {
                    listBoxSearchResultsZip.Items.Clear();
                    listBoxSearchResultsCity.Items.Clear();
                    listBoxSearchResultsState.Items.Clear();
                }
            }
        }

        private void listBoxSearchResults_SelectedValueChanged(object sender, EventArgs e)
        {
            // Zip

            // First clear text boxes
            textBoxRatingZip.Text = "";
            textBoxReviewsZip.Text = "";

            if (listBoxSearchResultsZip.SelectedItem == null) return;

            string name = fixString(listBoxSearchResultsZip.SelectedItem.ToString());

            // Then perform queries
            string query = "SELECT avg_rev FROM Businesses WHERE name ='" + name + "';";
            List<string> results = new List<string>();

            results = dataBase.SQLSELECTExec(query, "avg_rev");

            textBoxRatingZip.Text = results[0];                      // Should only be one result

            query = "SELECT num_revs FROM Businesses WHERE name = '" + name + "';";
            results = dataBase.SQLSELECTExec(query, "num_revs");
            textBoxReviewsZip.Text = results[0];
        }
        
        private void listBoxSearchResultsState_SelectedValueChanged(object sender, EventArgs e)
        {
            // State

            // First clear text boxes
            textBoxRatingState.Text = "";
            textBoxReviewsState.Text = "";

            if (listBoxSearchResultsState.SelectedItem == null) return;

            string name = fixString(listBoxSearchResultsState.SelectedItem.ToString());

            // Then perform queries
            string query = "SELECT avg_rev FROM Businesses WHERE name ='" + name + "';";
            List<string> results = new List<string>();

            results = dataBase.SQLSELECTExec(query, "avg_rev");

            textBoxRatingState.Text = results[0];                      // Should only be one result

            query = "SELECT num_revs FROM Businesses WHERE name = '" + name + "';";
            results = dataBase.SQLSELECTExec(query, "num_revs");
            textBoxReviewsState.Text = results[0];
        }

        private void listBoxSearchResultsCity_SelectedValueChanged(object sender, EventArgs e)
        {
            // City

            // First clear text boxes
            textBoxRatingCity.Text = "";
            textBoxReviewsCity.Text = "";

            if (listBoxSearchResultsCity.SelectedItem == null) return;

            string name = fixString(listBoxSearchResultsCity.SelectedItem.ToString());

            // Then perform queries
            string query = "SELECT avg_rev FROM Businesses WHERE name ='" + name + "';";
            List<string> results = new List<string>();

            results = dataBase.SQLSELECTExec(query, "avg_rev");

            textBoxRatingCity.Text = results[0];                      // Should only be one result

            query = "SELECT num_revs FROM Businesses WHERE name = '" + name + "';";
            results = dataBase.SQLSELECTExec(query, "num_revs");
            textBoxReviewsCity.Text = results[0];
        }

        // Business Search Events

        private void boxStateSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the left side boxes
            listBoxCitySearch.Items.Clear();
            listBoxZipSearch.Items.Clear();

            // City Query, will populate city box
            string qStr = "SELECT distinct city FROM Demographics WHERE state='" + boxStateSearch.SelectedItem.ToString() + "' ORDER BY city;";

            List<string> qResults = dataBase.SQLSELECTExec(qStr, "city");

            foreach (string result in qResults)
            {
                listBoxCitySearch.Items.Add(result);
            }

            // Zip Query for whole state, will be refined later
            qStr = "SELECT zipcode FROM Demographics WHERE state='" + boxStateSearch.SelectedItem.ToString() + "' ORDER BY zipcode;";

            qResults = dataBase.SQLSELECTExec(qStr, "zipcode");

            foreach (string result in qResults)
            {
                listBoxZipSearch.Items.Add(result);
            }
            
        }

        private void listBoxCitySearch_SelectedValueChanged(object sender, EventArgs e)
        {
            // Refine zipcodes based on selected city
            listBoxZipSearch.Items.Clear();

            string qStr = "SELECT zipcode FROM Demographics WHERE state='" + boxStateSearch.SelectedItem.ToString() + "' AND city='" + listBoxCitySearch.SelectedItem.ToString() + "' ORDER BY zipcode;";

            List<string> qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "zipcode");

            foreach (string result in qResults)
            {
                listBoxZipSearch.Items.Add(result);
            }
        }

        private void buttonAddSearch_Click(object sender, EventArgs e)
        {
            if (!listBoxSelectedCategoriesSearch.Items.Contains(listBoxCategorySearch.SelectedItem))
            {
                listBoxSelectedCategoriesSearch.Items.Add(listBoxCategorySearch.SelectedItem);

                // Update Grid
            }
        }

        private void buttonRemoveSearch_Click(object sender, EventArgs e)
        {
            if (listBoxSelectedCategoriesSearch.SelectedItems.Count != 0)             // Only redo search if there is still a selected category left
            {
                listBoxSelectedCategoriesSearch.Items.Remove(listBoxSelectedCategoriesSearch.SelectedItem);
                if (listBoxSelectedCategoriesDem.SelectedItems.Count != 0)         // If there's still something there
                    businessSearch();
            }
        }

        private void listBoxZipSearch_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            emptySearchGrid();

            try
            {
                searchQuery("name");
                searchQuery("state_code");
                searchQuery("city");
                searchQuery("zipcode");
                searchQuery("avg_rev");
                searchQuery("num_revs");
            }
            catch
            {
                resultsGrid.Rows[0].Cells[0].Value = "Search Error.";
            }
            

        }

        private void searchQuery(string columnName)
        {
            string query = "SELECT " + columnName + " FROM Businesses B ";

            string qStr = "";
            List<string> qResults = new List<string>();
            int tempIndex = 0;

            if (!allowSearch())
            {
                return;
            }
        
            
            query += "WHERE B.state_code='" + stateToStateCode(boxStateSearch.SelectedItem.ToString()) + "' ";
            query += "AND B.city='" + listBoxCitySearch.SelectedItem.ToString() + "' ";
            if (listBoxZipSearch.SelectedItem != null)
                query += "AND B.zipcode='" + listBoxZipSearch.SelectedItem.ToString() + "' ";
            query += "AND B.avg_rev >= " + numericMinRating.Value.ToString() + " ";
            query += "AND B.avg_rev <= " + numericMaxRating.Value.ToString() + " ";
            query += "AND B.num_revs >= " + numericMinReviews.Value.ToString() + " ";
            query += "AND B.num_revs <= " + numericMaxReviews.Value.ToString() + " ";

            query += "AND B.bid IN ";

            for (int i = 0; i < listBoxSelectedCategoriesSearch.Items.Count; i++)
            {
                query += "(SELECT bid FROM Categories C WHERE C.name = '" + listBoxSelectedCategoriesSearch.Items[i] + "' GROUP BY C.bid)";
                if (i + 1 == listBoxSelectedCategoriesSearch.Items.Count && listBoxSelectedAttributes.Items.Count == 0) query += ";";
                else if (i + 1 != listBoxSelectedCategoriesSearch.Items.Count) query += " AND ";
            }

            if (listBoxSelectedAttributes.Items.Count > 0)
            {
                query += "AND B.bid IN ";

                for (int i = 0; i < listBoxSelectedAttributes.Items.Count; i++)
                {
                    string[] attributes = parseAttribute(listBoxSelectedAttributes.Items[i].ToString());
                    query += "(SELECT bid FROM Attributes A WHERE A.name = '" + attributes[0] + "' AND A._value = '" + attributes[1] + "' GROUP BY A.bid)";
                    if (i + 1 == listBoxSelectedAttributes.Items.Count) query += ";";
                    else query += " AND ";
                }
            }
            
            qResults = dataBase.SQLSELECTExec(query, columnName);
            listBoxSearchResultsZip.Items.AddRange(qResults.ToArray());
            
            /*
            string state = boxStateSearch.SelectedItem.ToString();
            string city = listBoxCitySearch.SelectedItem.ToString();
            string zipcode = listBoxZipSearch.SelectedItem.ToString();

            qStr = "SELECT DISTINCT temp1.name FROM (SELECT DISTINCT B.name, B.bid AS B1 FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code='" + state + "' AND B.city ='" + city + "' AND B.zipcode = '" + zipcode + "' AND C.name = '" + listBoxSelectedCategoriesSearch.Items[0] + "') AS temp1 ";

            for (int i = 1; i < listBoxSelectedCategoriesSearch.Items.Count; i++)
            {
                tempIndex = i + 1;
                qStr += "INNER JOIN (SELECT DISTINCT B.name, B.bid AS B1 FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code ='" + state + "' AND B.city ='" + city + "' AND B.zipcode = '" + zipcode + "' AND C.name = '" + listBoxSelectedCategoriesSearch.Items[i] + "') AS temp" + tempIndex.ToString() + " ";
            }

            tempIndex = listBoxSelectedCategoriesSearch.Items.Count + 1;
            for (int i = 0; i < listBoxSelectedAttributes.Items.Count; i++)
            {
                string[] attributes = parseAttribute(listBoxSelectedAttributes.Items[i].ToString());
                qStr += "INNER JOIN (SELECT DISTINCT B.name, B.bid AS B1 FROM Businesses B, Attributes A WHERE B.bid = A.bid AND A.name = '" + attributes[0] + "' AND A._value = '" + attributes[1] + "') AS temp" + (tempIndex + i).ToString() + " ";
            }

            qStr += "ON ";

            for (int i = 0; i < listBoxSelectedCategoriesSearch.Items.Count + listBoxSelectedAttributes.Items.Count - 1; i++)
            {
                qStr += "temp" + (i + 1).ToString() + ".name=" + "temp" + (i + 2).ToString() + ".name ";
                if (i == listBoxSelectedCategoriesSearch.Items.Count + listBoxSelectedAttributes.Items.Count - 2) qStr += ";";
                else qStr += "AND ";
            }

            qResults = dataBase.SQLSELECTExec(qStr, "name");
            qResults.Sort();
            */




            while (resultsGrid.Rows.Count < qResults.Count)
            {
                resultsGrid.Rows.Add();
            }

            for (int i = 0; i < qResults.Count; i++)
            {
                string result = qResults[i];
                
                resultsGrid.Rows[i].Cells[getColumnIndex(columnName)].Value = result;
            }

            return;
        }

        private int getColumnIndex(string columnName)
        {
            switch (columnName)
            {
                case "name":
                    return 0;
                case "city":
                    return 1;
                case "state_code":
                    return 2;
                case "zipcode":
                    return 3;
                case "avg_rev":
                    return 4;
                case "num_revs":
                    return 5;
                default:
                    return -1;
            }
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            emptySearchGrid();
        }

        private void boxValue_DropDown(object sender, EventArgs e)
        {
            if (listBoxAttributes.SelectedItem == null) return;

            string query = "SELECT DISTINCT _value FROM Attributes WHERE name = '" + listBoxAttributes.SelectedItem.ToString() + "';";
            List<string> qResults = new List<string>();

            qResults = dataBase.SQLSELECTExec(query, "_value");

            boxValue.Items.Clear();
            boxValue.Items.AddRange(qResults.ToArray());
        }

        private void buttonAddAttribute_Click(object sender, EventArgs e)
        {
            if (boxValue.SelectedItem == null)
            {
                return;
            }

            if (!listBoxSelectedAttributes.Items.Contains(listBoxAttributes.SelectedItem.ToString() + "," + boxValue.SelectedItem.ToString()))
            {
                listBoxSelectedAttributes.Items.Add(listBoxAttributes.SelectedItem.ToString() + "," + boxValue.SelectedItem.ToString());
            }
        }

        private void listBoxAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            boxValue.Items.Clear(); 
        }

        #endregion

        #region Helper Functions

        private void fillStates()
        {
            // Find all possible state names within database. This has the potential to show errors, but we consider that to be okay as it would show flaws within the database.

            string query = "SELECT DISTINCT state FROM Demographics";
            List<string> results = new List<string>();
            results = dataBase.SQLSELECTExec(query, "state");
            results.Sort();

            boxStateDem.Items.Clear();
            boxStateSearch.Items.Clear();

            boxStateDem.Items.AddRange(results.ToArray());
            boxStateSearch.Items.AddRange(results.ToArray());

            return;
        }

        private void clearStateSummary()
        {
            textBoxPopState.Clear();
            textBoxIncomeState.Clear();
            textBox18State.Clear();
            textBox24State.Clear();
            textBox44State.Clear();
            textBox64State.Clear();
            textBox65State.Clear();
            textBoxAgeState.Clear();
            textBoxState.Clear();
        }

        private void clearCitySummary()
        {
            textBoxPopCity.Clear();
            textBoxIncomeCity.Clear();
            textBox18City.Clear();
            textBox24City.Clear();
            textBox44City.Clear();
            textBox64City.Clear();
            textBox65City.Clear();
            textBoxAgeCity.Clear();
            textBoxCity.Clear();
        }

        private void clearZipSummary()
        {
            textBoxPopZip.Clear();
            textBoxIncomeZip.Clear();
            textBox18Zip.Clear();
            textBox24Zip.Clear();
            textBox44Zip.Clear();
            textBox64Zip.Clear();
            textBox65Zip.Clear();
            textBoxAgeZip.Clear();
            textBoxZip.Clear();
        }

        private string formatPop(string population)
        {
            int formatCounter = 0;

            for (int i = population.Length - 1; i > 0; i--)
            {
                if (formatCounter == 3)
                {
                    population = population.Insert(i + 1, ",");
                    formatCounter = 0;
                }
                formatCounter++;
            }

            return population;
        }

        private void updateStateSummary()
        {
            List<string> qResults = new List<string>();

            string qStr = "SELECT SUM(population) FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "sum(population)");
            textBoxPopState.Text = qResults[0].ToString();
            textBoxPopState.Text = formatPop(textBoxPopState.Text);

            qStr = "SELECT (SUM(avg_income) / COUNT(avg_income)) AS avg_income FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "avg_income");
            textBoxIncomeState.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(under18) / COUNT(under18)) AS under18 FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "under18");
            textBox18State.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(18to24) / COUNT(18to24)) AS 18to24 FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "18to24");
            textBox24State.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(25to44) / COUNT(25to44)) AS 25to44 FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "25to44");
            textBox44State.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(45to64) / COUNT(45to64)) AS 45to64 FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "45to64");
            textBox64State.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(65andover) / COUNT(65andover)) AS 65andover FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "65andover");
            textBox65State.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(med_age) / COUNT(med_age)) AS med_age FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "med_age");
            textBoxAgeState.Text = qResults[0].ToString();
        }

        private void updateCitySummary()
        {
            List<string> qResults = new List<string>();

            string qStr = "SELECT SUM(population) FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' AND city='" + listBoxCityDem.SelectedItem.ToString() + "' GROUP BY state, city;";
            qResults = dataBase.SQLSELECTExec(qStr, "sum(population)");
            textBoxPopCity.Text = qResults[0].ToString();
            textBoxPopCity.Text = formatPop(textBoxPopState.Text);

            qStr = "SELECT (SUM(avg_income) / COUNT(avg_income)) AS avg_income FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' AND city='" + listBoxCityDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "avg_income");
            textBoxIncomeCity.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(under18) / COUNT(under18)) AS under18 FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' AND city='" + listBoxCityDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "under18");
            textBox18City.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(18to24) / COUNT(18to24)) AS 18to24 FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' AND city='" + listBoxCityDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "18to24");
            textBox24City.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(25to44) / COUNT(25to44)) AS 25to44 FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' AND city='" + listBoxCityDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "25to44");
            textBox44City.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(45to64) / COUNT(45to64)) AS 45to64 FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' AND city='" + listBoxCityDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "45to64");
            textBox64City.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(65andover) / COUNT(65andover)) AS 65andover FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' AND city='" + listBoxCityDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "65andover");
            textBox65City.Text = qResults[0].ToString();

            qStr = "SELECT (SUM(med_age) / COUNT(med_age)) AS med_age FROM Demographics WHERE state='" + boxStateDem.SelectedItem.ToString() + "' AND city='" + listBoxCityDem.SelectedItem.ToString() + "' GROUP BY state;";
            qResults = dataBase.SQLSELECTExec(qStr, "med_age");
            textBoxAgeCity.Text = qResults[0].ToString();
        }

        private void updateZipSummary()
        {
            string qStr = "SELECT population FROM Demographics WHERE zipcode='" + listBoxZipDem.SelectedItem.ToString() + "'";
            List<string> qResults = new List<string>();                 // Query must return list, even if we know there will only be one item
            qResults = dataBase.SQLSELECTExec(qStr, "population");
            textBoxPopZip.Text = qResults[0];                              // Zipcodes are unique, so we know there will only be one item from queryj

            qStr = "SELECT avg_income FROM Demographics WHERE zipcode='" + listBoxZipDem.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "avg_income");
            textBoxIncomeZip.Text = qResults[0];

            qStr = "SELECT under18 FROM Demographics WHERE zipcode='" + listBoxZipDem.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "under18");
            textBox18Zip.Text = qResults[0];

            qStr = "SELECT 18to24 FROM Demographics WHERE zipcode='" + listBoxZipDem.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "18to24");
            textBox24Zip.Text = qResults[0];

            qStr = "SELECT 25to44 FROM Demographics WHERE zipcode='" + listBoxZipDem.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "25to44");
            textBox44Zip.Text = qResults[0];

            qStr = "SELECT 45to64 FROM Demographics WHERE zipcode='" + listBoxZipDem.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "45to64");
            textBox64Zip.Text = qResults[0];

            qStr = "SELECT 65andover FROM Demographics WHERE zipcode='" + listBoxZipDem.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "65andover");
            textBox65Zip.Text = qResults[0];

            qStr = "SELECT med_age FROM Demographics WHERE zipcode='" + listBoxZipDem.SelectedItem.ToString() + "'";
            qResults = new List<string>();
            qResults = dataBase.SQLSELECTExec(qStr, "med_age");
            textBoxAgeZip.Text = qResults[0];

        }

        private void businessSearch()
        {
            listBoxSearchResultsZip.Items.Clear();
            listBoxSearchResultsCity.Items.Clear();
            listBoxSearchResultsState.Items.Clear();

            if (boxStateDem.SelectedItem != null)
            {
                // Do state search
                if (listBoxSelectedCategoriesDem.Items.Count < 2)
                {
                    string qStr = "SELECT DISTINCT B.name FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code='" + stateToStateCode(boxStateDem.SelectedItem.ToString()) + "' AND B.bid IN ";
                    for (int i = 0; i < listBoxSelectedCategoriesDem.Items.Count; i++)
                    {
                        qStr += "(SELECT DISTINCT bid FROM Categories WHERE name = '" + listBoxSelectedCategoriesDem.Items[i] + "' GROUP BY C.bid)";
                        if (i + 1 == listBoxSelectedCategoriesDem.Items.Count) qStr += ";";
                        else qStr += " AND ";
                    }
                    List<string> qResults = new List<string>();                 // Query must return list, even if we know there will only be one item
                    qResults = dataBase.SQLSELECTExec(qStr, "name");
                    qResults.Sort();

                    listBoxSearchResultsState.Items.AddRange(qResults.ToArray());

                    if (listBoxCityDem.SelectedItem != null)
                    {
                        // Do city search

                        qStr = "SELECT DISTINCT B.name FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code='" + stateToStateCode(boxStateDem.SelectedItem.ToString()) +
                            "' AND B.city ='" + listBoxCityDem.SelectedItem.ToString() + "' AND B.bid IN ";
                        for (int i = 0; i < listBoxSelectedCategoriesDem.Items.Count; i++)
                        {
                            qStr += "(SELECT DISTINCT bid FROM Categories WHERE name = '" + listBoxSelectedCategoriesDem.Items[i] + "' GROUP BY C.bid)";
                            if (i + 1 == listBoxSelectedCategoriesDem.Items.Count) qStr += ";";
                            else qStr += " AND ";
                        }
                        qResults = new List<string>();                 // Query must return list, even if we know there will only be one item
                        qResults = dataBase.SQLSELECTExec(qStr, "name");
                        qResults.Sort();

                        listBoxSearchResultsCity.Items.AddRange(qResults.ToArray());

                        if (listBoxZipDem.SelectedItem != null)
                        {
                            // Do zip search

                            qStr = "SELECT DISTINCT B.name FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code='" + stateToStateCode(boxStateDem.SelectedItem.ToString()) +
                                "' AND B.city ='" + listBoxCityDem.SelectedItem.ToString() + "' AND B.zipcode='" + listBoxZipDem.SelectedItem.ToString() + "' AND B.bid IN ";
                            for (int i = 0; i < listBoxSelectedCategoriesDem.Items.Count; i++)
                            {
                                qStr += "(SELECT DISTINCT bid FROM Categories WHERE name = '" + listBoxSelectedCategoriesDem.Items[i] + "' GROUP BY C.bid)";
                                if (i + 1 == listBoxSelectedCategoriesDem.Items.Count) qStr += ";";
                                else qStr += " AND ";
                            }
                            qResults = new List<string>();                 // Query must return list, even if we know there will only be one item
                            qResults = dataBase.SQLSELECTExec(qStr, "name");
                            qResults.Sort();

                            listBoxSearchResultsZip.Items.AddRange(qResults.ToArray());
                        }
                    }
                }

                if (listBoxSelectedCategoriesDem.Items.Count > 1)
                {
                    string state = stateToStateCode(boxStateDem.SelectedItem.ToString());

                    string qStr = "SELECT DISTINCT temp1.name FROM (SELECT DISTINCT B.name, B.bid AS B1 FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code='" + state + "' AND C.name = '" + listBoxSelectedCategoriesDem.Items[0] + "') AS temp1 ";

                    for (int i = 1; i < listBoxSelectedCategoriesDem.Items.Count; i++)
                    {
                        qStr += "INNER JOIN (SELECT DISTINCT B.name, B.bid AS B1 FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code ='" + state + "' AND C.name = '" + listBoxSelectedCategoriesDem.Items[i] + "') AS temp" + (i + 1).ToString() + " ";
                    }

                    qStr += "ON ";

                    for (int i = 0; i < listBoxSelectedCategoriesDem.Items.Count - 1; i++)
                    {
                        qStr += "temp" + (i + 1).ToString() + ".name=" + "temp" + (i + 2).ToString() + ".name ";
                        if (i == listBoxSelectedCategoriesDem.Items.Count - 2) qStr += ";";
                        else qStr += "AND ";
                    }

                    List<string> qResults = new List<string>();                 // Query must return list, even if we know there will only be one item
                    qResults = dataBase.SQLSELECTExec(qStr, "name");
                    qResults.Sort();

                    listBoxSearchResultsState.Items.AddRange(qResults.ToArray());

                    if (listBoxCityDem.SelectedItem != null)
                    {
                        // Do city search
                        
                        string city = listBoxCityDem.SelectedItem.ToString();

                        qStr = "SELECT DISTINCT temp1.name FROM (SELECT DISTINCT B.name, B.bid AS B1 FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code='" + state + "' AND B.city ='" + city + "' AND C.name = '" + listBoxSelectedCategoriesDem.Items[0] + "') AS temp1 ";

                        for (int i = 1; i < listBoxSelectedCategoriesDem.Items.Count; i++)
                        {
                            qStr += "INNER JOIN (SELECT DISTINCT B.name, B.bid AS B1 FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code ='" + state + "' AND B.city ='" + city + "' AND C.name = '" + listBoxSelectedCategoriesDem.Items[i] + "') AS temp" + (i + 1).ToString() + " ";
                        }

                        qStr += "ON ";

                        for (int i = 0; i < listBoxSelectedCategoriesDem.Items.Count - 1; i++)
                        {
                            qStr += "temp" + (i + 1).ToString() + ".name=" + "temp" + (i + 2).ToString() + ".name ";
                            if (i == listBoxSelectedCategoriesDem.Items.Count - 2) qStr += ";";
                            else qStr += "AND ";
                        }
                        
                        qResults = dataBase.SQLSELECTExec(qStr, "name");
                        qResults.Sort();

                        listBoxSearchResultsCity.Items.AddRange(qResults.ToArray());

                        if (listBoxZipDem.SelectedItem != null)
                        {
                            // Do zip search

                            string zipcode = listBoxZipDem.SelectedItem.ToString();

                            qStr = "SELECT DISTINCT temp1.name FROM (SELECT DISTINCT B.name, B.bid AS B1 FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code='" + state + "' AND B.city ='" + city + "' AND B.zipcode = '" + zipcode + "' AND C.name = '" + listBoxSelectedCategoriesDem.Items[0] + "') AS temp1 ";

                            for (int i = 1; i < listBoxSelectedCategoriesDem.Items.Count; i++)
                            {
                                qStr += "INNER JOIN (SELECT DISTINCT B.name, B.bid AS B1 FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code ='" + state + "' AND B.city ='" + city + "' AND B.zipcode = '" + zipcode + "' AND C.name = '" + listBoxSelectedCategoriesDem.Items[i] + "') AS temp" + (i + 1).ToString() + " ";
                            }

                            qStr += "ON ";

                            for (int i = 0; i < listBoxSelectedCategoriesDem.Items.Count - 1; i++)
                            {
                                qStr += "temp" + (i + 1).ToString() + ".name=" + "temp" + (i + 2).ToString() + ".name ";
                                if (i == listBoxSelectedCategoriesDem.Items.Count - 2) qStr += ";";
                                else qStr += "AND ";
                            }
                            
                            qResults = dataBase.SQLSELECTExec(qStr, "name");
                            qResults.Sort();

                            listBoxSearchResultsZip.Items.AddRange(qResults.ToArray());
                        }
                    }
                }
            }

            
        }

        private void emptyBusinessDem()
        {
            textBoxZip.Clear();
            textBoxReviewsZip.Clear();
            textBoxRatingZip.Clear();
        }

        private string stateToStateCode(string state)
        {
            string query = "SELECT DISTINCT state_code FROM Demographics WHERE state ='" + state + "';";
            List<string> results = new List<string>();
            results = dataBase.SQLSELECTExec(query, "state_code");
            string state_code = results[0];

            return state_code;
        }

        private void emptySearchGrid()
        {
            resultsGrid.Rows.Clear();
            reviewGrid.Rows.Clear();
        }

        private bool allowSearch()
        {
            if (boxStateSearch.SelectedItem == null) return false;
            if (listBoxCitySearch.SelectedItem == null) return false;
            if (listBoxZipSearch.SelectedItem == null) return false;
            if (listBoxSelectedCategoriesSearch.Items.Count == 0) return false;
            //if (listBoxSelectedAttributes.Items.Count == 0) return false;

            return true;
        }

        private void reviewSearch()
        {
            if (resultsGrid.SelectedRows.Count == 0) return;

            string name = fixString(resultsGrid.SelectedRows[0].Cells[0].Value.ToString());
            string city = fixString(resultsGrid.SelectedRows[0].Cells[1].Value.ToString());

            string query = "SELECT stars FROM Reviews WHERE Reviews.bid IN ( SELECT bid FROM Businesses WHERE name = '" + name + "' AND city = '" + city + "');";
            List<string> results = new List<string>();
            results = dataBase.SQLSELECTExec(query, "stars");

            while (reviewGrid.Rows.Count < results.Count) reviewGrid.Rows.Add();

            for (int i = 0; i < results.Count; i++)
            {
                string result = results[i];
                reviewGrid.Rows[i].Cells["Stars"].Value = result;
            }

            query = "SELECT text FROM Reviews WHERE Reviews.bid IN ( SELECT bid FROM Businesses WHERE name = '" + name + "' AND city = '" + city + "');";
            results = dataBase.SQLSELECTExec(query, "text");

            for (int i = 0; i < results.Count; i++)
            {
                string result = results[i];
                reviewGrid.Rows[i].Cells["Review"].Value = result;
            }

            return;
        }

        private string fixString(string query)
        {
            for (int i = 0; i < query.Length; i++)
            {
                if (query[i] == '\'')
                {
                    query = query.Insert(i, "'");
                    i++;
                }
            }

            return query;
        }

        #endregion

        private void resultsGrid_SelectionChanged(object sender, EventArgs e)
        {
            reviewGrid.Rows.Clear();
            reviewSearch();
        }

        private string[] parseAttribute(string attribute)
        {
            string[] attributes = new string[2];

            attributes = attribute.Split(',');

            return attributes;
        }
    }
}

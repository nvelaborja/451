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
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Business_Analyst
{
    class MySQLConnection
    {
        #region Class Members

        private MySqlConnection connection;             // using MySQL Connector/Net

        #endregion

        #region Constructors

        public MySQLConnection()
        {
            try
            {
                Initialize();
            }
            catch (MySqlException exception)
            {
                // Do something with exception
            }
        }

        #endregion

        #region Database Utility Functions

        private void Initialize()
        {
            string server;
            string database;
            string uid;
            string password;
            server = "localhost";
            database = "Milestone1DB";
            uid = "root";
            password = "laBor07tha";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException exception)
            {
                if (exception.Number == 0)
                {
                    // Can't c onnect to server
                    return false;
                }
                else if (exception.Number == 1045)
                {
                    //invalid username/password
                    return false;
                }
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch(MySqlException exception)
            {
                // handle exception
                return false;
            }
            return false;
        }

        #endregion

        #region Query Functions

        public List<String> SQLSELECTExec(string querySTR, string column_name)
        {
            List<String> qResult = new List<String>();
            if (this.OpenConnection())
            {
                // Run query
                MySqlCommand cmd = new MySqlCommand(querySTR, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    qResult.Add(dataReader.GetString(column_name));
                }

                // close the reader
                dataReader.Close();
                // close the connection
                this.CloseConnection();
            }

            return qResult;
        }

        #endregion
    }
}

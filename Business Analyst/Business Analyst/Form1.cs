using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Business_Analyst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Fill state drop box
            fillStates();

            

        }

        private void boxState_DropDown(object sender, EventArgs e)
        {

        }

        private void boxState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            listBoxCity.Items.Clear();
        }

        private void listBoxCity_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void listBoxZip_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void fillStates()
        {
            string[] states = new string[50] {"Alabama","Alaska","Arizona","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas",
                    "Kentucky","Louisiana","Maine","Maryland","Massachusetts","Michigan","Minnesota","Mississippi","Missouri","Montana","Nebraska","Nevada","New Hampshire","New Jersey","New Mexico",
                    "New York","North Carolina","North Dakota","Ohio","Oklahoma","Oregon","Pennsylvania","Rhode Island","South Carolina","South Dakota","Tennessee","Texas ","Utah ","Vermont",
                    "Virginia","Washington","West Virginia","Wisconsin","Wyoming"};

            boxState.Items.Clear();

            boxState.Items.AddRange(states);

            return;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace BaseballDisplay
{
    public partial class DisplayBaseballTable : Form
    {

        private BaseballDB.BaseballEntities dbcon;

        public DisplayBaseballTable()
        {
            InitializeComponent();
        }

        

        private void playerBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        // sets context and binding source to all players in database,
        // ordered by ID
        private void refreshSource()
        {
            if (dbcon != null)
                dbcon.Dispose();

            dbcon = new BaseballDB.BaseballEntities();
            dbcon.Players
                .OrderBy( Player => Player.PlayerID )
                .Load();

            playerBindingSource.DataSource = dbcon.Players.Local;
        }

        private void DisplayBaseballTable_Load(object sender, EventArgs e)
        {
            refreshSource();
        }

        private void lblLastName_Click(object sender, EventArgs e)
        {

        }

        // update the dataGridView to show only players whose 
        // last name matches the search query
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var lastNameQuery =
                from player in dbcon.Players
                where player.LastName.StartsWith(txtBoxSearch.Text)
                orderby player.FirstName
                select player;

            playerBindingSource.DataSource = lastNameQuery.ToList();
            playerBindingSource.MoveFirst();


        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            txtBoxSearch.Clear();
            refreshSource();
        }
    }
}

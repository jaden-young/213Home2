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

namespace Project2
{
    public partial class DisplayBaseballTable : Form
    {

        private BaseballDB.BaseballEntities dbcon;

        public DisplayBaseballTable()
        {
            InitializeComponent();
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void playerBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void DisplayBaseballTable_Load(object sender, EventArgs e)
        {
            refreshSource();
        }

        // sets the context and binding source to all players in database, 
        // sorted by ID
        private void refreshSource()
        {
            if (dbcon != null)
                dbcon.Dispose();

            dbcon = new BaseballDB.BaseballEntities();

            dbcon.Players
                .OrderBy(Player => Player.PlayerID)
                .Load();

            playerBindingSource.DataSource = dbcon.Players.Local;
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            txtBoxMin.Text = "0.000";
            txtBoxMax.Text = "1.000";
            refreshSource();
        }


        // Search for players with batting averages between min and max
        // and update the dataGridView to display only these players
        private void btnSearch_Click(object sender, EventArgs e)
        {
            decimal min = Convert.ToDecimal(txtBoxMin.Text);
            decimal max = Convert.ToDecimal(txtBoxMax.Text);
            var batAverageQuery =
                from player in dbcon.Players
                where player.BattingAverage >= min
                    && player.BattingAverage <= max
                select player;

            playerBindingSource.DataSource = batAverageQuery.ToList();
            playerBindingSource.MoveFirst();
                
        }


    }
}

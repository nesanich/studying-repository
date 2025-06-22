using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace октябрь_чеек
{
    public partial class client : Form
    {
        query control;
        public client()
        {
            InitializeComponent();
            control = new query("Data Source=BDQ.db;");
            string conect = "Data Source=BDQ.db;";
            control.poluchdata(conect, comboBox1, "SELECT ID FROM client", false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = control.Update("SELECT * FROM client");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            main main = new main();
            main.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            control.Add_client(textBox1.Text, maskedTextBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            control.Delete(comboBox1.Text, "DELETE FROM client WHERE ID = @ID");
        }
    }
}

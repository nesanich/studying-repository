using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace октябрь_чеек
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            tarifs tar = new tarifs();
            tar.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            contract contr = new contract();
            contr.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            statistic statistic = new statistic();
            statistic.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            client client = new client();
            client.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            employee employee = new employee();
            employee.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ad_contract ad_Contract = new ad_contract();
            ad_Contract.Show();
        }
    }
}

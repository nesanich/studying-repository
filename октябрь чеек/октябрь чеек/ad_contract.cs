using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace октябрь_чеек
{
    public partial class ad_contract : Form
    {
        query control;
        public ad_contract()
        {
            InitializeComponent();
            control = new query("Data Source=BDQ.db;");
            string conect = "Data Source=BDQ.db;";
            control.poluchdata(conect, comboBox2, "SELECT ID, FIO FROM employee", true);
            control.poluchdata(conect, comboBox3, "SELECT ID, FIO FROM client", true);
            control.poluchdata(conect, comboBox4, "SELECT ID FROM ad_contract", false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            main main = new main();
            main.Show();
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.CustomFormat = "yyyy.MM.dd";
        }
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker3.CustomFormat = "yyyy.MM.dd";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string asd = "SELECT ac.ID, e.FIO AS employee_FIO, ac.date, c.FIO AS client_FIO, ac.period FROM ad_contract ac JOIN employee e ON ac.employee_id = e.ID JOIN client c ON ac.client_id = c.ID; ";
            string datescore = "SELECT ac.ID, e.FIO AS employee_FIO, ac.date, c.FIO AS client_FIO, ac.period, ac.d_score FROM ad_contract ac JOIN employee e ON ac.employee_id = e.ID JOIN client c ON ac.client_id = c.ID WHERE ac.d_score BETWEEN @Number1 AND @Number2;";
            if (checkBox1.Checked) { dataGridView1.DataSource = control.GetRowsInRange("ad_contract", int.Parse(control.ConvertDateFormat(dateTimePicker2.Text)), int.Parse(control.ConvertDateFormat(dateTimePicker3.Text)), datescore); }
            else { dataGridView1.DataSource = control.Update(asd); }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            control.Add_AD(maskedTextBox2.Text, dateTimePicker1.Text, control.GetIdByValue("client", "FIO", comboBox3.Text).ToString(), control.GetIdByValue("employee", "FIO", comboBox2.Text).ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            control.Delete(comboBox4.Text, "DELETE FROM ad_contract WHERE ID = @ID");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(control.ConvertDateFormat(dateTimePicker2.Text));
        }
    }
}

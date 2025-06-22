using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static октябрь_чеек.query;


namespace октябрь_чеек
{
    public partial class contract : Form
    {
        query control;
        string retTarif = "SELECT ID, tarif_name FROM tarif WHERE archive <> 0;";
        string retEmployee = "SELECT ID, FIO FROM employee";
        string retClient = "SELECT ID, FIO FROM client";
        string retID = "SELECT ID FROM contract";


        public contract()
        {
            InitializeComponent();
            control = new query("Data Source=BDQ.db;");
            string conect = "Data Source=BDQ.db;";
            control.poluchdata(conect, comboBox1, retTarif, true);
            control.poluchdata(conect, comboBox2, retEmployee, true);
            control.poluchdata(conect, comboBox3, retClient, true);
            control.poluchdata(conect, comboBox4, retID, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string asd = "SELECT c.ID, e.FIO AS employee_FIO, c.date_contract, t.tarif_name, cl.FIO AS client_FIO FROM contract c JOIN employee e ON c.employee_id = e.ID JOIN tarif t ON c.tarif_id = t.ID JOIN client cl ON c.client_id = cl.ID;";
            string datescore = "SELECT c.ID, e.FIO AS employee_FIO, c.date_contract, t.tarif_name, cl.FIO AS client_FIO,c.d_score FROM contract c JOIN employee e ON c.employee_id = e.ID JOIN tarif t ON c.tarif_id = t.ID JOIN client cl ON c.client_id = cl.ID WHERE c.d_score BETWEEN @Number1 AND @Number2;";
            if (checkBox1.Checked) { dataGridView1.DataSource = control.GetRowsInRange("contract", int.Parse(control.ConvertDateFormat(dateTimePicker2.Text)), int.Parse(control.ConvertDateFormat(dateTimePicker3.Text)), datescore); }
            else { dataGridView1.DataSource = control.Update(asd); }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            main main = new main();
            this.Hide();
            main.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            control.Add_contract(control.GetIdByValue("employee", "FIO", comboBox2.Text).ToString(), dateTimePicker1.Text, control.GetIdByValue("tarif", "tarif_name", comboBox1.Text).ToString(), control.GetIdByValue("client", "FIO", comboBox3.Text).ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void contract_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (int.TryParse(comboBox4.Text, out int contractId))
            {
                control.Delete(contractId.ToString(), "DELETE FROM contract WHERE ID = @ID");
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректный ID контракта.");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

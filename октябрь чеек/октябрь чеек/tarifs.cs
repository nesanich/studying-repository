using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace октябрь_чеек
{
    public partial class tarifs : Form
    {
        query control;
        string conect = "Data Source=BDQ.db;";
        public tarifs()
        {
            InitializeComponent();
            control = new query(conect);
            control.poluchdata(conect, comboBox1, "SELECT ID, tarif_name FROM tarif", true);
            control.poluchdata(conect, comboBox2, "SELECT DISTINCT type FROM tarif", false);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = control.Update("SELECT * FROM tarif");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            main main = new main();
            this.Hide();
            main.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=BDQ.db;";
            int idToUpdate = control.retvalue(comboBox1);

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                string query = @"
                    UPDATE tarif
                    SET archive = CASE 
                        WHEN archive = 0 THEN 1 
                        ELSE 0 
                    END
                    WHERE ID = @ID";

                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@ID", idToUpdate);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Запись успешно обновлена!");
                    }
                    else
                    {
                        MessageBox.Show("Запись с указанным ID не найдена.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            control.Add_tarif(textBox1.Text, maskedTextBox1.Text, control.retvalue(comboBox2).ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            control.Delete(control.retvalue(comboBox1).ToString(), "DELETE FROM tarif WHERE ID = @ID");
        }
    }
}

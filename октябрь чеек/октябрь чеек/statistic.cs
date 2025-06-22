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
using System.Runtime.Remoting.Contexts;

namespace октябрь_чеек
{
    public partial class statistic : Form
    {
        query control;
        string rethelp = "SELECT tab, nam FROM help";
        public statistic()
        {
            InitializeComponent();
            string conect = "Data Source=BDQ.db;";
            control = new query("Data Source=BDQ.db;");
            control.poluchdata(conect, comboBox1, rethelp, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tar = "SELECT tarif.tarif_name, Count(contract.tarif_id) AS [Count-tarif_id] FROM tarif INNER JOIN contract ON tarif.ID = contract.tarif_id GROUP BY tarif.tarif_name ORDER BY Count(contract.tarif_id) DESC;";
            string a_emp = "SELECT \r\n    e.FIO,\r\n    COUNT(ac.employee_id) AS employee_count\r\nFROM \r\n    ad_contract ac\r\nJOIN \r\n    employee e ON ac.employee_id = e.id\r\nGROUP BY \r\n    e.FIO;";
            string c_emp = "SELECT \r\n    e.FIO,\r\n    COUNT(c.employee_id) AS employee_count\r\nFROM \r\n    contract c\r\nJOIN \r\n    employee e ON c.employee_id = e.id\r\nGROUP BY \r\n    e.FIO;";
            string c_cli = "SELECT \r\n    c.fio,\r\n    COUNT(c.id) AS count\r\nFROM \r\n    contract AS ct\r\nINNER JOIN \r\n    client AS c ON ct.client_id = c.id\r\nGROUP BY \r\n    c.fio;";
            string a_cli = "SELECT \r\n    c.fio,\r\n    COUNT(ac.client_id) AS count\r\nFROM \r\n    ad_contract AS ac\r\nINNER JOIN \r\n    client AS c ON ac.client_id = c.id\r\nGROUP BY \r\n    c.fio;";
            string val = control.GetIdByValue("help", "nam", comboBox1.Text).ToString();
            switch (val)
            {
                case "1":
                    label1.Text = "Подключение";
                    label2.Text = "Реклама";
                    dataGridView1.DataSource = control.Update(c_emp);
                    dataGridView2.DataSource = control.Update(a_emp);
                    break;
                case "2":
                    label1.Text = "Подключение";
                    label2.Text = "Реклама";
                    dataGridView1.DataSource = control.Update(c_cli);
                    dataGridView2.DataSource = control.Update(a_cli);
                    break;
                case "3":
                    label1.Text = "Тарифы";
                    label2.Text = "";
                    dataGridView1.DataSource = control.Update(tar);
                    dataGridView2.DataSource = "";
                    break;
            }
        }
    }
}

using practice_interface.формы;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice_interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSelectDates form = new FormSelectDates();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Гости guest = new Гости();
            guest.Show();
            BookingForm booking = new BookingForm();
            booking.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FreeRooms freeRooms = new FreeRooms();
            freeRooms.Show();
        }
    }
}

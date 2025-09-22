using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice_interface.формы
{
    public partial class FreeRooms : Form
    {
        private readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7108/api/")
        };
        public FreeRooms()
        {
            InitializeComponent();
        }


        private async void buttonFindFreeRooms_Click(object sender, EventArgs e)
        {
            var checkIn = dateTimePickerCheckIn.Value.ToString("yyyy-MM-dd");
            var checkOut = dateTimePickerCheckOut.Value.ToString("yyyy-MM-dd");

            var freeRooms = await _httpClient.GetFromJsonAsync<List<RoomDto>>(
                $"Rooms/Free?checkIn={checkIn}&checkOut={checkOut}");

            dataGridViewFreeRooms.DataSource = freeRooms;
        }
        public class RoomDto
        {
            public int id { get; set; }
            public int номер { get; set; }
            public int вместимость { get; set; }
            public decimal стоимость_за_ночь { get; set; }
            public int тип_номера { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

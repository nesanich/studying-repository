using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using static practice_interface.формы.FreeRooms;

namespace practice_interface.формы
{
    public partial class FormSelectDates : Form
    {
        private readonly HttpClient _httpClient;
        public FormSelectDates()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7108/api/")
            };
        }

        private async void buttonFindRooms_Click(object sender, EventArgs e)
        {
            {
                var checkIn = dateTimePickerCheckIn.Value.ToString("yyyy-MM-dd");
                var checkOut = dateTimePickerCheckOut.Value.ToString("yyyy-MM-dd");

                var freeRooms = await _httpClient.GetFromJsonAsync<List<RoomDto>>(
                    $"Rooms/Free?checkIn={checkIn}&checkOut={checkOut}");

                dataGridViewRooms.DataSource = freeRooms;
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (dataGridViewRooms.CurrentRow != null)
            {
                var selectedRoom = (RoomDto)dataGridViewRooms.CurrentRow.DataBoundItem;

                var formBooking = new FormBooking(
                    selectedRoom.id,
                    dateTimePickerCheckIn.Value,
                    dateTimePickerCheckOut.Value
                );
                formBooking.Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите номер.");
            }
        }
        public class RoomDto
        {
            public int id { get; set; }
            public int номер { get; set; }
            public int вместимость { get; set; }
            public decimal стоимость_за_ночь { get; set; }
            public int тип_номера { get; set; }
        }
    }
}

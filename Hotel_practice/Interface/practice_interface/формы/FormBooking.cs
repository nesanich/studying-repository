using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice_interface.формы
{
    public partial class FormBooking : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _roomId;
        private readonly DateTime _checkIn;
        private readonly DateTime _checkOut;
        public FormBooking(int roomId, DateTime checkIn, DateTime checkOut)
        {
            InitializeComponent();
            _httpClient = new HttpClient
            { BaseAddress = new Uri("https://localhost:7108/api/") };
            _roomId = roomId;
            _checkIn = checkIn;
            _checkOut = checkOut;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var guest = new GuestDto
            {
                имя = textBoxName.Text,
                фамилия = textBoxLastName.Text,
                телефон = textBoxPhone.Text,
                email = textBoxEmail.Text,
                дата_регистрации = DateTime.Now
            };
            var response = await _httpClient.PostAsJsonAsync("Guests", guest);
            response.EnsureSuccessStatusCode();
            var createdGuest = await response.Content.ReadFromJsonAsync<GuestDto>();

            // Теперь создаем бронь
            var booking = new BookingDto
            {
                Гость_id = createdGuest.id,
                Номер_id = _roomId,
                дата_заезда = _checkIn,
                дата_выезда = _checkOut,
                Статус = true
            };
            var bookingResponse = await _httpClient.PostAsJsonAsync("Bookings", booking);
            bookingResponse.EnsureSuccessStatusCode();

            MessageBox.Show("Бронь успешно оформлена!");
        }
        public class GuestDto
        {
            public int id { get; set; }
            public string имя { get; set; }
            public string фамилия { get; set; }
            public string телефон { get; set; }
            public string email { get; set; }
            public DateTime дата_регистрации { get; set; }
        }

        public class BookingDto
        {
            public int id { get; set; }
            public int Гость_id { get; set; }
            public int Номер_id { get; set; }
            public DateTime дата_заезда { get; set; }
            public DateTime дата_выезда { get; set; }
            public bool Статус { get; set; }
        }
    }
}

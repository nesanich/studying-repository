using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace practice_interface.формы
{
    public partial class BookingForm : Form
    {
        private readonly HttpClient _httpClient;

        public BookingForm()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7108/api/")
            };
        }

        private async void BookingsForm_Load(object sender, EventArgs e)
        {
            await LoadBookingsAsync();
        }

        private async Task LoadBookingsAsync()
        {
            var bookings = await _httpClient.GetFromJsonAsync<List<BookingViewModel>>("Bookings");
            dataGridViewBook.DataSource = bookings;
        }

        private async void buttonAdd_Click(object sender, EventArgs e)
        {
            var newBooking = new bookings
            {
                Номер_id = int.Parse(textBoxR.Text),
                Гость_id = int.Parse(textBoxG.Text),
                дата_заезда = dateTimePicker1.Value,
                дата_выезда = dateTimePicker2.Value,
                Статус = true // 👈 сразу true!
            };

            await _httpClient.PostAsJsonAsync("Bookings", newBooking);
            await LoadBookingsAsync();
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewBook.CurrentRow != null)
            {
                var bookingVM = (BookingViewModel)dataGridViewBook.CurrentRow.DataBoundItem;

                // Теперь берём только Id
                int bookingId = bookingVM.Id;

                await _httpClient.DeleteAsync($"Bookings/{bookingId}");
                await LoadBookingsAsync();
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewBook.CurrentRow != null)
            {
                var booking = (bookings)dataGridViewBook.CurrentRow.DataBoundItem;

                booking.Номер_id = int.Parse(textBoxR.Text);
                booking.Гость_id = int.Parse(textBoxG.Text);
                booking.дата_заезда = dateTimePicker1.Value;
                booking.дата_выезда = dateTimePicker2.Value;
                booking.Статус = true; // или добавь чекбокс, если надо менять вручную

                await _httpClient.PutAsJsonAsync($"Bookings/{booking.id}", booking);
                await LoadBookingsAsync();
            }
        }

        private async void buttonRefresh_Click(object sender, EventArgs e)
        {
            await LoadBookingsAsync();
        }

        private void dataGridViewBook_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewBook.CurrentRow != null)
            {
                var booking = (bookings)dataGridViewBook.CurrentRow.DataBoundItem;

                textBoxR.Text = booking.Номер_id.ToString();
                textBoxG.Text = booking.Гость_id.ToString();
                dateTimePicker1.Value = booking.дата_заезда;
                dateTimePicker2.Value = booking.дата_выезда;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonEmpty_Click(object sender, EventArgs e)
        {
            FreeRooms freeRooms = new FreeRooms();
            freeRooms.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    public class bookings
    {
        public int id { get; set; }
        public int Номер_id { get; set; }
        public int Гость_id { get; set; }
        public DateTime дата_заезда { get; set; }
        public DateTime дата_выезда { get; set; }
        public bool Статус { get; set; }
    }
    public class BookingViewModel
    {
        public int Id { get; set; }
        public DateTime Дата_заезда { get; set; }
        public DateTime Дата_выезда { get; set; }
        public bool Статус { get; set; }

        public string ИмяГостя { get; set; }
        public string ФамилияГостя { get; set; }
        public int НомерКомнаты { get; set; }
    }
}

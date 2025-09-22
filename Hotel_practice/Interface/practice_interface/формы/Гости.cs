using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice_interface.формы
{
    public partial class Гости : Form
    {
        private readonly HttpClient _httpClient;
        public Гости()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7108/api/")
            };
        }

        private async void FormGuests_Load(object sender, EventArgs e)
        {
            await LoadGuestsAsync();
        }

        private async Task LoadGuestsAsync()
        {
            var guests = await _httpClient.GetFromJsonAsync<List<guests>>("Guests");
            dataGridViewGuests.DataSource = guests;
        }

        private async void buttonAdd_Click(object sender, EventArgs e)
        {
            var newGuest = new guests
            {
                дата_регистрации = DateTime.Now,
                имя = textBoxName.Text,
                фамилия = textBoxLastName.Text,
                телефон = textBoxPhone.Text,
                email = textBoxEmail.Text
            };

            await _httpClient.PostAsJsonAsync("Guests", newGuest);
            await LoadGuestsAsync();
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewGuests.CurrentRow != null)
            {
                var guest = (guests)dataGridViewGuests.CurrentRow.DataBoundItem;
                await _httpClient.DeleteAsync($"Guests/{guest.id}");
                await LoadGuestsAsync();
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewGuests.CurrentRow != null)
            {
                var guest = (guests)dataGridViewGuests.CurrentRow.DataBoundItem;

                guest.имя = textBoxName.Text;
                guest.фамилия = textBoxLastName.Text;
                guest.телефон = textBoxPhone.Text;
                guest.email = textBoxEmail.Text;

                await _httpClient.PutAsJsonAsync($"Guests/{guest.id}", guest);
                await LoadGuestsAsync();
            }
        }

        private async void buttonRefresh_Click(object sender, EventArgs e)
        {
            await LoadGuestsAsync();
        }

        private void dataGridViewGuests_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewGuests.CurrentRow != null)
            {
                var guest = (guests)dataGridViewGuests.CurrentRow.DataBoundItem;

                textBoxName.Text = guest.имя;
                textBoxLastName.Text = guest.фамилия;
                textBoxPhone.Text = guest.телефон;
                textBoxEmail.Text = guest.email;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Гости_Load(object sender, EventArgs e)
        {

        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class guests
    {
        public int id { get; set; }
        public DateTime дата_регистрации { get; set; }
        public string имя { get; set; }
        public string фамилия { get; set; }
        public string телефон { get; set; }
        public string email { get; set; }
    }
}


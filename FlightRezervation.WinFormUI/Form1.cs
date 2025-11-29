
using FlightReservation.Core;
using FlightReservation.WinFormUI;
using System;
using System.Data;
using System.Windows.Forms;




namespace FlightRezervation.WinFormUI
{
    public partial class Form1 : Form
    {


        public static List<Flight> allFlights = new List<Flight>();
        static List<Customer> allCustomers = new List<Customer>();
        static List<Reservation> allReservations = new List<Reservation>();


        static Customer testCustomer; // Giriþ ekraný olmadýðý için sahte bir kullanýcý

        public Form1()
        {
            InitializeComponent();
        }


        private void SeedData()
        {
            // Önceki adýmdaki gibi, kapasitesi kadar koltuklarý 
            // otomatik oluþturan Airplane constructor'ýný kullandýk.
            Airplane boeing737 = new Airplane("Boeing 737", 150);
            Airplane airbusA320 = new Airplane("Airbus A320", 180);

            Flight flight1 = new Flight
            {
                Id = 1,
                FlightNumber = "TK1923",
                DepartureCity = "Ýstanbul",
                ArrivalCity = "Ankara",
                DepartureTime = DateTime.Now.AddDays(2).AddHours(3),
                Airplane = boeing737,
                BasePrice = 1000
            };
            allFlights.Add(flight1);

            Flight flight2 = new Flight
            {
                Id = 2,
                FlightNumber = "PC2023",
                DepartureCity = "Ýstanbul",
                ArrivalCity = "Ýzmir",
                DepartureTime = DateTime.Now.AddDays(3).AddHours(5),
                Airplane = airbusA320,
                BasePrice = 950
            };
            allFlights.Add(flight2);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string departure = txtDeparture.Text.Trim();
            string arrival = txtArrival.Text.Trim();

            var foundFlights = new List<Flight>();

            foreach (var flight in allFlights)
            {
                // Büyük/küçük harf duyarlýlýðý olmadan (IgnoreCase) þehir kontrolü
                bool cityMatch = flight.DepartureCity.Contains(departure, StringComparison.CurrentCultureIgnoreCase);

                bool arrivalMatch = flight.ArrivalCity.Contains(arrival, StringComparison.CurrentCultureIgnoreCase);

                // Tarih kontrolü: Geçmiþ tarihli uçuþlarý gösterme
                bool timeMatch = flight.DepartureTime > DateTime.Now;

                if (cityMatch && arrivalMatch && timeMatch)
                {
                    foundFlights.Add(flight);
                }
            }

            // ListBox'ý temizle ve bulunanlarý ekle
            listFlights.DataSource = null;
            listFlights.DataSource = foundFlights;
        }

        private void listFlights_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ListBox'tan seçilen nesneyi 'Flight' tipine dönüþtür (Casting)
            Flight selectedFlight = listFlights.SelectedItem as Flight;

            if (selectedFlight == null) return; // Seçim boþsa bir þey yapma

            // LINQ Sorgusu: Sadece durumu 'Available' (Boþ) olan koltuklarý getir
            // Dolu koltuklar listede görünmeyecek
            var availableSeats = selectedFlight.Airplane.Seats
                .Where(seat => seat.Status == SeatStatus.Available)
                .ToList();

            listSeats.DataSource = null; 
            listSeats.DataSource = availableSeats;
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            Flight selectedFlight = listFlights.SelectedItem as Flight;
            Seat selectedSeat = listSeats.SelectedItem as Seat;

            //Seçim kontrolü
            if (selectedFlight == null || selectedSeat == null)
            {
                MessageBox.Show("Lütfen bir uçuþ ve koltuk seçtiðinizden emin olun!", "Hata");
                return;
            }

            // Core katmanýndaki 'Customer' sýnýfýnýn metodunu çaðýrarak iþi bitir
            Reservation newReservation = testCustomer.MakeReservation(selectedFlight, selectedSeat);

            // 4. Sonucu Arayüzde (MessageBox) göster
            if (newReservation != null)
            {
                MessageBox.Show(
                    $"Rezervasyon baþarýlý!\n" +
                    $"PNR Kodu: {newReservation.Pnr}\n" +
                    $"Yolcu: {testCustomer.Name} {testCustomer.Surname}\n" +
                    $"Uçuþ: {selectedFlight.FlightNumber}\n" +
                    $"Koltuk: {selectedSeat.SeatNumber}",
                    "Rezervasyon Tamamlandý"
                );

                // Listeyi yenileme (dolu olanlar gözükmeyecek)
                listFlights_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show("Rezervasyon yapýlamadý! Koltuk dolu olabilir.", "Hata");
            }
        }


        private void Form1_Load_1(object sender, EventArgs e)
        {
            // Form açýlýr açýlmaz sahte verileri yükle
            SeedData();
            // Test müþterimizi oluþturalým
            testCustomer = new Customer { Id = 1, Name = "Test", Surname = "Kullanici", UserName = "test" };
            allCustomers.Add(testCustomer);

        }
        private void btnListReservations_Click(object sender, EventArgs e)
        {
            // 1. Form2'yi oluþtur
            Form2 reservationsForm = new Form2(testCustomer);

            // 2. Form2'nin "FormClosed" (kapatýldýðýnda) event'ine abone ol.
            //    Form2 kapandýðýnda, 'reservationsForm_FormClosed' metodu çalýþacak.
            reservationsForm.FormClosed += reservationsForm_FormClosed;

            // 3. Form2'yi göster (ShowDialog DEÐÝL, sadece Show)
            reservationsForm.Show();

            reservationsForm.StartPosition = FormStartPosition.Manual;
            reservationsForm.Location = this.Location;

            // 4. Bu formu (Form1) gizle
            this.Hide();
        }

        private void reservationsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 1. Form2 kapandýðý için, bu formu (Form1) tekrar göster
            this.Show();

            // 2. Form2'de bir iptal yapýlmýþ olabilir.
            //    Form1'deki koltuk listesini yenile.
            listFlights_SelectedIndexChanged(null, null);
        }

        private void flightManagement_Click(object sender, EventArgs e)
        {
            Form3 flightsForm = new Form3(testCustomer);
            flightsForm.FormClosed += flightsForm_FormClosed;

            // 3. Form3'yi göster (ShowDialog DEÐÝL, sadece Show)
            flightsForm.Show();

            flightsForm.StartPosition = FormStartPosition.Manual;
            flightsForm.Location = this.Location;

            // 4. Bu formu (Form1) gizle
            this.Hide();
        }

        private void flightsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 1. Form3 kapandýðý için, bu formu (Form1) tekrar göster
            this.Show();

            // 2. Form3'de bir iptal yapýlmýþ olabilir.
            //    Form1'deki koltuk listesini yenile.
            listFlights_SelectedIndexChanged(null, null);
        }
    }

}

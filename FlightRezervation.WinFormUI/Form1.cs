
using FlightReservation.Core;
using System;
using System.Data;
using System.Windows.Forms;
using UcakRezervasyon.Core;




namespace FlightRezervation.WinFormUI
{
    public partial class Form1 : Form
    {


        static List<Flight> allFlights = new List<Flight>();
        static List<Customer> allCustomers = new List<Customer>();
        static List<Reservation> allReservations = new List<Reservation>();


        static Customer testCustomer;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
            // 1. Verileri al ve temizle
            string departure = txtDeparture.Text.Trim();
            string arrival = txtArrival.Text.Trim();

            // 2. Boþ bir "bulunanlar" listesi oluþtur
            var foundFlights = new List<Flight>();

            // 3. "allFlights" listesindeki HER BÝR uçaða tek tek bakacaðýz
            foreach (var flight in allFlights)
            {
                // ---- BURAYA BREAKPOINT KOY! ----
                // Koþullarý tek tek kontrol et:

                bool cityMatch = flight.DepartureCity.Contains(departure, StringComparison.CurrentCultureIgnoreCase);

                bool arrivalMatch = flight.ArrivalCity.Contains(arrival, StringComparison.CurrentCultureIgnoreCase);

                bool timeMatch = flight.DepartureTime > DateTime.Now;

                // Eðer ÜÇ KOÞUL da doðruysa, listeye ekle
                if (cityMatch && arrivalMatch && timeMatch)
                {
                    foundFlights.Add(flight);
                }
            } // Döngü burada diðer uçuþ için tekrar baþlar

            // 4. Sonucu ListBox'a ata
            listFlights.DataSource = null;
            listFlights.DataSource = foundFlights;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listFlights_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Arayüzden seçilen veriyi al
            // (SelectedItem bir "object" olduðu için onu "Flight"a dönüþtürmeliyiz)
            Flight selectedFlight = listFlights.SelectedItem as Flight;

            if (selectedFlight == null) return; // Seçim boþsa bir þey yapma

            // 2. "Mutfaðý" (Core) kullanarak iþ mantýðýný çalýþtýr
            // Seçilen uçuþun uçaðýndaki (Airplane) müsait (Available) koltuklarý bul
            var availableSeats = selectedFlight.Airplane.Seats
                .Where(seat => seat.Status == SeatStatus.Available)
                .ToList();

            // 3. Sonucu Arayüzde (Koltuk Listesinde) göster
            listSeats.DataSource = null; // Listeyi temizle
            listSeats.DataSource = availableSeats;
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            // 1. Arayüzden seçili olan verileri al
            Flight selectedFlight = listFlights.SelectedItem as Flight;
            Seat selectedSeat = listSeats.SelectedItem as Seat;

            // 2. Kontrol yap
            if (selectedFlight == null || selectedSeat == null)
            {
                MessageBox.Show("Lütfen bir uçuþ ve koltuk seçtiðinizden emin olun!", "Hata");
                return;
            }

            // 3. "Mutfaðý" (Core) kullanarak ASIL ÝÞLEMÝ yap
            // testCustomer'ý form yüklenirken oluþturmuþtuk.
            // Core'daki Customer sýnýfýnýn metodunu çaðýrýyoruz:
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

                // Arayüzü Güncelle: Koltuk listesini yenile (artýk o koltuk dolu)
                // Bunu yapmak için listFlights_SelectedIndexChanged metodunu manuel tetikleyebiliriz
                // veya o metodun içindeki kodu bir fonksiyona alýp çaðýrabiliriz.
                // Þimdilik en basit yol:
                listFlights_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show("Rezervasyon yapýlamadý! Koltuk dolu olabilir.", "Hata");
            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

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
            //var foundReservations = new List<Reservation>();

            //if (selectedFlight == null)
            //{
            //    MessageBox.Show("Lütfen bir uçuþ seçtiðinizden emin olun!", "Hata");
            //    return;
            //}

            //foreach(var reservations in testCustomer.Reservations)
            //{
            //    foundReservations.Add( reservations );
            //}

            listReservations.DataSource = null;
            listReservations.DataSource = testCustomer.Reservations;
        }

        private void listReservations_SelectedIndexChanged(object sender, EventArgs e)
        {
            listReservations.DataSource = null;
            listReservations.DataSource = testCustomer.Reservations;


        }


        private void btnCancelReservation_Click(object sender, EventArgs e)
        {
            Reservation selectedreservation = listReservations.SelectedItem as Reservation;

            testCustomer.CancelReservation(selectedreservation.Pnr);

            listReservations.DataSource=null;
            listReservations.DataSource= testCustomer.Reservations;
        }
    }

}

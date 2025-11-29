using FlightReservation.Core;
using FlightRezervation.WinFormUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightReservation.WinFormUI
{
    public partial class Form3 : Form

    {
        private Customer _currentCustomer;

        public bool IsNavigatingBack { get; private set; } = false;

        public Form3(Customer customer)
        {
            InitializeComponent();

            _currentCustomer = customer;

            LoadFlights();

            this.FormClosing += Form3_FormClosing;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Form1'in public static listesine doğrudan eriş
            List<Flight> allFlights = Form1.allFlights;


        }

        private void addFlight_Click(object sender, EventArgs e)
        {

            try
            {
                // Formdaki textbox'lardan verileri al
                string flightnumber = txtFlightNumber.Text.Trim();
                string departurecity = txtDepartureCity.Text.Trim();
                string arrivalcity = txtArrivalCity.Text.Trim();
                decimal baseprice = Convert.ToDecimal(txtBasePrice.Text.Trim());
                DateTime departuretime = dtDepartureTime.Value;

                Flight newFlight = new Flight
                {
                    // Her yeni uçuş için otomatik olarak 150 koltuklu bir Boeing oluşturuluyor
                    // Airplane sınıfının constructor'ı (yapıcı metodu) 1'den 150'ye kadar koltukları otomatik üretir
                    Airplane = new Airplane("Boeing 737", 150),
                    Id = Form1.allFlights.Count + 1,// Basit ID atama 
                    FlightNumber = flightnumber,
                    DepartureCity = departurecity,
                    ArrivalCity = arrivalcity,
                    BasePrice = baseprice,
                    DepartureTime = departuretime,

                };

                // Admin sınıfının metodunu kullanarak uçuşu ekle
                Admin currentAdmin = new Admin();
                currentAdmin.AddFlight(newFlight, Form1.allFlights);

                LoadFlights();

                MessageBox.Show("Yeni uçuş başarıyla eklendi!", "Başarılı");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata");
            }
        }

        private void deleteFlight_Click(object sender, EventArgs e)
        {

            // Seçili uçuşu al
            Flight selectedFlight = listLoadFlights.SelectedItem as Flight;
            if (selectedFlight == null)
            {
                MessageBox.Show("Lütfen silinecek bir uçuş seçin.", "Hata");
                return;
            }
            // Admin sınıfının metodunu kullanarak uçuşu sil
            Admin currentAdmin = new Admin();
            currentAdmin.DeleteFlight(selectedFlight, Form1.allFlights);
            LoadFlights();
            MessageBox.Show("Uçuş başarıyla silindi!", "Başarılı");

        }


        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsNavigatingBack)
            {
                Application.Exit();
            }
        }



        private void LoadFlights()
        {
            listLoadFlights.DataSource = null;
            listLoadFlights.DataSource = Form1.allFlights.ToList();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.IsNavigatingBack = true;
            this.Close();
        }

    }
}

using FlightReservation.Core;
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
    public partial class Form2 : Form
    {

        private Customer _currentCustomer;

        // Bu bir kontrol bayrağıdır (Flag). 
        // Kullanıcının "Geri Dön" butonuna basarak mı yoksa pencereyi (X) kapatarak mı çıktığını anlamamızı sağlar.
        public bool IsNavigatingBack { get; private set; } = false;

        public Form2(Customer customer)
        {
            InitializeComponent();
            _currentCustomer = customer;
            LoadReservations();

            this.FormClosing += Form2_FormClosing;
        }

        private void LoadReservations()
        {
            listReservations.DataSource = null;
            listReservations.DataSource = _currentCustomer.Reservations.ToList();
        }

        private void btnCancelReservation_Click(object sender, EventArgs e)
        {
            //Cast işlemi
            Reservation selectedReservation = listReservations.SelectedItem as Reservation;

            if (selectedReservation == null)
            {
                MessageBox.Show("Lütfen iptal edilecek bir rezervasyon seçin.", "Hata");
                return;
            }

            _currentCustomer.CancelReservation(selectedReservation.Pnr);
            LoadReservations();
            MessageBox.Show($"PNR Kodu: {selectedReservation.Pnr} olan rezervasyonunuz iptal edilmiştir.","Rezervasyon İptal İşlemi Tamamlandı");
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            //Geri dön butonu aktif olduğundan bayrağı true yapılır
            this.IsNavigatingBack = true;
            this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Eğer kullanıcı "Geri Dön" butonuna basmadıysa (yani sağ üstteki X ile kapattıysa)
            if (!this.IsNavigatingBack)
            {
                Application.Exit();
            }
        }

    }
        
    }

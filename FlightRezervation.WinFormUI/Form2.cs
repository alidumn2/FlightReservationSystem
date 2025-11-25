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

        // Bu bir bayraktır, 'static' DEĞİLDİR
        public bool IsNavigatingBack { get; private set; } = false;

        // BU BİR CONSTRUCTOR'DIR, 'static' DEĞİLDİR
        public Form2(Customer customer)
        {
            InitializeComponent();
            _currentCustomer = customer;
            LoadReservations();

            // FormClosing event'ini burada koda eklemek daha garantilidir.
            // Tasarımcıdan bağlamayı unutmuş olabilirsiniz.
            this.FormClosing += Form2_FormClosing;
        }

        private void LoadReservations()
        {
            listReservations.DataSource = null;
            listReservations.DataSource = _currentCustomer.Reservations.ToList();
        }

        private void btnCancelReservation_Click(object sender, EventArgs e)
        {
            Reservation selectedReservation = listReservations.SelectedItem as Reservation;

            if (selectedReservation == null)
            {
                MessageBox.Show("Lütfen iptal edilecek bir rezervasyon seçin.", "Hata");
                return;
            }

            _currentCustomer.CancelReservation(selectedReservation.Pnr);
            LoadReservations();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.IsNavigatingBack = true;
            this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsNavigatingBack)
            {
                Application.Exit();
            }
        }

    }
        
    }

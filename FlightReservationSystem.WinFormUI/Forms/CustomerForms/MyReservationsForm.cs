using FlightReservation.Core;
using FlightReservation.Core.Entities;
using FlightReservation.Core.Enums;
using FlightReservation.DataAccess;
using FlightReservation.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FlightReservation.WinFormUI.Forms.CustomerForms
{
    public partial class MyPastReservationsForm : Form
    {
        private AppDbContext _db = new AppDbContext();
        private Customer _currentCustomer;

        // UI Kontrolleri
        private TabControl tabControl;
        private DataGridView gridActive;
        private DataGridView gridPast;
        private Label lblStatus;

        public MyPastReservationsForm(Customer customer)
        {
            _currentCustomer = customer;
            InitializeCustomDesign();
            LoadReservations();

        }
            private void InitializeCustomDesign()
        {
            this.Text = "Rezervasyonlarım";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // ÜST HEADER
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.FromArgb(41, 128, 185), Padding = new Padding(10) };
            pnlHeader.Controls.Add(new Label { Text = "REZERVASYON GEÇMİŞİM", ForeColor = Color.White, Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) });


            // ALT PANEL
            Panel pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 80, BackColor = Color.White };
            lblStatus = new Label { Text = "...", Font = new Font("Segoe UI", 10, FontStyle.Italic), ForeColor = Color.Gray, AutoSize = true, Location = new Point(20, 30) };
            pnlBottom.Controls.Add(lblStatus);

            Button btnCancel = new Button
            {
                Text = "SEÇİLİ AKTİF UÇUŞU İPTAL ET",
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(680, 20),
                Size = new Size(280, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
            };
            btnCancel.Location = new Point(pnlBottom.Width - btnCancel.Width - 20, 25);

            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            btnCancel.Click += BtnCancel_Click;
            pnlBottom.Controls.Add(btnCancel);
            this.Controls.Add(pnlBottom);

            // TAB CONTROL (Sekmeler)
            tabControl = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };

            // SEKME 1: AKTİF
            TabPage tabActive = new TabPage("Aktif Uçuşlar");
            tabActive.BackColor = Color.White;
            gridActive = CreateGrid(); 
            tabActive.Controls.Add(gridActive);
            tabControl.TabPages.Add(tabActive);

            // SEKME 2: GEÇMİŞ
            TabPage tabPast = new TabPage("Geçmiş Uçuşlar");
            tabPast.BackColor = Color.WhiteSmoke;
            gridPast = CreateGrid(); 
            tabPast.Controls.Add(gridPast);
            tabControl.TabPages.Add(tabPast);

            this.Controls.Add(tabControl);

            pnlHeader.SendToBack();
            pnlBottom.SendToBack();
            tabControl.BringToFront();
        }

        private DataGridView CreateGrid()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToResizeRows = false,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 }
            };
        }

        // İŞ MANTIĞI

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadReservations();
        }

        private void LoadReservations()
        {
            // Verileri veritabanından çekiyoruz
            var allList = _db.Reservations
                .Include(r => r.ReservedFlight).ThenInclude(f => f.DepartureAirport)
                .Include(r => r.ReservedFlight).ThenInclude(f => f.ArrivalAirport)
                .Include(r => r.SelectedSeat)
                .Where(r => r.ReservedCustomer.Id == _currentCustomer.Id)
                .OrderByDescending(r => r.ReservedFlight.DepartureTime)
                .ToList();

            // Tarihe göre ikiye ayır
            var activeReservations = allList.Where(r => r.ReservedFlight.DepartureTime > DateTime.Now).ToList();
            var pastReservations = allList.Where(r => r.ReservedFlight.DepartureTime <= DateTime.Now).ToList();

            // Gridleri Doldur
            FillGrid(gridActive, activeReservations);
            FillGrid(gridPast, pastReservations);

            // Durum mesajı
            lblStatus.Text = $"Aktif: {activeReservations.Count} | Geçmiş: {pastReservations.Count} rezervasyon.";
        }

        // Grid Doldurma Yardımcısı
        private void FillGrid(DataGridView grid, List<Reservation> data)
        {
            grid.DataSource = data.Select(r => new
            {
                PNR = r.Pnr,
                UcusNo = r.ReservedFlight.FlightNumber,
                Nereden = r.ReservedFlight.DepartureAirport.City,
                Nereye = r.ReservedFlight.ArrivalAirport.City,
                Tarih = r.ReservedFlight.DepartureTime,
                Koltuk = r.SelectedSeat != null
                         ? (r.SelectedSeat is FlightReservation.Core.Entities.BusinessSeat ? r.SelectedSeat.SeatNumber + " (Bus)" : r.SelectedSeat.SeatNumber)
                         : "İptal",
                Fiyat = r.PricePaid
            }).ToList();

            // Sütun Ayarları
            if (grid.Columns.Count > 0)
            {
                grid.Columns["PNR"].HeaderText = "PNR";
                grid.Columns["UcusNo"].HeaderText = "Uçuş No";
                grid.Columns["Nereden"].HeaderText = "Kalkış";
                grid.Columns["Nereye"].HeaderText = "Varış";
                grid.Columns["Tarih"].HeaderText = "Tarih";
                grid.Columns["Tarih"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                grid.Columns["Koltuk"].HeaderText = "Koltuk";
                grid.Columns["Fiyat"].HeaderText = "Tutar";
                grid.Columns["Fiyat"].DefaultCellStyle.Format = "C2";
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // Hangi sekmedeyiz kontrol et?
            if (tabControl.SelectedTab.Text.Contains("Geçmiş"))
            {
                MessageBox.Show("Geçmiş uçuşlar iptal edilemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Aktif gridinden seçim yapılmış mı?
            if (gridActive.CurrentRow == null)
            {
                MessageBox.Show("Lütfen iptal etmek için 'Aktif Uçuşlar' listesinden bir seçim yapın.");
                return;
            }

            // İPTAL MANTIĞI
            string pnr = gridActive.CurrentRow.Cells["PNR"].Value.ToString();
            var reservation = _db.Reservations
                .Include(r => r.SelectedSeat)
                .FirstOrDefault(r => r.Pnr == pnr);

            if (reservation != null)
            {
                DialogResult dialogResult = MessageBox.Show($"PNR: {pnr} olan uçuşunuzu iptal etmek istediğinize emin misiniz?", "İptal Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    // Customer sınıfındaki metodu çağır
                    ((Customer)_currentCustomer).CancelReservation(reservation);

                    // Veritabanı işlemleri
                    if (reservation.SelectedSeat != null)
                        _db.Entry(reservation.SelectedSeat).State = EntityState.Modified;

                    _db.Reservations.Remove(reservation);
                    _db.SaveChanges();

                    MessageBox.Show("Rezervasyon başarıyla iptal edildi.");
                    LoadReservations(); // Listeleri yenile
                }
            }
        }
    }
}

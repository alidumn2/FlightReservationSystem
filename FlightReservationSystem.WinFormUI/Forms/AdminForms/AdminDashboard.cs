using FlightReservation.Core;
using FlightReservation.Core.Entities;
using FlightReservation.DataAccess;
using FlightReservation.DataAccess.Context;
using FlightReservation.WinFormUI.Forms.AuthForms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FlightReservation.WinFormUI.Forms.AdminForms
{
    public partial class AdminDashboard : Form
    {
        private AppDbContext _db = new AppDbContext();
        private User _currentAdmin;

        // --- UI Kontrolleri ---
        private TabControl tabControl;
        private TabPage tabFlights, tabPlanes, tabReports;
        private DataGridView gridFlights, gridPlanes;

        // Uçuş Ekleme Kontrolleri
        private TextBox txtFlightNum, txtPrice;
        private DateTimePicker dtFlightDate;
        private ComboBox cmbPlaneSelector, cmbOrigin, cmbDest;

        // Uçak Ekleme Kontrolleri
        private TextBox txtPlaneModel, txtPlaneCapacity;

        // Rapor Label'ları
        private Label lblTotalRevenue, lblTotalReservations, lblPopularFlight;

        private int _flightIdToUpdate = 0; // 0 ise Ekleme modu, >0 ise Güncelleme modu
        private Button btnCancelUpdate; // Güncelleme modundan çıkmak için buton

        public AdminDashboard(User admin)
        {
            _currentAdmin = admin;
            InitializeCustomDesign();
        }

        private void InitializeCustomDesign()
        {
            // Form Ayarları
            this.Text = $"Yönetici Paneli - {_currentAdmin.Name} {_currentAdmin.Surname}";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // ÜST HEADER PANEL
            Panel pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 60;
            pnlHeader.BackColor = Color.FromArgb(44, 62, 80);
            this.Controls.Add(pnlHeader);

            Label lblTitle = new Label();
            lblTitle.Text = "YÖNETİM PANELİ";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 15);
            pnlHeader.Controls.Add(lblTitle);

            // Çıkış Butonu
            Button btnLogout = new Button();
            btnLogout.Text = "Çıkış Yap";
            btnLogout.BackColor = Color.FromArgb(192, 57, 43);
            btnLogout.ForeColor = Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Size = new Size(100, 35);
            btnLogout.Location = new Point(870, 12);
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) =>
            {
                DialogResult secim = MessageBox.Show("Çıkış yapmak istediğinize emin misiniz?", "Çıkış Yap", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (secim == DialogResult.Yes)
                {
                    // Bu formu gizle
                    this.Hide();

                    LoginForm login = new LoginForm();
                    login.ShowDialog(); // ShowDialog programın akışını burada tutar

                    this.Close();
                }
            };
            pnlHeader.Controls.Add(btnLogout);

            // 2. TAB CONTROL (Sekmeler)
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10);
            this.Controls.Add(tabControl);
            tabControl.BringToFront();

            // SEKME 1: UÇUŞ YÖNETİMİ
            tabFlights = new TabPage("Uçuş Yönetimi");
            tabFlights.BackColor = Color.White;
            SetupFlightTab();
            tabControl.TabPages.Add(tabFlights);

            // SEKME 2: UÇAK YÖNETİMİ
            tabPlanes = new TabPage("Uçak/Filo Yönetimi");
            tabPlanes.BackColor = Color.White;
            SetupPlaneTab();
            tabControl.TabPages.Add(tabPlanes);

            // SEKME 3: RAPORLAR
            tabReports = new TabPage("Raporlar ve İstatistikler");
            tabReports.BackColor = Color.White;
            SetupReportTab();
            tabControl.TabPages.Add(tabReports);
        }

        private void SetupFlightTab()
        {
            // 1. ÜST PANEL (Ekleme Formu)
            Panel pnlAddFlight = new Panel
            {
                Dock = DockStyle.Top,
                Height = 180,
                BackColor = Color.AliceBlue,
                BorderStyle = BorderStyle.FixedSingle
            };

            pnlAddFlight.Controls.Add(new Label { Text = "Yeni Uçuş Ekle", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(20, 10), AutoSize = true });

            pnlAddFlight.Controls.Add(new Label { Text = "Uçuş No:", Location = new Point(20, 45), AutoSize = true });
            txtFlightNum = new TextBox { Location = new Point(20, 70), Width = 170 };
            pnlAddFlight.Controls.Add(txtFlightNum);

            pnlAddFlight.Controls.Add(new Label { Text = "Tarih ve Saat:", Location = new Point(20, 105), AutoSize = true });
            dtFlightDate = new DateTimePicker { Location = new Point(20, 130), Width = 170, Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy HH:mm" };
            pnlAddFlight.Controls.Add(dtFlightDate);

            pnlAddFlight.Controls.Add(new Label { Text = "Kalkış Havalimanı:", Location = new Point(208, 45), AutoSize = true });
            cmbOrigin = new ComboBox
            {
                Location = new Point(208, 70),
                Width = 300,
                DropDownWidth = 450,
                DropDownStyle = ComboBoxStyle.DropDown
            };
            pnlAddFlight.Controls.Add(cmbOrigin);

            pnlAddFlight.Controls.Add(new Label { Text = "Varış Havalimanı:", Location = new Point(208, 105), AutoSize = true });
            cmbDest = new ComboBox
            {
                Location = new Point(208, 130),
                Width = 300,
                DropDownWidth = 450,
                DropDownStyle = ComboBoxStyle.DropDown
            };
            pnlAddFlight.Controls.Add(cmbDest);

            pnlAddFlight.Controls.Add(new Label { Text = "Uçak Seçiniz:", Location = new Point(525, 45), AutoSize = true });
            cmbPlaneSelector = new ComboBox { Location = new Point(525, 70), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            pnlAddFlight.Controls.Add(cmbPlaneSelector);

            pnlAddFlight.Controls.Add(new Label { Text = "Taban Fiyat (TL):", Location = new Point(525, 105), AutoSize = true });
            txtPrice = new TextBox { Location = new Point(525, 130), Width = 120 };
            pnlAddFlight.Controls.Add(txtPrice);

            Button btnAdd = new Button
            {
                Text = "UÇUŞU KAYDET", 
                Name = "btnSaveFlight", 
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(740, 128),
                Size = new Size(150, 32),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnAdd.Click += BtnAddFlight_Click;
            pnlAddFlight.Controls.Add(btnAdd);

            btnCancelUpdate = new Button
            {
                Text = "TEMİZLE / YENİ",
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(900, 128),
                Size = new Size(100, 32),
                Visible = false
            };

            btnCancelUpdate.Click += (s, e) => ResetForm(); // Formu sıfırlayan metoda gider
            pnlAddFlight.Controls.Add(btnCancelUpdate);

            // 2. LİSTE (Grid)
            gridFlights = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false
            };

            gridFlights.CellDoubleClick += GridFlights_CellDoubleClick;

            ContextMenuStrip menu = new ContextMenuStrip();

            menu.Items.Add("Seçili Uçuşu Sil", null, BtnDeleteFlight_Click);

            menu.Items.Add("Seçili Uçuşu Düzenle", null, (s, e) => EditSelectedFlight());

            gridFlights.ContextMenuStrip = menu;

            tabFlights.Controls.Add(gridFlights);
            tabFlights.Controls.Add(pnlAddFlight);

            pnlAddFlight.SendToBack();
        }

        // UÇAK SEKMESİ TASARIMI
        private void SetupPlaneTab()
        {
            // Üst Panel
            Panel pnlTop = new Panel { Dock = DockStyle.Top, Height = 80, BackColor = Color.AliceBlue };

            pnlTop.Controls.Add(new Label { Text = "Model:", Location = new Point(20, 15), AutoSize = true });
            txtPlaneModel = new TextBox { Location = new Point(20, 40), Width = 150 };
            pnlTop.Controls.Add(txtPlaneModel);

            pnlTop.Controls.Add(new Label { Text = "Kapasite:", Location = new Point(200, 15), AutoSize = true });
            txtPlaneCapacity = new TextBox { Location = new Point(200, 40), Width = 100 };
            pnlTop.Controls.Add(txtPlaneCapacity);

            Button btnAddPlane = new Button { Text = "Uçak Ekle", BackColor = Color.SteelBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(350, 35), Size = new Size(100, 30) };
            btnAddPlane.Click += BtnAddPlane_Click;
            pnlTop.Controls.Add(btnAddPlane);

            Button btnDelPlane = new Button { Text = "Seçileni Sil", BackColor = Color.IndianRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(470, 35), Size = new Size(100, 30) };
            btnDelPlane.Click += BtnDeletePlane_Click;
            pnlTop.Controls.Add(btnDelPlane);

            // Liste
            gridPlanes = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            tabPlanes.Controls.Add(gridPlanes);
            tabPlanes.Controls.Add(pnlTop);

            // BURASI ÇOK ÖNEMLİ: Paneli zemine itiyoruz
            pnlTop.SendToBack();
        }


        // 3. RAPOR SEKMESİ TASARIMI
        private void SetupReportTab()
        {
            FlowLayoutPanel flow = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            tabReports.Controls.Add(flow);

            lblTotalRevenue = CreateCard("Toplam Ciro", "0 TL", Color.Green, flow);
            lblTotalReservations = CreateCard("Toplam Rezervasyon", "0", Color.Orange, flow);
            lblPopularFlight = CreateCard("En Popüler Uçuş", "-", Color.Purple, flow);

            Button btnRefresh = new Button { Text = "Raporları Yenile", Size = new Size(150, 40), BackColor = Color.Gray, ForeColor = Color.White };
            btnRefresh.Click += (s, e) => LoadData();
            flow.Controls.Add(btnRefresh);
        }

        private Label CreateCard(string title, string value, Color color, Panel parent)
        {
            Panel card = new Panel { Size = new Size(250, 150), BackColor = color, Margin = new Padding(10) };
            Label lblTitle = new Label { Text = title, ForeColor = Color.White, Font = new Font("Segoe UI", 12), Location = new Point(20, 20), AutoSize = true };
            Label lblValue = new Label { Text = value, ForeColor = Color.White, Font = new Font("Segoe UI", 20, FontStyle.Bold), Location = new Point(20, 60), AutoSize = true };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            parent.Controls.Add(card);
            return lblValue;
        }

        // İŞ MANTIĞI VE VERİTABANI İŞLEMLERİ

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
        }

        private void LoadData()
        {
            // 1. Uçakları Yükle
            var planes = _db.Airplanes.ToList();
            gridPlanes.DataSource = planes;

            // ComboBox'ı doldur
            cmbPlaneSelector.DataSource = planes;
            cmbPlaneSelector.DisplayMember = "Model";
            cmbPlaneSelector.ValueMember = "Id";

            var airports = _db.Airports
                .Select(a => new
                {
                    Id = a.Id,
                    FullName = $"{a.City} - {a.Name} ({a.Code})"
                })
                .ToList();

            // 1. Kalkış (Origin)
            cmbOrigin.DataSource = airports;
            cmbOrigin.DisplayMember = "FullName";
            cmbOrigin.ValueMember = "Id";

            cmbOrigin.DropDownStyle = ComboBoxStyle.DropDown;
            cmbOrigin.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbOrigin.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            // 2. Varış (Dest)
            cmbDest.BindingContext = new BindingContext();
            cmbDest.DataSource = airports;
            cmbDest.DisplayMember = "FullName";
            cmbDest.ValueMember = "Id";

            cmbDest.DropDownStyle = ComboBoxStyle.DropDown;
            cmbDest.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbDest.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            // 2. Uçuşları Yükle (Airplane Include ederek)
            var flights = _db.Flights
                .Include(f => f.Airplane)
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .ToList();

            gridFlights.DataSource = flights.Select(f => new
            {
                ID = f.Id,
                No = f.FlightNumber,
                Nereden = f.DepartureAirport != null ? f.DepartureAirport.ToString() : "Silinmiş",
                Nereye = f.ArrivalAirport != null ? f.ArrivalAirport.ToString() : "Silinmiş",
                Tarih = f.DepartureTime,
                Fiyat = f.BasePrice,
                Ucak = f.Airplane != null ? f.Airplane.Model : "Yok"
            }).ToList();

            // 3. Raporları Hesapla
            lblTotalRevenue.Text = _db.Reservations.Sum(r => r.PricePaid).ToString("C2");
            lblTotalReservations.Text = _db.Reservations.Count().ToString() + " Adet";

            // En çok rezerve edilen uçuşu bul
            var popular = _db.Flights
                .Include(f => f.Airplane)
                .ThenInclude(a => a.Seats)
                .OrderByDescending(f => f.Airplane.Seats.Count(s => s.Status == FlightReservation.Core.Enums.SeatStatus.Occupied))
                .FirstOrDefault();

            if (popular != null)
                lblPopularFlight.Text = popular.FlightNumber;

            if (gridFlights.Columns.Count > 0)
            {
                if (gridFlights.Columns["ID"] != null) gridFlights.Columns["ID"].Visible = false;

                if (gridFlights.Columns["Nereden"] != null) gridFlights.Columns["Nereden"].FillWeight = 240;
                if (gridFlights.Columns["Nereye"] != null) gridFlights.Columns["Nereye"].FillWeight = 240;

                if (gridFlights.Columns["No"] != null) gridFlights.Columns["No"].FillWeight = 60;
                if (gridFlights.Columns["Fiyat"] != null) gridFlights.Columns["Fiyat"].FillWeight = 65;
                if (gridFlights.Columns["Tarih"] != null) gridFlights.Columns["Tarih"].FillWeight = 115;
            }
            gridFlights.Refresh();
        }

        // --- UÇUŞ İŞLEMLERİ ---
        private void BtnAddFlight_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbOrigin.SelectedValue == null || cmbDest.SelectedValue == null)
                {
                    MessageBox.Show("Lütfen havalimanı isimlerini listeden seçerek giriniz.\nElle yazdığınız isim listede bulunamadı.", "Hatalı Seçim");
                    return;
                }

                // Validasyonlar
                if (cmbPlaneSelector.SelectedItem == null) { MessageBox.Show("Lütfen bir uçak seçin."); return; }
                if (string.IsNullOrEmpty(txtFlightNum.Text)) { MessageBox.Show("Uçuş No boş olamaz."); return; }

                if (_flightIdToUpdate == 0)
                {
                    // --- EKLEME MODU ---
                    Flight f = ((Admin)_currentAdmin).AddFlight(
                        // Ortak alanları dolduran yardımcı bir metot da yazılabilir ama şimdilik elle yazalım
                        txtFlightNum.Text,
                        (int)cmbOrigin.SelectedValue,
                        (int)cmbDest.SelectedValue,
                        dtFlightDate.Value,
                        (int)cmbPlaneSelector.SelectedValue,
                        Convert.ToDecimal(txtPrice.Text)
                        );
                    // ID üzerinden gitmek daha güvenli

                    _db.Flights.Add(f);
                    MessageBox.Show("Uçuş başarıyla EKLENDİ.");
                }
                else
                {
                    // --- GÜNCELLEME MODU ---
                    var existingFlight = _db.Flights.Find(_flightIdToUpdate);
                    if (existingFlight != null)
                    {
                        var adminUser = (Admin)_currentAdmin;

                        adminUser.UpdateFlight(
                            existingFlight, // Hangi uçuş?
                            txtFlightNum.Text,
                            (int)cmbOrigin.SelectedValue,
                            (int)cmbDest.SelectedValue,
                            (int)cmbPlaneSelector.SelectedValue,
                            dtFlightDate.Value,
                            Convert.ToDecimal(txtPrice.Text)
                        );

                        MessageBox.Show("Uçuş başarıyla GÜNCELLENDİ.");
                    }
                }

                _db.SaveChanges();
                LoadData();   // Listeyi yenile
                ResetForm();  // Formu temizle ve ekleme moduna dön
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void BtnDeleteFlight_Click(object sender, EventArgs e)
        {
            if (gridFlights.CurrentRow != null)
            {
                int id = (int)gridFlights.CurrentRow.Cells["ID"].Value;
                var flight = _db.Flights.Find(id);
                if (flight != null)
                {
                    _db.Flights.Remove(flight);
                    _db.SaveChanges();
                    LoadData();
                }
            }
        }

        // --- UÇAK İŞLEMLERİ ---
        private void BtnAddPlane_Click(object sender, EventArgs e)
        {
            try
            {
                Airplane p = new Airplane(txtPlaneModel.Text, int.Parse(txtPlaneCapacity.Text));
                _db.Airplanes.Add(p);
                _db.SaveChanges();
                LoadData();
                txtPlaneModel.Clear(); txtPlaneCapacity.Clear();
            }
            catch { MessageBox.Show("Bilgileri kontrol edin."); }
        }

        private void BtnDeletePlane_Click(object sender, EventArgs e)
        {
            if (gridPlanes.CurrentRow != null)
            {
                Airplane selected = (Airplane)gridPlanes.CurrentRow.DataBoundItem;
                _db.Airplanes.Remove(selected);
                _db.SaveChanges();
                LoadData();
            }
        }

        // Formu temizleyip "Ekleme Moduna" döndüren metot
        private void ResetForm()
        {
            _flightIdToUpdate = 0;
            txtFlightNum.Clear();
            txtPrice.Clear();
            dtFlightDate.Value = DateTime.Now;

            // Butonları eski haline getir
            Control btnSave = this.Controls.Find("btnSaveFlight", true).FirstOrDefault();
            if (btnSave != null)
            {
                btnSave.Text = "UÇUŞU KAYDET";
                btnSave.BackColor = Color.SeaGreen;
            }

            btnCancelUpdate.Visible = false; // İptal butonunu gizle
            gridFlights.ClearSelection();
        }

        // Seçili uçuşu kutulara dolduran metot
        private void EditSelectedFlight()
        {
            if (gridFlights.CurrentRow == null) return;

            // Seçili ID'yi al
            _flightIdToUpdate = (int)gridFlights.CurrentRow.Cells["ID"].Value;

            // Veritabanından veriyi çek
            var flight = _db.Flights.Find(_flightIdToUpdate);
            if (flight == null) return;

            // Kutuları doldur
            txtFlightNum.Text = flight.FlightNumber;
            txtPrice.Text = flight.BasePrice.ToString();
            dtFlightDate.Value = flight.DepartureTime;

            // ComboBox Seçimleri
            cmbOrigin.SelectedValue = flight.DepartureAirportId;
            cmbDest.SelectedValue = flight.ArrivalAirportId;
            cmbPlaneSelector.SelectedValue = flight.AirplaneId;

            // Görünümü "Güncelleme Moduna" çevir
            Control btnSave = this.Controls.Find("btnSaveFlight", true).FirstOrDefault(); 


            foreach (Control c in ((Panel)tabFlights.Controls[1]).Controls) // Panel içindeki butonları ara
            {
                if (c is Button && c.Text.Contains("KAYDET")) c.Text = "GÜNCELLE";
                if (c is Button && c.Text == "GÜNCELLE") c.BackColor = Color.Orange;
            }

            btnCancelUpdate.Visible = true; // Vazgeçme butonu görünsün
        }

        // Grid'e çift tıklayınca çalışacak olay
        private void GridFlights_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) EditSelectedFlight();
        }
    }
}

using FlightReservation.Core;
using FlightReservation.Core.Entities;
using FlightReservation.Core.Enums;
using FlightReservation.Core.Helpers.Pricing;
using FlightReservation.DataAccess;
using FlightReservation.DataAccess.Context;
using FlightReservation.WinFormUI.Forms.AuthForms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FlightReservation.WinFormUI.Forms.CustomerForms
{
    public partial class CustomerSearchForm : Form
    {
        private AppDbContext _db = new AppDbContext();
        private Core.Entities.Customer _currentCustomer;

        // UI Kontrolleri
        private DataGridView gridFlights;
        private ComboBox cmbOrigin, cmbDest;
        private DateTimePicker dtDate;
        private Label lblSelectedFlightInfo;

        public CustomerSearchForm(User customer)
        {
            _currentCustomer = (Core.Entities.Customer)customer;
            InitializeCustomDesign();
        }

        private void InitializeCustomDesign()
        {
            // Form Ayarları
            this.Text = $"Müşteri Paneli - Hoşgeldiniz, {_currentCustomer.Name}";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // ÜST HEADER
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.FromArgb(52, 152, 219) }; // Açık Mavi
            this.Controls.Add(pnlHeader);

            Label lblTitle = new Label { Text = "UÇUŞ ARAMA VE REZERVASYON", ForeColor = Color.White, Font = new Font("Segoe UI", 18, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) };
            pnlHeader.Controls.Add(lblTitle);

            Button btnMyReservations = new Button
            {
                Text = "Rezervasyonlarım",
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 35),
                Location = new Point(800, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnMyReservations.FlatAppearance.BorderSize = 0;

            // MyReservationsForm'u aç
            btnMyReservations.Click += (s, e) =>
            {
                // Giriş yapan müşteriyi (currentCustomer) parametre olarak gönderiyoruz
                MyPastReservationsForm frm = new MyPastReservationsForm(_currentCustomer);
                frm.ShowDialog();

                // Eski bağlantıyı ve hafızasındaki eski verileri çöpe at
                _db.Dispose();

                // Yeni bir bağlantı aç
                _db = new AppDbContext();

                LoadFlights(cmbOrigin.Text.Trim(), cmbDest.Text.Trim(), dtDate.Value);

                lblSelectedFlightInfo.Text = "Liste güncellendi...";
            };

            pnlHeader.Controls.Add(btnMyReservations);

            Button btnLogout = new Button { Text = "Çıkış Yap", BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 35), Location = new Point(960, 20) };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) =>
            {
                DialogResult secim = MessageBox.Show("Çıkış yapmak istediğinize emin misiniz?", "Çıkış Yap", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (secim == DialogResult.Yes)
                {
                    this.Hide();

                    LoginForm login = new LoginForm();
                    login.ShowDialog();

                    this.Close();
                }
            };
            pnlHeader.Controls.Add(btnLogout);

            // ARAMA FİLTRELERİ
            Panel pnlFilters = new Panel { Dock = DockStyle.Top, Height = 80, BackColor = Color.White };
            this.Controls.Add(pnlFilters);
            pnlFilters.Controls.Add(new Label { Text = "Nereden:", Location = new Point(20, 10), AutoSize = true, ForeColor = Color.Gray });

            // Nereden ComboBox
            cmbOrigin = new ComboBox
            {
                Location = new Point(20, 35),
                Width = 295,
                Font = new Font("Segoe UI", 10),
                DropDownWidth = 350,
                DropDownStyle = ComboBoxStyle.DropDown 
            };
            pnlFilters.Controls.Add(cmbOrigin);


            // Nereye Label
            pnlFilters.Controls.Add(new Label { Text = "Nereye:", Location = new Point(330, 10), AutoSize = true, ForeColor = Color.Gray });

            // Nereye ComboBox
            cmbDest = new ComboBox
            {
                Location = new Point(330, 35),
                Width = 295,
                Font = new Font("Segoe UI", 10),
                DropDownWidth = 350,
                DropDownStyle = ComboBoxStyle.DropDown
            };
            pnlFilters.Controls.Add(cmbDest);

            pnlFilters.Controls.Add(new Label { Text = "Tarih:", Location = new Point(640, 10), AutoSize = true, ForeColor = Color.Gray });
            dtDate = new DateTimePicker { Location = new Point(640, 35), Width = 160, Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10) };
            pnlFilters.Controls.Add(dtDate);

            Button btnSearch = new Button { Text = "UÇUŞ ARA", BackColor = Color.Orange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(820, 32), Size = new Size(120, 32), Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += BtnSearch_Click;
            pnlFilters.Controls.Add(btnSearch);

            Button btnShowAll = new Button { Text = "Tümünü Göster", BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(950, 32), Size = new Size(120, 32) };
            btnShowAll.FlatAppearance.BorderSize = 0;
            btnShowAll.Click += (s, e) => LoadFlights(); // Filtresiz yükle
            pnlFilters.Controls.Add(btnShowAll);

            // ALT REZERVASYON PANELİ
            Panel pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 100, BackColor = Color.White };
            this.Controls.Add(pnlBottom);
            pnlBottom.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, pnlBottom.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);

            // Seçilen Uçuş Bilgisi
            lblSelectedFlightInfo = new Label
            {
                Text = "Lütfen listeden bir uçuş seçiniz...",
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                AutoSize = true,
                Location = new Point(20, 35),
                ForeColor = Color.DarkGray
            };
            pnlBottom.Controls.Add(lblSelectedFlightInfo);

            // KOLTUK SEÇ BUTONU
            Button btnOpenSeatMap = new Button
            {
                Text = "KOLTUK SEÇ \nVE TAMAMLA",
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(850, 15),
                Size = new Size(200, 70),
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnOpenSeatMap.FlatAppearance.BorderSize = 0;
            btnOpenSeatMap.Click += BtnOpenSeatMap_Click; // btnOpenSeatMap_Click metodu
            pnlBottom.Controls.Add(btnOpenSeatMap);

            // ORTA LİSTE
            gridFlights = new DataGridView { Dock = DockStyle.Fill, BackgroundColor = Color.WhiteSmoke, BorderStyle = BorderStyle.None, RowHeadersVisible = false, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            gridFlights.CellClick += GridFlights_CellClick; 
            this.Controls.Add(gridFlights);
            gridFlights.BringToFront();
        }

          // İŞ MANTIĞI
 
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // HAVALİMANLARINI ÇEK VE DOLDUR 
            var airports = _db.Airports
                .Select(a => new {
                    Id = a.Id,
                    FullName = $"{a.City} - {a.Name} ({a.Code})"
                })
                .ToList();

            // Nereden Kutusu
            cmbOrigin.DataSource = airports;
            cmbOrigin.DisplayMember = "FullName";
            cmbOrigin.ValueMember = "Id";
            cmbOrigin.SelectedIndex = -1; // Başlangıçta boş gelsin

            // Arama Ayarları
            cmbOrigin.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbOrigin.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            // 2. Nereye Kutusu (Context ayırarak)
            cmbDest.BindingContext = new BindingContext();
            cmbDest.DataSource = airports; // Listeyi tekrar veriyoruz
            cmbDest.DisplayMember = "FullName";
            cmbDest.ValueMember = "Id";
            cmbDest.SelectedIndex = -1; // Başlangıçta boş gelsin

            // Arama Ayarları
            cmbDest.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbDest.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            // LİSTEYİ YÜKLE
            LoadFlights();
        }

        private void LoadFlights(string fromText = null, string toText = null, DateTime? date = null, int? fromId = null, int? toId = null)
        {
            try
            {
                var query = _db.Flights
                    .Include(f => f.DepartureAirport)
                    .Include(f => f.ArrivalAirport)
                    .Include(f => f.Airplane).ThenInclude(a => a.Seats)
                    .AsQueryable();

                query = query.Where(f => f.DepartureTime > DateTime.Now);

                // KALKIŞ FİLTRESİ
                if (fromId.HasValue && fromId.Value > 0)
                {
                    // Eğer ID seçildiyse eşleşme yap
                    query = query.Where(f => f.DepartureAirportId == fromId.Value);
                }
                else if (!string.IsNullOrEmpty(fromText))
                {
                    // ID yoksa metin araması yap
                    string searchKey = fromText.Trim().ToLower();
                    query = query.Where(f =>
                        f.DepartureAirport.City.ToLower().Contains(searchKey) ||
                        f.DepartureAirport.Name.ToLower().Contains(searchKey) ||
                        f.DepartureAirport.Code.ToLower().Contains(searchKey));
                }

                // VARIŞ FİLTRESİ
                if (toId.HasValue && toId.Value > 0)
                {
                    query = query.Where(f => f.ArrivalAirportId == toId.Value);
                }
                else if (!string.IsNullOrEmpty(toText))
                {
                    string searchKey = toText.Trim().ToLower();
                    query = query.Where(f =>
                        f.ArrivalAirport.City.ToLower().Contains(searchKey) ||
                        f.ArrivalAirport.Name.ToLower().Contains(searchKey) ||
                        f.ArrivalAirport.Code.ToLower().Contains(searchKey));
                }

                // TARİH FİLTRESİ
                if (date.HasValue)
                {
                    query = query.Where(f => f.DepartureTime.Date == date.Value.Date);
                }
                else
                {
                    query = query.Where(f => f.DepartureTime > DateTime.Now);
                }

                var flights = query.ToList();

                var displayList = flights.Select(f => new FlightViewModel
                {
                    ID = f.Id,
                    UcusNo = f.FlightNumber,
                    Nereden = f.DepartureAirport != null ? f.DepartureAirport.ToString() : "???",
                    Nereye = f.ArrivalAirport != null ? f.ArrivalAirport.ToString() : "???",
                    Tarih = f.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Ucak = f.Airplane != null ? f.Airplane.Model : "Atanmamış",

                    Fiyat = (f.Airplane != null)
                            ? CalculateListingPrice(f) + " TL"
                            : f.BasePrice + " TL",

                    BosKoltuk = (f.Airplane != null)
                                ? f.Airplane.Seats.Count - _db.Reservations.Count(r => r.ReservedFlight.Id == f.Id)
                                : 0
                }).ToList();

                // Grid Ayarları ve Bağlama
                gridFlights.DataSource = null;
                gridFlights.Columns.Clear();

                // Veri varsa bağla
                if (displayList.Count > 0)
                {
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = displayList;

                    gridFlights.AutoGenerateColumns = true;
                    gridFlights.DataSource = bindingSource;

                    // ID Kolonunu Gizle
                    if (gridFlights.Columns["ID"] != null) gridFlights.Columns["ID"].Visible = false;

                    if (gridFlights.Columns.Count > 0)
                    {
                        gridFlights.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        gridFlights.Columns["Nereden"].FillWeight = 250; 
                        gridFlights.Columns["Nereye"].FillWeight = 250; 

                        gridFlights.Columns["UcusNo"].FillWeight = 70;
                        gridFlights.Columns["Ucak"].FillWeight = 80;
                        gridFlights.Columns["Fiyat"].FillWeight = 80;
                        gridFlights.Columns["BosKoltuk"].FillWeight = 70;
                        gridFlights.Columns["Tarih"].FillWeight = 110;

                        gridFlights.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {

                }

                gridFlights.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // ComboBox'tan seçilen ID'yi almaya çalışıyoruz
            int? sourceId = cmbOrigin.SelectedValue as int?;
            int? targetId = cmbDest.SelectedValue as int?;

            // Metinleri alırız
            string sourceText = cmbOrigin.Text;
            string targetText = cmbDest.Text;

            // Yeni parametrelerle metodu çağır
            LoadFlights(sourceText, targetText, dtDate.Value, sourceId, targetId);
        }

        private void GridFlights_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridFlights.CurrentRow == null) return;

            // Seçilen satırdaki ID'yi al
            int flightId = (int)gridFlights.CurrentRow.Cells["ID"].Value;

            // Veritabanından o uçuşu ve koltuklarını bul
            var flight = _db.Flights
                .Include(f => f.Airplane)
                .ThenInclude(a => a.Seats)
                .FirstOrDefault(f => f.Id == flightId);

            if (flight != null)
            {
                // Alt bilgi panelini güncelle
                lblSelectedFlightInfo.Text = $"{flight.FlightNumber} | {flight.DepartureAirport} -> {flight.ArrivalAirport} seçildi.";
                lblSelectedFlightInfo.ForeColor = Color.Black;
            }
        }

        private void BtnOpenSeatMap_Click(object sender, EventArgs e)
        {
            if (gridFlights.CurrentRow == null) return;
            int flightId = (int)gridFlights.CurrentRow.Cells["ID"].Value;

            // Uçuş verilerini çek
            var fullFlightData = _db.Flights
                .Include(f => f.Airplane).ThenInclude(a => a.Seats)
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .FirstOrDefault(f => f.Id == flightId);

            // Dolu koltukları çek
            var takenSeatIds = _db.Reservations
                .Where(r => r.ReservedFlight.Id == flightId)
                .Select(r => r.SelectedSeat.Id)
                .ToList();

            // FİYATI HESAPLA
            decimal calculatedPrice = CalculateListingPrice(fullFlightData);

            // Formu açarken hesaplanmış fiyatı da gönderiyoruz
            using (SeatSelectionForm seatForm = new SeatSelectionForm(fullFlightData, takenSeatIds, calculatedPrice))
            {
                if (seatForm.ShowDialog() == DialogResult.OK)
                {
                    // Rezervasyon yaparken de occupancyCount (doluluk sayısı) gönderiyoruz
                    int occupancyCount = takenSeatIds.Count;

                    // Rezervasyonu tamamla
                    CompleteReservationProcess(fullFlightData, seatForm.SelectedSeat);
                }
            }
        }
        
        private void CompleteReservationProcess(Flight flight, Seat seat)
        {
            try
            {
                // Müşteriyi bul (Context takibi için)
                var customer = _db.Customers.Find(_currentCustomer.Id);

                // Koltuğu veritabanından çek 
                var dbSeat = _db.Seats.Find(seat.Id);

                // Fiyatı Hesapla
                int occupancyCount = _db.Reservations.Count(r => r.ReservedFlight.Id == flight.Id);

                // Rezervasyon Nesnesini Oluştur
                Reservation res = customer.MakeReservation(flight, dbSeat, occupancyCount);


                // Kaydet
                _db.Reservations.Add(res);
                _db.SaveChanges();

                MessageBox.Show($"Rezervasyonunuz başarıyla oluşturuldu!\n\nPNR: {res.Pnr}\nKoltuk: {dbSeat.SeatNumber}\nFiyat: {res.PricePaid:C2}",
                                "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ekranı Yenile
                LoadFlights();
                lblSelectedFlightInfo.Text = "Lütfen yukarıdan bir uçuş seçiniz...";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyon sırasında hata oluştu: " + ex.Message);
            }
        }

        private decimal CalculateListingPrice(Flight flight)
        {
            // O uçuşun doluluk sayısını bul
            int filledSeats = _db.Reservations.Count(r => r.ReservedFlight.Id == flight.Id);

            // Motoru kur
            PriceCalculatorEngine engine = new PriceCalculatorEngine();
            engine.AddRule(new OccupancyRule(filledSeats)); // Doluluk kuralını ekle

            // Fiyatı hesapla (Koltuk null gönderiyoruz çünkü standart fiyatı istiyoruz)
            return engine.CalculateFinalPrice(flight, null);
        }

    }

    // FlightViewModel : Uçuş listesini göstermek için basit bir ViewModel
    public class FlightViewModel
    {
        public int ID { get; set; }
        public string UcusNo { get; set; }
        public string Nereden { get; set; }
        public string Nereye { get; set; }
        public string Tarih { get; set; }
        public string Ucak { get; set; }
        public string Fiyat { get; set; }
        public int BosKoltuk { get; set; }
    }
}
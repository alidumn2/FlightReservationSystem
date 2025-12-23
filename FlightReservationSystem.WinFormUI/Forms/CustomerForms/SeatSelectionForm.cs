using FlightReservation.Core.Entities;
using FlightReservation.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace FlightReservation.WinFormUI.Forms.CustomerForms
{
    public partial class SeatSelectionForm : Form
    {
        public Seat SelectedSeat { get; private set; }
        private Flight _currentFlight;
        private Panel _pnlSeats;
        private Label _lblInfo;
        private List<int> _reservedSeatIds;
        private System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
        private decimal _currentCalculatedPrice;

        public SeatSelectionForm(Flight flight, List<int> reservedSeatIds, decimal currentPrice)
        {
            _currentFlight = flight;
            _reservedSeatIds = reservedSeatIds;
            _currentCalculatedPrice = currentPrice;
            InitializeComponent();
            SetupCustomDesign();
            GenerateSeatMap();
        }

        private void SetupCustomDesign()
        {
            // Form Ayarları
            this.Text = $"Koltuk Seçimi - Uçuş: {_currentFlight.FlightNumber}";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.BackColor = Color.White;

            // Başlık
            Label lblTitle = new Label
            {
                Text = $"{_currentFlight.DepartureAirport.City} -> {_currentFlight.ArrivalAirport.City} | {_currentFlight.Airplane.Model}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20),
                ForeColor = Color.DarkSlateGray
            };
            this.Controls.Add(lblTitle);

            // Bilgi Label
            _lblInfo = new Label
            {
                Text = "Lütfen aşağıdan bir koltuk seçiniz.",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(20, 50),
                ForeColor = Color.Gray
            };
            this.Controls.Add(_lblInfo);

            // RENK AÇIKLAMALARI
            CreateLegendItem(500, 30, Color.IndianRed, "Dolu");
            CreateLegendItem(570, 30, Color.MediumSeaGreen, "Eco");
            CreateLegendItem(630, 30, Color.Gold, "Bus (+%50)");
            CreateLegendItem(720, 30, Color.Orange, "Seçili");

            // Standart Panel
            _pnlSeats = new Panel
            {
                Location = new Point(20, 80),
                Size = new Size(840, 500), 
                AutoScroll = true, 
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke,
            };
            this.Controls.Add(_pnlSeats);

            // Onay Butonu
            Button btnConfirm = new Button
            {
                Text = "SEÇİMİ ONAYLA",
                Size = new Size(200, 50),
                Location = new Point(660, 600),
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnConfirm.Click += BtnConfirm_Click;
            this.Controls.Add(btnConfirm);

            // İptal Butonu
            Button btnCancel = new Button
            {
                Text = "İptal",
                Size = new Size(100, 50),
                Location = new Point(550, 600),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancel);
        }

        private void CreateLegendItem(int x, int y, Color color, string text)
        {
            Panel pnlBox = new Panel
            {
                Size = new Size(15, 15),
                Location = new Point(x, y + 3),
                BackColor = color,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(pnlBox);

            Label lblText = new Label
            {
                Text = text,
                Location = new Point(x + 20, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Black
            };
            this.Controls.Add(lblText);
        }

        // GenerateSeatMap metodu koltuk haritasını oluşturur
        private void GenerateSeatMap()
        {
            _pnlSeats.Controls.Clear();
            if (_currentFlight.Airplane == null) return;

            var sortedSeats = _currentFlight.Airplane.Seats.OrderBy(s => s.Id).ToList();

            // AYARLAR
            int startX = 180;
            int startY = 20;
            int seatW = 50;
            int seatH = 50;
            int gap = 10;  
            int aisleGap = 70;

            int seatsPerRow = 6;

            for (int i = 0; i < sortedSeats.Count; i++)
            {
                var seat = sortedSeats[i];

                // Matematiksel Konum Hesaplama
                int rowIndex = i / seatsPerRow;
                int colIndex = i % seatsPerRow;

                // X konumu hesapla
                int xPos = startX + (colIndex * (seatW + gap));

                // 3. koltuktan sonra (index 0,1,2 bittiğinde) koridor koy
                if (colIndex >= 3)
                {
                    xPos += aisleGap;
                }

                // Y konumu hesapla
                int yPos = startY + (rowIndex * (seatH + gap));

                // Butonu Oluştur
                Button btnSeat = new Button
                {
                    Size = new Size(seatW, seatH),
                    Location = new Point(xPos, yPos),
                    FlatStyle = FlatStyle.Flat,
                    Tag = seat,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold)
                };

                // İsimlendirme (1A, 1B, 1C - 1D, 1E, 1F)
                btnSeat.Text = GetSeatName(rowIndex, colIndex);

                // RENKLENDİRME
                if (seat is BusinessSeat)
                {
                    btnSeat.BackColor = Color.Gold;
                    toolTip1.SetToolTip(btnSeat, $"Business Class\n{seat.SeatNumber}\nFiyat: {seat.CalculatePrice(_currentCalculatedPrice):C2}");
                }
                else
                {
                    btnSeat.BackColor = Color.MediumSeaGreen;
                    toolTip1.SetToolTip(btnSeat, $"Economy Class\n{seat.SeatNumber}\nFiyat: {seat.CalculatePrice(_currentCalculatedPrice):C2}");
                }

                // Doluluk Kontrolü
                if (_reservedSeatIds != null && _reservedSeatIds.Contains(seat.Id))
                {
                    btnSeat.BackColor = Color.IndianRed;
                    btnSeat.Enabled = false;
                    btnSeat.Text = "X";
                }
                else
                {
                    btnSeat.Cursor = Cursors.Hand;
                    btnSeat.Click += Seat_Click;
                }

                _pnlSeats.Controls.Add(btnSeat);
            }
        }

        //HARFLENDİRME (GetSeatName)
        private string GetSeatName(int rowIndex, int colIndex)
        {
            int rowNum = rowIndex + 1;
            string letter = "";

            switch (colIndex)
            {
                case 0: letter = "A"; break; 
                case 1: letter = "B"; break; 
                case 2: letter = "C"; break; 

                case 3: letter = "D"; break; 
                case 4: letter = "E"; break;
                case 5: letter = "F"; break; 

                default: letter = "?"; break;
            }
            return $"{rowNum}{letter}";
        }

        private void Seat_Click(object sender, EventArgs e)
        {
            // Önceki seçimi temizle (Rengi eski haline döndür)
            foreach (Control c in _pnlSeats.Controls)
            {
                if (c is Button b && b.BackColor == Color.Orange)
                {
                    Seat s = (Seat)b.Tag;
                    if (s is BusinessSeat) b.BackColor = Color.Gold;
                    else b.BackColor = Color.MediumSeaGreen;
                }
            }

            // Yeni seçilen butonu turuncu yap
            Button clicked = (Button)sender;
            clicked.BackColor = Color.Orange;

            // Veriyi al ve Label'a yaz
            SelectedSeat = (Seat)clicked.Tag;

            // Fiyat hesapla
            decimal seatPrice = SelectedSeat.CalculatePrice(_currentCalculatedPrice);

            _lblInfo.Text = $"Seçilen Koltuk: {clicked.Text} ({SelectedSeat.SeatNumber}) | Tutar: {seatPrice:C2}";
            _lblInfo.ForeColor = Color.Black;
            _lblInfo.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (SelectedSeat == null)
            {
                MessageBox.Show("Lütfen bir koltuk seçiniz.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
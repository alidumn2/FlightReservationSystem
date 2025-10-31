namespace FlightRezervation.WinFormUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblKalkis = new Label();
            lblUcuslar = new Label();
            lblKoltuklar = new Label();
            txtDeparture = new TextBox();
            txtArrival = new TextBox();
            btnSearch = new Button();
            lblVaris = new Label();
            listSeats = new ListBox();
            btnReserve = new Button();
            listFlights = new ListBox();
            listReservations = new ListBox();
            btnListReservations = new Button();
            btnCancelReservation = new Button();
            SuspendLayout();
            // 
            // lblKalkis
            // 
            lblKalkis.AutoSize = true;
            lblKalkis.Location = new Point(88, 143);
            lblKalkis.Name = "lblKalkis";
            lblKalkis.Size = new Size(78, 20);
            lblKalkis.TabIndex = 0;
            lblKalkis.Text = "Kalkış Yeri:";
            lblKalkis.Click += label1_Click;
            // 
            // lblUcuslar
            // 
            lblUcuslar.AutoSize = true;
            lblUcuslar.Location = new Point(384, 40);
            lblUcuslar.Name = "lblUcuslar";
            lblUcuslar.Size = new Size(106, 20);
            lblUcuslar.TabIndex = 1;
            lblUcuslar.Text = "Uygun Uçuşlar:";
            lblUcuslar.Click += label2_Click;
            // 
            // lblKoltuklar
            // 
            lblKoltuklar.AutoSize = true;
            lblKoltuklar.Location = new Point(384, 286);
            lblKoltuklar.Name = "lblKoltuklar";
            lblKoltuklar.Size = new Size(117, 20);
            lblKoltuklar.TabIndex = 2;
            lblKoltuklar.Text = "Uygun Koltuklar:\r\n";
            // 
            // txtDeparture
            // 
            txtDeparture.Location = new Point(88, 166);
            txtDeparture.Name = "txtDeparture";
            txtDeparture.Size = new Size(125, 27);
            txtDeparture.TabIndex = 3;
            txtDeparture.TextChanged += txtName_TextChanged;
            // 
            // txtArrival
            // 
            txtArrival.Location = new Point(260, 166);
            txtArrival.Name = "txtArrival";
            txtArrival.Size = new Size(125, 27);
            txtArrival.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(184, 223);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Uçuş Ara\r\n";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // lblVaris
            // 
            lblVaris.AutoSize = true;
            lblVaris.Location = new Point(260, 143);
            lblVaris.Name = "lblVaris";
            lblVaris.Size = new Size(71, 20);
            lblVaris.TabIndex = 7;
            lblVaris.Text = "Varış Yeri:";
            lblVaris.Click += label4_Click;
            // 
            // listSeats
            // 
            listSeats.FormattingEnabled = true;
            listSeats.Location = new Point(510, 286);
            listSeats.Name = "listSeats";
            listSeats.Size = new Size(180, 144);
            listSeats.TabIndex = 8;
            // 
            // btnReserve
            // 
            btnReserve.Location = new Point(184, 358);
            btnReserve.Name = "btnReserve";
            btnReserve.Size = new Size(149, 29);
            btnReserve.TabIndex = 9;
            btnReserve.Text = "Rezervasyon Yap";
            btnReserve.UseVisualStyleBackColor = true;
            btnReserve.Click += btnReserve_Click;
            // 
            // listFlights
            // 
            listFlights.FormattingEnabled = true;
            listFlights.Location = new Point(510, 40);
            listFlights.Name = "listFlights";
            listFlights.Size = new Size(180, 144);
            listFlights.TabIndex = 10;
            listFlights.SelectedIndexChanged += listFlights_SelectedIndexChanged;
            // 
            // listReservations
            // 
            listReservations.FormattingEnabled = true;
            listReservations.Location = new Point(748, 40);
            listReservations.Name = "listReservations";
            listReservations.Size = new Size(557, 244);
            listReservations.TabIndex = 11;
            listReservations.SelectedIndexChanged += listReservations_SelectedIndexChanged;
            // 
            // btnListReservations
            // 
            btnListReservations.Location = new Point(765, 307);
            btnListReservations.Name = "btnListReservations";
            btnListReservations.Size = new Size(178, 26);
            btnListReservations.TabIndex = 12;
            btnListReservations.Text = "Reservasyonları Listele";
            btnListReservations.UseVisualStyleBackColor = true;
            btnListReservations.Click += btnListReservations_Click;
            // 
            // btnCancelReservation
            // 
            btnCancelReservation.Location = new Point(1086, 311);
            btnCancelReservation.Name = "btnCancelReservation";
            btnCancelReservation.Size = new Size(155, 29);
            btnCancelReservation.TabIndex = 13;
            btnCancelReservation.Text = "Reservasyon İptal";
            btnCancelReservation.UseVisualStyleBackColor = true;
            btnCancelReservation.Click += btnCancelReservation_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1383, 604);
            Controls.Add(btnCancelReservation);
            Controls.Add(btnListReservations);
            Controls.Add(listReservations);
            Controls.Add(listFlights);
            Controls.Add(btnReserve);
            Controls.Add(listSeats);
            Controls.Add(lblVaris);
            Controls.Add(btnSearch);
            Controls.Add(txtArrival);
            Controls.Add(txtDeparture);
            Controls.Add(lblKoltuklar);
            Controls.Add(lblUcuslar);
            Controls.Add(lblKalkis);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblKalkis;
        private Label lblUcuslar;
        private Label lblKoltuklar;
        private TextBox txtDeparture;
        private TextBox txtArrival;
        private Button btnSearch;
        private Label lblVaris;
        private Button btnReserve;
        private ListBox listFlights;
        private ListBox listReservations;
        private Button btnListReservations;
        private Button btnCancelReservation;
        protected ListBox listSeats;
    }
}

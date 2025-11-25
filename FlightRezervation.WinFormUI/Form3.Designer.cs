namespace FlightReservation.WinFormUI
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            addFlight = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label1 = new Label();
            Tarih = new Label();
            dtDepartureTime = new DateTimePicker();
            txtArrivalCity = new TextBox();
            txtDepartureCity = new TextBox();
            txtFlightNumber = new TextBox();
            txtBasePrice = new TextBox();
            listLoadFlights = new ListBox();
            btnGoBack = new Button();
            SuspendLayout();
            // 
            // addFlight
            // 
            addFlight.Location = new Point(231, 348);
            addFlight.Name = "addFlight";
            addFlight.Size = new Size(133, 35);
            addFlight.TabIndex = 0;
            addFlight.Text = "Uçuş Ekle";
            addFlight.UseVisualStyleBackColor = true;
            addFlight.Click += addFlight_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(155, 181);
            label2.Name = "label2";
            label2.Size = new Size(47, 20);
            label2.TabIndex = 2;
            label2.Text = "Varış :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(131, 92);
            label3.Name = "label3";
            label3.Size = new Size(71, 20);
            label3.TabIndex = 3;
            label3.Text = "Uçuş No :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(155, 223);
            label4.Name = "label4";
            label4.Size = new Size(47, 20);
            label4.TabIndex = 4;
            label4.Text = "Fiyat :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(148, 137);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 9;
            label1.Text = "Kalkış :";
            // 
            // Tarih
            // 
            Tarih.AutoSize = true;
            Tarih.Location = new Point(155, 274);
            Tarih.Name = "Tarih";
            Tarih.Size = new Size(47, 20);
            Tarih.TabIndex = 10;
            Tarih.Text = "Tarih :";
            // 
            // dtDepartureTime
            // 
            dtDepartureTime.Location = new Point(231, 269);
            dtDepartureTime.Name = "dtDepartureTime";
            dtDepartureTime.Size = new Size(205, 27);
            dtDepartureTime.TabIndex = 11;
            // 
            // txtArrivalCity
            // 
            txtArrivalCity.Location = new Point(231, 181);
            txtArrivalCity.Name = "txtArrivalCity";
            txtArrivalCity.Size = new Size(152, 27);
            txtArrivalCity.TabIndex = 12;
            // 
            // txtDepartureCity
            // 
            txtDepartureCity.Location = new Point(231, 137);
            txtDepartureCity.Name = "txtDepartureCity";
            txtDepartureCity.Size = new Size(152, 27);
            txtDepartureCity.TabIndex = 13;
            // 
            // txtFlightNumber
            // 
            txtFlightNumber.Location = new Point(231, 92);
            txtFlightNumber.Name = "txtFlightNumber";
            txtFlightNumber.Size = new Size(152, 27);
            txtFlightNumber.TabIndex = 14;
            // 
            // txtBasePrice
            // 
            txtBasePrice.Location = new Point(231, 223);
            txtBasePrice.Name = "txtBasePrice";
            txtBasePrice.Size = new Size(152, 27);
            txtBasePrice.TabIndex = 15;
            // 
            // listLoadFlights
            // 
            listLoadFlights.FormattingEnabled = true;
            listLoadFlights.Location = new Point(511, 74);
            listLoadFlights.Name = "listLoadFlights";
            listLoadFlights.Size = new Size(426, 384);
            listLoadFlights.TabIndex = 16;
            // 
            // btnGoBack
            // 
            btnGoBack.Location = new Point(12, 12);
            btnGoBack.Name = "btnGoBack";
            btnGoBack.Size = new Size(95, 52);
            btnGoBack.TabIndex = 17;
            btnGoBack.Text = "Geri Dön";
            btnGoBack.UseVisualStyleBackColor = true;
            btnGoBack.Click += btnGoBack_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1032, 583);
            Controls.Add(btnGoBack);
            Controls.Add(listLoadFlights);
            Controls.Add(txtBasePrice);
            Controls.Add(txtFlightNumber);
            Controls.Add(txtDepartureCity);
            Controls.Add(txtArrivalCity);
            Controls.Add(dtDepartureTime);
            Controls.Add(Tarih);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(addFlight);
            Name = "Form3";
            Text = "Form3";
            Load += Form3_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button addFlight;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label1;
        private Label Tarih;
        private DateTimePicker dtDepartureTime;
        private TextBox txtArrivalCity;
        private TextBox txtDepartureCity;
        private TextBox txtFlightNumber;
        private TextBox txtBasePrice;
        private ListBox listLoadFlights;
        private Button btnGoBack;
    }
}

namespace FlightReservation.WinFormUI
{
    partial class Form2
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
            listReservations = new ListBox();
            btnCancelReservation = new Button();
            btnGoBack = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // listReservations
            // 
            listReservations.FormattingEnabled = true;
            listReservations.Location = new Point(301, 85);
            listReservations.Name = "listReservations";
            listReservations.Size = new Size(456, 324);
            listReservations.TabIndex = 0;
            // 
            // btnCancelReservation
            // 
            btnCancelReservation.Location = new Point(440, 437);
            btnCancelReservation.Name = "btnCancelReservation";
            btnCancelReservation.Size = new Size(171, 33);
            btnCancelReservation.TabIndex = 1;
            btnCancelReservation.Text = "Rezervasyon İptal Et";
            btnCancelReservation.UseVisualStyleBackColor = true;
            btnCancelReservation.Click += btnCancelReservation_Click;
            // 
            // btnGoBack
            // 
            btnGoBack.Location = new Point(25, 21);
            btnGoBack.Name = "btnGoBack";
            btnGoBack.Size = new Size(95, 52);
            btnGoBack.TabIndex = 3;
            btnGoBack.Text = "Geri Dön";
            btnGoBack.UseVisualStyleBackColor = true;
            btnGoBack.Click += btnGoBack_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(301, 53);
            label1.Name = "label1";
            label1.Size = new Size(115, 20);
            label1.TabIndex = 4;
            label1.Text = "Rezervasyonlar :";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1032, 583);
            Controls.Add(label1);
            Controls.Add(btnGoBack);
            Controls.Add(btnCancelReservation);
            Controls.Add(listReservations);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listReservations;
        private Button btnCancelReservation;
        private Button btnGoBack;
        private Label label1;
    }
}
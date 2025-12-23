using FlightReservation.Core.Helpers;
using FlightReservation.Core.Entities;  
using FlightReservation.DataAccess.Context;
using System.Runtime.InteropServices;

namespace FlightReservation.WinFormUI.Forms.AuthForms
{
    public partial class RegisterForm : Form
    {
        private AppDbContext _db = new AppDbContext();

        // UI Kontrolleri
        private Panel pnlHeader;
        private Label lblTitle;
        private Button btnClose;

        private TextBox txtName, txtSurname, txtTcNo, txtUsername, txtPassword;
        private Button btnRegister, btnLoginLink;

        // Sürükleme Özelliği İçin
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public RegisterForm()
        {
            InitializeCustomDesign();
        }

        private void InitializeCustomDesign()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(400, 600);
            this.BackColor = Color.White;

            // Çerçeve Kenarlığı
            this.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
            };

            // ÜST PANEL
            pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 50;
            pnlHeader.BackColor = Color.FromArgb(41, 128, 185);
            pnlHeader.MouseDown += (s, e) => { ReleaseCapture(); SendMessage(this.Handle, 0x112, 0xf012, 0); };
            this.Controls.Add(pnlHeader);

            lblTitle = new Label();
            lblTitle.Text = "YENİ ÜYELİK";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(15, 13);
            pnlHeader.Controls.Add(lblTitle);

            btnClose = new Button();
            btnClose.Text = "X";
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.ForeColor = Color.White;
            btnClose.Font = new Font("Verdana", 12, FontStyle.Bold);
            btnClose.Size = new Size(40, 50);
            btnClose.Dock = DockStyle.Right;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (s, e) => this.Close();
            pnlHeader.Controls.Add(btnClose);

            // FORM ELEMANLARI
            int startY = 80;
            int gap = 60;

            // Ad
            CreateInputLabel("Adınız:", 30, startY);
            txtName = CreateTextBox(30, startY + 25);

            // Soyad
            CreateInputLabel("Soyadınız:", 30, startY + gap);
            txtSurname = CreateTextBox(30, startY + gap + 25);

            // TC No
            CreateInputLabel("TC Kimlik No:", 30, startY + gap * 2);
            txtTcNo = CreateTextBox(30, startY + gap * 2 + 25);
            txtTcNo.MaxLength = 11; // TC 11 hanelidir

            // Kullanıcı Adı
            CreateInputLabel("Kullanıcı Adı:", 30, startY + gap * 3);
            txtUsername = CreateTextBox(30, startY + gap * 3 + 25);

            // Şifre
            CreateInputLabel("Şifre:", 30, startY + gap * 4);
            txtPassword = CreateTextBox(30, startY + gap * 4 + 25);
            txtPassword.UseSystemPasswordChar = true;

            // Kayıt Ol Butonu
            btnRegister = new Button();
            btnRegister.Text = "KAYDI TAMAMLA";
            btnRegister.BackColor = Color.FromArgb(46, 204, 113); // Yeşil Renk
            btnRegister.ForeColor = Color.White;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnRegister.Size = new Size(340, 45);
            btnRegister.Location = new Point(30, 480);
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.Click += BtnRegister_Click;
            this.Controls.Add(btnRegister);

            // "Zaten üye misin?" Linki
            btnLoginLink = new Button();
            btnLoginLink.Text = "Zaten hesabın var mı? Giriş Yap";
            btnLoginLink.BackColor = Color.Transparent;
            btnLoginLink.ForeColor = Color.Gray;
            btnLoginLink.FlatStyle = FlatStyle.Flat;
            btnLoginLink.FlatAppearance.BorderSize = 0;
            btnLoginLink.Font = new Font("Segoe UI", 9, FontStyle.Underline);
            btnLoginLink.AutoSize = true;
            btnLoginLink.Location = new Point(100, 540);
            btnLoginLink.Cursor = Cursors.Hand;
            btnLoginLink.Click += (s, e) => this.Close();
            this.Controls.Add(btnLoginLink);
        }

        // Yardımcı Metod: Label Oluşturucu
        private void CreateInputLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lbl.ForeColor = Color.DimGray;
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            this.Controls.Add(lbl);
        }

        // Yardımcı Metod: TextBox Oluşturucu
        private TextBox CreateTextBox(int x, int y)
        {
            TextBox txt = new TextBox();
            txt.Font = new Font("Segoe UI", 11);
            txt.Size = new Size(340, 30);
            txt.Location = new Point(x, y);
            this.Controls.Add(txt);
            return txt;
        }

        // İŞ MANTIĞI

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Boş Alan Kontrolü
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtSurname.Text) ||
                string.IsNullOrWhiteSpace(txtTcNo.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtTcNo.TextLength != 11 || txtTcNo.Text.LastOrDefault() % 2 != 0)
            { 
                MessageBox.Show("Lütfen TC kimlik numarasını doğru girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(txtPassword.TextLength < 8)
            {
                MessageBox.Show("Lütfen şifreyi minimum 8 haneli giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kullanıcı Adı Kontrolü (Veritabanında var mı?)
            if (_db.Users.Any(u => u.UserName == txtUsername.Text))
            {
                MessageBox.Show("Bu kullanıcı adı zaten alınmış. Lütfen başka bir tane deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Yeni Müşteri Oluşturma
            Customer newCustomer = new Customer
            {
                Name = txtName.Text.Trim(),
                Surname = txtSurname.Text.Trim(),
                TCKimlikNo = txtTcNo.Text.Trim(),
                UserName = txtUsername.Text.Trim(),
                Password = SecurityHelper.HashPassword(txtPassword.Text.Trim())
            };

            try
            {
                _db.Users.Add(newCustomer);
                _db.SaveChanges(); // Veritabanına kaydet

                MessageBox.Show("Kayıt işlemi başarıyla tamamlandı! Giriş yapabilirsiniz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

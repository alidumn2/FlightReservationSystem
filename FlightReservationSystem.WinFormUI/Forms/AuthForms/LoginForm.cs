using FlightReservation.Core.Entities;
using FlightReservation.Core.Helpers;
using FlightReservation.DataAccess.Context;
using FlightReservation.WinFormUI.Forms.AdminForms;
using FlightReservation.WinFormUI.Forms.CustomerForms;
using FlightReservation.WinFormUI.Forms.AuthForms;
using System.Runtime.InteropServices;
using CustomerSearchForm = FlightReservation.WinFormUI.Forms.CustomerForms.CustomerSearchForm;

namespace FlightReservation.WinFormUI.Forms.AuthForms
{
    public partial class LoginForm : Form
    {
        // Veritabanı Bağlantısı
        private AppDbContext _db = new AppDbContext();

        private Panel pnlLeft;
        private Label lblTitle;
        private Label lblSubtitle;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
        private Button btnClose;
        private Label lblUsername;
        private Label lblPassword;

        // Formu sürüklemek için gerekli kodlar (Borderless Form)
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public LoginForm()
        {
            InitializeCustomDesign();
        }

        // TASARIM KODLARI
        private void InitializeCustomDesign()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(750, 500);
            this.BackColor = Color.White;

            // SOL PANEL
            pnlLeft = new Panel();
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Width = 300;
            pnlLeft.BackColor = Color.FromArgb(41, 128, 185);
            pnlLeft.MouseDown += Form_MouseDown;
            this.Controls.Add(pnlLeft);

            // Başlık
            lblTitle = new Label();
            lblTitle.Text = "UÇUŞ\nREZERVASYON\nSİSTEMİ";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(10, 150);
            pnlLeft.Controls.Add(lblTitle);

            // Alt Başlık
            lblSubtitle = new Label();
            lblSubtitle.Text = "Hızlı • Güvenli • Kolay";
            lblSubtitle.ForeColor = Color.WhiteSmoke;
            lblSubtitle.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(15, 320);
            pnlLeft.Controls.Add(lblSubtitle);

            // KAPATMA BUTONU
            btnClose = new Button();
            btnClose.Text = "X";
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.ForeColor = Color.FromArgb(41, 128, 185);
            btnClose.Font = new Font("Verdana", 16, FontStyle.Bold);
            btnClose.Size = new Size(40, 40);
            btnClose.Location = new Point(710, 0);
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnClose);

            // SAĞ TARAF
            Label lblLoginHeader = new Label();
            lblLoginHeader.Text = "GİRİŞ YAP";
            lblLoginHeader.ForeColor = Color.FromArgb(41, 128, 185);
            lblLoginHeader.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblLoginHeader.AutoSize = true;
            lblLoginHeader.Location = new Point(340, 50);
            this.Controls.Add(lblLoginHeader);

            // Kullanıcı Adı
            lblUsername = new Label();
            lblUsername.Text = "Kullanıcı Adı:";
            lblUsername.Font = new Font("Segoe UI", 10);
            lblUsername.ForeColor = Color.Gray;
            lblUsername.Location = new Point(345, 130);
            lblUsername.AutoSize = true;
            this.Controls.Add(lblUsername);

            txtUsername = new TextBox();
            txtUsername.Font = new Font("Segoe UI", 12);
            txtUsername.Size = new Size(350, 30);
            txtUsername.Location = new Point(350, 155);
            this.Controls.Add(txtUsername);

            // Şifre
            lblPassword = new Label();
            lblPassword.Text = "Şifre:";
            lblPassword.Font = new Font("Segoe UI", 10);
            lblPassword.ForeColor = Color.Gray;
            lblPassword.Location = new Point(345, 210);
            lblPassword.AutoSize = true;
            this.Controls.Add(lblPassword);

            txtPassword = new TextBox();
            txtPassword.Font = new Font("Segoe UI", 12);
            txtPassword.Size = new Size(350, 30);
            txtPassword.Location = new Point(350, 235);
            txtPassword.UseSystemPasswordChar = true;
            this.Controls.Add(txtPassword);

            // Giriş Butonu
            btnLogin = new Button();
            btnLogin.Text = "GİRİŞ YAP";
            btnLogin.BackColor = Color.FromArgb(41, 128, 185);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnLogin.Size = new Size(150, 40);
            btnLogin.Location = new Point(350, 300);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);

            // Kayıt Ol Butonu
            btnRegister = new Button();
            btnRegister.Text = "Kayıt Ol";
            btnRegister.BackColor = Color.White;
            btnRegister.ForeColor = Color.FromArgb(41, 128, 185);
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderColor = Color.FromArgb(41, 128, 185);
            btnRegister.FlatAppearance.BorderSize = 2;
            btnRegister.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnRegister.Size = new Size(150, 40);
            btnRegister.Location = new Point(550, 300);
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.Click += BtnRegister_Click;
            this.Controls.Add(btnRegister);
        }

        // İŞ MANTIĞI

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string rawPassword = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(rawPassword))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifrenizi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Şifreyi hashleyip veritabanında ara
            var user = _db.Users.FirstOrDefault(u => u.UserName == username);

            // Kullanıcı bulunduysa, Abstract metodumuzu (Polymorphism) kullanarak şifreyi kontrol ettiriyoruz.
            // user.Login() metodu Customer ise Customer.cs'deki, Admin ise Admin.cs'deki kodu çalıştırır.
            if (user != null && user.Login(username, rawPassword))
            {
                this.Hide();

                if (user is Admin adminUser)
                {
                    AdminDashboard adminForm = new AdminDashboard(adminUser);
                    adminForm.ShowDialog();
                }
                else if (user is Customer customerUser)
                {
                    CustomerSearchForm searchForm = new CustomerSearchForm(customerUser);
                    searchForm.ShowDialog();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();
            this.Hide();
            regForm.ShowDialog();
            this.Show();
        }
    }
}

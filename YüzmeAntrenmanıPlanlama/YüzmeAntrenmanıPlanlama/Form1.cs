namespace YüzmeAntrenmanıPlanlama
{
    public partial class Form1 : Form
    {
        private Button[] biyomotorikButtons = Array.Empty<Button>();
        private Button[] yuzmeStiliButtons = Array.Empty<Button>();

        public Form1()
        {
            InitializeComponent();
            ConfigureInteractiveButtons();
        }

        private void ConfigureInteractiveButtons()
        {
            // Antrenman Oluştur butonunu okunur yap
            if (this.btnOlustur != null)
            {
                this.btnOlustur.AutoSize = false;
                this.btnOlustur.Width = 260;
                this.btnOlustur.Height = 44;
                this.btnOlustur.TextAlign = ContentAlignment.MiddleCenter;
                this.btnOlustur.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                this.btnOlustur.FlatStyle = FlatStyle.Flat;
                this.btnOlustur.FlatAppearance.BorderSize = 1;
            }

            // Grupları oluştur (Designer'daki adlarla uyumlu)
            this.biyomotorikButtons = new Button[] {
                    this.btnDayaniklilik,
                    this.btnSurat,
                    this.btnKuvvet,
                    this.btnEsneklik,
                    this.btnKoordinasyon
                };

            this.yuzmeStiliButtons = new Button[] {
                    this.btnSerbest,
                    this.btnSirtustu,
                    this.btnKurbagalama,
                    this.btnKelebek
                };

            // Her grup için ortak ayarlar ve event bağlama
            SetupGroupButtons(this.biyomotorikButtons);
            SetupGroupButtons(this.yuzmeStiliButtons);
        }

        private void SetupGroupButtons(Button[] group)
        {
            if (group == null) return;
            foreach (var btn in group)
            {
                if (btn == null) continue;

                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.UseCompatibleTextRendering = false;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.Gray;
                btn.BackColor = SystemColors.Control;
                btn.Margin = new Padding(4);
                btn.Click -= GroupButton_Click;
                btn.Click += GroupButton_Click;
            }
        }

        private void GroupButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button clicked) return;

            if (Array.Exists(this.biyomotorikButtons, b => ReferenceEquals(b, clicked)))
            {
                SetSelectedButton(clicked, this.biyomotorikButtons);
                return;
            }

            if (Array.Exists(this.yuzmeStiliButtons, b => ReferenceEquals(b, clicked)))
            {
                SetSelectedButton(clicked, this.yuzmeStiliButtons);
                return;
            }
        }

        private void SetSelectedButton(Button clicked, Button[] group)
        {
            foreach (var b in group)
            {
                if (b == null) continue;
                b.FlatAppearance.BorderSize = 1;
                b.FlatAppearance.BorderColor = Color.Gray;
                b.BackColor = SystemColors.Control;
            }

            clicked.FlatAppearance.BorderSize = 2;
            clicked.FlatAppearance.BorderColor = Color.DodgerBlue;
            clicked.BackColor = Color.FromArgb(230, 245, 255);
        }

        private void ProfilToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // Profil sekmesine geç
            this.tabControl1.SelectedTab = this.tabPageProfil;
        }

        private void AntrenmanOlusturToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // Antrenman Oluştur sekmesine geç
            this.tabControl1.SelectedTab = this.tabPageAntrenmanOlustur;
        }

        private void GecmisAntrenmanlarToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // Geçmiş Antrenmanlar sekmesine geç
            this.tabControl1.SelectedTab = this.tabPageGecmisAntrenmanlar;
        }
    }
}

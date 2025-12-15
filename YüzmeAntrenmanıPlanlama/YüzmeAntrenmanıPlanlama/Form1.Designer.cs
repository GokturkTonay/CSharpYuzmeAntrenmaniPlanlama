using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YüzmeAntrenmanıPlanlama
{
    partial class Form1
    {
        private IContainer components = null;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem profilToolStripMenuItem;
        private ToolStripMenuItem antrenmanOlusturToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tabPageAntrenmanOlustur;
        private TabPage tabPageProfil;
        private TabPage tabPageGecmisAntrenmanlar;

        // --- Antrenman Oluştur Sekmesi Bileşenleri ---
        private Panel leftContainer;
        private GroupBox grpBiyomotorik;
        private TableLayoutPanel tblBiyomotorik;
        private Button btnDayaniklilik;
        private Button btnSurat;
        private Button btnKuvvet;
        private Button btnEsneklik;
        private Button btnKoordinasyon;

        private GroupBox grpYuzmeStili;
        private TableLayoutPanel tblYuzmeStili;
        private Button btnSerbest;
        private Button btnSirtustu;
        private Button btnKurbagalama;
        private Button btnKelebek;

        private GroupBox grpAntrenmanBilgileri;
        private TableLayoutPanel tblAntrenmanBilgileri;
        private Label lblToplamSure;
        private Label lblHaftada;
        private Label lblToplamMesafe;
        private NumericUpDown nudToplamSure;
        private NumericUpDown nudHaftada;
        private NumericUpDown nudToplamMesafe;
        private Label lblAntrenmanGrupSec;
        private ComboBox cmbAntrenmanGrup;
        private CheckBox chkEkEkipman;
        private Button btnOlustur;

        private Panel panelSag;
        private Label lblProgramBaslik;
        private Button btnPdfIndir;
        private DataGridView dgvProgram;

        // --- Profil Sekmesi Bileşenleri ---
        private Panel panelProfilLeft;
        private Panel panelProfilInputsContainer;
        private Label lblOgrenciYonetimiBaslik;
        private Label lblAd;
        private ComboBox cmbAd;
        private Label lblSoyad;
        private ComboBox cmbSoyad;
        private Label lblGrupSec;
        private ComboBox cmbGrup;
        private Label lblGrupInput;
        private TextBox txtGrupInput;
        private Panel panelProfilButtons;
        private Button btnEkleOgrenci;
        private Button btnSilOgrenci;
        private Button btnSilGrup;
        private ListBox lstOgrenciListesi;
        private Panel panelProfilRight;
        private Label lblGecmisAntrenmanlarProfil;
        private DataGridView dgvGecmisAntrenmanlarProfil;
        private Button btnSeciliyiIndir;
        private Button btnPDFOlarakIndirProfil;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();

            // 1. MENU VE TAB CONTROL KURULUMU
            this.menuStrip1 = new MenuStrip();
            this.profilToolStripMenuItem = new ToolStripMenuItem();
            this.antrenmanOlusturToolStripMenuItem = new ToolStripMenuItem();
            this.tabControl1 = new TabControl();
            this.tabPageProfil = new TabPage();
            this.tabPageAntrenmanOlustur = new TabPage();
            this.tabPageGecmisAntrenmanlar = new TabPage();

            // Fontlar
            var mainFont = new Font("Segoe UI", 10F, FontStyle.Regular);
            var headerFont = new Font("Segoe UI", 12F, FontStyle.Bold);
            // Koordinasyon'un sığması için fontu çok az küçülttük (11'den 10'a) ama Bold yaptık.
            var bigBtnFont = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Menu Strip Ayarları
            this.menuStrip1.Font = mainFont;
            this.menuStrip1.Items.AddRange(new ToolStripItem[] { this.profilToolStripMenuItem, this.antrenmanOlusturToolStripMenuItem });
            this.profilToolStripMenuItem.Text = "Profil";
            this.antrenmanOlusturToolStripMenuItem.Text = "Antrenman Oluştur";

            // Tab Control Ayarları - BAŞLIKLARI GİZLEME
            this.tabControl1.Controls.Add(this.tabPageProfil);
            this.tabControl1.Controls.Add(this.tabPageAntrenmanOlustur);
            this.tabControl1.Controls.Add(this.tabPageGecmisAntrenmanlar);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Font = mainFont;
            // Aşağıdaki iki satır sekme başlıklarını gizler
            this.tabControl1.Appearance = TabAppearance.FlatButtons;
            this.tabControl1.ItemSize = new Size(0, 1);
            this.tabControl1.SizeMode = TabSizeMode.Fixed;

            this.tabPageProfil.Text = "Profil";
            this.tabPageProfil.Padding = new Padding(10);
            this.tabPageProfil.BackColor = Color.WhiteSmoke;

            this.tabPageAntrenmanOlustur.Text = "Antrenman Oluştur";
            this.tabPageAntrenmanOlustur.BackColor = Color.White;

            this.tabPageGecmisAntrenmanlar.Text = "Geçmiş";

            // ---------------------------------------------------------
            // 2. ANTRENMAN OLUŞTUR SEKMESİ (SOL VE SAĞ PANEL)
            // ---------------------------------------------------------
            this.leftContainer = new Panel();
            this.leftContainer.Dock = DockStyle.Left;
            // BURAYI ARTIRDIM: 550'den 620'ye çektim ki "Koordinasyon" rahat sığsın.
            this.leftContainer.Width = 620;
            this.leftContainer.Padding = new Padding(10);
            this.leftContainer.AutoScroll = true;

            this.panelSag = new Panel();
            this.panelSag.Dock = DockStyle.Fill;
            this.panelSag.Padding = new Padding(10);

            // -- Biyomotorik Grup --
            this.grpBiyomotorik = new GroupBox();
            this.grpBiyomotorik.Text = "1. Biyomotorik Yetenek";
            this.grpBiyomotorik.Font = headerFont;
            this.grpBiyomotorik.Dock = DockStyle.Top;
            this.grpBiyomotorik.Height = 140;
            this.grpBiyomotorik.Padding = new Padding(8);

            this.tblBiyomotorik = new TableLayoutPanel();
            this.tblBiyomotorik.Dock = DockStyle.Fill;
            this.tblBiyomotorik.ColumnCount = 5;
            this.tblBiyomotorik.RowCount = 1;
            for (int i = 0; i < 5; i++) this.tblBiyomotorik.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

            // Buton oluşturma yardımcı fonksiyonu
            Button CreateBigButton(string text) => new Button() { Text = text, Dock = DockStyle.Fill, FlatStyle = FlatStyle.Flat, Font = bigBtnFont, Margin = new Padding(3) };

            this.btnDayaniklilik = CreateBigButton("Dayanıklılık");
            this.btnSurat = CreateBigButton("Sürat");
            this.btnKuvvet = CreateBigButton("Kuvvet");
            this.btnEsneklik = CreateBigButton("Esneklik");
            this.btnKoordinasyon = CreateBigButton("Koordinasyon"); // İSİM DÜZELTİLDİ

            this.tblBiyomotorik.Controls.AddRange(new Control[] { btnDayaniklilik, btnSurat, btnKuvvet, btnEsneklik, btnKoordinasyon });
            this.grpBiyomotorik.Controls.Add(this.tblBiyomotorik);

            // -- Yüzme Stili Grup --
            this.grpYuzmeStili = new GroupBox();
            this.grpYuzmeStili.Text = "2. Yüzme Stili";
            this.grpYuzmeStili.Font = headerFont;
            this.grpYuzmeStili.Dock = DockStyle.Top;
            this.grpYuzmeStili.Height = 140;
            this.grpYuzmeStili.Padding = new Padding(8);

            this.tblYuzmeStili = new TableLayoutPanel();
            this.tblYuzmeStili.Dock = DockStyle.Fill;
            this.tblYuzmeStili.ColumnCount = 4;
            this.tblYuzmeStili.RowCount = 1;
            for (int i = 0; i < 4; i++) this.tblYuzmeStili.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            this.btnSerbest = CreateBigButton("Serbest");
            this.btnSirtustu = CreateBigButton("Sırtüstü");
            this.btnKurbagalama = CreateBigButton("Kurbağa");
            this.btnKelebek = CreateBigButton("Kelebek");

            this.tblYuzmeStili.Controls.AddRange(new Control[] { btnSerbest, btnSirtustu, btnKurbagalama, btnKelebek });
            this.grpYuzmeStili.Controls.Add(this.tblYuzmeStili);

            // -- Antrenman Bilgileri Grup (Detaylar) --
            this.grpAntrenmanBilgileri = new GroupBox();
            this.grpAntrenmanBilgileri.Text = "3. Antrenman Detayları";
            this.grpAntrenmanBilgileri.Dock = DockStyle.Fill;
            this.grpAntrenmanBilgileri.Padding = new Padding(10);

            this.tblAntrenmanBilgileri = new TableLayoutPanel();
            this.tblAntrenmanBilgileri.Dock = DockStyle.Top;
            this.tblAntrenmanBilgileri.Height = 240;
            this.tblAntrenmanBilgileri.ColumnCount = 2;
            this.tblAntrenmanBilgileri.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            this.tblAntrenmanBilgileri.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

            for (int i = 0; i < 5; i++) this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Buton satırı

            this.lblToplamSure = new Label() { Text = "Süre (dk):", AutoSize = true, Anchor = AnchorStyles.Left };
            this.nudToplamSure = new NumericUpDown() { Maximum = 300, Value = 60, Dock = DockStyle.Fill };

            this.lblHaftada = new Label() { Text = "Haftada:", AutoSize = true, Anchor = AnchorStyles.Left };
            this.nudHaftada = new NumericUpDown() { Maximum = 7, Value = 3, Dock = DockStyle.Fill };

            this.lblToplamMesafe = new Label() { Text = "Mesafe (m):", AutoSize = true, Anchor = AnchorStyles.Left };
            this.nudToplamMesafe = new NumericUpDown() { Maximum = 20000, Value = 1500, Increment = 100, Dock = DockStyle.Fill };

            this.lblAntrenmanGrupSec = new Label() { Text = "Grup:", AutoSize = true, Anchor = AnchorStyles.Left };
            this.cmbAntrenmanGrup = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };

            this.chkEkEkipman = new CheckBox() { Text = "Ekipman Kullan", AutoSize = true, Anchor = AnchorStyles.Left };

            this.btnOlustur = new Button()
            {
                Text = "ANTRENMANI OLUŞTUR",
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Dock = DockStyle.Fill,
            };

            this.tblAntrenmanBilgileri.Controls.Add(lblToplamSure, 0, 0); this.tblAntrenmanBilgileri.Controls.Add(nudToplamSure, 1, 0);
            this.tblAntrenmanBilgileri.Controls.Add(lblHaftada, 0, 1); this.tblAntrenmanBilgileri.Controls.Add(nudHaftada, 1, 1);
            this.tblAntrenmanBilgileri.Controls.Add(lblToplamMesafe, 0, 2); this.tblAntrenmanBilgileri.Controls.Add(nudToplamMesafe, 1, 2);
            this.tblAntrenmanBilgileri.Controls.Add(lblAntrenmanGrupSec, 0, 3); this.tblAntrenmanBilgileri.Controls.Add(cmbAntrenmanGrup, 1, 3);
            this.tblAntrenmanBilgileri.Controls.Add(chkEkEkipman, 1, 4);
            this.tblAntrenmanBilgileri.Controls.Add(btnOlustur, 1, 5);

            this.grpAntrenmanBilgileri.Controls.Add(this.tblAntrenmanBilgileri);

            this.leftContainer.Controls.Add(this.grpAntrenmanBilgileri);
            this.leftContainer.Controls.Add(this.grpYuzmeStili);
            this.leftContainer.Controls.Add(this.grpBiyomotorik);

            // Sağ Taraf (Program Tablosu)
            this.lblProgramBaslik = new Label() { Text = "Antrenman Programı", Dock = DockStyle.Top, Font = headerFont, Height = 40 };
            this.btnPdfIndir = new Button() { Text = "PDF Olarak Kaydet", Dock = DockStyle.Bottom, Height = 40, FlatStyle = FlatStyle.Flat, BackColor = Color.LightGray };

            this.dgvProgram = new DataGridView();
            this.dgvProgram.Dock = DockStyle.Fill;
            this.dgvProgram.BackgroundColor = Color.White;
            this.dgvProgram.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProgram.RowHeadersVisible = false;
            this.dgvProgram.ColumnHeadersVisible = false;
            this.dgvProgram.AllowUserToAddRows = false;
            this.dgvProgram.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgvProgram.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvProgram.Columns.Add("colContent", "İçerik");

            this.panelSag.Controls.Add(this.dgvProgram);
            this.panelSag.Controls.Add(this.lblProgramBaslik);
            this.panelSag.Controls.Add(this.btnPdfIndir);

            this.tabPageAntrenmanOlustur.Controls.Add(this.panelSag);
            this.tabPageAntrenmanOlustur.Controls.Add(this.leftContainer);

            // ---------------------------------------------------------
            // 3. PROFIL SEKMESI
            // ---------------------------------------------------------
            this.panelProfilLeft = new Panel();
            this.panelProfilLeft.Dock = DockStyle.Left;
            this.panelProfilLeft.Width = 350;
            this.panelProfilLeft.Padding = new Padding(10);
            this.panelProfilLeft.BackColor = Color.White;

            this.lblOgrenciYonetimiBaslik = new Label() { Text = "Öğrenci Yönetimi", Font = headerFont, Dock = DockStyle.Top, Height = 40 };

            this.panelProfilInputsContainer = new Panel();
            this.panelProfilInputsContainer.Dock = DockStyle.Top;
            this.panelProfilInputsContainer.AutoSize = true;
            this.panelProfilInputsContainer.Padding = new Padding(0, 0, 0, 10);

            this.lblAd = new Label() { Text = "Ad:", Dock = DockStyle.Top, Height = 25 };
            this.cmbAd = new ComboBox() { Dock = DockStyle.Top, Height = 30 };
            this.lblSoyad = new Label() { Text = "Soyad:", Dock = DockStyle.Top, Height = 25 };
            this.cmbSoyad = new ComboBox() { Dock = DockStyle.Top, Height = 30 };
            this.lblGrupSec = new Label() { Text = "Grup Seç:", Dock = DockStyle.Top, Height = 25 };
            this.cmbGrup = new ComboBox() { Dock = DockStyle.Top, Height = 30, DropDownStyle = ComboBoxStyle.DropDownList };
            this.lblGrupInput = new Label() { Text = "Yeni Grup Adı:", Dock = DockStyle.Top, Height = 25, Visible = false, ForeColor = Color.Red };
            this.txtGrupInput = new TextBox() { Dock = DockStyle.Top, Height = 30, Visible = false };

            this.panelProfilInputsContainer.Controls.Add(this.txtGrupInput);
            this.panelProfilInputsContainer.Controls.Add(this.lblGrupInput);
            this.panelProfilInputsContainer.Controls.Add(this.cmbGrup);
            this.panelProfilInputsContainer.Controls.Add(this.lblGrupSec);
            this.panelProfilInputsContainer.Controls.Add(this.cmbSoyad);
            this.panelProfilInputsContainer.Controls.Add(this.lblSoyad);
            this.panelProfilInputsContainer.Controls.Add(this.cmbAd);
            this.panelProfilInputsContainer.Controls.Add(this.lblAd);

            this.panelProfilButtons = new Panel();
            this.panelProfilButtons.Dock = DockStyle.Top;
            this.panelProfilButtons.Height = 160;
            this.panelProfilButtons.Padding = new Padding(0, 10, 0, 0);

            this.btnEkleOgrenci = new Button() { Text = "Ekle / Güncelle", Dock = DockStyle.Top, Height = 40, BackColor = Color.AliceBlue, FlatStyle = FlatStyle.Flat };
            this.btnSilOgrenci = new Button() { Text = "Seçili Öğrenciyi Sil", Dock = DockStyle.Top, Height = 40, BackColor = Color.WhiteSmoke, FlatStyle = FlatStyle.Flat };
            this.btnSilGrup = new Button() { Text = "GRUBU SİL", Dock = DockStyle.Top, Height = 40, BackColor = Color.MistyRose, ForeColor = Color.DarkRed, FlatStyle = FlatStyle.Flat };

            this.panelProfilButtons.Controls.Add(this.btnSilGrup);
            this.btnSilGrup.BringToFront();
            this.panelProfilButtons.Controls.Add(this.btnSilOgrenci);
            this.panelProfilButtons.Controls.Add(this.btnEkleOgrenci);

            this.lstOgrenciListesi = new ListBox();
            this.lstOgrenciListesi.Dock = DockStyle.Fill;
            this.lstOgrenciListesi.Font = new Font("Segoe UI", 10F);

            this.panelProfilLeft.Controls.Add(this.lstOgrenciListesi);
            this.panelProfilLeft.Controls.Add(this.panelProfilButtons);
            this.panelProfilLeft.Controls.Add(this.panelProfilInputsContainer);
            this.panelProfilLeft.Controls.Add(this.lblOgrenciYonetimiBaslik);

            this.panelProfilRight = new Panel();
            this.panelProfilRight.Dock = DockStyle.Fill;
            this.panelProfilRight.Padding = new Padding(10);

            this.lblGecmisAntrenmanlarProfil = new Label() { Text = "Geçmiş Antrenmanlar", Dock = DockStyle.Top, Font = headerFont, Height = 40 };

            this.dgvGecmisAntrenmanlarProfil = new DataGridView();
            this.dgvGecmisAntrenmanlarProfil.Dock = DockStyle.Fill;
            this.dgvGecmisAntrenmanlarProfil.BackgroundColor = Color.White;
            this.dgvGecmisAntrenmanlarProfil.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGecmisAntrenmanlarProfil.RowHeadersVisible = false;
            this.dgvGecmisAntrenmanlarProfil.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvGecmisAntrenmanlarProfil.AllowUserToAddRows = false;

            this.dgvGecmisAntrenmanlarProfil.Columns.Add("colTarih", "Tarih");
            this.dgvGecmisAntrenmanlarProfil.Columns.Add("colSure", "Süre");
            this.dgvGecmisAntrenmanlarProfil.Columns.Add("colMesafe", "Mesafe");
            this.dgvGecmisAntrenmanlarProfil.Columns.Add("colHiz", "Hız");

            this.btnSeciliyiIndir = new Button() { Text = "Seçiliyi İndir", Dock = DockStyle.Bottom, Height = 40, BackColor = Color.WhiteSmoke, FlatStyle = FlatStyle.Flat };
            this.btnPDFOlarakIndirProfil = new Button() { Text = "Tüm Listeyi PDF İndir", Dock = DockStyle.Bottom, Height = 40, BackColor = Color.LightGray, FlatStyle = FlatStyle.Flat };

            this.panelProfilRight.Controls.Add(this.dgvGecmisAntrenmanlarProfil);
            this.panelProfilRight.Controls.Add(this.btnSeciliyiIndir);
            this.panelProfilRight.Controls.Add(this.btnPDFOlarakIndirProfil);
            this.panelProfilRight.Controls.Add(this.lblGecmisAntrenmanlarProfil);

            this.tabPageProfil.Controls.Add(this.panelProfilRight);
            this.tabPageProfil.Controls.Add(this.panelProfilLeft);

            // ---------------------------------------------------------
            // EVENTLER
            // ---------------------------------------------------------
            this.cmbGrup.SelectedIndexChanged += (s, e) => {
                bool isNew = cmbGrup.SelectedItem != null && cmbGrup.SelectedItem.ToString() == "Yeni Grup Oluştur...";
                lblGrupInput.Visible = isNew;
                txtGrupInput.Visible = isNew;
            };

            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;

            // İlk sayı Genişlik (1400), İkinci sayı Yükseklik (850).
            this.ClientSize = new Size(1200, 600);

            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Yüzme Antrenmanı Planlama Asistanı";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);

            this.tabPageProfil.ResumeLayout(false);
            this.panelProfilLeft.ResumeLayout(false);
            this.panelProfilLeft.PerformLayout();
            this.panelProfilInputsContainer.ResumeLayout(false);
            this.panelProfilInputsContainer.PerformLayout();
            this.panelProfilButtons.ResumeLayout(false);
            this.panelProfilRight.ResumeLayout(false);
            ((ISupportInitialize)(this.dgvGecmisAntrenmanlarProfil)).EndInit();

            this.tabPageAntrenmanOlustur.ResumeLayout(false);
            this.leftContainer.ResumeLayout(false);
            this.grpBiyomotorik.ResumeLayout(false);
            this.tblBiyomotorik.ResumeLayout(false);
            this.grpYuzmeStili.ResumeLayout(false);
            this.tblYuzmeStili.ResumeLayout(false);
            this.grpAntrenmanBilgileri.ResumeLayout(false);
            this.tblAntrenmanBilgileri.ResumeLayout(false);
            this.tblAntrenmanBilgileri.PerformLayout();
            ((ISupportInitialize)(this.nudToplamSure)).EndInit();
            ((ISupportInitialize)(this.nudHaftada)).EndInit();
            ((ISupportInitialize)(this.nudToplamMesafe)).EndInit();
            this.panelSag.ResumeLayout(false);
            ((ISupportInitialize)(this.dgvProgram)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
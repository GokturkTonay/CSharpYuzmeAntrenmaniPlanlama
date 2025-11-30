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

        // Antrenman Oluştur Sekmesi Bileşenleri
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
        // YENİ EKLENEN KONTROLLER
        private Label lblAntrenmanGrupSec;
        private ComboBox cmbAntrenmanGrup;
        // -----------------------
        private CheckBox chkEkEkipman;
        private Button btnOlustur;

        private Panel panelSag;
        private Label lblProgramBaslik;
        private Button btnPdfIndir;
        private DataGridView dgvProgram;

        // Profil Sekmesi Bileşenleri
        private Panel panelProfilLeft;
        private Label lblOgrenciYonetimiBaslik;
        private Label lblAd;
        private ComboBox cmbAd;
        private Label lblSoyad;
        private ComboBox cmbSoyad;
        private Label lblGrupSec;
        private ComboBox cmbGrup;
        private Label lblGrupInput;
        private TextBox txtGrupInput;
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

            // Menü ve ana kontroller
            this.menuStrip1 = new MenuStrip();
            this.profilToolStripMenuItem = new ToolStripMenuItem();
            this.antrenmanOlusturToolStripMenuItem = new ToolStripMenuItem();
            this.tabControl1 = new TabControl();
            this.tabPageProfil = new TabPage();
            this.tabPageAntrenmanOlustur = new TabPage();
            this.tabPageGecmisAntrenmanlar = new TabPage();

            // Antrenman Oluştur - Sol panel ve alt kontroller
            this.leftContainer = new Panel();
            this.grpBiyomotorik = new GroupBox();
            this.tblBiyomotorik = new TableLayoutPanel();
            this.btnDayaniklilik = new Button();
            this.btnSurat = new Button();
            this.btnKuvvet = new Button();
            this.btnEsneklik = new Button();
            this.btnKoordinasyon = new Button();

            this.grpYuzmeStili = new GroupBox();
            this.tblYuzmeStili = new TableLayoutPanel();
            this.btnSerbest = new Button();
            this.btnSirtustu = new Button();
            this.btnKurbagalama = new Button();
            this.btnKelebek = new Button();

            this.grpAntrenmanBilgileri = new GroupBox();
            this.tblAntrenmanBilgileri = new TableLayoutPanel();
            this.lblToplamSure = new Label();
            this.lblHaftada = new Label();
            this.lblToplamMesafe = new Label();
            this.nudToplamSure = new NumericUpDown();
            this.nudHaftada = new NumericUpDown();
            this.nudToplamMesafe = new NumericUpDown();
            // YENİ
            this.lblAntrenmanGrupSec = new Label();
            this.cmbAntrenmanGrup = new ComboBox();
            // ----
            this.chkEkEkipman = new CheckBox();
            this.btnOlustur = new Button();

            // Antrenman Oluştur - Sağ panel ve alt kontroller
            this.panelSag = new Panel();
            this.lblProgramBaslik = new Label();
            this.btnPdfIndir = new Button();
            this.dgvProgram = new DataGridView();

            // Profil Sekmesi - Paneller
            this.panelProfilLeft = new Panel();
            this.panelProfilRight = new Panel();

            // Profil Sekmesi - Sol Panel Kontrolleri
            this.lblOgrenciYonetimiBaslik = new Label();
            this.lblAd = new Label();
            this.cmbAd = new ComboBox();
            this.lblSoyad = new Label();
            this.cmbSoyad = new ComboBox();
            this.lblGrupSec = new Label();
            this.cmbGrup = new ComboBox();
            this.lblGrupInput = new Label();
            this.txtGrupInput = new TextBox();
            this.btnEkleOgrenci = new Button();
            this.btnSilOgrenci = new Button();
            this.btnSilGrup = new Button();
            this.lstOgrenciListesi = new ListBox();

            // Profil Sekmesi - Sağ Panel Kontrolleri
            this.lblGecmisAntrenmanlarProfil = new Label();
            this.dgvGecmisAntrenmanlarProfil = new DataGridView();
            this.btnSeciliyiIndir = new Button();
            this.btnPDFOlarakIndirProfil = new Button();

            // SuspendLayout
            this.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageAntrenmanOlustur.SuspendLayout();
            this.tabPageProfil.SuspendLayout();
            this.leftContainer.SuspendLayout();
            this.grpBiyomotorik.SuspendLayout();
            this.tblBiyomotorik.SuspendLayout();
            this.grpYuzmeStili.SuspendLayout();
            this.tblYuzmeStili.SuspendLayout();
            this.grpAntrenmanBilgileri.SuspendLayout();
            this.tblAntrenmanBilgileri.SuspendLayout();
            ((ISupportInitialize)(this.nudToplamSure)).BeginInit();
            ((ISupportInitialize)(this.nudHaftada)).BeginInit();
            ((ISupportInitialize)(this.nudToplamMesafe)).BeginInit();
            this.panelSag.SuspendLayout();
            ((ISupportInitialize)(this.dgvProgram)).BeginInit();
            this.panelProfilLeft.SuspendLayout();
            this.panelProfilRight.SuspendLayout();
            ((ISupportInitialize)(this.dgvGecmisAntrenmanlarProfil)).BeginInit();

            // -------------------- MENUSTRIP --------------------
            this.menuStrip1.ImageScalingSize = new Size(20, 20);
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                        this.profilToolStripMenuItem,
                        this.antrenmanOlusturToolStripMenuItem
                    });
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(1200, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";

            this.profilToolStripMenuItem.Name = "profilToolStripMenuItem";
            this.profilToolStripMenuItem.Size = new Size(58, 24);
            this.profilToolStripMenuItem.Text = "Profil";
            this.profilToolStripMenuItem.Click += new EventHandler(this.ProfilToolStripMenuItem_Click);

            this.antrenmanOlusturToolStripMenuItem.Name = "antrenmanOlusturToolStripMenuItem";
            this.antrenmanOlusturToolStripMenuItem.Size = new Size(145, 24);
            this.antrenmanOlusturToolStripMenuItem.Text = "Antrenman Oluştur";
            this.antrenmanOlusturToolStripMenuItem.Click += new EventHandler(this.AntrenmanOlusturToolStripMenuItem_Click);

            // -------------------- TABCONTROL --------------------
            this.tabControl1.Controls.Add(this.tabPageProfil);
            this.tabControl1.Controls.Add(this.tabPageAntrenmanOlustur);
            this.tabControl1.Controls.Add(this.tabPageGecmisAntrenmanlar);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, this.menuStrip1.Height);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(1200, 700 - this.menuStrip1.Height);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Appearance = TabAppearance.FlatButtons;
            this.tabControl1.ItemSize = new Size(0, 1);
            this.tabControl1.SizeMode = TabSizeMode.Fixed;

            // Tab pages
            this.tabPageProfil.Name = "tabPageProfil";
            this.tabPageProfil.Text = "Profil";
            this.tabPageProfil.UseVisualStyleBackColor = true;

            this.tabPageAntrenmanOlustur.Name = "tabPageAntrenmanOlustur";
            this.tabPageAntrenmanOlustur.Text = "Antrenman Oluştur";
            this.tabPageAntrenmanOlustur.UseVisualStyleBackColor = true;

            this.tabPageGecmisAntrenmanlar.Name = "tabPageGecmisAntrenmanlar";
            this.tabPageGecmisAntrenmanlar.Text = "Geçmiş Antrenmanlar";
            this.tabPageGecmisAntrenmanlar.UseVisualStyleBackColor = true;

            // -------------------- ANTRENMAN OLUŞTUR SEKMESİ (Sol Panel) --------------------
            int leftPanelWidth = 820;
            int groupHeight = 135;
            // DEĞİŞİKLİK: Yeni satır eklendiği için bu grubun yüksekliği artırıldı.
            // Eski değer: 210, Yeni değer: 250 (Sığması için zorunlu)
            int antrenmanBilgileriGroupHeight = 250;

            this.leftContainer.Dock = DockStyle.Left;
            this.leftContainer.Width = leftPanelWidth;
            this.leftContainer.Padding = new Padding(8);

            // Biyomotorik grup
            this.grpBiyomotorik.Text = "Biyomotorik Yetenek";
            this.grpBiyomotorik.Dock = DockStyle.Top;
            this.grpBiyomotorik.Height = groupHeight;
            this.grpBiyomotorik.Padding = new Padding(8);

            this.tblBiyomotorik.Dock = DockStyle.Fill;
            this.tblBiyomotorik.ColumnCount = 5;
            this.tblBiyomotorik.RowCount = 1;
            this.tblBiyomotorik.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            for (int i = 0; i < 5; i++) this.tblBiyomotorik.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

            var btnFont = new Font("Segoe UI", 11F, FontStyle.Regular);
            var labelFont = new Font("Segoe UI", 11F, FontStyle.Regular);

            Button[] biyomotorikButtons = { btnDayaniklilik, btnSurat, btnKuvvet, btnEsneklik, btnKoordinasyon };
            string[] biyomotorikTexts = { "Dayanıklılık", "Sürat", "Kuvvet", "Esneklik", "Koordinasyon" };

            for (int i = 0; i < biyomotorikButtons.Length; i++)
            {
                var btn = biyomotorikButtons[i];
                btn.Text = biyomotorikTexts[i];
                btn.Dock = DockStyle.Fill;
                btn.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
                btn.AutoEllipsis = false;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.Gray;
                btn.MinimumSize = new Size(120, 60);
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Margin = new Padding(6);
                this.tblBiyomotorik.Controls.Add(btn, i, 0);
            }
            this.btnKoordinasyon.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.grpBiyomotorik.Controls.Add(this.tblBiyomotorik);

            // Yüzme Stili
            this.grpYuzmeStili.Text = "Yüzme Stili";
            this.grpYuzmeStili.Dock = DockStyle.Top;
            this.grpYuzmeStili.Height = groupHeight;
            this.grpYuzmeStili.Padding = new Padding(8);

            this.tblYuzmeStili.Dock = DockStyle.Fill;
            this.tblYuzmeStili.ColumnCount = 4;
            this.tblYuzmeStili.RowCount = 1;
            this.tblYuzmeStili.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            for (int i = 0; i < 4; i++) this.tblYuzmeStili.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            Button[] yuzmeStiliButtons = { btnSerbest, btnSirtustu, btnKurbagalama, btnKelebek };
            string[] yuzmeStiliTexts = { "Serbest", "Sırtüstü", "Kurbağalama", "Kelebek" };

            for (int i = 0; i < yuzmeStiliButtons.Length; i++)
            {
                var btn = yuzmeStiliButtons[i];
                btn.Text = yuzmeStiliTexts[i];
                btn.Dock = DockStyle.Fill;
                btn.Font = btnFont;
                btn.AutoEllipsis = false;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.Gray;
                btn.MinimumSize = new Size(140, 50);
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Margin = new Padding(6);
                this.tblYuzmeStili.Controls.Add(btn, i, 0);
            }
            this.grpYuzmeStili.Controls.Add(this.tblYuzmeStili);

            // Antrenman Bilgileri (GÜNCELLENDİ)
            this.grpAntrenmanBilgileri.Text = "Antrenman Bilgileri";
            this.grpAntrenmanBilgileri.Dock = DockStyle.Top;
            this.grpAntrenmanBilgileri.Height = antrenmanBilgileriGroupHeight;
            this.grpAntrenmanBilgileri.Padding = new Padding(8);

            this.tblAntrenmanBilgileri.Dock = DockStyle.Fill;
            this.tblAntrenmanBilgileri.ColumnCount = 2;
            this.tblAntrenmanBilgileri.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            this.tblAntrenmanBilgileri.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            // DEĞİŞİKLİK: Satır sayısı 4'ten 5'e çıkarıldı.
            this.tblAntrenmanBilgileri.RowCount = 5;
            // Mevcut 3 satır
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            // YENİ EKLENEN SATIR (Grup Seçimi için)
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            // SON SATIR (Buton için, %100 yükseklik)
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tblAntrenmanBilgileri.Padding = new Padding(4);

            this.lblToplamSure.Text = "Toplam Süre (dk):";
            this.lblToplamSure.Anchor = AnchorStyles.Left;
            this.lblToplamSure.Font = labelFont;
            this.lblToplamSure.AutoSize = true;

            this.nudToplamSure.Minimum = 0;
            this.nudToplamSure.Maximum = 10000;
            this.nudToplamSure.Dock = DockStyle.Fill;
            this.nudToplamSure.Font = btnFont;

            this.lblHaftada.Text = "Haftada Kaç Gün:";
            this.lblHaftada.Anchor = AnchorStyles.Left;
            this.lblHaftada.Font = labelFont;
            this.lblHaftada.AutoSize = true;

            this.nudHaftada.Minimum = 0;
            this.nudHaftada.Maximum = 7;
            this.nudHaftada.Dock = DockStyle.Fill;
            this.nudHaftada.Font = btnFont;

            this.lblToplamMesafe.Text = "Toplam Mesafe (m):";
            this.lblToplamMesafe.Anchor = AnchorStyles.Left;
            this.lblToplamMesafe.Font = labelFont;
            this.lblToplamMesafe.AutoSize = true;

            this.nudToplamMesafe.Minimum = 0;
            this.nudToplamMesafe.Maximum = 100000;
            this.nudToplamMesafe.Dock = DockStyle.Fill;
            this.nudToplamMesafe.Font = btnFont;

            // YENİ KONTROLLERİN TANIMLANMASI
            this.lblAntrenmanGrupSec.Text = "Grup Seç:";
            this.lblAntrenmanGrupSec.Anchor = AnchorStyles.Left;
            this.lblAntrenmanGrupSec.Font = labelFont;
            this.lblAntrenmanGrupSec.AutoSize = true;

            this.cmbAntrenmanGrup.Dock = DockStyle.Fill;
            this.cmbAntrenmanGrup.Font = btnFont;
            this.cmbAntrenmanGrup.DropDownStyle = ComboBoxStyle.DropDownList;
            // İtemler Form1.cs üzerinden yüklenecek.

            this.chkEkEkipman.Text = "Ek Ekipman Kullanılsın mı?";
            this.chkEkEkipman.Anchor = AnchorStyles.Left;
            this.chkEkEkipman.Font = labelFont;
            this.chkEkEkipman.AutoSize = true;

            this.btnOlustur.Text = "Antrenman Oluştur";
            this.btnOlustur.AutoSize = false;
            this.btnOlustur.Width = 150;
            this.btnOlustur.Height = 40;
            this.btnOlustur.Anchor = AnchorStyles.Right;
            this.btnOlustur.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnOlustur.FlatStyle = FlatStyle.Flat;
            this.btnOlustur.FlatAppearance.BorderSize = 1;
            this.btnOlustur.FlatAppearance.BorderColor = Color.Gray;

            // KONTROLLERİN TABLOYA EKLENMESİ (Sıra önemli)
            // Satır 0
            this.tblAntrenmanBilgileri.Controls.Add(this.lblToplamSure, 0, 0);
            this.tblAntrenmanBilgileri.Controls.Add(this.nudToplamSure, 1, 0);
            // Satır 1
            this.tblAntrenmanBilgileri.Controls.Add(this.lblHaftada, 0, 1);
            this.tblAntrenmanBilgileri.Controls.Add(this.nudHaftada, 1, 1);
            // Satır 2
            this.tblAntrenmanBilgileri.Controls.Add(this.lblToplamMesafe, 0, 2);
            this.tblAntrenmanBilgileri.Controls.Add(this.nudToplamMesafe, 1, 2);
            // YENİ SATIR 3 (Grup Seçimi)
            this.tblAntrenmanBilgileri.Controls.Add(this.lblAntrenmanGrupSec, 0, 3);
            this.tblAntrenmanBilgileri.Controls.Add(this.cmbAntrenmanGrup, 1, 3);
            // KAYDIRILAN SATIR 4 (Buton ve Checkbox)
            this.tblAntrenmanBilgileri.Controls.Add(this.chkEkEkipman, 0, 4);
            this.tblAntrenmanBilgileri.Controls.Add(this.btnOlustur, 1, 4);

            this.grpAntrenmanBilgileri.Controls.Add(this.tblAntrenmanBilgileri);

            this.leftContainer.Controls.Add(this.grpAntrenmanBilgileri);
            this.leftContainer.Controls.Add(this.grpYuzmeStili);
            this.leftContainer.Controls.Add(this.grpBiyomotorik);

            // -------------------- ANTRENMAN OLUŞTUR SEKMESİ (Sağ Panel) --------------------
            this.panelSag.Dock = DockStyle.Fill;
            this.panelSag.Padding = new Padding(12);

            this.lblProgramBaslik.Text = "Oluşturulan Antrenman Programı";
            this.lblProgramBaslik.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblProgramBaslik.Dock = DockStyle.Top;
            this.lblProgramBaslik.Height = 32;

            this.btnPdfIndir.Text = "PDF Olarak İndir";
            this.btnPdfIndir.Dock = DockStyle.Top;
            this.btnPdfIndir.Height = 36;
            this.btnPdfIndir.Font = btnFont;
            this.btnPdfIndir.FlatStyle = FlatStyle.Flat;
            this.btnPdfIndir.FlatAppearance.BorderSize = 1;
            this.btnPdfIndir.FlatAppearance.BorderColor = Color.Gray;
            this.btnPdfIndir.Margin = new Padding(0, 8, 0, 8);

            // DataGridView (dgvProgram) Görsel Ayarları - LİSTE GÖRÜNÜMÜ
            this.dgvProgram.Dock = DockStyle.Fill;
            this.dgvProgram.AllowUserToAddRows = false;
            this.dgvProgram.AllowUserToDeleteRows = false;
            this.dgvProgram.RowHeadersVisible = false;
            this.dgvProgram.ColumnHeadersVisible = false;
            this.dgvProgram.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProgram.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.dgvProgram.BackgroundColor = Color.White;
            this.dgvProgram.BorderStyle = BorderStyle.None;
            this.dgvProgram.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.dgvProgram.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.dgvProgram.DefaultCellStyle.ForeColor = Color.Black;
            this.dgvProgram.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 200, 200);
            this.dgvProgram.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.dgvProgram.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            this.dgvProgram.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            var colProgramIcerik = new DataGridViewTextBoxColumn()
            {
                Name = "colProgramIcerik",
                HeaderText = "",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            colProgramIcerik.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgvProgram.Columns.Add(colProgramIcerik);

            this.panelSag.Controls.Add(this.dgvProgram);
            this.panelSag.Controls.Add(this.btnPdfIndir);
            this.panelSag.Controls.Add(this.lblProgramBaslik);

            this.tabPageAntrenmanOlustur.Controls.Add(this.panelSag);
            this.tabPageAntrenmanOlustur.Controls.Add(this.leftContainer);

            // -------------------- PROFİL SEKMESİ --------------------

            // Sol Panel (panelProfilLeft) Ayarları
            this.panelProfilLeft.Dock = DockStyle.Left;
            this.panelProfilLeft.Width = 500;
            this.panelProfilLeft.Padding = new Padding(15);
            this.panelProfilLeft.BackColor = Color.LightGray;

            // Sağ Panel (panelProfilRight) Ayarları
            this.panelProfilRight.Dock = DockStyle.Fill;
            this.panelProfilRight.Padding = new Padding(15);
            this.panelProfilRight.BackColor = Color.WhiteSmoke;

            this.tabPageProfil.Controls.Add(this.panelProfilRight);
            this.tabPageProfil.Controls.Add(this.panelProfilLeft);

            // -------------------- PROFİL SOL PANEL KONTROLLERİ --------------------
            this.lblOgrenciYonetimiBaslik.Text = "Öğrenci Yönetimi";
            this.lblOgrenciYonetimiBaslik.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblOgrenciYonetimiBaslik.Dock = DockStyle.Top;
            this.lblOgrenciYonetimiBaslik.Height = 35;
            this.lblOgrenciYonetimiBaslik.TextAlign = ContentAlignment.MiddleLeft;

            this.lblAd.Text = "Ad:";
            this.lblAd.Font = labelFont;
            this.lblAd.Dock = DockStyle.Top;

            this.cmbAd.Dock = DockStyle.Top;
            this.cmbAd.Height = 28;
            this.cmbAd.Font = btnFont;
            this.cmbAd.DropDownStyle = ComboBoxStyle.DropDown;

            this.lblSoyad.Text = "Soyad:";
            this.lblSoyad.Font = labelFont;
            this.lblSoyad.Dock = DockStyle.Top;

            this.cmbSoyad.Dock = DockStyle.Top;
            this.cmbSoyad.Height = 28;
            this.cmbSoyad.Font = btnFont;
            this.cmbSoyad.DropDownStyle = ComboBoxStyle.DropDown;

            this.lblGrupSec.Text = "Grup Seç:";
            this.lblGrupSec.Font = labelFont;
            this.lblGrupSec.Dock = DockStyle.Top;

            this.cmbGrup.Dock = DockStyle.Top;
            this.cmbGrup.Height = 28;
            this.cmbGrup.Font = btnFont;
            this.cmbGrup.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbGrup.Items.AddRange(new object[] { "A Grubu", "B Grubu", "C Grubu", "Yeni Grup Oluştur..." });

            this.lblGrupInput.Text = "Yeni Grup Adı:";
            this.lblGrupInput.Font = labelFont;
            this.lblGrupInput.Dock = DockStyle.Top;
            this.lblGrupInput.Visible = false;

            this.txtGrupInput.Dock = DockStyle.Top;
            this.txtGrupInput.Height = 28;
            this.txtGrupInput.Font = btnFont;
            this.txtGrupInput.Visible = false;

            this.btnEkleOgrenci.Text = "Ekle";
            this.btnEkleOgrenci.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnEkleOgrenci.Height = 40;
            this.btnEkleOgrenci.Dock = DockStyle.Top;
            this.btnEkleOgrenci.FlatStyle = FlatStyle.Flat;
            this.btnEkleOgrenci.FlatAppearance.BorderSize = 1;
            this.btnEkleOgrenci.FlatAppearance.BorderColor = Color.DarkGray;

            this.btnSilOgrenci.Text = "Sil";
            this.btnSilOgrenci.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnSilOgrenci.Height = 40;
            this.btnSilOgrenci.Dock = DockStyle.Top;
            this.btnSilOgrenci.FlatStyle = FlatStyle.Flat;
            this.btnSilOgrenci.FlatAppearance.BorderSize = 1;
            this.btnSilOgrenci.FlatAppearance.BorderColor = Color.DarkGray;

            this.btnSilGrup.Text = "Grup Sil";
            this.btnSilGrup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnSilGrup.Height = 30;
            this.btnSilGrup.Dock = DockStyle.Top;
            this.btnSilGrup.FlatStyle = FlatStyle.Flat;
            this.btnSilGrup.FlatAppearance.BorderSize = 1;
            this.btnSilGrup.FlatAppearance.BorderColor = Color.IndianRed;
            this.btnSilGrup.ForeColor = Color.IndianRed;
            this.btnSilGrup.Margin = new Padding(0, 0, 0, 10);

            this.lstOgrenciListesi.Dock = DockStyle.Fill;
            this.lstOgrenciListesi.Font = btnFont;

            this.panelProfilLeft.Controls.Add(this.lstOgrenciListesi);
            this.panelProfilLeft.Controls.Add(this.btnSilOgrenci);
            this.panelProfilLeft.Controls.Add(this.btnEkleOgrenci);
            this.panelProfilLeft.Controls.Add(this.btnSilGrup);
            this.panelProfilLeft.Controls.Add(this.txtGrupInput);
            this.panelProfilLeft.Controls.Add(this.lblGrupInput);
            this.panelProfilLeft.Controls.Add(this.cmbGrup);
            this.panelProfilLeft.Controls.Add(this.lblGrupSec);
            this.panelProfilLeft.Controls.Add(this.cmbSoyad);
            this.panelProfilLeft.Controls.Add(this.lblSoyad);
            this.panelProfilLeft.Controls.Add(this.cmbAd);
            this.panelProfilLeft.Controls.Add(this.lblAd);
            this.panelProfilLeft.Controls.Add(this.lblOgrenciYonetimiBaslik);

            this.cmbGrup.SelectedIndexChanged += (sender, e) =>
            {
                bool show = this.cmbGrup.SelectedItem?.ToString() == "Yeni Grup Oluştur...";
                this.lblGrupInput.Visible = show;
                this.txtGrupInput.Visible = show;
            };

            // -------------------- PROFİL SAĞ PANEL KONTROLLERİ --------------------
            this.lblGecmisAntrenmanlarProfil.Text = "Geçmiş Antrenmanlar";
            this.lblGecmisAntrenmanlarProfil.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblGecmisAntrenmanlarProfil.Dock = DockStyle.Top;
            this.lblGecmisAntrenmanlarProfil.Height = 35;
            this.lblGecmisAntrenmanlarProfil.TextAlign = ContentAlignment.MiddleLeft;

            this.dgvGecmisAntrenmanlarProfil.Dock = DockStyle.Fill;
            this.dgvGecmisAntrenmanlarProfil.AllowUserToAddRows = false;
            this.dgvGecmisAntrenmanlarProfil.AllowUserToDeleteRows = false;
            this.dgvGecmisAntrenmanlarProfil.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGecmisAntrenmanlarProfil.RowHeadersVisible = false;
            this.dgvGecmisAntrenmanlarProfil.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvGecmisAntrenmanlarProfil.Font = btnFont;

            var colTarih = new DataGridViewTextBoxColumn() { Name = "colTarih", HeaderText = "Tarih", DataPropertyName = "Tarih" };
            var colSureProfil = new DataGridViewTextBoxColumn() { Name = "colSureProfil", HeaderText = "Süre (dk)", DataPropertyName = "Sure" };
            var colMesafeProfil = new DataGridViewTextBoxColumn() { Name = "colMesafeProfil", HeaderText = "Mesafe (m)", DataPropertyName = "Mesafe" };
            var colOrtHizProfil = new DataGridViewTextBoxColumn() { Name = "colOrtHizProfil", HeaderText = "Ort. Hız (dk/100m)", DataPropertyName = "OrtHiz" };
            this.dgvGecmisAntrenmanlarProfil.Columns.AddRange(new DataGridViewColumn[] { colTarih, colSureProfil, colMesafeProfil, colOrtHizProfil });

            this.dgvGecmisAntrenmanlarProfil.Rows.Add("01.01.2023", 60, 1500, "4:00");
            this.dgvGecmisAntrenmanlarProfil.Rows.Add("05.01.2023", 45, 1200, "3:45");

            this.btnPDFOlarakIndirProfil.Text = "PDF Olarak İndir";
            this.btnPDFOlarakIndirProfil.Dock = DockStyle.Bottom;
            this.btnPDFOlarakIndirProfil.Height = 36;
            this.btnPDFOlarakIndirProfil.Font = btnFont;
            this.btnPDFOlarakIndirProfil.FlatStyle = FlatStyle.Flat;
            this.btnPDFOlarakIndirProfil.FlatAppearance.BorderSize = 1;
            this.btnPDFOlarakIndirProfil.FlatAppearance.BorderColor = Color.DarkGray;

            this.btnSeciliyiIndir.Text = "Seçiliyi İndir";
            this.btnSeciliyiIndir.Dock = DockStyle.Bottom;
            this.btnSeciliyiIndir.Height = 36;
            this.btnSeciliyiIndir.Font = btnFont;
            this.btnSeciliyiIndir.FlatStyle = FlatStyle.Flat;
            this.btnSeciliyiIndir.FlatAppearance.BorderSize = 1;
            this.btnSeciliyiIndir.FlatAppearance.BorderColor = Color.DarkGray;

            this.panelProfilRight.Controls.Add(this.dgvGecmisAntrenmanlarProfil);
            this.panelProfilRight.Controls.Add(this.btnSeciliyiIndir);
            this.panelProfilRight.Controls.Add(this.btnPDFOlarakIndirProfil);
            this.panelProfilRight.Controls.Add(this.lblGecmisAntrenmanlarProfil);

            // -------------------- FORM AYARLARI --------------------
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1500, 700);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Yüzme Antrenmanı Planlama";

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageProfil.ResumeLayout(false);
            this.panelProfilLeft.ResumeLayout(false);
            this.panelProfilLeft.PerformLayout();
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
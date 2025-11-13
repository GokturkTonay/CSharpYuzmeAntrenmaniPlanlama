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
        private ToolStripMenuItem gecmisAntrenmanlarToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tabPageAntrenmanOlustur;
        private TabPage tabPageProfil;
        private TabPage tabPageGecmisAntrenmanlar;

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
        private CheckBox chkEkEkipman;
        private Button btnOlustur;

        private Panel panelSag;
        private Label lblProgramBaslik;
        private Button btnPdfIndir;
        private DataGridView dgvProgram;

        private SplitContainer splitContainerProfil;
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
            this.gecmisAntrenmanlarToolStripMenuItem = new ToolStripMenuItem();
            this.tabControl1 = new TabControl();
            this.tabPageProfil = new TabPage();
            this.tabPageAntrenmanOlustur = new TabPage();
            this.tabPageGecmisAntrenmanlar = new TabPage();

            // Sol panel ve alt kontroller
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
            this.chkEkEkipman = new CheckBox();
            this.btnOlustur = new Button();

            // Sağ panel ve alt kontroller
            this.panelSag = new Panel();
            this.lblProgramBaslik = new Label();
            this.btnPdfIndir = new Button();
            this.dgvProgram = new DataGridView();

            // Profil split ve kontroller
            this.splitContainerProfil = new SplitContainer();
            this.panelProfilLeft = new Panel();
            this.panelProfilRight = new Panel();

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
            this.lstOgrenciListesi = new ListBox();

            this.lblGecmisAntrenmanlarProfil = new Label();
            this.dgvGecmisAntrenmanlarProfil = new DataGridView();
            this.btnSeciliyiIndir = new Button();
            this.btnPDFOlarakIndirProfil = new Button();

            // SuspendLayout
            this.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageAntrenmanOlustur.SuspendLayout();
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

            ((ISupportInitialize)(this.splitContainerProfil)).BeginInit();
            this.splitContainerProfil.Panel1.SuspendLayout();
            this.splitContainerProfil.Panel2.SuspendLayout();
            this.splitContainerProfil.SuspendLayout();
            this.panelProfilLeft.SuspendLayout();
            this.panelProfilRight.SuspendLayout();
            ((ISupportInitialize)(this.dgvGecmisAntrenmanlarProfil)).BeginInit();

            // -------------------- MENUSTRIP --------------------
            this.menuStrip1.ImageScalingSize = new Size(20, 20);
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                        this.profilToolStripMenuItem,
                        this.antrenmanOlusturToolStripMenuItem,
                        this.gecmisAntrenmanlarToolStripMenuItem
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

            this.gecmisAntrenmanlarToolStripMenuItem.Name = "gecmisAntrenmanlarToolStripMenuItem";
            this.gecmisAntrenmanlarToolStripMenuItem.Size = new Size(164, 24);
            this.gecmisAntrenmanlarToolStripMenuItem.Text = "Geçmiş Antrenmanlar";
            this.gecmisAntrenmanlarToolStripMenuItem.Click += new EventHandler(this.GecmisAntrenmanlarToolStripMenuItem_Click);

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
            // Görünür tab başlıkları
            this.tabControl1.Appearance = TabAppearance.FlatButtons; // Sekmeleri buton gibi gösterir
            this.tabControl1.ItemSize = new Size(0, 1); // Sekme başlıklarını gizler
            this.tabControl1.SizeMode = TabSizeMode.Fixed; // Boyutlandırmayı sabitler

            // Tab pages
            this.tabPageProfil.Name = "tabPageProfil";
            this.tabPageProfil.Text = "Profil";
            this.tabPageProfil.UseVisualStyleBackColor = true;
            this.tabPageProfil.Controls.Add(this.splitContainerProfil);

            this.tabPageAntrenmanOlustur.Name = "tabPageAntrenmanOlustur";
            this.tabPageAntrenmanOlustur.Text = "Antrenman Oluştur";
            this.tabPageAntrenmanOlustur.UseVisualStyleBackColor = true;

            this.tabPageGecmisAntrenmanlar.Name = "tabPageGecmisAntrenmanlar";
            this.tabPageGecmisAntrenmanlar.Text = "Geçmiş Antrenmanlar";
            this.tabPageGecmisAntrenmanlar.UseVisualStyleBackColor = true;

            // -------------------- SOL PANEL (tabPageAntrenmanOlustur içinde) --------------------
            int leftPanelWidth = 820; // Genişletildi: koordinasyonun yatay sığması için
            int groupHeight = 135;
            int antrenmanBilgileriGroupHeight = 210;

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

            // Koordinasyon butonunun fontunu biraz küçültüyorum ki uzun metin kesinlikle yatay sığsın
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

            // Antrenman Bilgileri
            this.grpAntrenmanBilgileri.Text = "Antrenman Bilgileri";
            this.grpAntrenmanBilgileri.Dock = DockStyle.Top;
            this.grpAntrenmanBilgileri.Height = antrenmanBilgileriGroupHeight;
            this.grpAntrenmanBilgileri.Padding = new Padding(8);

            this.tblAntrenmanBilgileri.Dock = DockStyle.Fill;
            this.tblAntrenmanBilgileri.ColumnCount = 2;
            this.tblAntrenmanBilgileri.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            this.tblAntrenmanBilgileri.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            this.tblAntrenmanBilgileri.RowCount = 4;
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
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

            this.tblAntrenmanBilgileri.Controls.Add(this.lblToplamSure, 0, 0);
            this.tblAntrenmanBilgileri.Controls.Add(this.nudToplamSure, 1, 0);
            this.tblAntrenmanBilgileri.Controls.Add(this.lblHaftada, 0, 1);
            this.tblAntrenmanBilgileri.Controls.Add(this.nudHaftada, 1, 1);
            this.tblAntrenmanBilgileri.Controls.Add(this.lblToplamMesafe, 0, 2);
            this.tblAntrenmanBilgileri.Controls.Add(this.nudToplamMesafe, 1, 2);
            this.tblAntrenmanBilgileri.Controls.Add(this.chkEkEkipman, 0, 3);
            this.tblAntrenmanBilgileri.Controls.Add(this.btnOlustur, 1, 3);

            this.grpAntrenmanBilgileri.Controls.Add(this.tblAntrenmanBilgileri);

            // Sol panel içine grup ekleme (üstten aşağı)
            this.leftContainer.Controls.Add(this.grpAntrenmanBilgileri);
            this.leftContainer.Controls.Add(this.grpYuzmeStili);
            this.leftContainer.Controls.Add(this.grpBiyomotorik);

            // -------------------- SAĞ PANEL --------------------
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
            this.btnPdfIndir.Margin = new Padding(0, 8, 0, 8);

            this.dgvProgram.Dock = DockStyle.Fill;
            this.dgvProgram.AllowUserToAddRows = false;
            this.dgvProgram.AllowUserToDeleteRows = false;
            this.dgvProgram.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProgram.RowHeadersVisible = false;
            this.dgvProgram.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProgram.Font = btnFont;

            var colBolum = new DataGridViewTextBoxColumn() { Name = "colBolum", HeaderText = "Bölüm", DataPropertyName = "Bolum" };
            var colAktivite = new DataGridViewTextBoxColumn() { Name = "colAktivite", HeaderText = "Aktivite", DataPropertyName = "Aktivite" };
            var colDetay = new DataGridViewTextBoxColumn() { Name = "colDetay", HeaderText = "Detay", DataPropertyName = "Detay" };
            this.dgvProgram.Columns.AddRange(new DataGridViewColumn[] { colBolum, colAktivite, colDetay });

            this.panelSag.Controls.Add(this.dgvProgram);
            this.panelSag.Controls.Add(this.btnPdfIndir);
            this.panelSag.Controls.Add(this.lblProgramBaslik);

            // Antrenman oluştur sayfasına paneller ekle
            this.tabPageAntrenmanOlustur.Controls.Add(this.panelSag);
            this.tabPageAntrenmanOlustur.Controls.Add(this.leftContainer);

            // -------------------- SPLITCONTAINER PROFİL --------------------
            this.splitContainerProfil.Dock = DockStyle.Fill;
            this.splitContainerProfil.FixedPanel = FixedPanel.Panel1;
            this.splitContainerProfil.Panel1MinSize = 300;
            this.splitContainerProfil.SplitterDistance = 350;
            this.splitContainerProfil.TabIndex = 0;

            // Panel1 ve Panel2 içine panelProfilLeft/Right ekle
            this.splitContainerProfil.Panel1.Controls.Add(this.panelProfilLeft);
            this.splitContainerProfil.Panel2.Controls.Add(this.panelProfilRight);

            // panelProfilLeft ayarları
            this.panelProfilLeft.Dock = DockStyle.Fill;
            this.panelProfilLeft.Padding = new Padding(15);
            this.panelProfilLeft.BackColor = Color.LightGray;

            // panelProfilRight ayarları
            this.panelProfilRight.Dock = DockStyle.Fill;
            this.panelProfilRight.Padding = new Padding(15);
            this.panelProfilRight.BackColor = Color.WhiteSmoke;

            // SOL PANEL KONTROLLERİNİ ekle (dock-top düzeni ile görünür yapılır)
            this.lblOgrenciYonetimiBaslik.Text = "Öğrenci Yönetimi";
            this.lblOgrenciYonetimiBaslik.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblOgrenciYonetimiBaslik.Dock = DockStyle.Top;
            this.lblOgrenciYonetimiBaslik.Height = 35;
            this.lblOgrenciYonetimiBaslik.TextAlign = ContentAlignment.MiddleLeft;
            // Sıra değişti

            this.lblAd.Text = "Ad:";
            this.lblAd.Font = labelFont;
            this.lblAd.Dock = DockStyle.Top;
            // Sıra değişti

            this.cmbAd.Dock = DockStyle.Top;
            this.cmbAd.Height = 28;
            this.cmbAd.Font = btnFont;
            this.cmbAd.DropDownStyle = ComboBoxStyle.DropDown;
            // Sıra değişti

            this.lblSoyad.Text = "Soyad:";
            this.lblSoyad.Font = labelFont;
            this.lblSoyad.Dock = DockStyle.Top;
            // Sıra değişti

            this.cmbSoyad.Dock = DockStyle.Top;
            this.cmbSoyad.Height = 28;
            this.cmbSoyad.Font = btnFont;
            this.cmbSoyad.DropDownStyle = ComboBoxStyle.DropDown;
            // Sıra değişti

            this.lblGrupSec.Text = "Grup Seç:";
            this.lblGrupSec.Font = labelFont;
            this.lblGrupSec.Dock = DockStyle.Top;
            // Sıra değişti

            this.cmbGrup.Dock = DockStyle.Top;
            this.cmbGrup.Height = 28;
            this.cmbGrup.Font = btnFont;
            this.cmbGrup.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbGrup.Items.AddRange(new object[] { "A Grubu", "B Grubu", "C Grubu", "Yeni Grup Oluştur..." });
            // Sıra değişti

            this.lblGrupInput.Text = "Yeni Grup Adı:";
            this.lblGrupInput.Font = labelFont;
            this.lblGrupInput.Dock = DockStyle.Top;
            this.lblGrupInput.Visible = false;
            // Sıra değişti

            this.txtGrupInput.Dock = DockStyle.Top;
            this.txtGrupInput.Height = 28;
            this.txtGrupInput.Font = btnFont;
            this.txtGrupInput.Visible = false;
            // Sıra değişti

            this.btnEkleOgrenci.Text = "Ekle";
            this.btnEkleOgrenci.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnEkleOgrenci.Height = 40;
            this.btnEkleOgrenci.Dock = DockStyle.Top;
            this.btnEkleOgrenci.FlatStyle = FlatStyle.Flat;
            this.btnEkleOgrenci.FlatAppearance.BorderSize = 1;
            this.btnEkleOgrenci.FlatAppearance.BorderColor = Color.DarkGray;
            // Sıra değişti

            this.btnSilOgrenci.Text = "Sil";
            this.btnSilOgrenci.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnSilOgrenci.Height = 40;
            this.btnSilOgrenci.Dock = DockStyle.Top;
            this.btnSilOgrenci.FlatStyle = FlatStyle.Flat;
            this.btnSilOgrenci.FlatAppearance.BorderSize = 1;
            this.btnSilOgrenci.FlatAppearance.BorderColor = Color.DarkGray;
            // Sıra değişti

            this.lstOgrenciListesi.Dock = DockStyle.Fill;
            this.lstOgrenciListesi.Font = btnFont;
            this.lstOgrenciListesi.Items.AddRange(new object[] {
                "Ali Yılmaz (A Grubu)",
                "Ayşe Demir (B Grubu)",
                "Can Kara (A Grubu)",
                "Deniz Ak (C Grubu)"
            });
            // Sıra değişti


            // ***** ÖNEMLİ: KONTROLLERİN panelProfilLeft'e EKLENME SIRASI *****
            // DockStyle.Fill olanı ilk eklemek, en altta kalmasını ve diğer DockStyle.Top/Bottom kontrollerin üzerine yerleşmesini sağlar.
            this.panelProfilLeft.Controls.Add(this.lstOgrenciListesi); // ListBox (DockStyle.Fill) en alta yerleşmesi için ilk eklenir.

            // DockStyle.Top olan butonlar (en alttaki en sona gelecek şekilde)
            this.panelProfilLeft.Controls.Add(this.btnSilOgrenci);
            this.panelProfilLeft.Controls.Add(this.btnEkleOgrenci);

            // Yeni Grup Input (Combobox seçimine bağlı olarak görünür/gizlenir)
            this.panelProfilLeft.Controls.Add(this.txtGrupInput);
            this.panelProfilLeft.Controls.Add(this.lblGrupInput);

            // Combobox ve Label'lar
            this.panelProfilLeft.Controls.Add(this.cmbGrup);
            this.panelProfilLeft.Controls.Add(this.lblGrupSec);
            this.panelProfilLeft.Controls.Add(this.cmbSoyad);
            this.panelProfilLeft.Controls.Add(this.lblSoyad);
            this.panelProfilLeft.Controls.Add(this.cmbAd);
            this.panelProfilLeft.Controls.Add(this.lblAd);

            this.panelProfilLeft.Controls.Add(this.lblOgrenciYonetimiBaslik); // Başlık (DockStyle.Top) en üste yerleşmesi için en son eklenir.
            // ***** EKLENME SIRASI DEĞİŞİKLİĞİ BİTTİ *****


            // ComboBox seçimine göre yeni grup input göster/gizle (Bu kısım yerinde kalmalı)
            this.cmbGrup.SelectedIndexChanged += (sender, e) =>
            {
                bool show = this.cmbGrup.SelectedItem?.ToString() == "Yeni Grup Oluştur...";
                this.lblGrupInput.Visible = show;
                this.txtGrupInput.Visible = show;
            };

            // -------------------- SAĞ PANEL KONTROLLERİ (Geçmiş Antrenmanlar) --------------------
            this.lblGecmisAntrenmanlarProfil.Text = "Geçmiş Antrenmanlar";
            this.lblGecmisAntrenmanlarProfil.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblGecmisAntrenmanlarProfil.Dock = DockStyle.Top;
            this.lblGecmisAntrenmanlarProfil.Height = 35;
            this.lblGecmisAntrenmanlarProfil.TextAlign = ContentAlignment.MiddleLeft;

            // DataGridView ayarları
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

            // örnek veriler
            this.dgvGecmisAntrenmanlarProfil.Rows.Add("01.01.2023", 60, 1500, "4:00");
            this.dgvGecmisAntrenmanlarProfil.Rows.Add("05.01.2023", 45, 1200, "3:45");

            // Butonlar (bottom dock)
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

            // panelProfilRight: add top label, then fill grid, then bottom buttons
            this.panelProfilRight.Controls.Clear();
            this.panelProfilRight.Controls.Add(this.btnSeciliyiIndir);
            this.panelProfilRight.Controls.Add(this.btnPDFOlarakIndirProfil);
            this.panelProfilRight.Controls.Add(this.dgvGecmisAntrenmanlarProfil);
            this.panelProfilRight.Controls.Add(this.lblGecmisAntrenmanlarProfil);

            // SplitContainer Panel1 ve Panel2 içindeki panel'ler hazır; splitContainer zaten tabPageProfil'e eklendi

            // -------------------- FORM AYARLARI --------------------
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1500, 700);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Yüzme Antrenmanı Planlama";

            // ResumeLayout
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);

            this.splitContainerProfil.Panel1.ResumeLayout(false);
            this.splitContainerProfil.Panel2.ResumeLayout(false);
            ((ISupportInitialize)(this.splitContainerProfil)).EndInit();

            ((ISupportInitialize)(this.dgvGecmisAntrenmanlarProfil)).EndInit();
            ((ISupportInitialize)(this.dgvProgram)).EndInit();

            this.tabPageAntrenmanOlustur.ResumeLayout(false);
            this.leftContainer.ResumeLayout(false);
            this.grpBiyomotorik.ResumeLayout(false);
            this.tblBiyomotorik.ResumeLayout(false);
            this.grpYuzmeStili.ResumeLayout(false);
            this.tblYuzmeStili.ResumeLayout(false);
            this.grpAntrenmanBilgileri.ResumeLayout(false);
            this.tblAntrenmanBilgileri.ResumeLayout(false);

            this.panelSag.ResumeLayout(false);

            this.panelProfilLeft.ResumeLayout(false);
            this.panelProfilRight.ResumeLayout(false);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}